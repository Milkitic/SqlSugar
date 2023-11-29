using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace SqlSugar
{
    public class MySqlBlukCopy<T> 
    {
        internal SqlSugarProvider Context { get; set; }
        internal ISqlBuilder Builder { get; set; }
        internal T[] Entitys { get; set; }
        internal string Chara { get; set; }
        private MySqlBlukCopy()
        {

        }
        public MySqlBlukCopy(SqlSugarProvider context, ISqlBuilder builder, T []entitys)
        {
            this.Context = context;
            this.Builder = builder;
            this.Entitys = entitys;
        }
        public bool ExecuteBulkCopy(string characterSet) 
        {
            this.Chara = characterSet;
            return ExecuteBulkCopy();
        }

        public bool ExecuteBulkCopy()
        {
            throw new DbNotSupportedException(DbType.MySql);
        }

        public Task<bool> ExecuteBulkCopyAsync()
        {
            return Task.FromResult(ExecuteBulkCopy());
        }

        public Task<bool> ExecuteBulkCopyAsync(string characterSet)
        {
            this.Chara = characterSet;
            return Task.FromResult(ExecuteBulkCopy());
        }

        #region  Helper
        private string GetChara()
        {
            if (this.Chara == null)
            {
                return "utf8mb4";
            }
            else
            {
                return this.Chara;
            }
        }

        private void CloseDb()
        {
            if (this.Context.CurrentConnectionConfig.IsAutoCloseConnection && this.Context.Ado.Transaction == null)
            {
                this.Context.Ado.Connection.Close();
            }
        }

        /// <summary>
        ///DataTable to CSV
        /// </summary>
        /// <param name="table">datatable</param>
        /// <returns>CSV</returns>
        public string DataTableToCsvString(DataTable table)
        {
            if (table.Rows.Count == 0)
                return "";
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && (row[colum].ToString().Contains(",") || row[colum].ToString().Contains("\r") || row[colum].ToString().Contains("\"") || row[colum].ToString().Contains("\n")))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else if (colum.DataType == typeof(bool))
                    {
                        if (row[colum] == DBNull.Value)
                        {
                            sb.Append("NULL");
                        }
                        else
                        {
                            sb.Append(row[colum].ObjToBool() ? 1 : 0);
                        }
                    }
                    else if (colum.DataType == UtilConstants.DateType&& row[colum] != null && row[colum] != DBNull.Value) 
                    {
                        sb.Append(row[colum].ObjToDate().ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    }
                    else if (row[colum] == null || row[colum] == DBNull.Value)
                    {
                        sb.Append("NULL");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }


        private static object GetValue(PropertyInfo p, T item)
        {
            var result= p.GetValue(item, null);
            if (result != null && UtilMethods.GetUnderType(p.PropertyType) == UtilConstants.BoolType) 
            {
                if (result.ObjToBool() == false) 
                {
                    result = null;
                }
            }
            return result;
        }

        #endregion
    }
}