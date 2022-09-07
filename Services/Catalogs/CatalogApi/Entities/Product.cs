using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CatalogApi.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }= default!;
        [BsonElement("Name")]
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string Summary { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageName { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }
}
