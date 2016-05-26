using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotToDashboard
{
    public class TestResult
    {
        public int testId { get; set; }
        public string TestTitle { get; set; }
        public string Result { get; set; }

        public TestResult(int id, string title, string result)
        {
            testId = id;
            TestTitle = title;
            Result = result;
        }
    }
}
