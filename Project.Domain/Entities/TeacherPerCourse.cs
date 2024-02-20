using System;
using System.Collections.Generic;

namespace Project.Persistance;

public partial class TeacherPerCourse
{
    public long Id { get; set; }

    public long TeacherId { get; set; }

    public long CourseId { get; set; }

    public virtual ICollection<ClassEnrollment> ClassEnrollments { get; set; } = new List<ClassEnrollment>();

    public virtual Course Course { get; set; } = null!;

    public virtual User Teacher { get; set; } = null!;

    public virtual ICollection<TeacherPerCoursePerSessionTime> TeacherPerCoursePerSessionTimes { get; set; } = new List<TeacherPerCoursePerSessionTime>();
}
