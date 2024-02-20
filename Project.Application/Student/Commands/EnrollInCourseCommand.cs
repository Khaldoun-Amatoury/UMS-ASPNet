
using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Persistance;
using Project.Persistance.Context;

namespace Project.Application.Student.Commands
{
    public class EnrollInCourseCommand : IRequest<bool>, IRequest<Course>
    {
        // Properties for enrolling in a course
        public long CourseId { get; set; }
        public long StudentId { get; set; }
    }

    public class EnrollInCourseCommandHandler : IRequestHandler<EnrollInCourseCommand, bool>
    {
        private readonly UmDbContext _context;

        public EnrollInCourseCommandHandler(UmDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(EnrollInCourseCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the course
            var course = await _context.Courses.FindAsync(request.CourseId);

            // Check if the course exists and has enrollment date range
            if (course == null || course.EnrolmentDateRange == null)
                return false;

            // Convert current DateTime to System.DateOnly
            var currentDate = System.DateOnly.FromDateTime(DateTime.Now);

            // Check if today's date falls within the enrollment date range
            var isInEnrollmentRange = course.EnrolmentDateRange.Value.Contains(currentDate);
            if (!isInEnrollmentRange)
                return false;

            // Check if the course has reached maximum student capacity
            var currentStudentsCount = await _context.ClassEnrollments
                .CountAsync(e => e.ClassId == request.CourseId, cancellationToken);
            if (currentStudentsCount >= course.MaxStudentsNumber)
                return false;

            // Enroll the student in the course
            var enrollment = new ClassEnrollment
            {
                ClassId = request.CourseId,
                StudentId = request.StudentId
            };

            _context.ClassEnrollments.Add(enrollment);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
