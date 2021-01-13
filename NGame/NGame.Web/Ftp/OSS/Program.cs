using System;
using NClient.OSS.Common;

namespace NClient.OSS.Samples
{
    public class Program
    {
        /// <summary>
        /// SDK的示例程序
        /// </summary>
        public static void Main(string[] args)
        {
            UnityEngine.Debug.LogFormat("Aliyun SDK for .NET Samples!");

            const string bucketName = "nullfly";

            try
            {
                CreateBucketSample.CreateBucket(bucketName);//创建存储桶

                ListBucketsSample.ListBuckets();//获取存储桶列表

                SetBucketCorsSample.SetBucketCors(bucketName);//跨域资源共享

                GetBucketCorsSample.GetBucketCors(bucketName);//跨域资源共享

                DeleteBucketCorsSample.DeleteBucketCors(bucketName);//删除跨域资源共享

                SetBucketLoggingSample.SetBucketLogging(bucketName);//设置访问日志

                GetBucketLoggingSample.GetBucketLogging(bucketName);//获取访问日志

                DeleteBucketLoggingSample.DeleteBucketLogging(bucketName);

                SetBucketAclSample.SetBucketAcl(bucketName);//设置存储空间的访问权限

                GetBucketAclSample.GetBucketAcl(bucketName);//获取存储空间的访问权限

                SetBucketWebsiteSample.SetBucketWebsite(bucketName);//静态网站托管

                GetBucketWebsiteSample.GetBucketWebsite(bucketName);//获取静态网站托管

                DeleteBucketWebsiteSample.DeleteBucketWebsite(bucketName);//删除静态网站托管

                SetBucketRefererSample.SetBucketReferer(bucketName);//防盗链

                GetBucketRefererSample.GetBucketReferer(bucketName);//获取防盗链

                SetBucketLifecycleSample.SetBucketLifecycle(bucketName);//生命周期

                GetBucketLifecycleSample.GetBucketLifecycle(bucketName);//获取生命周期

                DoesBucketExistSample.DoesBucketExist(bucketName);//判断存储空间是否存在

                PutObjectSample.PutObject(bucketName);//上传文件

                ResumbaleSample.ResumableUploadObject(bucketName);//断点续传上传

                CreateEmptyFolderSample.CreateEmptyFolder(bucketName);//c创建空文件夹

                AppendObjectSample.AppendObject(bucketName);//追加上传

                ListObjectsSample.ListObjects(bucketName);//文件列表

                UrlSignatureSample.UrlSignature(bucketName);//授权访问

                GetObjectSample.GetObjects(bucketName);//下载文件
                GetObjectByRangeSample.GetObjectPartly(bucketName);//下载文件范围，断点下载

                DeleteObjectsSample.DeleteObject(bucketName);//删除文件
                DeleteObjectsSample.DeleteObjects(bucketName);//删除文件列表

                //const string sourceBucket = bucketName;
                //const string sourceKey = "ResumableUploadObject";
                //const string targetBucket = bucketName;
                //const string targetKey = "ResumableUploadObject2";
                //CopyObjectSample.CopyObject(sourceBucket, sourceKey, targetBucket, targetKey);//拷贝文件
                //CopyObjectSample.AsyncCopyObject(sourceBucket, sourceKey, targetBucket, targetKey);

                //ResumbaleSample.ResumableCopyObject(sourceBucket, sourceKey, targetBucket, targetKey);//断点续传上传

                //ModifyObjectMetaSample.ModifyObjectMeta(bucketName);

                DoesObjectExistSample.DoesObjectExist(bucketName);

                //MultipartUploadSample.UploadMultipart(bucketName);
                //MultipartUploadSample.AsyncUploadMultipart(bucketName);

                //MultipartUploadSample.UploadMultipartCopy(targetBucket, targetKey, sourceBucket, sourceKey);

                //MultipartUploadSample.AsyncUploadMultipartCopy(targetBucket, targetKey, sourceBucket, sourceKey);

                //MultipartUploadSample.ListMultipartUploads(bucketName);

                //CNameSample.CNameOperation(bucketName);

                //PostPolicySample.GenPostPolicy(bucketName);

                //DeleteBucketSample.DeleteNoEmptyBucket(bucketName);

                //SetObjectAclSample.SetObjectAcl(bucketName);

                //GetObjectAclSample.GetBucketAcl(bucketName);

                //ImageProcessSample.ImageProcess(bucketName);

                ProgressSample.Progress(bucketName);//上传进度条和下载进度条

                //UploadCallbackSample.UploadCallback(bucketName);

                //LiveChannelSample.LiveChannel(bucketName);
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

            UnityEngine.Debug.LogFormat("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}