using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkTrace.Application.DTOs.StatusDTO.Information;
using WorkTrace.Application.Services;

namespace WorkTrace.Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class StatusController(IStatusService statusService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<StatusInformationResponse>>> GetAll()
    {
        var statusesInSystem = await statusService.GetAllAsync();
        return Ok(statusesInSystem);
    }

    [Authorize]
    [HttpGet]
    public async Task<StatusInformationResponse> GetById(string id) =>
        await statusService.GeyByIdAsync(id);

    [Authorize]
    [HttpPost]
    public async Task<StatusInformationResponse> Create(CreateStatusRequest request) =>
        await statusService.CreateStatusAsync(request);

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<StatusInformationResponse>> Update(string id, [FromBody] UpdateStatusRequest request)
    {
        var updatedStatus = await statusService.UpdateStatusAsync(id, request);
        return Ok(updatedStatus);
    }

    [Authorize]
    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> DeactivateStatus(string id)
    {
        var succes = await statusService.SetStatusInactive(id);
        if (!succes) return NotFound("Status No se encontró o esta Inactivo");
        return Ok("Ahora Status esta Inactivo");
    }
}