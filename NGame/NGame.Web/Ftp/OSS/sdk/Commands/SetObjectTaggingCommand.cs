/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using NClient.OSS.Common.Communication;
using NClient.OSS.Util;
using NClient.OSS.Transform;
using NClient.OSS.Domain;

namespace NClient.OSS.Commands
{
    internal class SetObjectTaggingCommand : OssCommand
    {
        private readonly SetObjectTaggingRequest _request;

        protected override HttpMethod Method
        {
            get { return HttpMethod.Put; }
        }

        protected override string Bucket
        {
            get { return _request.BucketName; }
        }

        protected override string Key
        {
            get { return _request.Key; }
        }

        protected override Stream Content
        {
            get
            {
                return SerializerFactory.GetFactory().CreateSetObjectTaggingRequestSerializer()
                    .Serialize(_request);
            }
        }

        private SetObjectTaggingCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                        SetObjectTaggingRequest setObjectTaggingRequest)
            : base(client, endpoint, context)
        {
            _request = setObjectTaggingRequest;
        }

        public static SetObjectTaggingCommand Create(IServiceClient client, Uri endpoint,
                                                       ExecutionContext context,
                                                       SetObjectTaggingRequest request)
        {
            OssUtils.CheckBucketName(request.BucketName);
            OssUtils.CheckObjectKey(request.Key);
            return new SetObjectTaggingCommand(client, endpoint, context, request);
        }


        protected override IDictionary<string, string> Parameters
        {
            get
            {
                var parameters = new Dictionary<string, string>()
                {
                    { RequestParameters.SUBRESOURCE_TAGGING, null }
                };
                if (!string.IsNullOrEmpty(_request.VersionId))
                {
                    parameters.Add(RequestParameters.SUBRESOURCE_VERSIONID, _request.VersionId);
                }
                return parameters;
            }
        }
    }
}

