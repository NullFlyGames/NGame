/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using NClient.OSS.Common.Communication;

namespace NClient.OSS.Common.Authentication
{
    internal interface IRequestSigner
    {
        void Sign(ServiceRequest request, ICredentials credentials);
    }
}
