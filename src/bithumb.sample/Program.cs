using System;
using System.Text;

namespace CCXT.NET.Bithumb.Sample
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
       /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var provider = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);

            Bithumb.Start();

            while (Console.ReadLine() != "quit")
                Console.WriteLine("Enter 'quit' to stop the services and end the process...");
        }
    }
}