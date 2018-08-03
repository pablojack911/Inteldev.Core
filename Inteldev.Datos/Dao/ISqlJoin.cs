using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inteldev.Datos.Dao
{
    public interface ISqlJoin
    {
        SqlJoinType JoinType {get;set;}
        String Left  {get;set;}
        String Right  {get;set;}
        ISqlUpdate Condition<ValueType>(string campo, ValueType value);        
    }
}
