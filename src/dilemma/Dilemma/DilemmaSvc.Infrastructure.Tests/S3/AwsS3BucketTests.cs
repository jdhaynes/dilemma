using System;
using DilemmaSvc.Infrastructure.S3;
using NUnit.Framework;

namespace DilemmaSvc.Infrastructure.Tests.S3
{
    [TestFixture]
    public class AwsS3BucketTests
    {
        [Test]
        public void GivenBucketNameAndRegionWhenGetBucketUrlShouldReturnCorrectFormattedUrl()
        {
            AwsS3Bucket bucket = new AwsS3Bucket("test-bucket", "eu-west-2");
            string bucketUrl = bucket.Url;

            Assert.AreEqual("https://test-bucket.s3.eu-west-2.amazonaws.com", bucketUrl);
        }

        [Test]
        public void GivenObjectKeyWhenGetObjectUrlShouldReturnCorrectFormattedUrl()
        {
            AwsS3Bucket bucket = new AwsS3Bucket("test-bucket", "eu-west-2");
            string objectUrl = bucket.GetUrlForObject("test.txt");

            Assert.AreEqual("https://test-bucket.s3.eu-west-2.amazonaws.com/test.txt", objectUrl);
        }

        [Test]
        public void GivenNullOrEmptyBucketNameWhenInstantiateShouldThrow()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    AwsS3Bucket bucket = new AwsS3Bucket(null, "eu-west-2");
                });
                
                Assert.Throws<ArgumentException>(() =>
                {
                    AwsS3Bucket bucket = new AwsS3Bucket(string.Empty, "eu-west-2");
                });
            });
        }
        
        [Test]
        public void GivenNullOrEmptyRegionWhenInstantiateShouldThrow()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    AwsS3Bucket bucket = new AwsS3Bucket("test-bucket", null);
                });
                
                Assert.Throws<ArgumentException>(() =>
                {
                    AwsS3Bucket bucket = new AwsS3Bucket("test-bucket", string.Empty);
                });
            });
        }

        [Test]
        public void GivenNullOrEmptyKeyWhenGetObjectUrlShouldThrow()
        {
            AwsS3Bucket bucket = new AwsS3Bucket("test-bucket", "eu-west-2");

            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() => { bucket.GetUrlForObject(null); });
                Assert.Throws<ArgumentException>(() =>
                {
                    bucket.GetUrlForObject(String.Empty);
                });
            });
        }
    }
}