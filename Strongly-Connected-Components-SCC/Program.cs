using System;
using System.IO;

namespace Strongly_Connected_Components_SCC
{
    class Program
    {
        static void Main(string[] args)
        {

            ExtractDataFromFile("TestFile.txt");

        }

        static void ExtractDataFromFile(string filePath)
        {
            using(TextReader reader = File.OpenText(@filePath))
            {
                string text = reader.ReadLine();
                while(text != null)
                {
                    // do shit here

                    text = reader.ReadLine();
                }
            }
        }
    }
}
