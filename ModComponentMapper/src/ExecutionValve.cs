using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

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

        public void execute(function function)
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
