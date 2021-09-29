using System;
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
		public static readonly PerformanceParameters FortyFiveFifteen
			= new PerformanceParameters
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
	}
}
