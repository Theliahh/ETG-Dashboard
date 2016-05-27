using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml;
using System.IO;

namespace RobotToDashboard
{
    public class Parser
    {
        
        //Parses XML to C# class
        public Parser()
        {
            StringBuilder output = new StringBuilder();
            List<TestResult> results = new List<TestResult>();
            int testCount = 0;
            
            string xmlString = System.IO.File.ReadAllText("output.xml");
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                
                reader.ReadToFollowing("robot");
                reader.MoveToFirstAttribute();
                string dateTime = reader.Value;
                output.AppendLine("Tests performed at: " + dateTime);
                string critical = "no";
                while (reader.Read())
                {
                    testCount++;
                    reader.ReadToFollowing("test");
                    string testName = reader.GetAttribute("name");
                    if(testName != null)
                        output.AppendLine("Test name: " + testName);                     
                    string passFail = "FAIL";
                    reader.ReadToFollowing("status");
                    if (reader.Name == "status")
                        while (critical != "yes")
                        {
                            reader.ReadToFollowing("status");
                            critical = reader.GetAttribute("critical");
                        }
                    critical = "no";
                    passFail = reader.GetAttribute("status");
                    if (passFail != null)
                    {
                        output.AppendLine("Test status: " + passFail);
                        TestResult newResult = new TestResult(testCount,testName,passFail);
                        results.Add(newResult);
                    }
                        
                }
            }
            Console.Write(output.ToString());
            WriteToHtml(results);
            //Console.WriteLine("Press any key to exit");
            //Console.ReadKey();
            System.Diagnostics.Process.Start("output.html");
        }

        private void ParseRobotXml()
        {

        }

        private void WriteToHtml(List<TestResult> testResults)
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
