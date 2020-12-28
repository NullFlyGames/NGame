using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml.Linq;
using System.Data.Entity.Infrastructure;

namespace NGame.Sql
{
    public partial class DBManaged
    {
        public class DBHandle<T> : DbContext, IHandle where T : class, ITable, new()
        {
            public DbSet<T> Ado { get; set; }
            public DbModelBuilder builder;
            public DBHandle() : base(ConnectdString)
            {
                this.Configuration.LazyLoadingEnabled = true;
                this.Configuration.AutoDetectChangesEnabled = true;  //自动监测变化，默认值为 true 
                this.Database.Log = Ex.Log;
            }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                builder = modelBuilder;
                base.OnModelCreating(modelBuilder);
            }

            internal DBHandle<T> OnStarted()
            {
                this.Database.CreateIfNotExists();
                DBManaged.CreateIfNotExists<T>();
                return this;
            }
            public T Where(Expression<Func<T, bool>> predicate)
            {
                try
                {
                    return Ado.Where(predicate).FirstOrDefault();
                }
                catch
                {
                    return default;
                }
            }
            public List<T> Wheres(Expression<Func<T, bool>> predicate)
            {
                try
                {
                    return Ado.Where(predicate).ToList();
                }
                catch
                {
                    return default;
                }
            }

            public int Count()
            {
                try
                {
                    return Ado.Count();
                }
                catch
                {
                    return default;
                }
            }

            public DBHandle<T> Add(T table)
            {
                Ado.Add(table);
                SaveChanges();
                return this;
            }
            public DBHandle<T> AddRange(List<T> table)
            {
                this.Ado.AddRange(table);
                SaveChanges();
                return this;
            }
            public DBHandle<T> Remove(T table)
            {
                T result = Where(a => a.id == table.id);
                if (result == null) return this;
                this.Ado.Remove(table);
                SaveChanges();
                return this;
            }
            public DBHandle<T> RemoveRange(List<T> table)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    if (Where(a => a.id == table[i].id) != null)
                    {
                        Remove(table[i]);
                    }
                }
                return this;
            }
            public void Release()
            {
                SaveChanges();
                Dispose();
            }
        }
    }
}