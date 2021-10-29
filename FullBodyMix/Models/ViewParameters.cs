namespace FullBodyMix.Models
{
	public record ViewParameters
	{
		/// <summary>
		/// What <see cref="Mode"/> are we currently at?
		/// </summary>
		public Mode CurrentMode { get; init; }

		/// <summary>
		/// What <see cref="Exercise"/> and <see cref="PerformanceParameters"/>
		/// are we currently at?
		/// </summary>
		public PlaylistEntry CurrentEntry { get; init; }

		/// <summary>
		/// The progress of the current <see cref="PlaylistEntry"/>.
		/// </summary>
		public string CurrentProgress { get; init; }

		/// <summary>
		/// The progress across the entire <see cref="Workout"/>.
		/// </summary>
		public string OverallProgress { get; init; }

		/// <summary>
		/// Is there any text we want to hear?
		/// </summary>
		public string SpokenAnnouncement { get; init; }
	}
}
