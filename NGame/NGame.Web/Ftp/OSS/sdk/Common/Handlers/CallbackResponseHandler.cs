/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using NClient.OSS.Common.Communication;

namespace NClient.OSS.Common.Handlers
{
    internal class CallbackResponseHandler : ErrorResponseHandler
    {
        public override void Handle(ServiceResponse response)
        {
            base.Handle(response);

            if (response.IsSuccessful() && (int)response.StatusCode != 203)
                return;

            ErrorHandle(response);
        }
    }
}

