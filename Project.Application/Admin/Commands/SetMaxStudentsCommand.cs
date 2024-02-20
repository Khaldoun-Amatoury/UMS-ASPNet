


using MediatR;
using Project.Persistance;
using Project.Persistance.Context;

namespace Project.Application.Admin.Commands
{
    public class SetMaxStudentsCommand : IRequest<bool>, IRequest<Course>
    {
        // Properties for setting max students
        public long CourseId { get; set; }
        public int MaxStudentsNumber { get; set; }
    }

    public class SetMaxStudentsCommandHandler : IRequestHandler<SetMaxStudentsCommand, bool>
    {
        private readonly UmDbContext _context;

        public SetMaxStudentsCommandHandler(UmDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(SetMaxStudentsCommand request, CancellationToken cancellationToken)
        {
            var course = await _context.Courses.FindAsync(request.CourseId);
            if (course == null)
                return false;

            course.MaxStudentsNumber = request.MaxStudentsNumber;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}