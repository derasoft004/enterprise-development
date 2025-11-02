using Xunit;
using Polyclinic.Subjects;

namespace Polyclinic.Tests;

public class PolyclinicTests(TestFixture fixture) : IClassFixture<TestFixture>
{
    [Fact]
    public void DoctorsExperienceMore10()
    {
        List<int> expectedId = [1, 2, 3, 5, 6];

        var experiencedId = fixture.Doctors
            .Where(d => d.Experience >= 10)
            .Select(d => d.Id)
            .Order()
            .ToList();

        Assert.NotNull(experiencedId);
        Assert.Equal(expectedId, experiencedId);
    }
}