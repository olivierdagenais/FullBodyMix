using System.Text.Json.Serialization;

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
		[JsonConverter(typeof(JsonStringEnumMemberConverter))]
		public FocusArea FocusArea { get; init; }

		/// <summary>
		/// Most exercises activate muscles on both sides of the body or
		/// are performed in an alternating fashion. Some exercises are
		/// performed by focusing on one side at a time. If the exercise
		/// is in the latter camp, set this property to <c>2</c>.
		/// </summary>
		public int Sides { get; init; } = 1;

		/// <summary>
		/// What details would help a newcomer perform this exercise correctly?
		/// </summary>
		public string Description { get; init; }
	};
}
