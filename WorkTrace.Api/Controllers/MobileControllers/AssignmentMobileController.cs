using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkTrace.Application.DTOs.AssignmentDTO.Mobile;
using WorkTrace.Application.Services;

namespace WorkTrace.Api.Controllers.MobileControllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AssignmentMobileController(IAssignmentService assignmentService) : ControllerBase
    {
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAssignmentsByUser(
            string userId,
            [FromQuery] DateTime start,
            [FromQuery] DateTime end)
        {
            var result = await assignmentService.GetAssigmentByUserandRangeAsync(userId, start, end);
            return Ok(result);
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> StartAssignment(string id, [FromBody] StartAssigmentRequest request)
        {
            var result = await assignmentService.StartAssignmentAsync(id, request);
            return Ok(result);
        }

        [HttpPost("{id}/finish")]
        public async Task<IActionResult> FinishAssignment(string id, [FromBody] FinishAssignmentRequest request)
        {
            var result = await assignmentService.FinishAssignmentAsync(id, request);
            return Ok(result);
        }

        [HttpPut("{id}/location")]
        public async Task<IActionResult> UpdateLocation(string id, [FromBody] UpdateLocationRequest request)
        {
            var result = await assignmentService.UpdateLocationAsync(id, request);
            return Ok(result);
        }

        [HttpPut("{id}/progress")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProgress(string id, [FromForm] UpdateProgressRequest request)
        {
            var result = await assignmentService.UpdateProgressAsync(id, request);
            return Ok(result);
        }
    }
}