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
    internal class GetBucketWebsiteCommand : OssCommand<BucketWebsiteResult>
    {
         private readonly string _bucketName;

         protected override string Bucket
         {
             get { return _bucketName; }
         }

        private GetBucketWebsiteCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                       string bucketName, IDeserializer<ServiceResponse, BucketWebsiteResult> deserializer)
            : base(client, endpoint, context, deserializer)
        {
            OssUtils.CheckBucketName(bucketName);
            _bucketName = bucketName;
        }

        public static GetBucketWebsiteCommand Create(IServiceClient client, Uri endpoint,
                                                    ExecutionContext context,
                                                    string bucketName)
        {
            return new GetBucketWebsiteCommand(client, endpoint, context, bucketName,
                                           DeserializerFactory.GetFactory().CreateGetBucketWebSiteResultDeserializer());
        }

        protected override IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { RequestParameters.SUBRESOURCE_WEBSITE, null }
                };
            }
        }
    }
}
