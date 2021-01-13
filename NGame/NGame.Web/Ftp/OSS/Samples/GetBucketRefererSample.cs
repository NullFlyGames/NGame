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
    /// Sample for getting bucket referer list.
    /// </summary>
    public static class GetBucketRefererSample
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        public static void GetBucketReferer(string bucketName)
        {
            try
            {
                var rc = client.GetBucketReferer(bucketName);

                UnityEngine.Debug.LogFormat("Get bucket:{0} Referer succeeded ", bucketName);

                UnityEngine.Debug.LogFormat("allow？" + (rc.AllowEmptyReferer ? "yes" : "no"));
                if (rc.RefererList.Referers != null)
                {
                    for (var i = 0; i < rc.RefererList.Referers.Length; i++)
                        UnityEngine.Debug.LogFormat(rc.RefererList.Referers[i]);
                }
                else
                {
                    UnityEngine.Debug.LogFormat("Empty Referer List");
                }
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
            finally
            {
                client.SetBucketReferer(new SetBucketRefererRequest(bucketName));
            }
        }
    }
}
