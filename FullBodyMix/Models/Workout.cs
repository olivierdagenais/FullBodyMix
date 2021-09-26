using System.Collections.Immutable;

namespace FullBodyMix.Models
{
	public record Workout
	{
		/// <summary>
		/// Every workout must be at least one exercise.
		/// </summary>
		public ImmutableList<Exercise> Exercises { get; init; }
	}
}
