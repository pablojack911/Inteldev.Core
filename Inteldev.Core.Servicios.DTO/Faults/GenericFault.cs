using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Inteldev.Core.DTO.Faults
{
    [DataContract]
    public class GenericFault
    {
        private string _reason;
        [DataMember]
        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
    }
}
