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
            string objectUrl = bucket.GetUrlForObjectKey("test.txt");

            Assert.AreEqual("https://test-bucket.s3.eu-west-2.amazonaws.com/test.txt", objectUrl);
        }

        [Test]
        public void GivenNullKeyWhenGetObjectUrlShouldThrowException()
        {
            AwsS3Bucket bucket = new AwsS3Bucket("test-bucket", "eu-west-2");
            Assert.Throws<ArgumentException>(() => { bucket.GetUrlForObjectKey(null); });

            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() => { bucket.GetUrlForObjectKey(null); });
                Assert.Throws<ArgumentException>(() =>
                {
                    bucket.GetUrlForObjectKey(String.Empty);
                });
            });
        }
    }
}