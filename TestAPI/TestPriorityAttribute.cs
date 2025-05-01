using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class TestPriorityAttribute : Attribute
    {
        public int Priority { get; private set; }

        public TestPriorityAttribute(int priority) => Priority = priority;
    }
}
