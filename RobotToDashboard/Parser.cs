using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            //XML Parsing example
            string xmlString = System.IO.File.ReadAllText("output.xml");
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                reader.ReadToFollowing("robot");
                reader.MoveToFirstAttribute();
                string dateTime = reader.Value;
                output.AppendLine("Tests performed at: " + dateTime);

                reader.ReadToFollowing("test");
                output.AppendLine("Test name: " + reader.GetAttribute("name"));
                string critical = "no";
                string passFail = "FAIL";
                while (critical != "yes")
                {
                    reader.ReadToFollowing("status");
                    critical = reader.GetAttribute("critical");
                }
                passFail = reader.GetAttribute("status");
                output.AppendLine("Test status: " + passFail);
                //reader.ReadToFollowing("price");
                //output.AppendLine("Price of the book: " + reader.ReadElementContentAsString());
            }
            Console.Write(output.ToString());
            Console.Read();
        }

        private void ParseRobotXml()
        {

        }
    }
}
