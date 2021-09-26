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
	}
}
