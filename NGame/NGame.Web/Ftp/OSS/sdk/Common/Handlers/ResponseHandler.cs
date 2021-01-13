/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.Diagnostics;
using NClient.OSS.Common.Communication;

namespace NClient.OSS.Common.Handlers
{
    internal class ResponseHandler : IResponseHandler
    {
        public virtual void Handle(ServiceResponse response)
        {
            Debug.Assert(response != null);
        }
    }
}
