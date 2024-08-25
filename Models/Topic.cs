using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace asp.netDay2.Models;

public partial class Topic
{
    public int TopId { get; set; }
    [Required]
    public string? TopName { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
