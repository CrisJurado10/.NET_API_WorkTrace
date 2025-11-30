using Microsoft.AspNetCore.Http;

namespace WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

public class UpdateProgressRequest
{
    //public List<StepProgress> StepProgresses { get; set; }
    public List<IFormFile> MediaFiles { get; set; }
    public string Comment { get; set; }
}