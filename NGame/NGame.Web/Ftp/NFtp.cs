using NClient.Editor.Tree;
using NClient.OSS;
using NClient.OSS.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace NClient.Editor
{
    public enum CheckType
    {
        Cloud,
        Local,
    }
    public class NFtp : EditorWindow
    {
        public class FtpTreeViewItem : MultiColumnTreeViewItem
        {
            public override void OnBeginEditor()
            {
                throw new NotImplementedException();
            }

            public override void OnEditorEnd()
            {
                throw new NotImplementedException();
            }

            public override void OnGUI(Rect rect, int Column)
            {
                throw new NotImplementedException();
            }
            public static MultiColumnHeaderState MultiColumnHeaderState()
            {
                MultiColumnHeaderState state = new MultiColumnHeaderState(new MultiColumnHeaderState.Column[]
                {
                    new MultiColumnHeaderState.Column()
                    {
                        headerContent = new GUIContent("资源名"),
                        headerTextAlignment = TextAlignment.Center,
                        sortedAscending = true,
                        sortingArrowAlignment = TextAlignment.Center,
                        width = 150,
                        autoResize = true,
                        allowToggleVisibility = false
                    },
                    new MultiColumnHeaderState.Column()
                    {
                        headerContent = new GUIContent("大小"),
                        headerTextAlignment = TextAlignment.Center,
                        sortedAscending = true,
                        sortingArrowAlignment = TextAlignment.Center,
                        width = 150,
                        autoResize = true,
                        allowToggleVisibility = false
                    },
                    new MultiColumnHeaderState.Column()
                    {
                        headerContent = new GUIContent("时间"),
                        headerTextAlignment = TextAlignment.Center,
                        sortedAscending = true,
                        sortingArrowAlignment = TextAlignment.Center,
                        width = 150,
                        autoResize = true,
                        allowToggleVisibility = false
                    },
                    new MultiColumnHeaderState.Column()
                    {
                        headerContent = new GUIContent("MD5"),
                        headerTextAlignment = TextAlignment.Center,
                        sortedAscending = true,
                        sortingArrowAlignment = TextAlignment.Center,
                        width = 150,
                        autoResize = true,
                        allowToggleVisibility = false
                    },
                    new MultiColumnHeaderState.Column()
                    {
                        headerContent = new GUIContent("状态"),
                        headerTextAlignment = TextAlignment.Center,
                        sortedAscending = true,
                        sortingArrowAlignment = TextAlignment.Center,
                        width = 150,
                        autoResize = true,
                        allowToggleVisibility = false
                    }
                });
                
                return state;
            }
        }


        private static NFtp managed;
        static SearchField m_SearchField;
        static SearchField m_bucketSearchField;

        public static CheckType m_checktype = CheckType.Cloud;
        public static int m_sever_index = 0, m_local_index = 0, m_plat_index = 0;
        public static List<string> m_local_folder, m_server_folder;
        public static string[] m_plat = new string[] { "android", "ios", "pc" };

        public static string m_bucketdeleteorcreate = "";

        private static string accessKeyId = OSS.Samples.Config.AccessKeyId;
        private static string accessKeySecret = OSS.Samples.Config.AccessKeySecret;
        private static string endpoint = OSS.Samples.Config.Endpoint;
        public static OssClient ossClient = new OSS.OssClient(endpoint, accessKeyId, accessKeySecret);
        public static MultiColumnTreeView<FtpTreeViewItem> m_MultiColumnTreeView;
        public static Action m_updatecallback;
        public static string LocalFolder
        {
            get
            {

                return m_local_folder[m_local_index];
            }
        }
        public static string Bucket
        {
            get
            {
     
                return m_server_folder[m_sever_index];
            }
        }

        public static string Type
        {
            get
            {
                return m_plat[m_plat_index];
            }
        }
        public static string ServerPath
        {
            get
            {
                return Bucket + "/" + Type + "/";
            }
        }
        public static string LocalPath
        {
            get
            {
                return LocalFolder + "/" + Type + "/";
            }
        }
        public static void Open()
        {
            if (m_local_folder == null)
            {
                GetLocalBucket();
            }
            if (m_server_folder == null)
            {
                GetServerBucket();
            }
            if (managed == null)
            {
                managed = EditorWindow.GetWindow<NFtp>("云存储管理");
                managed.maxSize = new Vector2(1330, 650);
                managed.minSize = new Vector2(1330, 650);
            }
          
            if (m_MultiColumnTreeView == null)
            {
                MultiColumnHeaderState viewState = FtpTreeViewItem.MultiColumnHeaderState();
                MultiColumnHeader header = new MultiColumnHeader(viewState);
                m_MultiColumnTreeView = new MultiColumnTreeView<FtpTreeViewItem>(new TreeViewState(), header,"资源列表");

                m_SearchField = new SearchField();
                m_SearchField.downOrUpArrowKeyPressed += m_MultiColumnTreeView.SetFocusAndEnsureSelectedItem;
            }
            
            managed.Show();
        }

        private static void GetServerBucket()
        {
            var buckets = ossClient.ListBuckets();
            m_server_folder = new List<string>();
            foreach (var item in buckets)
            {
                m_server_folder.Add(item.Name);
                for (int i = 0; i < m_plat.Length; i++)
                {
                    if (!ossClient.DoesObjectExist(Bucket, m_plat[i]))
                    {
                        CreateBucketFolder(item.Name, m_plat[i] + "/");
                    }
                }
            }
        }
        private static void CreateBucketFolder(string bucket,string name)
        {
            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    ossClient.PutObject(bucket, name, memStream);
                }
            }
            catch (OssException ex)
            {
                Debug.LogFormat("CreateBucket Failed with error info: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
        }

        private static void GetLocalBucket()
        {
            m_local_folder = Utility.FileSystem.GetFolder(EditorMeun.GetLocalHotFixRootPath());
            for (int i = 0; i < m_local_folder.Count; i++)
            {
                for (int j = 0; j < m_plat.Length; j++)
                {
                    string dir = EditorMeun.GetLocalHotFixPath(m_local_folder[i], m_plat[j]);
                    if (!Utility.FileSystem.HasFolder(dir))
                    {
                        Utility.FileSystem.CreateFolder(dir);
                    }
                }
            }
        }

        public void Update()
        {
            m_updatecallback?.Invoke();
        }
        public void OnGUI()
        {
            Open();
            GUI.Box(new Rect(5, 5, 1320, 640), "", "HelpBox");

            GUI.Label(new Rect(10, 7, 100, 20), "基准:");
            m_checktype = (CheckType)EditorGUI.EnumPopup(new Rect(40, 10, 60, 20), m_checktype);

            GUI.Label(new Rect(110, 7, 100, 20), "本地库:");
            m_local_index = EditorGUI.Popup(new Rect(160, 10, 200, 22), m_local_index, m_local_folder.ToArray());

            GUI.Label(new Rect(370, 7, 100, 20), "云资源:");
            m_sever_index = EditorGUI.Popup(new Rect(420, 10, 200, 22), m_sever_index, m_server_folder.ToArray());

            GUI.Label(new Rect(630, 7, 60, 20), "平台:");
            m_plat_index = EditorGUI.Popup(new Rect(660, 10, 200, 22), m_plat_index, m_plat);

            m_MultiColumnTreeView.OnGUI(new Rect(10, 35, 1310, 605));
            if (GUI.Button(new Rect(870, 10, 110, 18), "提交对比"))
            {
                m_MultiColumnTreeView.Reload();
            }
            if (GUI.Button(new Rect(1240, 8, 80, 20), "提交同步"))
            {
            }


        }
    }
}