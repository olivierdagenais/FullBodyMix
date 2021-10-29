using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FullBodyMix.Models
{
	/// <summary>
	/// A class to test <see cref="Exercise"/>.
	/// </summary>
	[TestClass]
	public class ExerciseTest
	{
		public static readonly Exercise ArmCircles = new()
		{
			Name = "Arm circles",
			FocusArea = FocusArea.Upper,
		};

		public static readonly Exercise Burpees = new()
		{
			Name = nameof(Burpees),
			FocusArea = FocusArea.Cardio,
			Description = @"Start standing up
1. Crouch down
2. Kick your feet backwards to get into a high plank
3. Perform a push up
4. Bring your feet back into a crouching position
5. Stand up
6. Repeat",
		};

		public static readonly Exercise Crunches = new()
		{
			Name = nameof(Crunches),
			FocusArea = FocusArea.Middle,
		};

		public static readonly Exercise Lunges = new()
		{
			Name = nameof(Lunges),
			Sides = 2,
			FocusArea = FocusArea.Lower,
		};

		public static readonly Exercise Squats = new()
		{
			Name = nameof(Squats),
			FocusArea = FocusArea.Lower,
		};

		[TestMethod]
		public void SerializationRoundTrip()
		{
			var exercise = Burpees;
			var exerciseAsString = JsonSerializer.Serialize(exercise);

			var actual = JsonSerializer.Deserialize<Exercise>(exerciseAsString);

			Assert.AreEqual(exercise, actual);
		}

		[TestMethod]
		public void Sides_DefaultsToOne()
		{
			var actual = Burpees.Sides;

			Assert.AreEqual(1, actual);
		}

		[TestMethod]
		public void Sides_LungesHaveTwo()
		{
			var actual = Lunges.Sides;

			Assert.AreEqual(2, actual);
		}
	}
}
