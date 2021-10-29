using System;
using System.Text;
using System.Text.Json.Serialization;

namespace FullBodyMix.Models
{
	public record PerformanceParameters
	{
		/// <summary>
		/// How many repetitions to perform?
		/// </summary>
		public int? Repetitions { get; init; }

		/// <summary>
		/// How long is the pause until the next exercise?
		/// </summary>
		[JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan? RestTime { get; init; }

		/// <summary>
		/// How long should the exercise be performed for?
		/// </summary>
		[JsonConverter(typeof(JsonTimeSpanConverter))]
		public TimeSpan? WorkTime { get; init; }

		public string Describe()
		{
			var sb = new StringBuilder();
			if (WorkTime.HasValue)
			{
				sb.Append(WorkTime.Value.TotalSeconds);
				sb.Append(" seconds");
			}
			if (WorkTime.HasValue && Repetitions.HasValue)
			{
				sb.Append(" OR ");
			}
			if (Repetitions.HasValue)
			{
				sb.Append(Repetitions.Value);
			}
			if (sb.Length > 0)
			{
				return sb.ToString();
			}
			else
			{
				return "unspecified";
			}
		}
	}
}
