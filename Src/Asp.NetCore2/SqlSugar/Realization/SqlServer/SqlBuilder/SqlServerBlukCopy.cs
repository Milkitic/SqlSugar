using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar 
{
    public class SqlServerBlukCopy
    {
        internal List<IGrouping<int, DbColumnInfo>> DbColumnInfoList { get;   set; }
        internal SqlSugarProvider Context { get;   set; }
        internal ISqlBuilder Builder { get; set; }
        internal InsertBuilder InsertBuilder { get; set; }
        internal object[] Inserts { get;  set; }

        public int ExecuteBulkCopy()
        {
            throw new DbNotSupportedException(DbType.SqlServer);
        }

        public async Task<int> ExecuteBulkCopyAsync()
        {
            throw new DbNotSupportedException(DbType.SqlServer);
        }

        private DataTable GetCopyData()
        {
            var dt = this.Context.Ado.GetDataTable("select top 0 * from " + InsertBuilder.GetTableNameString);
            foreach (var rowInfos in DbColumnInfoList)
            {
                var dr = dt.NewRow();
                foreach (var value in rowInfos)
                {
                    if (value.Value != null && UtilMethods.GetUnderType(value.Value.GetType()) == UtilConstants.DateType)
                    {
                        if (value.Value != null && value.Value.ToString() == DateTime.MinValue.ToString())
                        {
                            value.Value = Convert.ToDateTime("1753/01/01");
                        }
                    }
                    if (value.Value == null)
                    {
                        value.Value = DBNull.Value;
                    }
                    dr[value.DbColumnName] = value.Value;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private void CloseDb()
        {
            if (this.Context.CurrentConnectionConfig.IsAutoCloseConnection && this.Context.Ado.Transaction == null)
            {
                this.Context.Ado.Connection.Close();
            }
        }
    }
}
