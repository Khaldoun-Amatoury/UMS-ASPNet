
using MediatR;
using NpgsqlTypes;
using Project.Persistance;
using Project.Persistance.Context;

namespace Project.Application.Admin.Commands
{
    public class SetEnrollmentDateRangeCommand : IRequest<bool>, IRequest<ClassEnrollment>
    {
        // Properties for setting enrollment date range
        public long CourseId { get; set; }
        public NpgsqlRange<DateOnly> EnrolmentDateRange { get; set; }
    }

    public class SetEnrollmentDateRangeCommandHandler : IRequestHandler<SetEnrollmentDateRangeCommand, bool>
    {
        private readonly UmDbContext _context;

        public SetEnrollmentDateRangeCommandHandler(UmDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(SetEnrollmentDateRangeCommand request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FindAsync(request.CourseId);
            if (course == null)
                return false;

            course.EnrolmentDateRange = request.EnrolmentDateRange;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
