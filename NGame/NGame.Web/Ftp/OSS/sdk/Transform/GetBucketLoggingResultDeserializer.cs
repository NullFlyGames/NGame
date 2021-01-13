/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.IO;
using NClient.OSS.Common.Communication;
using NClient.OSS.Model;

namespace NClient.OSS.Transform
{
    internal class GetBucketLoggingResultDeserializer 
        : ResponseDeserializer<BucketLoggingResult, SetBucketLoggingRequestModel>
    {
        public GetBucketLoggingResultDeserializer(IDeserializer<Stream, SetBucketLoggingRequestModel> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override BucketLoggingResult Deserialize(ServiceResponse xmlStream)
        {
            var model = ContentDeserializer.Deserialize(xmlStream.Content);
            var bucketLoggingResult = new BucketLoggingResult
            {
                TargetBucket = model.LoggingEnabled.TargetBucket,
                TargetPrefix = model.LoggingEnabled.TargetPrefix
            };

            DeserializeGeneric(xmlStream, bucketLoggingResult);

            return bucketLoggingResult;
       }
    }
}
