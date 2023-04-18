using OOP_Application.Serializers;

namespace OOP_Application.Factories
{
    public class BinaryFactory : SerializerFactory
    {
        public override ISerializer CreateSerializer()
        {
            return new Binary_Serializer();
        }
    }
}
