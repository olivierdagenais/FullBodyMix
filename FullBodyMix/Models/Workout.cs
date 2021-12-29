﻿using System;
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
			foreach (var playlistEntry in Playlist)
			{
				// TODO: de-hardcode overallProgress
				var overallProgress = $"1 of {Playlist.Count}";
				Countdown(playlistEntry, overallProgress, callback);
				StartEntry(playlistEntry, overallProgress, callback);
				var parameters = playlistEntry.PerformanceParameters;
				if (parameters.WorkTime != null)
				{
					PerformTimedEntry(playlistEntry, overallProgress, callback);
					StopTimedEntry(playlistEntry, overallProgress, callback);
				}
			}
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

		internal static void StopTimedEntry(
			PlaylistEntry entry,
			string overallProgress,
			Callback callback)
		{
			var pp = entry.PerformanceParameters;
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
