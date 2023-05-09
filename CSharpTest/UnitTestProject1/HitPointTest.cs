using CSharpTests;
using FluentAssertions;
using System;
using Xunit;

#region HitPoint
public class HitPointTests
{
    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(10, 10, 0)]
    [InlineData(10, 15, 0)]
    public void Subtract_WithValidAmount_ShouldReturnNewHitPoint(int initialValue, int amount, int expectedValue)
    {
        // Arrange
        var hitPoint = new HitPoint(initialValue);

        // Act
        var newHitPoint = hitPoint.Subtract(amount);

        // Assert
        newHitPoint.Value.Should().Be(expectedValue);
    }

    [Fact]
    public void Same_WithDifferentHitPoints_ShouldReturnFalse()
    {
        // Arrange
        var hitPoint1 = new HitPoint(10);
        var hitPoint2 = new HitPoint(5);

        // Act
        var result = hitPoint1.Same(hitPoint2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Same_WithSameHitPoints_ShouldReturnTrue()
    {
        // Arrange
        var hitPoint1 = new HitPoint(10);
        var hitPoint2 = new HitPoint(10);

        // Act
        var result = hitPoint1.Same(hitPoint2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Constructor_WithNegativeValue_ShouldThrowArgumentException()
    {
        // Arrange
        Action act = () => new HitPoint(-1);

        // Act & Assert
        act.Should().Throw<ArgumentException>().WithMessage("체력은 0 이상이어야 합니다.");
    }
}
#endregion

#region MagicPoint
public class MagicPointTests
{
    [Theory]
    [InlineData(10, 5, 5)]
    [InlineData(10, 10, 0)]
    [InlineData(10, 15, 0)]
    public void Subtract_WithValidAmount_ShouldReturnNewMagicPoint(int initialValue, int amount, int expectedValue)
    {
        // Arrange
        var magicPoint = new MagicPoint(initialValue);

        // Act
        var newMagicPoint = magicPoint.Subtract(amount);

        // Assert
        newMagicPoint.Value.Should().Be(expectedValue);
    }

    [Fact]
    public void Same_WithDifferentMagicPoints_ShouldReturnFalse()
    {
        // Arrange
        var magicPoint1 = new MagicPoint(10);
        var magicPoint2 = new MagicPoint(5);

        // Act
        var result = magicPoint1.Same(magicPoint2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Same_WithSameMagicPoints_ShouldReturnTrue()
    {
        // Arrange
        var magicPoint1 = new MagicPoint(10);
        var magicPoint2 = new MagicPoint(10);

        // Act
        var result = magicPoint1.Same(magicPoint2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Constructor_WithNegativeValue_ShouldThrowArgumentException()
    {
        // Arrange
        Action act = () => new MagicPoint(-1);

        // Act & Assert
        act.Should().Throw<ArgumentException>().WithMessage("마나는 0 이상이어야 합니다.");
    }
}
#endregion

#region Player
public class PlayerTests
{
    [Theory]
    [InlineData(50, 30, 20, 10, 30, 20)]
    [InlineData(100, 50, 30, 20, 70, 30)]
    public void Damage_ReduceHitPointAndMagicPoint(int initialHp, int initialMp, int damage, int mpCost, int expectedHp, int expectedMp)
    {
        // Arrange
        var player = new Player(new HitPoint(initialHp), new MagicPoint(initialMp));
        var damagePoint = new HitPoint(damage);
        var mpCostPoint = new MagicPoint(mpCost);

        // Act
        player.Damage(damagePoint, mpCostPoint);

        // Assert
        player.HP.Value.Should().Be(expectedHp);
        player.MP.Value.Should().Be(expectedMp);
    }

    [Theory]
    [InlineData(0, 30, 20, 10, 0, 20)]
    [InlineData(50, 30, 20, 10, 30, 20)]
    [InlineData(100, 50, 50, 50, 50, 0)]
    public void Damage_ReducesHitPointAndMagicPoint(
    int initialHP, int initialMP, int damageValue, int mpCostValue,
    int expectedHP, int expectedMP)
    {
        // Arrange
        var player = new Player(new HitPoint(initialHP), new MagicPoint(initialMP));
        var damage = new HitPoint(damageValue);
        var mpCost = new MagicPoint(mpCostValue);

        // Act
        player.Damage(damage, mpCost);

        // Assert
        player.HP.Value.Should().Be(expectedHP); // HP 확인
        player.MP.Value.Should().Be(expectedMP); // MP 확인
    }
}
#endregion

