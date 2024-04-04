using Domain.Interfaces;

namespace Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Email { get; set; } = "";
    }
}