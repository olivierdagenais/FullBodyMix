namespace FullBodyMix.Models
{
	/// <summary>
	/// A single item in a <see cref="Workout"/>'s playlist.
	/// </summary>
	public record PlaylistEntry
	{
		public Exercise Exercise { get; init; }

		public PerformanceParameters PerformanceParameters { get; init; }
	}
}
