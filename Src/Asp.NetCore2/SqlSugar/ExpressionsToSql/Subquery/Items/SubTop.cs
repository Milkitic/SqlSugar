using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SqlSugar
{
    public class SubTop : ISubOperation
    {
        public bool HasWhere
        {
            get; set;
        }

        public ExpressionContext Context
        {
            get; set;
        }

        public Expression Expression
        {
            get; set;
        }

        public string Name
        {
            get
            {
                return "Top";
            }
        }

        public int Sort
        {
            get
            {
                {
                    return 490;
                }
            }
        }


        public string GetValue(Expression expression)
        {
            if (this.Context.GetLimit() != null)
            {
                return this.Context.GetLimit();
            }
            else
            {
                return "limit 0,1";
            }
        }
    }
}
