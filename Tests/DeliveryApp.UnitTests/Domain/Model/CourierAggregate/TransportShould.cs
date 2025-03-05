using DeliveryApp.Core.Domain.Model.CourierAggregate;
using FluentAssertions;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Model.CourierAggregate
{
	public class TransportShould
	{
		[Theory]
		[InlineData("Bicycle")]
		[InlineData("Pedestrian")]
		[InlineData("Car")]
		public void CanGetTransportByNameWhenNameIsCorrect(string name)
		{
			//Arrange

			//Act
			var result = Transport.FromName(name);

			//Assert
			result.IsSuccess.Should().BeTrue();
			result.Value.Name.Should().Be(name);
		}

		[Theory]
		[InlineData("Skier")]
		[InlineData("Motorcyclist")]
		public void ReturnsErrorWhenNameIsInCorrect(string name)
		{
			//Arrange

			//Act
			var result = Transport.FromName(name);

			//Assert
			result.IsSuccess.Should().BeFalse();
			result.Error.Should().Be(Transport.Errors.TransportIsWrong());
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void CanGetTransportByIdWhenIdIsCorrect(int id)
		{
			// Arrange

			// Act
			var result = Transport.FromId(id);

			// Assert
			result.IsSuccess.Should().BeTrue();
			result.Value.Id.Should().Be(id);
		}

		[Theory]
		[InlineData(10)]
		[InlineData(0)]
		[InlineData(4)]
		public void ReturnsErrorWhenIdIsInCorrect(int id)
		{
			// Arrange

			// Act
			var result = Transport.FromId(id);

			// Assert
			result.IsSuccess.Should().BeFalse();
			result.Error.Should().Be(Transport.Errors.TransportIsWrong());
		}

		// Два транспорта должны быть равны, если их ID равны
		// TODO Не поняла, нужен ли этот тест вообще
		[Fact]
		public void TwoTransportsAreEqualWhenIdsAreEqual()
		{
			// Arrange
			var transport1 = Transport.Car;
			var transport2 = Transport.Car;

			// Act
			var result = transport1 == transport2;

			// Assert
			result.Should().BeTrue();
		}
	}
}
