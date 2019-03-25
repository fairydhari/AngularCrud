using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public struct StoredProcedureParameter
    {
        public Type Type;
        public string ColumnName;
        public object Value;
        public ParameterDirection? Direction; 

        public StoredProcedureParameter(Type type, string columnName, object value, ParameterDirection? direction=null)
        {
            Type = type;
            ColumnName = columnName;
            Value = value;
            if (direction == null)
            {
                Direction = ParameterDirection.Input;
            }
            else
            {
                Direction = ParameterDirection.Output;
            }
        }
        
        }
}
