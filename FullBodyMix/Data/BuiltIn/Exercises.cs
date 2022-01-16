using FullBodyMix.Models;

namespace FullBodyMix.Data.BuiltIn
{
	/// <summary>
	/// Hard-coded exercises, mostly to test with.
	/// </summary>
	internal class Exercises
	{
		public static readonly Exercise AlternatingLunges = new()
		{
			Name = "Alternating lunges",
			Sides = 1,
			FocusArea = FocusArea.Lower,
		};

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

		public static readonly Exercise ButtKickers = new()
		{
			Name = "Butt kickers",
			FocusArea = FocusArea.Cardio,
		};

		public static readonly Exercise CrossCountrySkiing = new()
		{
			Name = "Cross-country skiing",
			FocusArea = FocusArea.Cardio,
		};

		public static readonly Exercise Crunches = new()
		{
			Name = nameof(Crunches),
			FocusArea = FocusArea.Middle,
		};

		public static readonly Exercise DynamicStretching = new()
		{
			Name = "Dynamic stretching",
			FocusArea = FocusArea.Lower,
		};

		public static readonly Exercise HighKnees = new()
		{
			Name = "High knees",
			FocusArea = FocusArea.Cardio,
		};

		public static readonly Exercise JumpRope = new()
		{
			Name = "Jump rope",
			FocusArea = FocusArea.Cardio,
		};

		public static readonly Exercise JumpSquats = new()
		{
			Name = "Jump squats",
			FocusArea = FocusArea.Lower,
		};

		public static readonly Exercise LegRaises = new()
		{
			Name = "Leg raises",
			FocusArea = FocusArea.Lower,
		};

		public static readonly Exercise Lunges = new()
		{
			Name = nameof(Lunges),
			Sides = 2,
			FocusArea = FocusArea.Lower,
		};

		public static readonly Exercise Plank = new()
		{
			Name = nameof(Plank),
			FocusArea = FocusArea.Middle,
		};

		public static readonly Exercise PushUps = new()
		{
			Name = "Push ups",
			FocusArea = FocusArea.Upper,
		};

		public static readonly Exercise ShadowBoxing = new()
		{
			Name = "Shadow boxing",
			FocusArea = FocusArea.Upper,
		};

		public static readonly Exercise SittingTwists = new()
		{
			Name = "Sitting twists",
			FocusArea = FocusArea.Middle,
		};

		public static readonly Exercise Squats = new()
		{
			Name = nameof(Squats),
			FocusArea = FocusArea.Lower,
		};

		public static readonly Exercise SquatKicks = new()
		{
			Name = "Squat kicks",
			FocusArea = FocusArea.Lower,
		};
	}
}
