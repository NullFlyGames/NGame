/*
 * Copyright (C) Alibaba Cloud Computing
 * All rights reserved.
 * 
 */

using System;
using NClient.OSS.Common;
using System.Text;
using System.IO;

namespace NClient.OSS.Samples
{
    /// <summary>
    /// Sample for modify object meta
    /// </summary>
    public static class ModifyObjectMetaSample
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;
        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        public static void ModifyObjectMeta(string bucketName)
        {
            const string key = "key1";
            try
            {
                byte[] binaryData = Encoding.ASCII.GetBytes("forked from aliyun/aliyun-oss-csharp-sdk "); 
                var stream = new MemoryStream(binaryData);

                client.PutObject(bucketName, key, stream);

                var oldMeta = client.GetObjectMetadata(bucketName, key);

                var newMeta = new ObjectMetadata()
                {
                    ContentType = "application/msword",
                    ExpirationTime = oldMeta.ExpirationTime,
                    ContentEncoding = null,
                    CacheControl = ""
                };

                client.ModifyObjectMeta(bucketName, key, newMeta);

                UnityEngine.Debug.LogFormat("Modify object meta succeeded");
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
