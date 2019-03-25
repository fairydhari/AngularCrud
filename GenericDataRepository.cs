using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity.Core.Objects;

namespace DataAccessLayer
{
    public class ICompanySPFactory<T> : IGenericDataRepository<T> where T: class
        {
        //private JobPortalEntities entities = null;
        //DbSet<T> _objectSet;
        JobPortalEntities context = null;
        private DbSet<T> entities = null;
        public ICompanySPFactory(JobPortalEntities context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public ICompanySPFactory() { }
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new JobPortalEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }
        public virtual IEnumerable<T> GetAll()
        {
            List<T> list;
            using (var context = new JobPortalEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
              

                list = dbQuery                   
                    .ToList<T>();
            }
            return list;
        }
        //public List<U> GetBy<T, U>(DbContext context, Expression<Func<T, bool>> exp, Expression<Func<T, U>> columns)
        //         where T : class
        //         where U : class
        //{
        //    List<T> list;
        //   // IQueryable<T> dbQuery = context.Set<T>();
        //    //list = dbQuery

        //    //        .ToList<T>().Where(exp).Select<T, U>(columns).ToList();

        //    return context.Set<T>().Where(exp).Select<T, U>(columns).ToList();
        //}
        //public virtual IList<T> GetSingleList()
        //{
        //    List<T> list;
        //    using (var context = new JobPortalEntities())
        //    {
        //        //List<T> list;
        //        //using (var context = _contextFactory())
        //        //{
        //        //    IQueryable<T> dbQuery = context.Set<T>();

        //        //    list = dbQuery.ToList<T>();
        //        //}

        //        //return list;
        //        IQueryable<T> dbQuery = context.Set<T>();

        //        //Apply eager loading
        //        //foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
        //        //    dbQuery = dbQuery.Include<T, object>(navigationProperty);

        //        list = dbQuery

        //            .ToList<T>();
        //    }
        //    return list;
        //}
        public virtual IList<T> GetList(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new JobPortalEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (var context = new JobPortalEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            return item;
        }
        public virtual void Add(params T[] items)
        {
            using (var context = new JobPortalEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Added;
                }
                context.SaveChanges();                
            }
        }
        public virtual void Update(params T[] items)
        {
            using (var context = new JobPortalEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public virtual void Remove(params T[] items)
        {
            using (var context = new JobPortalEntities())
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }
        public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
        {
            
            using (var context = new JobPortalEntities())
            {
                
                return context.Database.SqlQuery<T>(sql, parameters);
            }
        }

        /// <summary>
        /// Get Data From Database
        /// <para>Use it when to retive data through a stored procedure</para>
        /// </summary>
        public IEnumerable<T> ExecuteQuery(string spQuery, object[] parameters)
        {
            using (var context = new JobPortalEntities())
            {
                if(parameters!=null)
                return context.Database.SqlQuery<T>(spQuery, parameters).ToList();
                else
                    return context.Database.SqlQuery<T>(spQuery).ToList();
            }
        }
        /// <summary>
        /// Get Single Data From Database
        /// <para>Use it when to retive single data through a stored procedure</para>
        /// </summary>
        public T ExecuteQuerySingle(string spQuery, object[] parameters)
        {
            using (var context = new JobPortalEntities())
            {
                return context.Database.SqlQuery<T>(spQuery, parameters).FirstOrDefault();
            }
        }
        /// <summary>
        /// Insert/Update/Delete Data To Database
        /// <para>Use it when to Insert/Update/Delete data through a stored procedure</para>
        /// </summary>
        public int ExecuteCommand(string spQuery, object[] parameters)
        {
            int result = 0;
            try
            {
                using (var context = new JobPortalEntities())
                {
                    result = context.Database.SqlQuery<int>(spQuery, parameters).FirstOrDefault();
                }
            }
            catch { }
            return result;
        }
        /// <summary>
        /// With Return Value
        /// </summary>
        /// <param name="spQuery"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public int ExecuteSqlCommandWithOutput(string spQuery, StoredProcedureParameter[] sqlParameters)
        {
            int result = 0;
            try
            {
                using (context = new JobPortalEntities())
                {

                    //myContext.GetCustomerCount(output);
                    //Console.WriteLine(output.Value);
                    // var s = context.Database.SqlQuery<int>(spQuery, parameters);
                    /*  var bookIdParameter = new SqlParameter();
                      bookIdParameter.ParameterName = "@CompanyId";
                      bookIdParameter.Direction = ParameterDirection.Output;
                      bookIdParameter.SqlDbType = SqlDbType.Int;

                      var authors = context.Database.ExecuteSqlCommand("ins_Company @CompanyId OUT,@CompanyName",
                          bookIdParameter,new SqlParameter("@CompanyName", "Book")
                           );*/
                    // Console.WriteLine(bookIdParameter.Value);
                    // var query2 = context.Database.ExecuteSqlCommand("ins_Company", parameters);
                    var execProc = new StringBuilder(spQuery + " ");
                    // SqlParameter[] dparameters = new SqlParameter[sqlParameters.Length];
                    List<SqlParameter> sqlParams = new List<SqlParameter>();
                    foreach (var parameter in sqlParameters)                    
                    {
                        
                        switch (parameter.Type.Name.ToLower())
                        {
                            case "int32":
                                //query.SetInt32(parameter.ColumnName.ToLower(), int.Parse(parameter.Value.ToString()));
                                sqlParams.Add(new SqlParameter() { ParameterName = "@" + parameter.ColumnName, SqlDbType = SqlDbType.Int, Direction = (ParameterDirection)parameter.Direction, Value = parameter.Value });
                                break;
                            case "int":
                                //query.SetInt32(parameter.ColumnName.ToLower(), int.Parse(parameter.Value.ToString()));
                                sqlParams.Add(new SqlParameter() { ParameterName = "@" + parameter.ColumnName, SqlDbType = SqlDbType.Int, Direction = (ParameterDirection)parameter.Direction, Value = parameter.Value });
                                break;
                            //case "guid":
                            //    query.SetGuid(parameter.ColumnName.ToLower(), Guid.Parse(parameter.Value.ToString()));
                            //    break;
                            //case "nullable`1": // Nullable Guid
                            //    if (parameter.Value != null && !string.IsNullOrWhiteSpace(parameter.Value.ToString()))
                            //        query.SetGuid(parameter.ColumnName.ToLower(), Guid.Parse(parameter.Value.ToString()));
                            //    else
                            //        query.SetParameter(parameter.ColumnName.ToLower(), null, NHibernateUtil.Guid);
                            //    break;
                            case "boolean":
                                sqlParams.Add(new SqlParameter() { ParameterName = "@" + parameter.ColumnName, SqlDbType = SqlDbType.Bit, Direction = (ParameterDirection)parameter.Direction, Value = parameter.Value });
                               // query.SetBoolean(parameter.ColumnName.ToLower(), bool.Parse(parameter.Value.ToString()));
                                break;
                            case "datetime":
                                sqlParams.Add(new SqlParameter() { ParameterName = "@" + parameter.ColumnName, SqlDbType = SqlDbType.DateTime, Direction = (ParameterDirection)parameter.Direction, Value = parameter.Value });
                                // query.SetDateTime(parameter.ColumnName.ToLower(), DateTime.Parse(parameter.Value.ToString()));
                                break;
                            case "decimal":
                                sqlParams.Add(new SqlParameter() { ParameterName = "@" + parameter.ColumnName, SqlDbType = SqlDbType.Decimal, Direction = (ParameterDirection)parameter.Direction, Value = parameter.Value });
                                //  query.SetDecimal(parameter.ColumnName.ToLower(), decimal.Parse(parameter.Value.ToString()));
                                break;
                            case "double":
                                // query.SetDouble(parameter.ColumnName.ToLower(), double.Parse(parameter.Value.ToString()));
                                sqlParams.Add(new SqlParameter() { ParameterName = "@" + parameter.ColumnName, SqlDbType = SqlDbType.Float, Direction = (ParameterDirection)parameter.Direction, Value = parameter.Value });
                                break;
                            case "string":
                                sqlParams.Add(new SqlParameter() { ParameterName = "@" + parameter.ColumnName, SqlDbType = SqlDbType.VarChar, Direction = (ParameterDirection)parameter.Direction, Value = parameter.Value });
                                break;
                            case "timespan":
                                sqlParams.Add(new SqlParameter() { ParameterName = "@" + parameter.ColumnName, SqlDbType = SqlDbType.Timestamp, Direction = (ParameterDirection)parameter.Direction, Value = parameter.Value });
                                break;
                        }
                        
                        if (parameter.Direction==ParameterDirection.Output)
                        execProc.Append("@"+parameter.ColumnName +" OUT" + ", ");
                        else
                            execProc.Append("@"+parameter.ColumnName + ", ");
                        
                    }
                    //"ins_Company @CompanyId OUT, @CompanyName"
                    var query = context.Database.ExecuteSqlCommand(execProc.ToString().Remove(execProc.ToString().LastIndexOf(',')), sqlParams.ToArray());
                    var outputColumn = sqlParams.Find(a => a.Direction == ParameterDirection.Output).Value;
                     result = (int)sqlParams.ToArray()[0].Value;
                }
            }
            catch (Exception ex) { }
            return result;
        }
       
        /*  private bool disposed = false;

          protected virtual void Dispose(bool disposing)
          {
              if (!this.disposed)
              {
                  if (disposing)
                  {
                      context.Dispose();
                  }
              }
              this.disposed = true;
          }

          public void Dispose()
          {
              Dispose(true);
              GC.SuppressFinalize(this);
          }*/
        /* rest of code omitted */
        /*
                public object ExecuteStoredProcedure(string spProcName, List<Structs.StoredProcedureParameter> parameters)
                {
                    var execProc = new StringBuilder("exec " + spProcName + " ");
                    foreach (var parameter in parameters)
                    {
                        execProc.Append("@" + parameter.ColumnName + "=:" + parameter.ColumnName.ToLower() + ", ");
                    }
                    var result = parameters.Count > 0
                        ? execProc.ToString().Substring(0, execProc.Length - 2)
                        : execProc.ToString();

                    var query = CreateSQLQuery(result);
                    foreach (var parameter in parameters)
                    {
                        switch (parameter.Type.Name.ToLower())
                        {
                            case "int32":
                                query.SetInt32(parameter.ColumnName.ToLower(), int.Parse(parameter.Value.ToString()));
                                break;
                            case "guid":
                                query.SetGuid(parameter.ColumnName.ToLower(), Guid.Parse(parameter.Value.ToString()));
                                break;
                            case "boolean":
                                query.SetBoolean(parameter.ColumnName.ToLower(), bool.Parse(parameter.Value.ToString()));
                                break;
                            case "datetime":
                                query.SetDateTime(parameter.ColumnName.ToLower(), DateTime.Parse(parameter.Value.ToString()));
                                break;
                            case "decimal":
                                query.SetDecimal(parameter.ColumnName.ToLower(), decimal.Parse(parameter.Value.ToString()));
                                break;
                            case "double":
                                query.SetDouble(parameter.ColumnName.ToLower(), double.Parse(parameter.Value.ToString()));
                                break;
                            case "string":
                                query.SetString(parameter.ColumnName.ToLower(), parameter.Value.ToString());
                                break;
                            case "timespan":
                                query.SetTimeSpan(parameter.ColumnName.ToLower(), TimeSpan.Parse(parameter.Value.ToString()));
                                break;
                        }
                    }
                    return query.UniqueResult();
                }

                public IList<T> ExecuteStoredProcedureForList<T>(string spProcName, List<Structs.StoredProcedureParameter> parameters)
                {
                    var execProc = new StringBuilder("exec " + spProcName + " ");

                    foreach (var parameter in parameters)
                    {
                        execProc.Append("@" + parameter.ColumnName + "=:" + parameter.ColumnName.ToLower() + ", ");
                    }


                    var result = parameters.Count > 0
                        ? execProc.ToString().Substring(0, execProc.Length - 2)
                        : execProc.ToString();

                    var query = CreateSQLQuery(result);
                    foreach (var parameter in parameters)
                    {
                        switch (parameter.Type.Name.ToLower())
                        {
                            case "int32":
                                query.SetInt32(parameter.ColumnName.ToLower(), int.Parse(parameter.Value.ToString()));
                                break;
                            case "guid":
                                query.SetGuid(parameter.ColumnName.ToLower(), Guid.Parse(parameter.Value.ToString()));
                                break;
                            case "nullable`1": // Nullable Guid
                                if (parameter.Value != null && !string.IsNullOrWhiteSpace(parameter.Value.ToString()))
                                    query.SetGuid(parameter.ColumnName.ToLower(), Guid.Parse(parameter.Value.ToString()));
                                else
                                    query.SetParameter(parameter.ColumnName.ToLower(), null, NHibernateUtil.Guid);
                                break;
                            case "boolean":
                                query.SetBoolean(parameter.ColumnName.ToLower(), bool.Parse(parameter.Value.ToString()));
                                break;
                            case "datetime":
                                query.SetDateTime(parameter.ColumnName.ToLower(), DateTime.Parse(parameter.Value.ToString()));
                                break;
                            case "decimal":
                                query.SetDecimal(parameter.ColumnName.ToLower(), decimal.Parse(parameter.Value.ToString()));
                                break;
                            case "double":
                                query.SetDouble(parameter.ColumnName.ToLower(), double.Parse(parameter.Value.ToString()));
                                break;
                            case "string":
                                query.SetString(parameter.ColumnName.ToLower(), parameter.Value.ToString());
                                break;
                            case "timespan":
                                query.SetTimeSpan(parameter.ColumnName.ToLower(), TimeSpan.Parse(parameter.Value.ToString()));
                                break;
                        }
                    }


                    return query.AddEntity(typeof(T)).List<T>();
                }
                public IList<T> ExecuteStoredProcedureAsListNoUpdate<T>(string spProcName, List<Structs.StoredProcedureParameter> parameters)
                {
                    var execProc = new StringBuilder("exec " + spProcName + " ");

                    foreach (var parameter in parameters)
                    {
                        execProc.Append("@" + parameter.ColumnName + "=:" + parameter.ColumnName.ToLower() + ", ");
                    }


                    var result = parameters.Count > 0
                        ? execProc.ToString().Substring(0, execProc.Length - 2)
                        : execProc.ToString();

                    var query = CreateSQLQuery(result);
                    foreach (var parameter in parameters)
                    {
                        switch (parameter.Type.Name.ToLower())
                        {
                            case "int32":
                                query.SetInt32(parameter.ColumnName.ToLower(), int.Parse(parameter.Value.ToString()));
                                break;
                            case "guid":
                                query.SetGuid(parameter.ColumnName.ToLower(), Guid.Parse(parameter.Value.ToString()));
                                break;
                            case "nullable`1": // Nullable Guid
                                if (parameter.Value != null && !string.IsNullOrWhiteSpace(parameter.Value.ToString()))
                                    query.SetGuid(parameter.ColumnName.ToLower(), Guid.Parse(parameter.Value.ToString()));
                                else
                                    query.SetParameter(parameter.ColumnName.ToLower(), null, NHibernateUtil.Guid);
                                break;
                            case "boolean":
                                query.SetBoolean(parameter.ColumnName.ToLower(), bool.Parse(parameter.Value.ToString()));
                                break;
                            case "datetime":
                                query.SetDateTime(parameter.ColumnName.ToLower(), DateTime.Parse(parameter.Value.ToString()));
                                break;
                            case "decimal":
                                query.SetDecimal(parameter.ColumnName.ToLower(), decimal.Parse(parameter.Value.ToString()));
                                break;
                            case "double":
                                query.SetDouble(parameter.ColumnName.ToLower(), double.Parse(parameter.Value.ToString()));
                                break;
                            case "string":
                                query.SetString(parameter.ColumnName.ToLower(), parameter.Value.ToString());
                                break;
                            case "timespan":
                                query.SetTimeSpan(parameter.ColumnName.ToLower(), TimeSpan.Parse(parameter.Value.ToString()));
                                break;
                        }
                    }
                    return query.List().Cast<T>().ToList();

                }

                */
    }
}
