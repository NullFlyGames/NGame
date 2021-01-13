/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using NClient.OSS.Common.Communication;
using NClient.OSS.Util;
using NClient.OSS.Transform;
using System.Collections.Generic;

namespace NClient.OSS.Commands
{
    internal class GetBucketMetadataCommand : OssCommand<BucketMetadata>
    {
        private readonly string _bucketName;

        protected override HttpMethod Method
        {
            get { return HttpMethod.Head; }
        }

        protected override string Bucket
        {
            get { return _bucketName; }
        }

        private GetBucketMetadataCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                         IDeserializer<ServiceResponse, BucketMetadata> deserializer,
                                         string bucketName)
            : base(client, endpoint, context, deserializer)
        {
            OssUtils.CheckBucketName(bucketName);

            _bucketName = bucketName;
        }

        public static GetBucketMetadataCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context,
                                                      string bucketName)
        {
            return new GetBucketMetadataCommand(client, endpoint, context,
                                                DeserializerFactory.GetFactory().CreateGetBucketMetadataResultDeserializer(),
                                                bucketName);
        }
    }

}
