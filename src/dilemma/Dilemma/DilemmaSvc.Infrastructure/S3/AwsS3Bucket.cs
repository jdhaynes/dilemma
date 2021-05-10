using System;
using DilemmaApp.Services.Dilemma.Application.Interfaces;

namespace DilemmaApp.Services.Dilemma.Infrastructure.S3
{
    public class AwsS3Bucket : IFileStore
    {
        private readonly string _bucketName;
        private readonly string _region;
        public string Url => $"https://{_bucketName}.s3.{_region}.amazonaws.com";

        public AwsS3Bucket(string bucketName, string region)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentException("Bucket name must be specified.", nameof(bucketName));
            }
            
            if (string.IsNullOrEmpty(region))
            {
                throw new ArgumentException("Region must be specified.", nameof(region));
            }
            
            _bucketName = bucketName;
            _region = region;
        }

        public string GetPublicUrlForObject(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Object key must not be null or empty.", nameof(key));
            }

            return $"{Url}/{key}";
        }
    }
}