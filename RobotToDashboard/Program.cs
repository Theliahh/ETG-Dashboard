namespace RobotToDashboard
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //creates a parser and runs it
            Parser xmlParser = new Parser();
            xmlParser.ParseXmlFile(args.Length > 0 ? args[0] : "output.xml");
        }
    }
}
