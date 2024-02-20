


using MediatR;
using NpgsqlTypes;
using Project.Application.DTO;
using Project.Persistance;
using Project.Persistance.Context;

namespace Project.Application.Admin.Commands
{
    public class CreateCourseCommand : IRequest<CourseDto>, IRequest<Course>
    {
        // Properties for course creation
        public string Name { get; set; }
        public int? MaxStudentsNumber { get; set; }
        public NpgsqlRange<DateOnly>? EnrolmentDateRange { get; set; }
    }

    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseDto>
    {
        private readonly UmDbContext _context;

        public CreateCourseCommandHandler(UmDbContext context)
        {
            _context = context;
        }

        public async Task<CourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course
            {
                Name = request.Name,
                MaxStudentsNumber = request.MaxStudentsNumber,
                EnrolmentDateRange = request.EnrolmentDateRange
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync(cancellationToken);

            // Mapping course entity to DTO
            var courseDto = new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                MaxStudentsNumber = course.MaxStudentsNumber,
                EnrolmentDateRange = course.EnrolmentDateRange
            };

            return courseDto;
        }
    }
}
