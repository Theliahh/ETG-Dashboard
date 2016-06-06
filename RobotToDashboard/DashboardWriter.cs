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
        private readonly IEnumerable<TestResult> _testResults;
        public DashboardWriter(IEnumerable<TestResult> results )
        {
            _testResults = results;
            WriteToHtml();
        }

        private void WriteToHtml()
        {   
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter writer = new HtmlTextWriter(stringWriter);

            AddHead(writer);

            writer.RenderBeginTag("body");
            writer.AddAttribute("class", "container");
            writer.RenderBeginTag("div");
            writer.RenderBeginTag("h2");
            writer.Write("Test Results");
            writer.RenderEndTag();

            writer.WriteLine();
            writer.AddAttribute("class","table table-bordered table-hover");
            writer.RenderBeginTag("table");
            writer.RenderBeginTag("thead");
            writer.Indent++;
            writer.RenderBeginTag("tr");
            writer.Indent++;
            writer.RenderBeginTag("th");
            writer.Write("Test Number");
            writer.RenderEndTag();
            writer.RenderBeginTag("th");
            writer.Write("Test Name");
            writer.RenderEndTag();
            writer.RenderBeginTag("th");
            writer.Write("Test Result");
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.Indent--;
            writer.RenderBeginTag("tbody");
            foreach (var result in _testResults)
            {
                writer.RenderBeginTag("tr");
                writer.RenderBeginTag("td");
                writer.Write($"{result.TestId}");
                writer.RenderEndTag();
                writer.RenderBeginTag("td");
                writer.Write($"{result.TestTitle}");
                writer.RenderEndTag();
                //Adds attribute to NEXT render
                writer.AddAttribute("class", result.Result == "PASS" ? "passed-test" : "failed-test");
                writer.RenderBeginTag("td");
                writer.Write($"{result.Result}");
                writer.RenderEndTag();
                writer.WriteLine();
                writer.RenderEndTag();

            }
            writer.RenderEndTag();
            writer.Indent--;
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            File.WriteAllText("output.html", stringWriter.ToString());
            Console.WriteLine("HTML Output written to output.html");
        }

        private void AddHead(HtmlTextWriter writer)
        {

            //Will change this later

            writer.RenderBeginTag("head");
            AddScripts(writer);
            AddStyles(writer);
            writer.RenderEndTag();
        }

        private void AddScripts(HtmlTextWriter writer)
        {
            //writer.Write("<script src='https://code.jquery.com/jquery-2.2.4.min.js' integrity='sha256-BbhdlvQf/xTY9gja0Dq3HiwQF8LaCRTXxZKRutelT44=' crossorigin='anonymous'></script>");
            //writer.Write("<script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js' integrity='sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS' crossorigin='anonymous'></script>");
        }

        private void AddStyles(HtmlTextWriter writer)
        {
            string css = File.ReadAllText("C:\\Users\\VMDW73\\Documents\\Visual Studio 2015\\Projects\\RobotToDashboard\\RobotToDashboard\\style.css");

            writer.Write("<link href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7' crossorigin='anonymous'>");
            writer.RenderBeginTag("style");
            writer.Write(css);
            writer.RenderEndTag();
        }
    }
}
