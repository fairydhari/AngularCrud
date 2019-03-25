using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    class SqlHelper
    {
        /*  public object ExecuteStoredProcedure(string spProcName, List<BusinessLayer.StoredProcedureParameter> parameters)
          {
              //var execProc = new StringBuilder("exec " + spProcName + " ");
              var execProc = new StringBuilder(spProcName + " ");
              foreach (var parameter in parameters)
              {
                  execProc.Append("@" + parameter.ColumnName + "=:" + parameter.ColumnName.ToLower() + ", ");
              }
              var result = parameters.Count > 0
                  ? execProc.ToString().Substring(0, execProc.Length - 2)
                  : execProc.ToString();

              //  var query = CreateSQLQuery(result);
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
              return query.UniqueResult();*/
    }
}

