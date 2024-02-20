using Project.Application.Admin.Commands;

namespace Project.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;




[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("courses")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.GetType().GetProperty("Success")?.GetValue(result) as bool? ?? false)
            return Ok(result.GetType().GetProperty("Data")?.GetValue(result));
        return BadRequest(result.GetType().GetProperty("ErrorMessage")?.GetValue(result) as string);
    }

    [HttpPost("courses/{courseId}/maxstudents")]
    public async Task<IActionResult> SetMaxStudents([FromRoute] long courseId, [FromBody] SetMaxStudentsCommand command)
    {
        command.CourseId = courseId;
        var result = await _mediator.Send(command);
        if (result.GetType().GetProperty("Success")?.GetValue(result) as bool? ?? false)
            return Ok(result.GetType().GetProperty("Data")?.GetValue(result));
        return BadRequest(result.GetType().GetProperty("ErrorMessage")?.GetValue(result) as string);
    }

    [HttpPost("courses/{courseId}/enrollmentdaterange")]
    public async Task<IActionResult> SetEnrollmentDateRange([FromRoute] long courseId,
        [FromBody] SetEnrollmentDateRangeCommand command)
    {
        command.CourseId = courseId;
        var result = await _mediator.Send(command);
        if (result.GetType().GetProperty("Success")?.GetValue(result) as bool? ?? false)
            return Ok(result.GetType().GetProperty("Data")?.GetValue(result));
        return BadRequest(result.GetType().GetProperty("ErrorMessage")?.GetValue(result) as string);
    }
}