using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp.netDay2.Models;

public partial class Course
{
    public int CrsId { get; set; }
    [StringLength(50, MinimumLength = 2)]
    [Required(ErrorMessage = "Name is required")]
    public string? CrsName { get; set; }
    [Required]
    public int? CrsDuration { get; set; }
    [Required]
    public int? TopId { get; set; }

    public virtual ICollection<InsCourse> InsCourses { get; set; } = new List<InsCourse>();

    public virtual ICollection<StudCourse> StudCourses { get; set; } = new List<StudCourse>();

    public virtual Topic? Top { get; set; }
}
