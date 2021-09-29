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
		public void SerializationRoundTrip_Naked()
		{
			var parameters = new PerformanceParameters
			{
			};
			var parametersAsString = JsonSerializer.Serialize(parameters);

			var actual = JsonSerializer.Deserialize<PerformanceParameters>(parametersAsString);

			Assert.AreEqual(parameters.RestTime, actual.RestTime);
			Assert.AreEqual(parameters.WorkTime, actual.WorkTime);
		}

		[TestMethod]
		public void SerializationRoundTrip_WithWorkAndRestTime()
		{
			var parameters = new PerformanceParameters
			{
				RestTime = TimeSpan.FromSeconds(15),
				WorkTime = TimeSpan.FromSeconds(45),
			};
			var parametersAsString = JsonSerializer.Serialize(parameters);

			var actual = JsonSerializer.Deserialize<PerformanceParameters>(parametersAsString);

			Assert.AreEqual(parameters.RestTime, actual.RestTime);
			Assert.AreEqual(parameters.WorkTime, actual.WorkTime);
		}
	}
}
