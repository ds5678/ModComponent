using UnityEngine;

/* I'm not sure what this is supposed to be used for.
 * However, I can see that it allows a function to be
 * invoked at a regular interval with a minimum delay
 * between invokations. It is completely isolated from
 * the rest of ModComponent.
 */

namespace ModComponentMapper
{
	class ExecutionValve
	{
		public delegate void function();

		private float nextExecutionTime = Time.time;
		private float minDelay;

		public ExecutionValve(float minDelay)
		{
			this.minDelay = minDelay;
		}

		public void Execute(function function)
		{
			if (Time.time < nextExecutionTime)
			{
				return;
			}

			function.Invoke();
			nextExecutionTime = Time.time + minDelay;
		}
	}
}
