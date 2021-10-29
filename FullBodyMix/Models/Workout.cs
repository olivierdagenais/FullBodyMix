using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace FullBodyMix.Models
{
	public record Workout
	{
		/// <summary>
		/// Every workout must be at least one <see cref="PlaylistEntry"/>.
		/// </summary>
		public ImmutableList<PlaylistEntry> Playlist { get; init; }

		/// <summary>
		/// How long until the first exercise starts?
		/// </summary>
		[JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan StartDelay { get; init; } = TimeSpan.FromSeconds(5);

		public void Perform(Func<ViewParameters, Result> callback)
		{

		}
	}
}
