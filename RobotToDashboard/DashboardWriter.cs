using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace RobotToDashboard
{
    class DashboardWriter
    {
        private IEnumerable<TestResult> testResults;
        public DashboardWriter(IEnumerable<TestResult> results )
        {
            testResults = results;
            WriteToHtml();
        }

        private void WriteToHtml()
        {
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter(stringWriter);

            writer.RenderBeginTag("h2");
            writer.Write("Test Results");
            writer.RenderEndTag();
            writer.WriteLine();
            writer.RenderBeginTag("ol");
            writer.Indent++;
            foreach (var result in testResults)
            {
                writer.RenderBeginTag("li");
                writer.Write($"{result.TestId} {result.TestTitle} ");
                writer.WriteBreak();

                writer.Write($"&nbsp &nbsp &nbsp Result: {result.Result}");
                writer.RenderEndTag();
                writer.WriteLine();

            }
            writer.Indent--;
            writer.RenderEndTag();

            File.WriteAllText("output.html", stringWriter.ToString());
            Console.WriteLine("HTML Output written to output.html");
        }
    }
}
