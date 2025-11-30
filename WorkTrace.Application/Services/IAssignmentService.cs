using WorkTrace.Application.DTOs.AssignmentDTO.Management;
using WorkTrace.Application.DTOs.AssignmentDTO.Mobile;

namespace WorkTrace.Application.Services;

public interface IAssignmentService
{
    Task<AssignmentResponse> CreateAssigmentAdminAsync(CreateAssignmentRequest assignmentRequest);
    Task<List<AssignmentResponse>> GetAllAsync();
    Task<AssignmentResponse> GetByIdAsync(string id);
    Task<AssignmentResponse> UpdateAssignmentAsync(string id, UpdateAssignmentWebRequest request);
    Task<List<ClientHistoryResponse>> GetClientHistoryAsync(string cliendId);
    Task ValidateExistance(CreateAssignmentRequest assignmentRequest);
    Task<List<AssigmentMobileDashboardResponse>> GetAssigmentByUserandRangeAsync(string userId, DateTime start, DateTime end); 
    Task<AssignmentMobileResponse> StartAssignmentAsync(string id, StartAssigmentRequest request);
    Task<AssignmentMobileResponse> FinishAssignmentAsync(string id, FinishAssignmentRequest request);
    Task<AssignmentMobileResponse> UpdateLocationAsync(string id, UpdateLocationRequest request);
    Task<AssignmentMobileResponse> UpdateProgressAsync(string id, UpdateProgressRequest request);
    Task<List<AssignmentListResponse>> GetAssignmentsForListAsync(string userId);
    Task<AssignmentTrackingResponse?> GetAssignmentTrackingAsync(string assignmentId);
    //Task<AssignmentResponse> UpdateAssignmentMobileAsync(string id, UpdateAssignmentMobileRequest request);
}