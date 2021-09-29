using System.Collections.Immutable;

namespace FullBodyMix.Models
{
	public record Workout
	{
		/// <summary>
		/// Every workout must be at least one <see cref="PlaylistEntry"/>.
		/// </summary>
		public ImmutableList<PlaylistEntry> Playlist { get; init; }
	}
}
