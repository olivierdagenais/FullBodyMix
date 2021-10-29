using System;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FullBodyMix.Models
{
	/// <summary>
	/// A class to test <see cref="PerformanceParameters"/>.
	/// </summary>
	[TestClass]
	public class PerformanceParametersTest
	{
		[TestMethod]
		public void Describe_Blank()
		{
			var cut = new PerformanceParameters();

			var actual = cut.Describe();

			Assert.AreEqual("unspecified", actual);
		}

		[TestMethod]
		public void Describe_Repetitions()
		{
			var cut = new PerformanceParameters
			{
				Repetitions = 20,
			};

			var actual = cut.Describe();

			Assert.AreEqual("20", actual);
		}

		static void SerializationRoundTrip(PerformanceParameters parameters)
		{
			var parametersAsString = JsonSerializer.Serialize(parameters);

			var actual = JsonSerializer.Deserialize<PerformanceParameters>(parametersAsString);

			Assert.AreEqual(parameters.Repetitions, actual.Repetitions);
			Assert.AreEqual(parameters.RestTime, actual.RestTime);
			Assert.AreEqual(parameters.WorkTime, actual.WorkTime);
		}

		[TestMethod]
		public void SerializationRoundTrip_Naked()
		{
			var parameters = new PerformanceParameters
			{
			};

			SerializationRoundTrip(parameters);
		}

		[TestMethod]
		public void SerializationRoundTrip_WithEverything()
		{
			var parameters = new PerformanceParameters
			{
				Repetitions = 10,
				RestTime = TimeSpan.FromSeconds(15),
				WorkTime = TimeSpan.FromSeconds(45),
			};

			SerializationRoundTrip(parameters);
		}

		[TestMethod]
		public void SerializationRoundTrip_WithRepetitions()
		{
			var parameters = new PerformanceParameters
			{
				Repetitions = 10,
			};

			SerializationRoundTrip(parameters);
		}

		[TestMethod]
		public void SerializationRoundTrip_WithWorkAndRestTime()
		{
			var parameters = new PerformanceParameters
			{
				RestTime = TimeSpan.FromSeconds(15),
				WorkTime = TimeSpan.FromSeconds(45),
			};

			SerializationRoundTrip(parameters);
		}
	}
}
