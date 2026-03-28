using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System.IO;



namespace App;

public class S3UploadService : IDisposable
	{
		private readonly string _bucketName;
		private readonly AmazonS3Client _s3Client;

		public S3UploadService(string accessKeyId, string secretAccessKey, string bucketName, RegionEndpoint region)
		{
			_bucketName = bucketName;
			var credentials = new BasicAWSCredentials(accessKeyId, secretAccessKey);
			_s3Client = new AmazonS3Client(credentials, region);
		}

		public async Task SyncFolderTrueMirrorAsync(string localDirectoryPath)
		{
			if (!Directory.Exists(localDirectoryPath))
				throw new DirectoryNotFoundException($"The folder {localDirectoryPath} does not exist.");

			if (!localDirectoryPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
			{
				localDirectoryPath += Path.DirectorySeparatorChar;
			}

			// 1. Fetch metadata of everything currently in S3
			var s3ObjectsMetadata = new Dictionary<string, DateTime>();
			var listRequest = new ListObjectsV2Request { BucketName = _bucketName };
			ListObjectsV2Response listResponse;

			do
			{
				listResponse = await _s3Client.ListObjectsV2Async(listRequest);
				foreach (var s3Object in listResponse.S3Objects)
				{
					s3ObjectsMetadata[s3Object.Key] = s3Object.LastModified.Value.ToUniversalTime();
				}
				listRequest.ContinuationToken = listResponse.NextContinuationToken;
			} while (listResponse.IsTruncated ?? true);

			// 2. Setup a collection to track what exists locally
			string[] localFiles = Directory.GetFiles(localDirectoryPath, "*.*", SearchOption.AllDirectories);
			var localFileKeys = new HashSet<string>();

			// 3. Upload new and modified files
			using (var transferUtility = new TransferUtility(_s3Client))
			{
				foreach (var filePath in localFiles)
				{
					string relativePath = filePath.Substring(localDirectoryPath.Length);
					string s3Key = relativePath.Replace('\\', '/');

					// Keep track of this key to prevent it from being deleted later
					localFileKeys.Add(s3Key);

					FileInfo fileInfo = new FileInfo(filePath);
					DateTime localLastModified = fileInfo.LastWriteTimeUtc;

					bool shouldUpload = !s3ObjectsMetadata.ContainsKey(s3Key) || localLastModified > s3ObjectsMetadata[s3Key];

					if (shouldUpload)
					{
						await transferUtility.UploadAsync(filePath, _bucketName, s3Key);
					}
				}
			}

			// 4. Determine which files are in S3 but no longer exist locally
			var keysToDelete = new List<string>();
			foreach (var s3Key in s3ObjectsMetadata.Keys)
			{
				if (!localFileKeys.Contains(s3Key))
				{
					keysToDelete.Add(s3Key);
				}
			}

			// 5. Batch delete the orphaned S3 files
			if (keysToDelete.Any())
			{
				// Amazon S3 limits batch deletions to 1,000 items per request, so we chunk them
				for (int i = 0; i < keysToDelete.Count; i += 1000)
				{
					var deleteRequest = new DeleteObjectsRequest { BucketName = _bucketName };
					var batch = keysToDelete.Skip(i).Take(1000);

					foreach (var key in batch)
					{
						deleteRequest.AddKey(key);
					}

					await _s3Client.DeleteObjectsAsync(deleteRequest);
				}
			}
		}

		public void Dispose()
		{
			_s3Client?.Dispose();
		}
	}