using Domain.Interfaces;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }
    }
}