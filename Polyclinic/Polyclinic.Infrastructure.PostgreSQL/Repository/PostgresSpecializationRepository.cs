using Microsoft.EntityFrameworkCore;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Domain.Subjects;

namespace Polyclinic.Infrastructure.PostgreSql.Repository;

public class PostgresSpecializationRepository(PolyclinicDbContext context) : IRepository<Specialization, int>
{
    public int Create(Specialization entity)
    {
        context.Specializations.Add(entity);
        context.SaveChanges();
        return entity.Id;
    }

    public List<Specialization> ReadAll()
    {
        return [.. context.Specializations];
    }

    public Specialization? Read(int id)
    {
        return context.Specializations.Find(id);
    }

    public Specialization? Update(int id, Specialization entity)
    {
        var existing = context.Specializations.Find(id);
        if (existing == null)
            return null;

        existing.Name = entity.Name;
        context.SaveChanges();
        return existing;
    }

    public bool Delete(int id)
    {
        var specialization = context.Specializations.Find(id);
        if (specialization == null)
            return false;

        context.Specializations.Remove(specialization);
        context.SaveChanges();
        return true;
    }
}