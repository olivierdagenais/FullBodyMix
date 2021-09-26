using System;
using System.Text.Json.Serialization;

namespace FullBodyMix.Models
{
	public record TimedWorkout : Workout
	{
		/// <summary>
		/// How long would this workout take to perform?
		/// </summary>
		[JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan Duration { get; init; }

		/// <summary>
		/// How long is the pause until the next exercise?
		/// </summary>
		[JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan RestTime { get; init; }

		/// <summary>
		/// How long should each exercise be performed for?
		/// </summary>
		[JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan WorkTime { get; init; }
	}
}
