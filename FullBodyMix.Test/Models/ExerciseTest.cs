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
		public static readonly Exercise Burpees = new()
		{
			Name = "Burpees",
			FocusArea = FocusArea.Cardio,
			Description = @"Start standing up
1. Crouch down
2. Kick your feet backwards to get into a high plank
3. Perform a push up
4. Bring your feet back into a crouching position
5. Stand up
6. Repeat",
		};

		[TestMethod]
		public void SerializationRoundTrip()
		{
			var exercise = Burpees;
			var exerciseAsString = JsonSerializer.Serialize(exercise);

			var actual = JsonSerializer.Deserialize<Exercise>(exerciseAsString);

			Assert.AreEqual(exercise, actual);
		}
	}
}
