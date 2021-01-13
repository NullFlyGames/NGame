/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using System.Collections.Generic;
using NClient.OSS.Common;

namespace NClient.OSS.Samples
{
    /// <summary>
    /// Sample for setting bucket referer list.
    /// </summary>
    public static class SetBucketRefererSample
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        public static void SetBucketReferer(string bucketName)
        {
            try
            {
                var refererList = new List<string>();
                refererList.Add(" http://www.aliyun.com");
                refererList.Add(" http://www.*.com");
                refererList.Add(" http://www.?.aliyuncs.com");
                var srq = new SetBucketRefererRequest(bucketName, refererList);
                client.SetBucketReferer(srq);

                UnityEngine.Debug.LogFormat("Set bucket:{0} Referer succeeded ", bucketName);
            }
            catch (OssException ex)
            {
                UnityEngine.Debug.LogFormat("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}", 
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogFormat("Failed with error info: {0}", ex.Message);
            }
        }
    }
}
