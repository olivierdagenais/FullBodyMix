using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace FullBodyMix.Models
{
	public record Workout
	{
		/// <summary>
		/// Every workout must be at least one exercise.
		/// </summary>
		public ImmutableList<Exercise> Exercises { get; init; }

		/// <summary>
		/// How long would this workout take to perform?
		/// </summary>
		[JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan Duration { get; init; } 
	}
}
