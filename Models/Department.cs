using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp.netDay2.Models;

public partial class Department
{
    public int DeptId { get; set; }
    [StringLength(50, MinimumLength = 2)]
    [Required(ErrorMessage = "Name is required")]
    public string? DeptName { get; set; }
    
    [Required(ErrorMessage = "describtion is required")]
    public string? DeptDesc { get; set; }
    
    [Required(ErrorMessage = "location is required")]
    public string? DeptLocation { get; set; }

    public int? DeptManager { get; set; }

    public DateOnly? ManagerHiredate { get; set; }

    public virtual Instructor? DeptManagerNavigation { get; set; }

    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
