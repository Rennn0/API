﻿using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public IEnumerable<Product> Products { get; set; }
    }
}