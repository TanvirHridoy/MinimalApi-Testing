using System;
using System.Collections.Generic;

namespace MinimalApi.Models
{
    public partial class Convocation
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Year { get; set; }
        public DateTime? ProgramDate { get; set; }
        public bool? Status { get; set; }
    }
}
