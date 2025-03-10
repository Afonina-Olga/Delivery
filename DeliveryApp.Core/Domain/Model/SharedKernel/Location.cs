﻿using CSharpFunctionalExtensions;
using System.Diagnostics.CodeAnalysis;
using Primitives;

namespace DeliveryApp.Core.Domain.Model.SharedKernel
{
	public class Location : ValueObject
	{
		private static readonly Location _maxLocation = new(10, 10);
		private static readonly Location _minLocation = new(1, 1);

		/// <summary>
		/// Конструктор
		/// </summary>
		[ExcludeFromCodeCoverage]
		private Location()
		{

		}

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="x">Координата x</param>
		/// <param name="y">Координана y</param>
		private Location(byte x, byte y) : this()
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Координана X
		/// </summary>
		public byte X { get; }

		/// <summary>
		/// Координана Y
		/// </summary>
		public byte Y { get; }

		/// <summary>
		/// Factory method
		/// </summary>
		/// <param name="x">Координата х</param>
		/// <param name="y">Координата y</param>
		public static Result<Location, Error> Create(byte x, byte y)
		{
			if (x < _minLocation.X || x > _maxLocation.X)
			{
				return GeneralErrors.ValueIsRequired(nameof(x));
			}

			if (y < _minLocation.Y || y > _maxLocation.Y)
			{
				return GeneralErrors.ValueIsRequired(nameof(y));
			}

			return new Location(x, y);
		}

		/// <summary>
		/// Создать Location со случайными координатами
		/// </summary>
		public static Location CreateRandom()
		{
			var random = new Random();
			var x = (byte)random.Next(_minLocation.X, _maxLocation.X + 1);
			var y = (byte)random.Next(_maxLocation.Y, _maxLocation.Y + 1);
			return new Location(x, y);
		}

		/// <summary>
		/// Вычисление расстояния между пунктами назначения
		/// </summary>
		/// <param name="location">Пункт назначения</param>
		public Result<int, Error> DistanceTo(Location location)
		{
			if (location is null)
			{
				return GeneralErrors.ValueIsRequired(nameof(location));
			}

			var dx = Math.Abs(location.X - X);
			var dy = Math.Abs(location.Y - Y);

			return dx + dy;
		}

		/// <summary>
		/// Перегрузка для определения идентичности
		/// </summary>
		[ExcludeFromCodeCoverage]
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return X;
			yield return Y;
		}
	}
}
