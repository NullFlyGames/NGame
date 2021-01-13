using System;
using NClient.OSS.Common;

namespace NClient.OSS.Samples
{
    internal class Config
    {
        public static string AccessKeyId = "LTAI4FkCEzjzfKx8hdD3haNQ";

        public static string AccessKeySecret = "UUctRVy7nt1jVhm1qTrHNQ9YsfmvfX";

        public static string Endpoint = "oss-cn-chengdu.aliyuncs.com";

        public static string DirToDownload = "";

        public static string FileToUpload = "";

        public static string BigFileToUpload = "<your local big file to upload>";
        public static string ImageFileToUpload = "<your local image file to upload>";
        public static string CallbackServer = "<your callback server uri>";
    }
}