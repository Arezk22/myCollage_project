using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp.netDay2.Models;

public partial class Instructor
{
    
    public int InsId { get; set; }
    [StringLength(50, MinimumLength = 2)]
    [Required(ErrorMessage = "Name is required")]
    public string? InsName { get; set; }
    [Required]
    public string? InsDegree { get; set; }
    [Range(4000, 9000, ErrorMessage = "salary must be between 4000 and 9000")]
    [Required]
    public decimal? Salary { get; set; }
    
    public int? DeptId { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual Department? Dept { get; set; }

    public virtual ICollection<InsCourse> InsCourses { get; set; } = new List<InsCourse>();
}
