
using MediatR;
using Project.Persistance;
using Project.Persistance.Context;

namespace Project.Application.Teacher.Commands

{
    public class RegisterAsTeacherCommand : IRequest<bool>, IRequest<TeacherPerCourse>
    {
        // Properties for registering as a teacher
        public long TeacherId { get; set; }
        public long CourseId { get; set; }
    }

    public class RegisterAsTeacherCommandHandler : IRequestHandler<RegisterAsTeacherCommand, bool>
    {
        private readonly UmDbContext _context;

        public RegisterAsTeacherCommandHandler(UmDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RegisterAsTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacherCourse = new TeacherPerCourse
            {
                TeacherId = request.TeacherId,
                CourseId = request.CourseId
            };

            _context.TeacherPerCourses.Add(teacherCourse);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
