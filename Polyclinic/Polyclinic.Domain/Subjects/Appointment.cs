namespace Polyclinic.Domain.Subjects;

public class Appointment
{
    public required int Id { get; set; }
    public required Patient Patient { get; set; }
    public required DateTime AppointmentDateTime { get; set; }
    public Doctor? Doctor { get; set; }
    public int? RoomNumber { get; set; }
    public required bool RepeatAppointment { get; set; }
}