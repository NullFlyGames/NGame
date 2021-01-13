/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using NClient.OSS.Common.Communication;
using NClient.OSS.Model;
using System.IO;

namespace NClient.OSS.Transform
{
    internal class GetBucketWebSiteResultDeserializer : ResponseDeserializer<BucketWebsiteResult, SetBucketWebsiteRequestModel>
    {
        public GetBucketWebSiteResultDeserializer(IDeserializer<Stream, SetBucketWebsiteRequestModel> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override BucketWebsiteResult Deserialize(ServiceResponse xmlStream)
        {
            var model = ContentDeserializer.Deserialize(xmlStream.Content);
            var bucketWebsiteResult = new BucketWebsiteResult
            {
                IndexDocument = model.IndexDocument.Suffix,
                ErrorDocument = model.ErrorDocument.Key
            };

            DeserializeGeneric(xmlStream, bucketWebsiteResult);

            return bucketWebsiteResult;
        }
    }
}
