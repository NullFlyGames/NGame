/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using System.Threading;
using NClient.OSS.Common;

namespace NClient.OSS.Samples
{
    /// <summary>
    /// Sample for copying object.
    /// </summary>
    public static class CopyObjectSample
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        static AutoResetEvent _event = new AutoResetEvent(false);

        public static void CopyObject(string sourceBucket, string sourceKey, string targetBucket, string targetKey)
        {
            try
            {
                var metadata = new ObjectMetadata();
                metadata.AddHeader("mk1", "mv1");
                metadata.AddHeader("mk2", "mv2");
                var req = new CopyObjectRequest(sourceBucket, sourceKey, targetBucket, targetKey)
                {
                    NewObjectMetadata = metadata
                };
                client.CopyObject(req);

                UnityEngine.Debug.LogFormat("Copy object succeeded");
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

        public static void AsyncCopyObject(string sourceBucket, string sourceKey, string targetBucket, string targetKey)
        {
            try
            {
                var metadata = new ObjectMetadata();
                metadata.AddHeader("mk1", "mv1");
                metadata.AddHeader("mk2", "mv2");
                var req = new CopyObjectRequest(sourceBucket, sourceKey, targetBucket, targetKey)
                {
                    NewObjectMetadata = metadata
                };
                client.BeginCopyObject(req, CopyObjectCallback, null);

                _event.WaitOne();
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

        private static void CopyObjectCallback(IAsyncResult ar)
        {
            try
            {
                client.EndCopyResult(ar);
                UnityEngine.Debug.LogFormat("Copy object succeeded");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogFormat(ex.Message);
            }
            finally
            {
                _event.Set();
            }
        }
    }
}
