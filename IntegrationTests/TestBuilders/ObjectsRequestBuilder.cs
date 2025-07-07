using IntegrationTests.Models;

namespace IntegrationTests.TestBuilders
{
    public class ObjectsRequestBuilder
    {
        private string _name = "test-object";
        private ObjectsAttributes _data;

        private ObjectsRequestBuilder()
        {
            _data = ObjectsAttributesBuilder.Create().Build();
        }

        public static ObjectsRequestBuilder Create()
        {
            return new ObjectsRequestBuilder();
        }

        public ObjectsRequestBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ObjectsRequestBuilder WithData(ObjectsAttributes data)
        {
            _data = data;
            return this;
        }

        public ObjectsRequestBuilder WithData(Action<ObjectsAttributesBuilder> configure)
        {
            ArgumentNullException.ThrowIfNull(configure);
            
            var builder = ObjectsAttributesBuilder.Create();
            configure(builder);
            _data = builder.Build();
            return this;
        }

        public ObjectsRequest Build()
        {
            return new ObjectsRequest
            {
                Name = _name,
                Data = _data
            };
        }

        public static ObjectsRequest CreateDefault()
        {
            return Create().Build();
        }
    }
}
