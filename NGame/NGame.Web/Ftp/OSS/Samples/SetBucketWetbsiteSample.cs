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
    /// Sample for setting bucket website.
    /// </summary>
    public static class SetBucketWebsiteSample
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        public static void SetBucketWebsite(string bucketName)
        {
            try
            {
                var request = new SetBucketWebsiteRequest(bucketName, "index.html", "error.html");

                client.SetBucketWebsite(request);

                UnityEngine.Debug.LogFormat("Set bucket:{0} Wetbsite succeeded ", bucketName);
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
