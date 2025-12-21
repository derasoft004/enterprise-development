using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polyclinic.Domain.Enums;
using Polyclinic.Domain.Subjects;
using Polyclinic.Infrastructure.PostgreSQL.Configurations;

namespace Polyclinic.Infrastructure.PostgreSQL;

/// <summary>
/// Database seeder for initial data
/// </summary>
public class PostgresSeeder(
    PolyclinicDbContext context,
    IConfiguration configuration,
    ILogger<PostgresSeeder> logger)
{
    /// <summary>
    /// Seed the database with initial data
    /// </summary>
    public void Seed()
    {
        try
        {
            var settings = new PostgresSettings();
            configuration.GetSection(PostgresSettings.SectionName).Bind(settings);
            
            logger.LogInformation("Starting database seeding for database: {DatabaseName}", 
                settings.DatabaseName);
            
            context.Database.EnsureCreated();
            
            if (context.Specializations.Any())
            {
                logger.LogInformation("Database already seeded");
                return;
            }
            
            SeedSpecializations();
            SeedPatients();
            SeedDoctors();
            SeedAppointments();
            
            logger.LogInformation("Database seeding completed successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }
    
    private void SeedSpecializations()
    {
        var specializations = new List<Specialization>
        {
            new() { Name = "Кардиолог" },
            new() { Name = "Невропатолог" },
            new() { Name = "Дерматовенеролог" },
            new() { Name = "Окулист" },
            new() { Name = "Терапевт" },
            new() { Name = "Педиатр" },
            new() { Name = "Стоматолог-хирург" },
            new() { Name = "Ортопед-травматолог" },
            new() { Name = "Гастроэнтеролог" },
            new() { Name = "Эндокринолог" }
        };
        
        context.Specializations.AddRange(specializations);
        context.SaveChanges();
        
        logger.LogInformation("Seeded {Count} specializations", specializations.Count);
    }
    
    private void SeedPatients()
    {
        var patients = new List<Patient>
        {
            new()
            {
                PassportNumber = "4561 234567",
                FullName = "Козлов Алексей Владимирович",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2001, 5, 12, 0, 0, 0, DateTimeKind.Utc),
                Address = "ул. Центральная, д.15",
                BloodGroup = BloodGroup.B,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89161234567"
            },
            new()
            {
                PassportNumber = "6543 987654",
                FullName = "Михайлова Анна Романовна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1985, 8, 24, 0, 0, 0, DateTimeKind.Utc),
                Address = "ул. Советская, д.89, кв 32",
                BloodGroup = BloodGroup.Ab,
                ResusFactor = ResusFactor.Negative,
                PhoneNumber = "89037654321"
            },
            new()
            {
                PassportNumber = "2345 876543",
                FullName = "Никитина Ольга Петровна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1975, 11, 8, 0, 0, 0, DateTimeKind.Utc),
                Address = "пр. Ленина, д. 67, кв 154",
                BloodGroup = BloodGroup.O,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89158765432"
            },
            new()
            {
                PassportNumber = "3456 654321",
                FullName = "Александров Александр Александрович",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1998, 2, 18, 0, 0, 0, DateTimeKind.Utc),
                Address = "ул. Молодежная, д. 45",
                BloodGroup = BloodGroup.A,
                ResusFactor = ResusFactor.Negative,
                PhoneNumber = "89166543210"
            },
            new()
            {
                PassportNumber = "5678 987654",
                FullName = "Павлова Екатерина Олеговна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2003, 9, 7, 0, 0, 0, DateTimeKind.Utc),
                Address = "ул. Лесная, д. 23, кв. 89",
                BloodGroup = BloodGroup.B,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89159876543"
            },
            new()
            {
                PassportNumber = "7890 123456",
                FullName = "Сергеев Максим Алексеевич",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1999, 4, 3, 0, 0, 0, DateTimeKind.Utc),
                Address = "ул. Школьная, д.34, кв. 12",
                BloodGroup = BloodGroup.A,
                ResusFactor = ResusFactor.Negative,
                PhoneNumber = "89151234567"
            },
            new()
            {
                PassportNumber = "8901 234567",
                FullName = "Макарова Виктория Андреевна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1997, 12, 15, 0, 0, 0, DateTimeKind.Utc),
                Address = "пр. Победы, д.56, кв.45",
                BloodGroup = BloodGroup.O,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89162345678"
            },
            new()
            {
                PassportNumber = "9012 345678",
                FullName = "Дмитриев Дмитрий Дмитриевич",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2000, 7, 22, 0, 0, 0, DateTimeKind.Utc),
                Address = "ул. Строителей, д. 12, кв. 67",
                BloodGroup = BloodGroup.Ab,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89173456789"
            },
            new()
            {
                PassportNumber = "0123 456789",
                FullName = "Андреев Андрей Андреевич",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1988, 10, 30, 0, 0, 0, DateTimeKind.Utc),
                Address = "ул. Садовая, д.78, кв. 33",
                BloodGroup = BloodGroup.B,
                ResusFactor = ResusFactor.Negative,
                PhoneNumber = "89184567890"
            },
            new()
            {
                PassportNumber = "1230 567890",
                FullName = "Егорова Наталья Владимировна",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(2005, 3, 14, 0, 0, 0, DateTimeKind.Utc),
                Address = "ул. Парковая, д.91, кв. 124",
                BloodGroup = BloodGroup.A,
                ResusFactor = ResusFactor.Positive,
                PhoneNumber = "89195678901"
            }
        };
        
        context.Patients.AddRange(patients);
        context.SaveChanges();
        
        logger.LogInformation("Seeded {Count} patients", patients.Count);
    }
    
    private void SeedDoctors()
    {
        var specializations = context.Specializations.ToList();
        
        var doctors = new List<Doctor>
        {
            new()
            {
                PassportNumber = "4512 784512",
                FullName = "Орлов Дмитрий Викторович",
                YearOfBirth = 1978,
                Specialization = specializations[0],
                Experience = 15
            },
            new()
            {
                PassportNumber = "7852 963214",
                FullName = "Лебедева Елена Сергеевна",
                YearOfBirth = 1972,
                Specialization = specializations[1],
                Experience = 22
            },
            new()
            {
                PassportNumber = "9632 741852",
                FullName = "Кузнецов Артем Игоревич",
                YearOfBirth = 1982,
                Specialization = specializations[3],
                Experience = 12
            },
            new()
            {
                PassportNumber = "8521 456123",
                FullName = "Морозов Владислав Петрович",
                YearOfBirth = 1991,
                Specialization = specializations[4],
                Experience = 6
            },
            new()
            {
                PassportNumber = "7415 789456",
                FullName = "Волкова Светлана Дмитриевна",
                YearOfBirth = 1983,
                Specialization = specializations[6],
                Experience = 14
            },
            new()
            {
                PassportNumber = "4569 321478",
                FullName = "Семенова Татьяна Викторовна",
                YearOfBirth = 1975,
                Specialization = specializations[6],
                Experience = 25
            },
            new()
            {
                PassportNumber = "7896 147258",
                FullName = "Федоров Андрей Николаевич",
                YearOfBirth = 1988,
                Specialization = specializations[6],
                Experience = 9
            }
        };
        
        context.Doctors.AddRange(doctors);
        context.SaveChanges();
        
        logger.LogInformation("Seeded {Count} doctors", doctors.Count);
    }
    
    private void SeedAppointments()
    {
        var patients = context.Patients.ToList();
        var doctors = context.Doctors
            .Include(d => d.Specialization)
            .ToList();
        
        var appointments = new List<Appointment>
        {
            new()
            {
                Patient = patients[0],
                Doctor = doctors[0],
                AppointmentDateTime = new DateTime(2025, 7, 20, 9, 30, 0, DateTimeKind.Utc),
                RoomNumber = 301,
                RepeatAppointment = false
            },
            new()
            {
                Patient = patients[1],
                Doctor = doctors[1],
                AppointmentDateTime = new DateTime(2025, 11, 15, 10, 15, 0, DateTimeKind.Utc),
                RoomNumber = 205,
                RepeatAppointment = true
            },
            new()
            {
                Patient = patients[2],
                Doctor = doctors[2],
                AppointmentDateTime = new DateTime(2025, 8, 12, 11, 0, 0, DateTimeKind.Utc),
                RoomNumber = 118,
                RepeatAppointment = false
            },
            new()
            {
                Patient = patients[2],
                Doctor = doctors[3],
                AppointmentDateTime = new DateTime(2025, 3, 18, 14, 45, 0, DateTimeKind.Utc),
                RoomNumber = 267,
                RepeatAppointment = true
            },
            new()
            {
                Patient = patients[4],
                Doctor = doctors[4],
                AppointmentDateTime = new DateTime(2025, 4, 5, 12, 30, 0, DateTimeKind.Utc),
                RoomNumber = 312,
                RepeatAppointment = false
            },
            new()
            {
                Patient = patients[3],
                Doctor = doctors[5],
                AppointmentDateTime = new DateTime(2025, 12, 10, 15, 20, 0, DateTimeKind.Utc),
                RoomNumber = 423,
                RepeatAppointment = true
            },
            new()
            {
                Patient = patients[6],
                Doctor = doctors[5],
                AppointmentDateTime = new DateTime(2025, 12, 25, 16, 0, 0, DateTimeKind.Utc),
                RoomNumber = 215,
                RepeatAppointment = false
            },
            new()
            {
                Patient = patients[7],
                Doctor = doctors[6],
                AppointmentDateTime = new DateTime(2025, 12, 18, 17, 30, 0, DateTimeKind.Utc),
                RoomNumber = 134,
                RepeatAppointment = true
            },
            new()
            {
                Patient = patients[8],
                Doctor = doctors[6],
                AppointmentDateTime = new DateTime(2025, 9, 8, 13, 15, 0, DateTimeKind.Utc),
                RoomNumber = 278,
                RepeatAppointment = false
            },
            new()
            {
                Patient = patients[9],
                Doctor = doctors[6],
                AppointmentDateTime = new DateTime(2025, 10, 22, 11, 45, 0, DateTimeKind.Utc),
                RoomNumber = 192,
                RepeatAppointment = true
            }
        };
        
        context.Appointments.AddRange(appointments);
        context.SaveChanges();
        
        logger.LogInformation("Seeded {Count} appointments", appointments.Count);
    }
}