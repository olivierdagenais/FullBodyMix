using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FullBodyMix.Models
{
	/// <summary>
	/// A class to test <see cref="Workout"/>.
	/// </summary>
	[TestClass]
	public class WorkoutTest
	{
		public static readonly PerformanceParameters FortyFiveFifteen = new()
		{
			WorkTime = TimeSpan.FromSeconds(45),
			RestTime = TimeSpan.FromSeconds(15),
		};

		public static readonly ImmutableList<PlaylistEntry> TestPlaylist = new[]
		{
			new PlaylistEntry {
				Exercise = ExerciseTest.ArmCircles,
				PerformanceParameters = FortyFiveFifteen,
			},
			new PlaylistEntry {
				Exercise = ExerciseTest.Burpees,
				PerformanceParameters = FortyFiveFifteen,
			},
			new PlaylistEntry {
				Exercise = ExerciseTest.Crunches,
				PerformanceParameters = FortyFiveFifteen,
			},
			new PlaylistEntry {
				Exercise = ExerciseTest.Squats,
				PerformanceParameters = FortyFiveFifteen,
			},
		}.ToImmutableList();

		public static readonly PlaylistEntry BurpeesFiveFive = new()
		{
			Exercise = ExerciseTest.Burpees,
			PerformanceParameters = new PerformanceParameters
			{
				WorkTime = TimeSpan.FromSeconds(5),
				RestTime = TimeSpan.FromSeconds(5),
			},
		};

		public static readonly Workout SmallestPossibleWorkout = new()
		{
			Playlist = new[]
			{
				BurpeesFiveFive,
			}.ToImmutableList(),
		};

		private static void TestCallbackNotCalled(
			Action<Func<ViewParameters, Result>> mut
		)
		{
			var executed = false;

			mut.Invoke(vp =>
			{
				executed = true;
				return Result.Continue;
			});

			Assert.IsFalse(executed);
		}

		[TestMethod]
		public void Countdown_BurpeesFiveFive()
		{
			var expected = new[]
			{
				new ViewParameters
				{
					CurrentMode = Mode.Preparing,
					CurrentEntry = BurpeesFiveFive,
					CurrentProgress = "3",
					OverallProgress = "1 of 1",
					SpokenAnnouncement = "3",
				},
				new ViewParameters
				{
					CurrentMode = Mode.Preparing,
					CurrentEntry = BurpeesFiveFive,
					CurrentProgress = "2",
					OverallProgress = "1 of 1",
					SpokenAnnouncement = "2",
				},
				new ViewParameters
				{
					CurrentMode = Mode.Preparing,
					CurrentEntry = BurpeesFiveFive,
					CurrentProgress = "1",
					OverallProgress = "1 of 1",
					SpokenAnnouncement = "1",
				},
			}.ToImmutableList();

			var actual = new List<ViewParameters>();
			Workout.Countdown(BurpeesFiveFive, "1 of 1", vp => {
				actual.Add(vp);
				return Result.Continue;
			});

			ListsEqual(expected, actual);
		}

		[TestMethod]
		public void Perform_EmptyPlaylist()
		{
			var cut = new Workout()
			{
				Playlist = ImmutableList.Create<PlaylistEntry>(),
			};

			TestCallbackNotCalled(cut.Perform);
		}

		[TestMethod]
		public void Perform_NullPlaylist()
		{
			var cut = new Workout();

			TestCallbackNotCalled(cut.Perform);
		}

		[TestMethod]
		public void PerformTimedEntry_BurpeesFiveFive()
		{
			var expected = new[]
			{
				new ViewParameters
				{
					CurrentMode = Mode.Performing,
					CurrentEntry = BurpeesFiveFive,
					CurrentProgress = "4",
					OverallProgress = "1 of 1",
				},
				new ViewParameters
				{
					CurrentMode = Mode.Performing,
					CurrentEntry = BurpeesFiveFive,
					CurrentProgress = "3",
					OverallProgress = "1 of 1",
					SpokenAnnouncement = "3",
				},
				new ViewParameters
				{
					CurrentMode = Mode.Performing,
					CurrentEntry = BurpeesFiveFive,
					CurrentProgress = "2",
					OverallProgress = "1 of 1",
					SpokenAnnouncement = "2",
				},
				new ViewParameters
				{
					CurrentMode = Mode.Performing,
					CurrentEntry = BurpeesFiveFive,
					CurrentProgress = "1",
					OverallProgress = "1 of 1",
					SpokenAnnouncement = "1",
				},
			}.ToImmutableList();

			var actual = new List<ViewParameters>();
			Workout.PerformTimedEntry(BurpeesFiveFive, "1 of 1", vp => {
				actual.Add(vp);
				return Result.Continue;
			});

			ListsEqual(expected, actual);

		}

		[TestMethod]
		public void SerializationRoundTrip()
		{
			var workout = new Workout
			{
				Playlist = TestPlaylist,
			};
			var workoutAsString = JsonSerializer.Serialize(workout);

			var actual = JsonSerializer.Deserialize<Workout>(workoutAsString);

			// We can't assert the workout & actual instances directly
			// because the Playlist won't be the same instance
			CollectionAssert.AreEqual(workout.Playlist, actual.Playlist);
		}

		[TestMethod]
		public void StartEntry_BurpeesFiveFive()
		{
			var expected = new[]
			{
				new ViewParameters
				{
					CurrentMode = Mode.Performing,
					CurrentEntry = BurpeesFiveFive,
					CurrentProgress = "5",
					OverallProgress = "1 of 1",
					SpokenAnnouncement = "Go!",
				},
			}.ToImmutableList();

			var actual = new List<ViewParameters>();
			Workout.StartEntry(BurpeesFiveFive, "1 of 1", vp => {
				actual.Add(vp);
				return Result.Continue;
			});

			ListsEqual(expected, actual);
		}

		[TestMethod]
		public void StartWorkout_EmptyPlaylist()
		{
			var cut = new Workout()
			{
				Playlist = ImmutableList.Create<PlaylistEntry>(),
			};

			TestCallbackNotCalled(cut.StartWorkout);
		}

		[TestMethod]
		public void StartWorkout_NullPlaylist()
		{
			var cut = new Workout();

			TestCallbackNotCalled(cut.StartWorkout);
		}

		[TestMethod]
		public void StartWorkout_SmallestPossibleWorkout()
		{
			var expected = new[]
			{
				new ViewParameters
				{
					CurrentMode = Mode.Preparing,
					CurrentEntry = BurpeesFiveFive,
					CurrentProgress = "5",
					OverallProgress = "1 of 1",
					SpokenAnnouncement = "Next: 5 seconds Burpees",
				},
				new ViewParameters
				{
					CurrentMode = Mode.Preparing,
					CurrentEntry = BurpeesFiveFive,
					CurrentProgress = "4",
					OverallProgress = "1 of 1",
				},
			}.ToImmutableList();

			var actual = new List<ViewParameters>();
			SmallestPossibleWorkout.StartWorkout(vp => {
				actual.Add(vp);
				return Result.Continue;
			});

			ListsEqual(expected, actual);
		}

		[TestMethod]
		public void StopTimedEntry_BurpeesFiveFive()
		{
			var expected = new[]
			{
				new ViewParameters
				{
					CurrentMode = Mode.Performing,
					CurrentEntry = BurpeesFiveFive,
					CurrentProgress = "0",
					OverallProgress = "1 of 1",
					SpokenAnnouncement = "Stop!",
				},
			}.ToImmutableList();

			var actual = new List<ViewParameters>();
			Workout.StopTimedEntry(BurpeesFiveFive, "1 of 1", vp => {
				actual.Add(vp);
				return Result.Continue;
			});

			ListsEqual(expected, actual);
		}

		internal static void ListsEqual<T>(IList<T> expected, IList<T> actual)
		{
			var smallestCount = Math.Min(expected.Count, actual.Count);
			for (int i = 0; i < smallestCount; i++)
			{
				Assert.AreEqual(expected[i], actual[i], $"Lists differ at index {i}");
			}
			const string message = "Expected lists to be of the same length";
			Assert.AreEqual(expected.Count, actual.Count, message);
		}
	}
}
