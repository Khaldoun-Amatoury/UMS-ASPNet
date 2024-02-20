

using MediatR;
using Project.Persistance;
using Project.Persistance.Context;

namespace Project.Application.Teacher.Commands
{
    public class AssignCourseToTimeSlotCommand : IRequest<bool>, IRequest<TeacherPerCourse>
    {
        // Properties for assigning a course to a time slot
        public long TeacherPerCourseId { get; set; }
        public long SessionTimeId { get; set; }
    }

    public class AssignCourseToTimeSlotCommandHandler : IRequestHandler<AssignCourseToTimeSlotCommand, bool>
    {
        private readonly UmDbContext _context;

        public AssignCourseToTimeSlotCommandHandler(UmDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AssignCourseToTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var teacherCourse = await _context.TeacherPerCourses.FindAsync(request.TeacherPerCourseId);
            if (teacherCourse == null)
                return false;

            var sessionTime = await _context.SessionTimes.FindAsync(request.SessionTimeId);
            if (sessionTime == null)
                return false;

            var teacherCourseSession = new TeacherPerCoursePerSessionTime
            {
                TeacherPerCourseId = teacherCourse.Id,
                SessionTimeId = sessionTime.Id
            };

            _context.TeacherPerCoursePerSessionTimes.Add(teacherCourseSession);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}