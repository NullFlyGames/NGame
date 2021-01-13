/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System.Diagnostics;
using System.Net;
using NClient.OSS.Common;
using NClient.OSS.Common.Communication;

namespace NClient.OSS.Util
{
    internal static class ServiceClientFactory
    {
        static ServiceClientFactory()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = ClientConfiguration.ConnectionLimit;
        }

        public static IServiceClient CreateServiceClient(ClientConfiguration configuration)
        {
            Debug.Assert(configuration != null);

            var retryableServiceClient =
                new RetryableServiceClient(ServiceClient.Create(configuration))
                {
                    MaxRetryTimes = configuration.MaxErrorRetry
                };

            return retryableServiceClient;
        }
    }
}
