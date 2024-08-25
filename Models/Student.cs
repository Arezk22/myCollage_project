using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp.netDay2.Models;

public partial class Student
{   
    public int StId { get; set; }
    [StringLength(50, MinimumLength = 3)]
    [Required(ErrorMessage = "First Name is required")]
    public string? StFname { get; set; }
    [StringLength(50, MinimumLength = 3)]
    [Required(ErrorMessage = "Last Name is required")]
    public string? StLname { get; set; }
    [Required(ErrorMessage = "Address is required")]
    public string? StAddress { get; set; }
    [Required(ErrorMessage = "Age is required")]
    public int? StAge { get; set; }
    [Required(ErrorMessage = "Department Id is required")]
    public int? DeptId { get; set; }

    public int? StSuper { get; set; }

    public virtual Department? Dept { get; set; }

    public virtual ICollection<Student> InverseStSuperNavigation { get; set; } = new List<Student>();

    public virtual Student? StSuperNavigation { get; set; }

    public virtual ICollection<StudCourse> StudCourses { get; set; } = new List<StudCourse>();
}
