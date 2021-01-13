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
    /// Sample for setting bucket cors.
    /// </summary>
    public static class DeleteBucketCorsSample
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        public static void DeleteBucketCors(string bucketName)
        {
            try
            {
                client.DeleteBucketCors(bucketName);

                UnityEngine.Debug.LogFormat("Delete bucket:{0} Cors succeeded ", bucketName);
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
