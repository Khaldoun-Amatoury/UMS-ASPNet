
using NpgsqlTypes;

namespace Project.Application.DTO
{
    public class CourseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? MaxStudentsNumber { get; set; }
        public NpgsqlRange<System.DateOnly>? EnrolmentDateRange { get; set; }
    }
}
