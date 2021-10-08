using System;
using System.Collections.Generic;

namespace MinimalApi.Models
{
    public partial class Department
    {
        public int Id { get; set; }
        public string? DeptName { get; set; }
        public string? DeptSname { get; set; }
    }
}
