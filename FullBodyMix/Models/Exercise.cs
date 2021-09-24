namespace FullBodyMix.Models
{
	public record Exercise
	{
		/// <summary>
		/// A short, unique name for the exercise.
		/// </summary>
		public string Name { get; init; }

		/// <summary>
		/// What is the primary area of focus for the exercise?
		/// </summary>
		public FocusArea FocusArea { get; init; } 

		/// <summary>
		/// What details would help a newcomer perform this exercise correctly?
		/// </summary>
		public string Description { get; init; }
	};
}
