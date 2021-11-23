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

		internal static void Countdown
			(PlaylistEntry entry, string overallProgress, Callback callback)
		{
			for (int sec = 3; sec > 0; sec--)
			{
				var vp = new ViewParameters
				{
					CurrentEntry = entry,
					CurrentMode = Mode.Preparing,
					CurrentProgress = sec.ToString(),
					OverallProgress = overallProgress,
					SpokenAnnouncement = sec.ToString(),
				};
				callback(vp);
			}
		}

		internal void StartWorkout(Callback callback)
		{
			if (Playlist == null || Playlist.Count < 1)
			{
				return;
			}
			var firstEntry = Playlist[0];
			var firstExercise = firstEntry.Exercise;
			var firstParameters = firstEntry.PerformanceParameters;
			var nextUp = $"Next: {firstParameters.Describe()} {firstExercise.Name}";
			var spoken = nextUp;
			int startDelaySeconds = Convert.ToInt32(StartDelay.TotalSeconds);
			for (int sec = startDelaySeconds; sec >= 4; sec--)
			{
				var vp = new ViewParameters
				{
					CurrentEntry = firstEntry,
					CurrentMode = Mode.Preparing,
					CurrentProgress = sec.ToString(),
					OverallProgress = $"1 of {Playlist.Count}",
					SpokenAnnouncement = spoken,
				};
				callback(vp);
				if (sec == startDelaySeconds)
				{
					spoken = null;
				}
			}
		}
	}
}
