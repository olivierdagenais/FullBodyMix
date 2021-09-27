using System;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FullBodyMix.Models
{
	/// <summary>
	/// A class to test <see cref="TimedPerformanceParameters"/>.
	/// </summary>
	[TestClass]
	public class TimedPerformanceParametersTest
	{
		[TestMethod]
		public void SerializationRoundTrip()
		{
			var parameters = new TimedPerformanceParameters
			{
				RestTime = TimeSpan.FromSeconds(15),
				WorkTime = TimeSpan.FromSeconds(45),
			};
			var parametersAsString = JsonSerializer.Serialize(parameters);

			var actual = JsonSerializer.Deserialize<TimedPerformanceParameters>(parametersAsString);

			Assert.AreEqual(parameters.RestTime, actual.RestTime);
			Assert.AreEqual(parameters.WorkTime, actual.WorkTime);
		}
	}
}
