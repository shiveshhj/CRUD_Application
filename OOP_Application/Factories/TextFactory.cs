using OOP_Application.Serializers;

namespace OOP_Application.Factories
{
    public class TextFactory : SerializerFactory
    {
        public override ISerializer CreateSerializer()
        {
            return new Text_Serializer();
        }
    }
}
