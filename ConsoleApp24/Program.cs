using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ConsoleApp24
{

    class Program
    {

        static void Main(string[] args)
        {
            getTheTimezone();
        }


        private static void getTheTimezone()
        {
            var apiEndpoint = "https://en.wikipedia.org/w/api.php?action=parse&section=0&prop=text&format=json&page=pizza";
            string getJson = CallRestMethod(apiEndpoint);
            JsonDocument doc = JsonDocument.Parse(getJson);
            JsonElement root = doc.RootElement;
            var u1 = root;
            var u2 = u1.GetProperty("parse");
            var u3 = u2.GetProperty("text");
            var u4 = u3.GetProperty("*");
            FindStringRepeatedNumberOfTimes(u4.ToString(), "pizza");
        }

        public static void FindStringRepeatedNumberOfTimes(string inputString, string findString)
        {
            Console.WriteLine($"Input string:  { inputString }");

            int startCounter = 0;
            int counter = -1;
            int idx = -1;

            while (startCounter != -1)
            {
                startCounter = inputString.ToLower().IndexOf(findString.ToLower(), idx + 1);
                counter += 1;
                idx = startCounter;
            }

            Console.Write($"The string '{findString}' occurs  { counter }   times.");
        }

        public static string CallRestMethod(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            //webrequest.ContentType = "application/json";
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }
    }
}
