using Application.Validations;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands
{
    public sealed class C_Test : IRequest<string>
    {
        [NoDefaultInt]
        public int Age { get; set; }

        [Required]
        public string LastName { get; set; } = "";

        [Required]
        public string Name { get; set; } = "";
    }
}