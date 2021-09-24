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
		[TestMethod]
		public void SerializationRoundTrip()
		{
			var workout = new Workout
			{
				Duration = TimeSpan.FromMinutes(45),
				Exercises = ImmutableList.Create(
					new Exercise
					{
						Name = "Arm circles",
						FocusArea = FocusArea.Upper,
					},
					new Exercise
					{
						Name = "Burpees",
						FocusArea = FocusArea.Cardio,
					},
					new Exercise
					{
						Name = "Crunches",
						FocusArea = FocusArea.Middle,
					},
					new Exercise
					{
						Name = "Squats",
						FocusArea = FocusArea.Lower,
					}
				),
			};
			var workoutAsString = JsonSerializer.Serialize(workout);

			var actual = JsonSerializer.Deserialize<Workout>(workoutAsString);

			// We can't assert the workout & actual instances directly
			// because the list of exercises won't be the same instance
			Assert.AreEqual(workout.Duration, actual.Duration);
			CollectionAssert.AreEqual(workout.Exercises, actual.Exercises);
		}
	}
}
