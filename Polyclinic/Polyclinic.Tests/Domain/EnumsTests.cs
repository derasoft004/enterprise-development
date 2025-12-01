using Polyclinic.Domain.Enums;
using Xunit;

namespace Polyclinic.Tests.Domain;

/// <summary>
/// Tests for Enums
/// </summary>
public class EnumsTests
{
    [Fact]
    public void Gender_Values_Correct()
    {
        // Arrange & Act
        var male = Gender.Male;
        var female = Gender.Female;

        // Assert
        Assert.Equal(0, (int)male);
        Assert.Equal(1, (int)female);
    }

    [Fact]
    public void BloodGroup_Values_Correct()
    {
        // Arrange & Act
        var o = BloodGroup.O;
        var a = BloodGroup.A;
        var b = BloodGroup.B;
        var ab = BloodGroup.Ab;

        // Assert
        Assert.Equal(0, (int)o);
        Assert.Equal(1, (int)a);
        Assert.Equal(2, (int)b);
        Assert.Equal(3, (int)ab);
    }

    [Fact]
    public void ResusFactor_Values_Correct()
    {
        // Arrange & Act
        var positive = ResusFactor.Positive;
        var negative = ResusFactor.Negative;

        // Assert
        Assert.Equal(0, (int)positive);
        Assert.Equal(1, (int)negative);
    }
}