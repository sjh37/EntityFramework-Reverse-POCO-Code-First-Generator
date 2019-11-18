using System;
using System.IO;
using BuildTT.Application;

namespace BuildTT.Infrastructure
{
    public class FileReaderStrategy : IReaderStrategy
    {
        /// <summary>
        /// Reads input data from a file and returns it
        /// </summary>
        /// <param name="inputSource">The name of the file to open and read</param>
        /// <returns>True if successful, including the data, false otherwise with null data</returns>
        public (bool success, string[] data) ReadInput(string inputSource)
        {
            if (string.IsNullOrWhiteSpace(inputSource))
                return (false, null);

            if (!File.Exists(inputSource))
            {
                Console.WriteLine("Input file {0} does not exist", inputSource);
                return (false, null);
            }

            try
            {
                Console.WriteLine(inputSource);
                var data = File.ReadAllLines(inputSource);
                return (true, data);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to read input file {0}", inputSource);
                Console.WriteLine(e.Message);
                return (false, null);
            }
        }
    }
}