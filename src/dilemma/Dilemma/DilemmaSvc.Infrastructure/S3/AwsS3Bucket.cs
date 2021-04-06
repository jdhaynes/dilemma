using System;
using DilemmaSvc.Application.Common;

namespace DilemmaSvc.Infrastructure.S3
{
    public class AwsS3Bucket : IFileStore
    {
        private readonly string _bucketName;
        private readonly string _region;
        public string Url => $"https://{_bucketName}.s3.{_region}.amazonaws.com";

        public AwsS3Bucket(string bucketName, string region)
        {
            _bucketName = bucketName;
            _region = region;
        }

        public string GetUrlForObjectKey(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Object key must not be null or empty.", nameof(key));
            }

            return $"{Url}/{key}";
        }
    }
}