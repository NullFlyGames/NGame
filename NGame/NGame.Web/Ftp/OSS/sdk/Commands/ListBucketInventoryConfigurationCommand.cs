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
using NClient.OSS.Model;
namespace NClient.OSS.Commands
{
    internal class ListBucketInventoryConfigurationCommand : OssCommand<ListBucketInventoryConfigurationResult>
    {
        private readonly ListBucketInventoryConfigurationRequest _request;

        protected override string Bucket
        {
            get { return _request.BucketName; }
        }

        private ListBucketInventoryConfigurationCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                    ListBucketInventoryConfigurationRequest request,
                                    IDeserializer<ServiceResponse, ListBucketInventoryConfigurationResult> deserializer)
            : base(client, endpoint, context, deserializer)
        {
            OssUtils.CheckBucketName(request.BucketName);
            _request = request;
        }

        public static ListBucketInventoryConfigurationCommand Create(IServiceClient client, Uri endpoint,
                                                     ExecutionContext context,
                                                    ListBucketInventoryConfigurationRequest request)
        {
            return new ListBucketInventoryConfigurationCommand(client, endpoint, context, request,
                                              DeserializerFactory.GetFactory().CreateListBucketInventoryConfigurationResultDeserializer());
        }

        protected override IDictionary<string, string> Parameters
        {
            get
            {
                var parameters = base.Parameters;
                parameters[RequestParameters.SUBRESOURCE_INVENTORY] = null;
                if (!string.IsNullOrEmpty(_request.ContinuationToken))
                {
                    parameters[RequestParameters.SUBRESOURCE_CONTINUATIONTOKEN] = _request.ContinuationToken;
                }
                return parameters;
            }
        }
    }
}
