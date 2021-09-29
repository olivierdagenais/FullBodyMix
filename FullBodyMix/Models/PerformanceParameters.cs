using System;
using System.Text.Json.Serialization;

namespace FullBodyMix.Models
{
	public record PerformanceParameters
	{
		/// <summary>
		/// How long is the pause until the next exercise?
		/// </summary>
		[JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan RestTime { get; init; }

		/// <summary>
		/// How long should the exercise be performed for?
		/// </summary>
		[JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan WorkTime { get; init; }
	}
}
