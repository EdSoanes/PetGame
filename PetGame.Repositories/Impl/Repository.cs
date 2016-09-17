using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using PetGame.Models;

namespace PetGame.Repositories.Impl
{
    public abstract class Repository
    {
        private readonly string _connectionStringName = "";

        /// <summary>
        /// The connection string.
        /// Comments are located in the 'dbo.Comments' table.
        /// </summary>
        private string _connectionString = "";
        protected string ConnString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                    _connectionString = ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString;

                return _connectionString;
            }
        }

        public Repository(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        protected T ExecuteScalar<T>(string sql, object parms = null)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                return conn.ExecuteScalar<T>(sql, parms);
            }
        }

        protected int Execute(string sql, int? timeout = null, object parms = null)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                return conn.Execute(sql, parms, null, timeout, null);
            }
        }

        protected int Execute(string sql, object parms)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                var res = conn.Execute(sql, parms);

                return res;
            }
        }

        protected T BaseGetById<T>(long id) where T : class
        {
            string pk = GetPrimaryKey<T>();
            return BaseGetByKey<T>(pk, id).FirstOrDefault();
        }

        protected IEnumerable<T> BaseGetByIds<T>(long[] ids) where T : class
        {
            string pk = GetPrimaryKey<T>();
            return BaseGetByKeys<T>(pk, ids);
        }

        protected IEnumerable<T> BaseGet<T>(string sql, object param = null) where T : class
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();

                var res = conn.Query<T>(sql, param);

                return res;
            }
        }

        protected async Task<IEnumerable<T>> BaseGetAsync<T>(string sql, object param = null)
            where T : class
        {
            using (var conn = new SqlConnection(ConnString))
            {
                await conn.OpenAsync();

                var result = await conn.QueryAsync<T>(sql, param);
                return result;
            }
        }

        protected async Task<T> BaseGetMultipleAsync<T>(string sql, CommandType commandType, object param, Func<SqlMapper.GridReader, Task<T>> action)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                await conn.OpenAsync();

                using (var reader = await conn.QueryMultipleAsync(sql, param, commandType: commandType))
                {
                    return await action(reader);
                }
            }
        }

        protected IEnumerable<T> BaseGetAll<T>() where T : class
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();

                var tableName = typeof(T).Name;
                var query = string.Format("SELECT * FROM {0} ", tableName);
                var res = conn.Query<T>(query);

                return res;
            }
        }

        protected int BaseUpdate(string sql)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                return conn.Execute(sql);
            }
        }

        protected T BaseUpdate<T>(T entity) where T : class
        {
            T existing = null;

            var tbl = typeof(T).Name;
            var pk = GetPrimaryKey<T>();
            var id = GetPrimaryKeyValue<T>(entity);
            var fields = GetNonPrimaryKeys<T>();

            //Check to see if the entity already exists...
            if (id > 0)
                existing = BaseGetById<T>(id);

            //If so then update the existing entity
            if (existing != null)
            {
                var val = "";
                foreach (var field in fields)
                    val += string.Format("{0} = @{1}, ", field, field);

                if (val.Length > 2)
                    val = val.Substring(0, val.Length - 2);

                //Create an update statement
                var sql = string.Format("UPDATE {0} SET {1} WHERE {2} = {3}", tbl, val, pk, id);

                //Execute!
                using (var conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    conn.Execute(sql, entity);

                    return BaseGetById<T>(id);
                }
            }
            else
            {
                var fieldList = "[" + string.Join("], [", fields) + "]";
                var valList = "@" + string.Join(", @", fields);

                var sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2});SELECT CAST(SCOPE_IDENTITY() as int)", tbl, fieldList, valList);

                using (var conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    var newId = conn.Query<int>(sql, entity).Single();

                    return BaseGetById<T>(newId);
                }
            }
        }

        protected async Task<T> BaseUpdateAsync<T>(T entity) where T : class
        {
            T existing = null;

            var tbl = typeof(T).Name;
            var pk = GetPrimaryKey<T>();
            var id = GetPrimaryKeyValue<T>(entity);
            var fields = GetNonPrimaryKeys<T>();

            //Check to see if the entity already exists...
            if (id > 0)
                existing = BaseGetById<T>(id);

            //If so then update the existing entity
            if (existing != null)
            {
                var val = "";
                foreach (var field in fields)
                    val += string.Format("{0} = @{1}, ", field, field);

                if (val.Length > 2)
                    val = val.Substring(0, val.Length - 2);

                //Create an update statement
                var sql = string.Format("UPDATE [dbo].[{0}] SET {1} WHERE {2} = {3}", tbl, val, pk, id);

                //Execute!
                using (var conn = new SqlConnection(ConnString))
                {
                    await conn.OpenAsync();
                    await conn.ExecuteAsync(sql, entity);

                    return BaseGetById<T>(id);
                }
            }
            else
            {
                var fieldList = "[" + string.Join("], [", fields) + "]";
                var valList = "@" + string.Join(", @", fields);

                var sql = string.Format("INSERT INTO [dbo].[{0}] ({1}) VALUES ({2});SELECT CAST(SCOPE_IDENTITY() as bigint)", tbl, fieldList, valList);

                using (var conn = new SqlConnection(ConnString))
                {
                    await conn.OpenAsync();
                    var newIds = await conn.QueryAsync<long>(sql, entity);
                    var newId = newIds.Single();
                    return BaseGetById<T>(newId);
                }
            }
        }

        protected bool BaseDelete<T>(long id) where T : class
        {
            var tbl = typeof(T).Name;
            var pk = GetPrimaryKey<T>();
            var sql = string.Format("DELETE FROM {0} WHERE {1} = {2}", tbl, pk, id);

            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                return conn.Execute(sql) > 0;
            }
        }

        protected bool BaseDelete<T>(string id) where T : class
        {
            var tbl = typeof(T).Name;
            var pk = GetPrimaryKey<T>();
            var sql = string.Format("DELETE FROM {0} WHERE {1} = '{2}'", tbl, pk, id);

            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                return conn.Execute(sql) > 0;
            }
        }

        protected IEnumerable<T> BaseGetByKey<T>(string key, long id) where T : class
        {
            if (!string.IsNullOrEmpty(key))
            {
                using (var conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    var tbl = typeof(T).Name;
                    var query = string.Format("SELECT * FROM {0} WHERE {1} = {2}", tbl, key, id);
                    var res = conn.Query<T>(query);

                    return res;
                }
            }
            else
                return Enumerable.Empty<T>();
        }

        protected IEnumerable<T> BaseGetByKey<T>(string key, string id) where T : class
        {
            if (!string.IsNullOrEmpty(key))
            {
                using (var conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    var tbl = typeof(T).Name;
                    var query = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", tbl, key, id);
                    var res = conn.Query<T>(query);

                    return res;
                }
            }
            else
                return Enumerable.Empty<T>();
        }

        protected IEnumerable<T> BaseGetByKeys<T>(string key, long[] ids) where T : class
        {
            if (!string.IsNullOrEmpty(key) && ids.Any())
            {
                using (var conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    var tbl = typeof(T).Name;
                    var query = string.Format("SELECT * FROM {0} WHERE {1} IN ({2})", tbl, key, string.Join(", ", ids.Select(x => x.ToString())));
                    var res = conn.Query<T>(query);

                    return res;
                }
            }
            else
                return Enumerable.Empty<T>();
        }

        private string GetPrimaryKey<T>() where T : class
        {
            var t = typeof(T);
            foreach (PropertyInfo prop in t.GetProperties())
            {
                object[] attributes = prop.GetCustomAttributes(typeof(KeyAttribute), true);
                if (attributes.Length == 1)
                {
                    return prop.Name;
                }
            }
            return string.Empty;
        }

        private string[] GetNonPrimaryKeys<T>() where T : class
        {
            var res = new List<string>();

            var t = typeof(T);
            foreach (PropertyInfo prop in t.GetProperties())
            {
                object[] attributes = prop.GetCustomAttributes(true);
                if (!attributes.Any(x => x is RepoIgnoreAttribute || x is KeyAttribute))
                    res.Add(prop.Name);
            }

            return res.ToArray();
        }

        private long GetPrimaryKeyValue<T>(T entity) where T : class
        {
            var t = typeof(T);
            foreach (PropertyInfo prop in t.GetProperties())
            {
                object[] attributes = prop.GetCustomAttributes(typeof(KeyAttribute), true);
                if (attributes.Length == 1)
                {
                    return (long)prop.GetValue(entity, null);
                }
            }
            return -1;
        }
    }
}
