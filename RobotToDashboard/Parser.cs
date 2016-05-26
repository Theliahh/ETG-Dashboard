using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace RobotToDashboard
{
    class Parser
    {
        //Parses XML to C# class
        public Parser()
        {
            StringBuilder output = new StringBuilder();

            //XML Parsing example
            const string xmlString =
                   @"<bookstore>
                        <book genre='autobiography' publicationdate='1981-03-22' ISBN='1-861003-11-0'>
                            <title>The Autobiography of Benjamin Franklin</title>
                            <author>
                                <first-name>Benjamin</first-name>
                                <last-name>Franklin</last-name>
                            </author>
                            <price>8.99</price>
                        </book>
                    </bookstore>";
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;
                reader.ReadToFollowing("book");
                reader.MoveToFirstAttribute();
                string genre = reader.Value;
                output.AppendLine("The genre value: " + genre);

                reader.ReadToFollowing("title");
                output.AppendLine("Content of the title element: " + reader.ReadElementContentAsString());
                reader.ReadToFollowing("price");
                output.AppendLine("Price of the book: " + reader.ReadElementContentAsString());
            }
            Console.Write(output.ToString());
            Console.Read();
        }

        private void ParseRobotXml()
        {

        }
    }
}
