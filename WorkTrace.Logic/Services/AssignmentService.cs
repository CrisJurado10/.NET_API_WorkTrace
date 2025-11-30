using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using WorkTrace.Application.DTOs.AssignmentDTO.Management;
using WorkTrace.Application.DTOs.AssignmentDTO.Mobile;
using WorkTrace.Application.Repositories;
using WorkTrace.Application.Services;
using WorkTrace.Data.Models;

namespace WorkTrace.Logic.Services;

public class AssignmentService(IAssignmentRepository _assignmentRepository, IClientRepository _clientRepository, IFileService fileService, IGeocodingService _geocodingService, IServiceRepository _serviceRepository, IStatusRepository _statusRepository, IUserRepository _userRepository, IMapper _mapper) : IAssignmentService
{
    public async Task<AssignmentResponse> CreateAssigmentAdminAsync(CreateAssignmentRequest assignmentRequest)
    {
        await ValidateExistance(assignmentRequest);

        var assignment = _mapper.Map<Assignment>(assignmentRequest);

        assignment.DestinationLocation = await _geocodingService.GetGeoPointAsync(assignmentRequest.Address);

        await _assignmentRepository.CreateAsync(assignment);
        return _mapper.Map<AssignmentResponse>(assignment);
    }

    public async Task<List<AssignmentResponse>> GetAllAsync()
    {
        var assignmentsInSystem = await _assignmentRepository.GetAsync();
        return _mapper.Map<List<AssignmentResponse>>(assignmentsInSystem);
    }

    public async Task<AssignmentResponse> GetByIdAsync(string id)
    {
        var assignmentById = await _assignmentRepository.GetAsync(id) ?? throw new Exception("Asignación no encontrada");
        var response = _mapper.Map<AssignmentResponse>(assignmentById);
        return response;
    }

    public async Task<List<ClientHistoryResponse>> GetClientHistoryAsync(string clientId)
    {
        var rawData = await _assignmentRepository.GetClientAssignmentRawAsync(clientId);

        var mapResult = rawData.Select(doc =>
        {
            DateTime assigned = doc.Contains("AssignedDate") && !doc["AssignedDate"].IsBsonNull
                ? doc["AssignedDate"].ToLocalTime()
                : DateTime.MinValue;

            DateTime? co = null;
            if (doc.Contains("CheckOut") && !doc["CheckOut"].IsBsonNull)
                co = doc["CheckOut"].ToLocalTime();

            return new ClientHistoryResponse
            {
                Service = doc["Service"].AsString,
                Date = assigned == DateTime.MinValue ? "" : assigned.ToString("dd-MM-yyyy"),
                Time = assigned == DateTime.MinValue ? "" : assigned.ToString("HH:mm"),
                CheckOutDate = co == null ? "" : co.Value.ToString("dd-MM-yyyy"),
                CheckOutTime = co == null ? "" : co.Value.ToString("HH:mm"),
                Status = doc["Status"].AsString,
                Address = doc["Address"].AsString,
                Users = doc["Users"]
                    .AsBsonArray
                    .Select(u => u.AsString)
                    .ToList()
            };
        })
        .ToList();
        return mapResult;
    }

    public async Task<AssignmentResponse> UpdateAssignmentAsync(string id, UpdateAssignmentWebRequest request)
    {
        var assignment = await _assignmentRepository.GetAsync(id) ?? throw new Exception("Asignación no encontrada");

        if (!string.IsNullOrEmpty(request.Address))
        {
            assignment.DestinationLocation = await _geocodingService.GetGeoPointAsync(request.Address);
        }

        _mapper.Map(request, assignment);

        await _assignmentRepository.UpdateAsync(id, assignment);

        return _mapper.Map<AssignmentResponse>(assignment);
    }

    public async Task<List<AssignmentListResponse>> GetAssignmentsForListAsync(string userId)
    {
        var raw = await _assignmentRepository.GetAssignmentsListByUserRawAsync(userId);

        var list = raw.Select(x =>
        {
            var assignedDateLocal = DateTime.MinValue;
            if (x.TryGetValue("AssignedDate", out var assignedVal) && assignedVal != BsonNull.Value)
            {
                if (assignedVal.IsBsonDateTime)
                {
                    var utc = assignedVal.AsBsonDateTime.ToUniversalTime();
                    assignedDateLocal = utc.ToLocalTime();
                }
                else if (assignedVal.IsValidDateTime)
                {
                    var utc = (DateTime)assignedVal;
                    if (utc.Kind == DateTimeKind.Unspecified)
                        utc = DateTime.SpecifyKind(utc, DateTimeKind.Utc);
                    assignedDateLocal = utc.ToLocalTime();
                }
            }

            string client = "";
            if (x.TryGetValue("Client", out var clientVal) && clientVal != BsonNull.Value)
            {
                client = clientVal.AsString;
            }

            string service = "";
            if (x.TryGetValue("Service", out var serviceVal) && serviceVal != BsonNull.Value)
            {
                service = serviceVal.AsString;
            }

            return new AssignmentListResponse
            {
                Id = x["_id"].ToString(),
                Client = client,
                Service = service,
                AssignedDate = assignedDateLocal,
                AssignedTime = assignedDateLocal
            };
        }).ToList();

        return list;
    }

    public async Task<AssignmentTrackingResponse?> GetAssignmentTrackingAsync(string assignmentId)
    {
        var doc = await _assignmentRepository.GetAssignmentTrackingRawAsync(assignmentId);
        if (doc == null) return null;

        return new AssignmentTrackingResponse
        {
            Id = doc["_id"].ToString(),
            Client = doc.GetValue("Client").AsString,
            Service = doc.GetValue("Service").AsString,
            Address = doc.GetValue("Address").AsString,

            CheckInDate = doc["CheckIn"].IsBsonNull
                ? null
                : doc["CheckIn"].ToLocalTime().ToString("dd-MM-yyyy"),

            CheckInTime = doc["CheckIn"].IsBsonNull
                ? null
                : doc["CheckIn"].ToLocalTime().ToString("HH:mm"),

            CheckOutDate = doc["CheckOut"].IsBsonNull
                ? null
                : doc["CheckOut"].ToLocalTime().ToString("dd-MM-yyyy"),

            CheckOutTime = doc["CheckOut"].IsBsonNull
                ? null
                : doc["CheckOut"].ToLocalTime().ToString("HH:mm"),

            CurrentLocation = doc.GetValue("CurrentLocation").IsBsonNull
                ? null
                : BsonSerializer.Deserialize<GeoPoint>(doc["CurrentLocation"].AsBsonDocument),

            DestinationLocation = doc.GetValue("DestinationLocation").IsBsonNull
                ? null
                : BsonSerializer.Deserialize<GeoPoint>(doc["DestinationLocation"].AsBsonDocument)
        };
    }

    //Mobile
    public async Task<List<AssigmentMobileDashboardResponse>> GetAssigmentByUserandRangeAsync(
    string userId, DateTime start, DateTime end)
    {
        var data = await _assignmentRepository.GetAssignmentByUserAndDateRangeAsync(userId, start, end);

        var result = new List<AssigmentMobileDashboardResponse>();

        foreach (var assignment in data)
        {
            var client = await _clientRepository.GetAsync(assignment.Client.ToString());
            var service = await _serviceRepository.GetAsync(assignment.Service.ToString());
            var createdBy = await _userRepository.GetAsync(assignment.CreatedByUser.ToString());
            var status = await _statusRepository.GetAsync(assignment.Status.ToString());

            var dto = new AssigmentMobileDashboardResponse
            {
                Id = assignment.Id.ToString(),
                Client = client?.FullName ?? "Sin nombre",
                Service = service?.Name ?? "Sin nombre",
                Status = status?.Name ?? "Sin nombre",
                Address = assignment.Address,
                AssignedDate = assignment.AssignedDate.ToLocalTime().ToString("dd-MM-yyyy"),
                AssignedTime = assignment.AssignedDate.ToLocalTime().ToString("HH:mm"),
                CreatedByUser = createdBy?.FullName ?? "N/A"
            };
            result.Add(dto);
        }
        return result;
    }


    public async Task<AssignmentMobileResponse> StartAssignmentAsync(string id, StartAssigmentRequest request)
    {
        var assignment = await _assignmentRepository.GetAsync(id)
            ?? throw new Exception("Asignación no encontrada");
        if (assignment.CheckIn != null)
            throw new Exception("La asignación ya fue iniciada.");

        assignment.CheckIn = request.CheckIn;
        assignment.CurrentLocation = request.CurrentLocation;

        await _assignmentRepository.UpdateAsync(id, assignment);

        return _mapper.Map<AssignmentMobileResponse>(assignment);
    }

    public async Task<AssignmentMobileResponse> FinishAssignmentAsync(string id, FinishAssignmentRequest request)
    {
        var assignment = await _assignmentRepository.GetAsync(id)
            ?? throw new Exception("Asignación no encontrada");

        assignment.CheckOut = request.CheckOut;

        await _assignmentRepository.UpdateAsync(id, assignment);

        return _mapper.Map<AssignmentMobileResponse>(assignment);
    }

    public async Task<AssignmentMobileResponse> UpdateLocationAsync(string id, UpdateLocationRequest request)
    {
        var assignment = await _assignmentRepository.GetAsync(id)
            ?? throw new Exception("Asignación no encontrada");

        assignment.CurrentLocation = request.CurrentLocation;

        await _assignmentRepository.UpdateAsync(id, assignment);

        return _mapper.Map<AssignmentMobileResponse>(assignment);
    }

    public async Task<AssignmentMobileResponse> UpdateProgressAsync(string id, UpdateProgressRequest request)
    {
        var assignment = await _assignmentRepository.GetAsync(id)
            ?? throw new Exception("Asignación no encontrada");

        // Procesar archivos
        if (request.MediaFiles != null)
        {
            assignment.MediaFiles ??= new List<MediaFile>();

            foreach (var file in request.MediaFiles)
            {
                var path = await fileService.SaveFileAsync(file, "uploads/media");

                assignment.MediaFiles.Add(new MediaFile
                {
                    Url = path,
                    UploadedAt = DateTime.UtcNow
                });
            }
        }

        //assignment.StepsProgress = request.StepProgresses;
        assignment.Comment = request.Comment;

        await _assignmentRepository.UpdateAsync(id, assignment);

        return _mapper.Map<AssignmentMobileResponse>(assignment);
    }

    //public async Task<AssignmentResponse> UpdateAssignmentMobileAsync(string id, UpdateAssignmentMobileRequest request)
    //{
    //    var assignment = await _assignmentRepository.GetAsync(id) ?? throw new Exception("Asignación no encontrada");
    //    if (request.MediaFiles != null && request.MediaFiles.Any())
    //    {
    //        foreach (var media in request.MediaFiles)
    //        {
    //            var filePath = fileService.SaveBase64Image(media.Url, "uploads/media");
    //            media.Url = filePath;
    //            media.UploadedAt = DateTime.UtcNow;
    //        }
    //    }
    //    _mapper.Map(request, assignment);
    //    await _assignmentRepository.UpdateAsync(id, assignment);
    //    return _mapper.Map<AssignmentResponse>(assignment);
    //}

    public async Task ValidateExistance(CreateAssignmentRequest assignmentRequest)
    {
        var client = await _clientRepository.GetAsync(assignmentRequest.Client);
        if (client == null) throw new Exception("Cliente no existe");

        var service = await _serviceRepository.GetAsync(assignmentRequest.Service);
        if (service == null) throw new Exception("Servicio no existe");

        var status = await _statusRepository.GetAsync(assignmentRequest.Status);
        if (status == null) throw new Exception("Status no existe");

        foreach (var userId in assignmentRequest.Users)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null) throw new Exception($"Usuario no existe");
        }
    }
}