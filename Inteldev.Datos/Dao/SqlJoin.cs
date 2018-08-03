using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Inteldev.Datos.Dao
{
    public class SqlJoin:ISqlJoin
    {
        ArrayList Conditions;
        ArrayList ConditionFields;

        SqlJoinType joinType;
        public SqlJoinType JoinType
        {
            get
            {
                return joinType;
            }
            set
            {
                joinType = value;
            }
        }

        public SqlJoin(SqlJoinType JoinType, string TableLeft,string TableRight)
        {
            this.JoinType = JoinType;
            this.Left = TableLeft;
            this.Right = TableRight;
        }

        public ISqlJoin Condition<ValueType>(string campo, ValueType value)
        {
            this.Conditions.Add(value);
            this.ConditionFields.Add(campo);
            return this;
        }

        string tableleft;
        public string Left
        {
            get
            {
                return tableleft;
            }
            set
            {
                tableleft = value;
            }
        }
        string tableright;
        public string Right
        {
            get
            {
                return tableright;
            }
            set
            {
                tableright = value;
            }
        }

        public override string ToString()
        {
            string lcJoin = this.Left + this.JoinType.ToString() + " JOIN " + this.Right +" ON " ;

            
            return lcJoin;

        }


        ISqlUpdate ISqlJoin.Condition<ValueType>(string campo, ValueType value)
        {
            throw new NotImplementedException();
        }
    }
}
