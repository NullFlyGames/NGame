/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using System.Collections.Generic;
using NClient.OSS.Common.Communication;
using NClient.OSS.Util;
using NClient.OSS.Transform;

namespace NClient.OSS.Commands
{
    internal class GetBucketStorageCapacityCommand : OssCommand<GetBucketStorageCapacityResult>
    {
         private readonly string _bucketName;

         protected override string Bucket
         {
             get { return _bucketName; }
         }

         private GetBucketStorageCapacityCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                       string bucketName, IDeserializer<ServiceResponse, GetBucketStorageCapacityResult> deserializer)
            : base(client, endpoint, context, deserializer)
        {
            OssUtils.CheckBucketName(bucketName);
            _bucketName = bucketName;
        }

         public static GetBucketStorageCapacityCommand Create(IServiceClient client, Uri endpoint,
                                                    ExecutionContext context,
                                                    string bucketName)
        {
            return new GetBucketStorageCapacityCommand(client, endpoint, context, bucketName,
                                           DeserializerFactory.GetFactory().CreateGetBucketStorageCapacityResultDeserializer());
        }

        protected override IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { RequestParameters.SUBRESOURCE_QOS, null }
                };
            }
        }
    }
}
