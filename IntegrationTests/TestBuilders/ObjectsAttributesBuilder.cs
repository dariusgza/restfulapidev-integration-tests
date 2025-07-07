using IntegrationTests.Models;

namespace IntegrationTests.TestBuilders
{
    public class ObjectsAttributesBuilder
    {
        private string _description = "Default test description";

        private ObjectsAttributesBuilder() { }

        public static ObjectsAttributesBuilder Create()
        {
            return new ObjectsAttributesBuilder();
        }

        public ObjectsAttributesBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ObjectsAttributes Build()
        {
            return new ObjectsAttributes
            {
                Description = _description
            };
        }
    }
}
