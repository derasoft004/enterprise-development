namespace Polyclinic.Subjects;

public class Appointment
{
    public Patient Patient { get; set; }
    public DateTime AppointmentDate { get; set; }
    public Doctor Doctor { get; set; }
    public int RoomNumber { get; set; }
    public bool RepeatAppointment { get; set; }
}