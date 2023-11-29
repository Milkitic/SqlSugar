using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar.GBase 
{
   
    public class GBaseFastBuilder:FastBuilder,IFastBuilder
    {
        public override bool IsActionUpdateColumns { get; set; } = true;
        public override DbFastestProperties DbFastestProperties { get; set; } = new DbFastestProperties() {
          HasOffsetTime=true
        };
        public async Task<int> ExecuteBulkCopyAsync(DataTable dt)
        {

            throw new DbNotSupportedException(DbType.GBase);
        }
    

    }
}
