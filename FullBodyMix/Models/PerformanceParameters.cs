using System;

namespace FullBodyMix.Models
{
	public record PerformanceParameters
	{
		/// <summary>
		/// How many repetitions to perform?
		/// </summary>
		public int? Repetitions { get; init; }

		/// <summary>
		/// How long is the pause until the next exercise?
		/// </summary>
		public TimeSpan? RestTime { get; init; }

		/// <summary>
		/// How long should the exercise be performed for?
		/// </summary>
		public TimeSpan? WorkTime { get; init; }
	}
}
