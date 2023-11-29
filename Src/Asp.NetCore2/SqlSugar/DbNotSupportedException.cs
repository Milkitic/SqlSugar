using System;

namespace SqlSugar
{
    public class DbNotSupportedException : NotSupportedException
    {
        public DbNotSupportedException(DbType dbType) : base(
            $"DbType {dbType} not supported. Use full version of SqlSugar instead.")
        {
        }
    }
}