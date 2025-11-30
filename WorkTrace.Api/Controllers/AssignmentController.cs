using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkTrace.Application.DTOs.AssignmentDTO.Management;
using WorkTrace.Application.Services;

namespace WorkTrace.Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AssignmentController(IAssignmentService assignmentService) : Controller
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AssignmentResponse>> Create([FromBody] CreateAssignmentRequest request)
    {
        var result = await assignmentService.CreateAssigmentAdminAsync(request);
        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<AssignmentResponse>>> GetAll()
    {
        var assignments = await assignmentService.GetAllAsync();
        return Ok(assignments);
    }

    [Authorize]
    [HttpGet]
    public async Task<AssignmentResponse> GetById(string id) =>
        await assignmentService.GetByIdAsync(id);

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<AssignmentResponse>> UpdateAssignment(string id, [FromBody] UpdateAssignmentWebRequest request)
    {
        var result = await assignmentService.UpdateAssignmentAsync(id, request);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{clientId}")]
    public async Task<IActionResult> GetClientHistory(string clientId)
    {
        var clientHistory = await assignmentService.GetClientHistoryAsync(clientId);
        return Ok(clientHistory);
    }

    [Authorize]
    [HttpGet("by-user/{userId}")]
    public async Task<ActionResult<List<AssignmentListResponse>>> GetAssignmentsList(string userId)
    {
        var result = await assignmentService.GetAssignmentsForListAsync(userId);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("tracking/{assignmentId}")]
    public async Task<ActionResult<AssignmentTrackingResponse>> GetAssignmentTracking(string assignmentId)
    {
        var result = await assignmentService.GetAssignmentTrackingAsync(assignmentId);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
