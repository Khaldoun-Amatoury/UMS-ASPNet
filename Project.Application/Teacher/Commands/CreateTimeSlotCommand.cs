
using MediatR;
using Project.Persistance;
using Project.Persistance.Context;

namespace Project.Application.Teacher.Commands
{
    public class CreateTimeSlotCommand : IRequest<bool>, IRequest<SessionTime>
    {
        // Properties for creating a time slot
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
    }

    public class CreateTimeSlotCommandHandler : IRequestHandler<CreateTimeSlotCommand, bool>
    {
        private readonly UmDbContext _context;

        public CreateTimeSlotCommandHandler(UmDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var timeSlot = new SessionTime
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Duration = request.Duration
            };

            _context.SessionTimes.Add(timeSlot);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
