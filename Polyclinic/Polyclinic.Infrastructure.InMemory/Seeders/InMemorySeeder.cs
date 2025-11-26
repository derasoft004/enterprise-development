using Polyclinic.Domain.Enums;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Infrastructure.InMemory.Seeders;

/// <summary>
/// In-memory data seeder for polyclinic domain
/// </summary>
public static class InMemorySeeder
{
    /// <summary>
    /// Get seeded specializations
    /// </summary>
    public static List<Specialization> GetSpecializations()
    {
        return
        [
            new Specialization { Id = 1, Name = "Кардиолог" },
            new Specialization { Id = 2, Name = "Невропатолог" },
            new Specialization { Id = 3, Name = "Дерматовенеролог" },
            new Specialization { Id = 4, Name = "Окулист" },
            new Specialization { Id = 5, Name = "Терапевт" },
            new Specialization { Id = 6, Name = "Педиатр" },
            new Specialization { Id = 7, Name = "Стоматолог-хирург" },
            new Specialization { Id = 8, Name = "Ортопед-травматолог" },
            new Specialization { Id = 9, Name = "Гастроэнтеролог" },
            new Specialization { Id = 10, Name = "Эндокринолог" },
        ];
    }

    /// <summary>
    /// Get seeded doctors
    /// </summary>
    public static List<Doctor> GetDoctors(List<Specialization> specializations)
    {
        return
        [
            new Doctor
            {
                Id = 1,
                PassportNumber = "4512 784512",
                FullName = "Орлов Дмитрий Викторович",
                YearOfBirth = 1978,
                Specialization = specializations[0], // Кардиолог
                Experience = 15
            },
            new Doctor
            {
                Id = 2,
                PassportNumber = "7852 963214",
                FullName = "Лебедева Елена Сергеевна",
                YearOfBirth = 1972,
                Specialization = specializations[1], // Невропатолог
                Experience = 22
            },
            new Doctor
            {
                Id = 3,
                PassportNumber = "9632 741852",
                FullName = "Кузнецов Артем Игоревич",
                YearOfBirth = 1982,
                Specialization = specializations[3], // Окулист
                Experience = 12
            },
            new Doctor
            {
                Id = 4,
                PassportNumber = "8521 456123",
                FullName = "Морозов Владислав Петрович",
                YearOfBirth = 1991,
                Specialization = specializations[4], // Терапевт
                Experience = 6
            },
            new Doctor
            {
                Id = 5,
                PassportNumber = "7415 789456",
                FullName = "Волкова Светлана Дмитриевна",
                YearOfBirth = 1983,
                Specialization = specializations[6], // Стоматолог-хирург
                Experience = 14
            },
            new Doctor
            {
                Id = 6,
                PassportNumber = "4569 321478",
                FullName = "Семенова Татьяна Викторовна",
                YearOfBirth = 1975,
                Specialization = specializations[6], // Стоматолог-хирург
                Experience = 25
            },
            new Doctor
            {
                Id = 7,
                PassportNumber = "7896 147258",
                FullName = "Федоров Андрей Николаевич",
                YearOfBirth = 1988,
                Specialization = specializations[6], // Стоматолог-хирург
                Experience = 9
            },
        ];
    }

    /// <summary>
    /// Get seeded patients
    /// </summary>
    public static List<Patient> GetPatients()
    {
        return
        [
            new Patient
            {
                Id = 1,
                PassportNumber = "4561 234567",
                FullName = "Козлов Алексей Владимирович",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2001, 5, 12),
                Address = "ул. Центральная, д.15",
                BloodGroup = BloodGroup.B,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89161234567"
            },
            new Patient
            {
                Id = 2,
                PassportNumber = "6543 987654",
                FullName = "Михайлова Анна Романовна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1985, 8, 24),
                Address = "ул. Советская, д.89, кв 32",
                BloodGroup = BloodGroup.Ab,
                ResusFactor = ResusFactor.Negative,
                PhoneNumber = "89037654321"
            },
            new Patient
            {
                Id = 3,
                PassportNumber = "2345 876543",
                FullName = "Никитина Ольга Петровна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1975, 11, 8),
                Address = "пр. Ленина, д. 67, кв 154",
                BloodGroup = BloodGroup.O,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89158765432"
            },
            new Patient
            {
                Id = 4,
                PassportNumber = "3456 654321",
                FullName = "Александров Александр Александрович",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1998, 2, 18),
                Address = "ул. Молодежная, д. 45",
                BloodGroup = BloodGroup.A,
                ResusFactor = ResusFactor.Negative,
                PhoneNumber = "89166543210"
            },
            new Patient
            {
                Id = 5,
                PassportNumber = "5678 987654",
                FullName = "Павлова Екатерина Олеговна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2003, 9, 7),
                Address = "ул. Лесная, д. 23, кв. 89",
                BloodGroup = BloodGroup.B,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89159876543"
            },
            new Patient
            {
                Id = 6,
                PassportNumber = "7890 123456",
                FullName = "Сергеев Максим Алексеевич",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1999, 4, 3),
                Address = "ул. Школьная, д.34, кв. 12",
                BloodGroup = BloodGroup.A,
                ResusFactor = ResusFactor.Negative,
                PhoneNumber = "89151234567"
            },
            new Patient
            {
                Id = 7,
                PassportNumber = "8901 234567",
                FullName = "Макарова Виктория Андреевна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1997, 12, 15),
                Address = "пр. Победы, д.56, кв.45",
                BloodGroup = BloodGroup.O,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89162345678"
            },
            new Patient
            {
                Id = 8,
                PassportNumber = "9012 345678",
                FullName = "Дмитриев Дмитрий Дмитриевич",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2000, 7, 22),
                Address = "ул. Строителей, д. 12, кв. 67",
                BloodGroup = BloodGroup.Ab,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89173456789"
            },
            new Patient
            {
                Id = 9,
                PassportNumber = "0123 456789",
                FullName = "Андреев Андрей Андреевич",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1988, 10, 30),
                Address = "ул. Садовая, д.78, кв. 33",
                BloodGroup = BloodGroup.B,
                ResusFactor = ResusFactor.Negative,
                PhoneNumber = "89184567890"
            },
            new Patient
            {
                Id = 10,
                PassportNumber = "1230 567890",
                FullName = "Егорова Наталья Владимировна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2005, 3, 14),
                Address = "ул. Парковая, д.91, кв. 124",
                BloodGroup = BloodGroup.A,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89195678901"
            },
        ];
    }

    /// <summary>
    /// Get seeded appointments
    /// </summary>
    public static List<Appointment> GetAppointments(List<Patient> patients, List<Doctor> doctors)
    {
        return
        [
            new Appointment{
                Id = 1,
                Patient = patients[0],
                Doctor = doctors[0],
                AppointmentDateTime = new DateTime(2025, 7, 20, 9, 30,0),
                RoomNumber = 301,
                RepeatAppointment = false
            },
            new Appointment{
                Id = 2,
                Patient = patients[1],
                Doctor = doctors[1],
                AppointmentDateTime = new DateTime(2025, 11, 15, 10, 15,0),
                RoomNumber = 205,
                RepeatAppointment = true
            },
            new Appointment{
                Id = 3,
                Patient = patients[2],
                Doctor = doctors[2],
                AppointmentDateTime = new DateTime(2025, 8, 12, 11, 0,0),
                RoomNumber = 118,
                RepeatAppointment = false
            },
            new Appointment{
                Id = 4,
                Patient = patients[2],
                Doctor = doctors[3],
                AppointmentDateTime = new DateTime(2025, 3, 18, 14, 45,0),
                RoomNumber = 267,
                RepeatAppointment = true
            },
            new Appointment{
                Id = 5,
                Patient = patients[4],
                Doctor = doctors[4],
                AppointmentDateTime = new DateTime(2025, 4, 5, 12, 30,0),
                RoomNumber = 312,
                RepeatAppointment = false
            },
            new Appointment{
                Id = 6,
                Patient = patients[3],
                Doctor = doctors[5],
                AppointmentDateTime = new DateTime(2025, 12, 10, 15, 20,0),
                RoomNumber = 423,
                RepeatAppointment = true
            },
            new Appointment{
                Id = 7,
                Patient = patients[6],
                Doctor = doctors[5],
                AppointmentDateTime = new DateTime(2025, 12, 25, 16, 0,0),
                RoomNumber = 215,
                RepeatAppointment = false
            },
            new Appointment{
                Id = 8,
                Patient = patients[7],
                Doctor = doctors[6],
                AppointmentDateTime = new DateTime(2025, 12, 18, 17, 30,0),
                RoomNumber = 134,
                RepeatAppointment = true
            },
            new Appointment{
                Id = 9,
                Patient = patients[8],
                Doctor = doctors[6],
                AppointmentDateTime = new DateTime(2025, 9, 8, 13, 15,0),
                RoomNumber = 278,
                RepeatAppointment = false
            },
            new Appointment{
                Id = 10,
                Patient = patients[9],
                Doctor = doctors[6],
                AppointmentDateTime = new DateTime(2025, 10, 22, 11, 45,0),
                RoomNumber = 192,
                RepeatAppointment = true
            },
        ];
    }
}