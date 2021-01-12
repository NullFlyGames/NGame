using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NGame.SqlServer
{
    public partial class DBManaged
    {

        private static string ConnectdString;
        private static List<IHandle> Handles;
        internal static void Started()
        {
            Handles = new List<IHandle>();
        }
        internal static void Dispose()
        {
            Handles.ForEach(ReferencePool.Release);
        }
        public static void Install(string adder, ushort port, string schema, string user, string pwd)
        {
            ConnectdString = $"server={adder};port={port};database={schema};uid={user};password={pwd};";
            //ConnectdString = $"server=localhost;port=3306;database=games_schemas;uid=root;password=NullFly;";
        }
        internal static bool IsExists<T>() where T : class, ITable, new()
        {
            DBHandle<T> handle = GetHandle<T>();
            if (handle == null) handle = LoadHandle<T>();

            string sql = "select * from information_schema.tables where table_name ='" + typeof(T).Name.ToLower() + "s';";
            if (handle.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                handle.Database.Connection.Open();
            }
            DbCommand command = handle.Database.Connection.CreateCommand();
            command.CommandText = sql;
            object o = command.ExecuteScalar();
            return o != null;
        }
        internal static void CreateIfNotExists<T>() where T : class, ITable, new()
        {
            if (IsExists<T>()) return;
            DBHandle<T> handle = GetHandle<T>();
            if (handle == null) handle = LoadHandle<T>();
            if (handle.Database.Connection.State != System.Data.ConnectionState.Open)
            {
                handle.Database.Connection.Open();
            }
            DbCommand command = handle.Database.Connection.CreateCommand();
            command.CommandText = "create table `" + typeof(T).Name.ToLower() + "s`(";
            PropertyInfo[] methods = typeof(T).GetProperties();
            for (int i = 0; i < methods.Length; i++)
            {
                command.CommandText += "`" + methods[i].Name + "` ";
                command.CommandText += methods[i].PropertyType.GetFieldTypeName() + "(";
                command.CommandText += methods[i].PropertyType.GetTypeLength() + ") not null" + (methods[i].Name == "id" ? " AUTO_INCREMENT," : ",");
            }
            command.CommandText += "primary key(`id`)";
            command.CommandText += ")  ENGINE = InnoDB DEFAULT CHARSET = utf8; ";
            int result = command.ExecuteNonQuery();
            if (result != 0)
            {
                Debug.Log("创建失败");
                return;
            }
            //System.Data.Entity.Migrations.DbMigrator
            command = handle.Database.Connection.CreateCommand();
            command.CommandText = "INSERT INTO __migrationhistory (MigrationId,ContextKey,Model,ProductVersion)VALUES(";
            command.CommandText += "'" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_InitialCreate',";
            command.CommandText += "'NServer.DB.DBHandle`1[" + typeof(T).FullName + "]',";
            string hex = new ModelCompressor().Compress(handle.GetModel()).ToHex();
            System.Xml.Linq.XDocument document = new ModelCompressor().Decompress(hex.HexToString());
            command.CommandText += "0x" +hex + ",";
            command.CommandText += "'6.4.4');";
            result = command.ExecuteNonQuery();
            Debug.Log("创建成功");
        }



        /// <summary>
        /// 加载数据表代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DBHandle<T> LoadHandle<T>() where T : class, ITable, new()
        {
            DBHandle<T> handle = GetHandle<T>();
            if (handle != null) return handle;
            handle = ReferencePool.Acquire<DBHandle<T>>();
            Handles.Add(handle);
            return handle;
        }
        /// <summary>
        /// 获取数据表代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DBHandle<T> GetHandle<T>() where T : class, ITable, new()
        {
            for (int i = 0; i < Handles.Count; i++)
            {
                if (Handles[i].GetType() == typeof(T))
                    return (DBHandle<T>)Handles[i];
            }
            return default;
        }
        /// <summary>
        /// 获取表长度
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int Count<T>() where T : class, ITable, new()
        {
            DBHandle<T> handle = GetHandle<T>();
            if (handle == null) { handle = LoadHandle<T>(); handle.OnStarted(); }
            return handle.Count();
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">查询方式</param>
        /// <returns></returns>
        public static T Where<T>(Expression<Func<T, bool>> predicate) where T : class, ITable, new()
        {
            DBHandle<T> handle = GetHandle<T>();
            if (handle == null) { handle = LoadHandle<T>(); handle.OnStarted(); }
            return handle.Where(predicate);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">查询方式</param>
        /// <returns></returns>
        public static List<T> Wheres<T>(Expression<Func<T, bool>> predicate) where T : class, ITable, new()
        {
            DBHandle<T> handle = GetHandle<T>();
            if (handle == null) { handle = LoadHandle<T>(); handle.OnStarted(); }
            return handle.Wheres(predicate);
        }
        /// <summary>
        /// 添加一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DBHandle<T> Add<T>(T table) where T : class, ITable, new()
        {
            DBHandle<T> handle = GetHandle<T>();
            if (handle == null) { handle = LoadHandle<T>(); handle.OnStarted(); }
            return handle.Add(table);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DBHandle<T> AddRange<T>(List<T> table) where T : class, ITable, new()
        {
            DBHandle<T> handle = GetHandle<T>();
            if (handle == null) { handle = LoadHandle<T>(); handle.OnStarted(); }
            return handle.AddRange(table);
        }
        /// <summary>
        /// 移除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DBHandle<T> Remove<T>(T table) where T : class, ITable, new()
        {
            DBHandle<T> handle = GetHandle<T>();
            if (handle == null) { handle = LoadHandle<T>(); handle.OnStarted(); }
            return handle.Remove(table);
        }
        /// <summary>
        /// 移除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DBHandle<T> RemoveRange<T>(List<T> table) where T : class, ITable, new()
        {
            DBHandle<T> handle = GetHandle<T>();
            if (handle == null) { handle = LoadHandle<T>(); handle.OnStarted(); }
            return handle.RemoveRange(table);
        }
        /// <summary>
        /// 保存修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DBHandle<T> SaveChange<T>() where T : class, ITable, new()
        {
            DBHandle<T> handle = GetHandle<T>();
            if (handle == null) return null;
            handle.SaveChanges();
            return handle;
        }
        /// <summary>
        /// 异步保存修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<DBHandle<T>> SaveChangeAsync<T>() where T : class, ITable, new()
        {
            DBHandle<T> handle = GetHandle<T>();
            if (handle == null) return null;
            await handle.SaveChangesAsync();
            return handle;
        }
    }
}
