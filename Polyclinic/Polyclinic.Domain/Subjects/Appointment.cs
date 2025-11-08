namespace Polyclinic.Domain.Subjects;

public class Appointment
{
    public int Id { get; set; }
    public Patient Patient { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public Doctor Doctor { get; set; }
    public int RoomNumber { get; set; }
    public bool RepeatAppointment { get; set; }
}