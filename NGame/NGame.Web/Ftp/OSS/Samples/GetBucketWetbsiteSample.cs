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
    /// Sample for getting bucket website.
    /// </summary>
    public static class GetBucketWebsiteSample
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        public static void GetBucketWebsite(string bucketName)
        {
            try
            {
                var result = client.GetBucketWebsite(bucketName);

                UnityEngine.Debug.LogFormat("Get bucket:{0} Wetbsite succeeded, index doc:{1}, error doc:{2}", 
                                  bucketName, result.IndexDocument, result.ErrorDocument);
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