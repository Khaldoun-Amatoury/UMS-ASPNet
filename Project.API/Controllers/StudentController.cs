using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Student.Commands;
using Project.Application.Student.Queries;

namespace Project.API.Controllers;



[ApiController]
[Route("api/student")]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("enroll")]
    public async Task<IActionResult> EnrollInCourse([FromBody] EnrollInCourseCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.GetType().GetProperty("Success")?.GetValue(result) as bool? ?? false)
            return Ok(result.GetType().GetProperty("Data")?.GetValue(result));
        return BadRequest(result.GetType().GetProperty("ErrorMessage")?.GetValue(result) as string);
    }

    [HttpPost("courses/{courseId}/enrollment")]
    public async Task<IActionResult> CheckEnrollmentDateRange([FromRoute] CheckEnrollmentDateRangeQuery query)
    {
        var result = await _mediator.Send(query);
        if (result.GetType().GetProperty("Success")?.GetValue(result) as bool? ?? false)
            return Ok(result.GetType().GetProperty("Data")?.GetValue(result));
        return BadRequest(result.GetType().GetProperty("ErrorMessage")?.GetValue(result) as string);
    }
}