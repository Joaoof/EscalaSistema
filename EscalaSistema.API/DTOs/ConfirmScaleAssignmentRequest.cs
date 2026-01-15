namespace EscalaSistema.API.DTOs;

public class ConfirmScaleAssignmentRequest
{
    public Guid AssignmentId { get; set; }
    public bool WillAttend { get; set; }
}
