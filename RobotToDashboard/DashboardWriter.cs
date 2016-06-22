using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            float passRate = 0;
            DateTime thisDay = DateTime.Today;

            AddHead(writer);

            writer.RenderBeginTag("div");

            foreach (var result in _testResults)
            {
                if (result.Result == "PASS")
                {
                    passRate++;
                }
            }
            passRate = passRate/_testResults.ToList().Count;
            writer.RenderBeginTag("h3");
            writer.WriteLine($@"Pass Rate: {passRate*100:0.00}%");
            writer.RenderEndTag();

            writer.RenderBeginTag("h4");
            writer.WriteLine($@"Test Date: {thisDay.ToString("d")}");
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
            try
            {
                File.Delete("day4.html to make room for new one");
                Console.WriteLine("day4.html deleted");
                for (int i = 0; i < 4; i++)
                {
                    File.Copy($"day{i}.html", $"day{i + 1}.html", true);
                    Console.WriteLine($"day{i}.html renamed to day{i + 1}.html");
                }


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
            File.WriteAllText("day0.html", stringWriter.ToString());
            Console.WriteLine("HTML Output written to day0.html");

            
        }

        private static void AddHead(HtmlTextWriter writer)
        {

            //Will change this later

            writer.RenderBeginTag("head");
            AddScripts(writer);
            AddStyles(writer);
            writer.RenderEndTag();
        }

        // ReSharper disable once UnusedParameter.Local
        private static void AddScripts(HtmlTextWriter writer)
        {
            //writer.Write("<script src='https://code.jquery.com/jquery-2.2.4.min.js' integrity='sha256-BbhdlvQf/xTY9gja0Dq3HiwQF8LaCRTXxZKRutelT44=' crossorigin='anonymous'></script>");
            //writer.Write("<script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js' integrity='sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS' crossorigin='anonymous'></script>");
        }

        private static void AddStyles(HtmlTextWriter writer)
        {
            string css = File.ReadAllText("../../style.css");

            //writer.Write("<link href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css' rel='stylesheet' integrity='sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7' crossorigin='anonymous'>");
            writer.RenderBeginTag("style");
            writer.Write(css);
            writer.RenderEndTag();
        }
    }
}
