using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Teacher.Commands;

namespace Project.API.Controllers;




[ApiController]
[Route("api/teacher")]
public class TeacherController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsTeacher([FromBody] RegisterAsTeacherCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.GetType().GetProperty("Success")?.GetValue(result) as bool? ?? false)
            return Ok(result.GetType().GetProperty("Data")?.GetValue(result));
        return BadRequest(result.GetType().GetProperty("ErrorMessage")?.GetValue(result) as string);
    }

    [HttpPost("timeslots")]
    public async Task<IActionResult> CreateTimeSlot([FromBody] CreateTimeSlotCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.GetType().GetProperty("Success")?.GetValue(result) as bool? ?? false)
            return Ok(result.GetType().GetProperty("Data")?.GetValue(result));
        return BadRequest(result.GetType().GetProperty("ErrorMessage")?.GetValue(result) as string);
    }

    [HttpPost("courses/{courseId}/timeslots/{timeSlotId}")]
    public async Task<IActionResult> AssignCourseToTimeSlot([FromRoute] AssignCourseToTimeSlotCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.GetType().GetProperty("Success")?.GetValue(result) as bool? ?? false)
            return Ok(result.GetType().GetProperty("Data")?.GetValue(result));
        return BadRequest(result.GetType().GetProperty("ErrorMessage")?.GetValue(result) as string);
    }
}