using System;
using System.Collections.Immutable;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FullBodyMix.Models
{
	/// <summary>
	/// A class to test <see cref="TimedWorkout"/>.
	/// </summary>
	[TestClass]
	public class TimedWorkoutTest
	{
		[TestMethod]
		public void SerializationRoundTrip()
		{
			var workout = new TimedWorkout
			{
				Duration = TimeSpan.FromMinutes(4),
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
				RestTime = TimeSpan.FromSeconds(15),
				WorkTime = TimeSpan.FromSeconds(45),
			};
			var workoutAsString = JsonSerializer.Serialize(workout);

			var actual = JsonSerializer.Deserialize<TimedWorkout>(workoutAsString);

			// We can't assert the workout & actual instances directly
			// because the list of exercises won't be the same instance
			Assert.AreEqual(workout.Duration, actual.Duration);
			Assert.AreEqual(workout.RestTime, actual.RestTime);
			Assert.AreEqual(workout.WorkTime, actual.WorkTime);
			CollectionAssert.AreEqual(workout.Exercises, actual.Exercises);
		}
	}
}
