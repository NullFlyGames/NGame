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
    /// Sample for listing buckets.
    /// </summary>
    public static class ListBucketsSample
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        public static void ListBuckets()
        {
            ListAllBuckets();
            ListBucketsByConditions();
        }

        // 1. Try to list all buckets. 
        public static void ListAllBuckets()
        {
            try
            {
                var buckets = client.ListBuckets();

                UnityEngine.Debug.LogFormat("List all buckets: ");
                foreach (var bucket in buckets)
                {
                    UnityEngine.Debug.LogFormat("Name:{0}", bucket.Name);
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

        // 2. List buckets by specified conditions, such as prefix/marker/max-keys.
        public static void ListBucketsByConditions()
        {
            try
            {
                var req = new ListBucketsRequest {Prefix = "test", MaxKeys = 3, Marker = "test2"};
                var result = client.ListBuckets(req);
                var buckets = result.Buckets;
                UnityEngine.Debug.LogFormat("List buckets by page: ");
                UnityEngine.Debug.LogFormat("Prefix: {0}, MaxKeys: {1},  Marker: {2}, NextMarker:{3}", 
                                   result.Prefix, result.MaxKeys, result.Marker, result.NextMaker);
                foreach (var bucket in buckets)
                {
                    UnityEngine.Debug.LogFormat("Name:{0}, Location:{1}, Owner:{2}", bucket.Name, bucket.Location, bucket.Owner);
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
    }
}
