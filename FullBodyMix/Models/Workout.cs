using System;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

using Callback = System.Func<FullBodyMix.Models.ViewParameters, FullBodyMix.Models.Result>;

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

		public void Perform(Callback callback)
		{

		}

		internal void Start(Callback callback)
		{

		}
	}
}
