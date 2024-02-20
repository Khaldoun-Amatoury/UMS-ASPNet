

using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Persistance;
using Project.Persistance.Context;

namespace Project.Application.Student.Queries
{
    public class CheckEnrollmentDateRangeQuery : IRequest<bool>, IRequest<Course>
    {
        public long CourseId { get; set; }
        public long StudentId { get; set; }

        public class CheckEnrollmentDateRangeQueryHandler : IRequestHandler<CheckEnrollmentDateRangeQuery, bool>
        {
            private readonly UmDbContext _context;

            public CheckEnrollmentDateRangeQueryHandler(UmDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(CheckEnrollmentDateRangeQuery request, CancellationToken cancellationToken)
            {
                // Get the course
                var course = await _context.Courses.FindAsync(request.CourseId);

                // Check if the course exists and has an enrollment date range
                if (course == null || !course.EnrolmentDateRange.HasValue)
                    return false;

                // Check if today's date is within the enrollment date range
                var currentDate = System.DateOnly.FromDateTime(DateTime.Now);
                return course.EnrolmentDateRange.Value.Contains(currentDate);
            }
        }
    }
}