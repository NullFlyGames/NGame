/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using NClient.OSS.Common.Communication;

namespace NClient.OSS.Common.Handlers
{
    internal interface IResponseHandler
    {
        void Handle(ServiceResponse response);
    }
}
