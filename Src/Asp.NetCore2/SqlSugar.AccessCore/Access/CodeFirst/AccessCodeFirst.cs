using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlSugar.Access
{
    public class AccessCodeFirst : CodeFirstProvider
    {
        protected override string GetTableName(EntityInfo entityInfo)
        {
            throw new DbNotSupportedException(DbType.Access);
        }
    }
}
