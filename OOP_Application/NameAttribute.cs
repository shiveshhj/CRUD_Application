using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application
{
    [AttributeUsage(AttributeTargets.Field  | AttributeTargets.Class)]
    class NameAttribute : System.Attribute
    {
        private string name;
        public NameAttribute(string _name)
        {
            this.name = _name;
        }
        public string Name{
            get
            {
                return name;
            }    
        }
    }
}
