using System;
using System.Text;

namespace ModComponentUtils
{
	public static class DiagnosticUtils
	{
		public static string ComparePerformance(Action action1, Action action2, int numRuns) => ComparePerformance("Action 1", action1, "Action 2", action2, numRuns);
		public static string ComparePerformance(string action1Name, Action action1, string action2Name, Action action2, int numRuns)
		{
			double action1Runtime = GetTotalRuntime(action1, numRuns);
			double action2Runtime = GetTotalRuntime(action2, numRuns);
			double ratio1 = action1Runtime / action2Runtime;
			double ratio2 = action2Runtime / action1Runtime;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("");
			stringBuilder.AppendLine("");
			stringBuilder.AppendLine(string.Format("Total runtime (in milliseconds) for '{0}' : '{1}'", action1Name, action1Runtime));
			stringBuilder.AppendLine(string.Format("Total runtime (in milliseconds) for '{0}' : '{1}'", action2Name, action2Runtime));
			if (action1Runtime < action2Runtime)
			{
				stringBuilder.AppendLine(string.Format("'{0}' was '{1}' times faster than '{2}'", action1Name, ratio2, action2Name));
			}
			else
			{
				stringBuilder.AppendLine(string.Format("'{0}' was '{1}' times faster than '{2}'", action2Name, ratio1, action1Name));
			}
			return stringBuilder.ToString();
		}

		public static double GetTotalRuntime(Action action, int numRuns)
		{
			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();
			for (int i = 0; i < numRuns; ++i)
			{
				action.Invoke();
			}
			stopwatch.Stop();
			return stopwatch.Elapsed.TotalMilliseconds;
		}

		public static double GetAverageRuntime(Action action, int numRuns) => GetTotalRuntime(action, numRuns) / numRuns;
	}
}
