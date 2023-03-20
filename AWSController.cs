using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class AWSController
    {

        public static bool sendMyFileToS3(string localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
        {
            string accessKey = "AKIA5N5WQKZMRFLXPL7Y";
            string secretKey = "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";

            using (IAmazonS3 client = new AmazonS3Client(accessKey,
                    secretKey, RegionEndpoint.EUWest2))
            {


                TransferUtility utility = new TransferUtility(client);

                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

                if (string.IsNullOrEmpty(subDirectoryInBucket))
                {
                    request.BucketName = bucketName; //no subdirectory just bucket name
                }
                else
                {   // subdirectory and bucket name
                    request.BucketName = bucketName + @"/" + subDirectoryInBucket;
                }
                request.Key = fileNameInS3; //file name up in S3
                request.FilePath = localFilePath; //local file name
                utility.Upload(request); //commensing the transfer

                return true; //indicate that the file was sent
            }
        }
    }
}
