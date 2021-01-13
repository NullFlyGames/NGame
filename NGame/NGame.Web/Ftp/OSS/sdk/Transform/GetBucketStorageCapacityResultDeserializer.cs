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
    internal class GetBucketStorageCapacityResultDeserializer : ResponseDeserializer<GetBucketStorageCapacityResult, BucketStorageCapacityModel>
    {
        public GetBucketStorageCapacityResultDeserializer(IDeserializer<Stream, BucketStorageCapacityModel> contentDeserializer)
            : base(contentDeserializer)
        { }

        public override GetBucketStorageCapacityResult Deserialize(ServiceResponse xmlStream)
        {
            var model = ContentDeserializer.Deserialize(xmlStream.Content);
            var getBucketStorageCapacityResult = new GetBucketStorageCapacityResult
            {
                StorageCapacity = model.StorageCapacity
            };

            DeserializeGeneric(xmlStream, getBucketStorageCapacityResult);

            return getBucketStorageCapacityResult;
        }
    }
}
