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
			if (Playlist is null)
			{
				return;
			}
			StartWorkout(callback);
			// ci = current index, ni = next index
			for (int ci = 0, ni = 1; ci < Playlist.Count; ci++, ni++)
			{
				var playlistEntry = Playlist[ci];
				var overallProgress = GetOverallProgress(ci + 1);
				Countdown(playlistEntry, overallProgress, callback);
				StartEntry(playlistEntry, overallProgress, callback);
				var parameters = playlistEntry.PerformanceParameters;
				if (parameters.WorkTime != null)
				{
					PerformTimedEntry(playlistEntry, overallProgress, callback);
					StopTimedEntry(playlistEntry, overallProgress, callback);
				}
				if (ni < Playlist.Count && parameters.RestTime != null)
				{
					var nextEntry = Playlist[ni];
					var restTime = parameters.RestTime.Value;
					var nextProgress = GetOverallProgress(ni + 1);
					StartPreparing(nextEntry, restTime, nextProgress, callback);
				}
			}
		}

		internal string GetOverallProgress(int step)
		{
			var result = $"{step} of {Playlist.Count}";
			return result;
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

		internal static void StartEntry(
			PlaylistEntry entry,
			string overallProgress,
			Callback callback)
		{
			var pp = entry.PerformanceParameters;
			string currentProgress = null;
			if (pp.WorkTime != null)
			{
				var workTime = pp.WorkTime.Value;
				currentProgress = workTime.TotalSeconds.ToString();
			}
			var vp = new ViewParameters
			{
				CurrentEntry = entry,
				CurrentMode = Mode.Performing,
				CurrentProgress = currentProgress,
				OverallProgress = overallProgress,
				SpokenAnnouncement = "Go!",
			};
			callback(vp);
		}

		internal static void PerformTimedEntry(
			PlaylistEntry entry,
			string overallProgress,
			Callback callback)
		{
			var pp = entry.PerformanceParameters;
			var workTime = pp.WorkTime.Value;
			// minus one because StartEntry already used the first one
			int seconds = Convert.ToInt32(workTime.TotalSeconds) - 1;
			for (int sec = seconds; sec > 0; sec--)
			{
				var currentProgress = sec.ToString();
				string spoken = null;
				if (sec <= 3)
				{
					spoken = sec.ToString();
				}
				var vp = new ViewParameters
				{
					CurrentEntry = entry,
					CurrentMode = Mode.Performing,
					CurrentProgress = currentProgress,
					OverallProgress = overallProgress,
					SpokenAnnouncement = spoken,
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
			var overallProgress = GetOverallProgress(1);
			StartPreparing(firstEntry, StartDelay, overallProgress, callback);
		}

		internal void StartPreparing(
			PlaylistEntry entry,
			TimeSpan preparationTime,
			string overallProgress,
			Callback callback)
		{
			int seconds = Convert.ToInt32(preparationTime.TotalSeconds);
			var firstExercise = entry.Exercise;
			var parameters = entry.PerformanceParameters;
			var nextUp = $"Next: {parameters.Describe()} {firstExercise.Name}";
			var spoken = nextUp;
			for (int sec = seconds; sec >= 4; sec--)
			{
				var vp = new ViewParameters
				{
					CurrentEntry = entry,
					CurrentMode = Mode.Preparing,
					CurrentProgress = sec.ToString(),
					OverallProgress = overallProgress,
					SpokenAnnouncement = spoken,
				};
				callback(vp);
				if (sec == seconds)
				{
					spoken = null;
				}
			}
		}

		internal static void StopTimedEntry(
			PlaylistEntry entry,
			string overallProgress,
			Callback callback)
		{
			var vp = new ViewParameters
			{
				CurrentEntry = entry,
				CurrentMode = Mode.Performing,
				CurrentProgress = "0",
				OverallProgress = overallProgress,
				SpokenAnnouncement = "Stop!",
			};
			callback(vp);
		}
	}
}
