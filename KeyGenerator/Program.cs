using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace KeyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            var networkInterface = interfaces.FirstOrDefault();
            var addressBytes = networkInterface.GetPhysicalAddress().GetAddressBytes();
            var dateBinary = DateTime.Now.Date.ToBinary();
            var dateBytes = BitConverter.GetBytes(dateBinary);
            var length = addressBytes.Length;
            var keyValues = new List<int>();
            for (var i = 0; i < length; i++)
            {
                var value = (dateBytes[i] ^ addressBytes[i]) * 10;
                keyValues.Add(value);
            }
            var key = string.Empty;
            foreach (var value in keyValues)
            {
                key += value;
                if (keyValues.Last() != value)
                    key += "-";
            }
            Console.WriteLine(key);
            Console.ReadKey();
        }
    }
}
