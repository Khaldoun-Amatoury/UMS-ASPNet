using System;
using System.Collections.Generic;

namespace Project.Persistance;

public partial class ClassEnrollment
{
    public long Id { get; set; }

    public long ClassId { get; set; }

    public long StudentId { get; set; }

    public virtual TeacherPerCourse Class { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
