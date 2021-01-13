/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using System.Globalization;
using NClient.OSS.Common;

namespace NClient.OSS.Samples
{
    /// <summary>
    /// Sample for getting bucket lifecycle
    /// </summary>
    public static class GetBucketLifecycleSample
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        public static void GetBucketLifecycle(string bucketName)
        {
            try
            {
                var rules = client.GetBucketLifecycle(bucketName);

                UnityEngine.Debug.LogFormat("Get bucket:{0} Lifecycle succeeded ", bucketName);

                foreach (var rule in rules)
                {
                    UnityEngine.Debug.LogFormat("ID: {0}", rule.ID);
                    UnityEngine.Debug.LogFormat("Prefix: {0}", rule.Prefix);
                    UnityEngine.Debug.LogFormat("Status: {0}", rule.Status);
                    if (rule.ExpriationDays.HasValue)
                        UnityEngine.Debug.LogFormat("ExpirationDays: {0}", rule.ExpriationDays);
                    if (rule.ExpirationTime.HasValue)
                        UnityEngine.Debug.LogFormat("ExpirationTime: {0}", FormatIso8601Date(rule.ExpirationTime.Value));
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
        }

        private static string FormatIso8601Date(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                               CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}
