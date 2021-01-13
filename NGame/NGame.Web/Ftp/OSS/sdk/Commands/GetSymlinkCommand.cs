/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;

using NClient.OSS.Common.Communication;
using NClient.OSS.Common.Handlers;
using NClient.OSS.Common.Internal;
using NClient.OSS.Util;
using NClient.OSS.Transform;

namespace NClient.OSS.Commands
{
    internal class GetSymlinkCommand : OssCommand<OssSymlink>
    {
        private readonly GetSymlinkRequest _request;

        protected override string Bucket
        {
            get { return _request.BucketName; }
        }

        protected override string Key
        {
            get { return _request.Key; }
        }

        protected override HttpMethod Method
        {
            get { return HttpMethod.Get; }
        }

        protected override IDictionary<string, string> Parameters
        {
            get
            {
                var parameters = new Dictionary<string, string>()
                {
                    {RequestParameters.SUBRESOURCE_SYMLINK, null}
                };
                if (!string.IsNullOrEmpty(_request.VersionId))
                {
                    parameters.Add(RequestParameters.SUBRESOURCE_VERSIONID, _request.VersionId);
                }
                return parameters;
            }
        }

        protected override IDictionary<string, string> Headers
        {
            get
            {
                var headers = base.Headers;
                if (_request.RequestPayer == RequestPayer.Requester)
                {
                    headers.Add(OssHeaders.OssRequestPayer, RequestPayer.Requester.ToString().ToLowerInvariant());
                }
                return headers;
            }
        }

        private GetSymlinkCommand(IServiceClient client, Uri endpoint, ExecutionContext context,
                                  IDeserializer<ServiceResponse, OssSymlink> deserializer,
                                  GetSymlinkRequest request)
            : base(client, endpoint, context, deserializer)
        {
            OssUtils.CheckBucketName(request.BucketName);
            OssUtils.CheckObjectKey(request.Key);
            _request = request;
        }

        public static GetSymlinkCommand Create(IServiceClient client, Uri endpoint,
                                                 ExecutionContext context,
                                                 IDeserializer<ServiceResponse, OssSymlink> deserializer,
                                                 GetSymlinkRequest request)
        {
            return new GetSymlinkCommand(client, endpoint, context, deserializer, request);
        }
    }
}
