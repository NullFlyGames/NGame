/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using NClient.OSS.Common;

namespace NClient.OSS.Samples
{
    /// <summary>
    /// Sample for getting bucket cors.
    /// </summary>
    public static class GetBucketLoggingSample
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        public static void GetBucketLogging(string bucketName)
        {
            try
            {
                var result = client.GetBucketLogging(bucketName);

                UnityEngine.Debug.LogFormat("Get bucket:{0} Logging succeeded, prefix:{1}", bucketName, result.TargetPrefix);
            }
            catch (OssException ex)
            {
                UnityEngine.Debug.LogFormat("Failed with error info: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}", 
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }
    }
}
