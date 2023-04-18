using OOP_Application.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Application.Factories
{
    public class XmlFactory : SerializerFactory
    {
        public override ISerializer CreateSerializer()
        {
            return new Xml_Serializer();
        }
    }
}
