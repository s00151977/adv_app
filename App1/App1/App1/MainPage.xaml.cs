using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            string externalip = new WebClient().DownloadString("http://icanhazip.com");
            Console.WriteLine(externalip);
            externalip = externalip.Substring(0, externalip.Length - 2);

            GetUserCountryByIp(externalip);




        }

        public string GetUserCountryByIp(string ip)
        {
            String x = "";
            IpInfo ipInfo = new IpInfo();
            try
            {
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip + "/geo");        //&" + ip); //http://ipinfo.io?token=725ce71d0ebb19
                Console.WriteLine(info.ToString());

                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                x = ipInfo.Region.ToString();

            }
            catch (Exception EX)
            {
                Console.WriteLine("ERROR" + EX.ToString());
                ipInfo.Country = null;
            }
            Console.WriteLine();
            Test.Text = x.ToString();
            Console.WriteLine();
            Console.WriteLine(ipInfo.Org + " " + ipInfo.City);
            Console.WriteLine("Loc " + ipInfo.Loc);
            return ipInfo.Country;
        }


        public static string CityStateCountByIp(string IP)
        {
            var url = "http://freegeoip.net/json/" + IP;
            var request = System.Net.WebRequest.Create(url);

            using (WebResponse wrs = request.GetResponse())
            using (Stream stream = wrs.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                var obj = JObject.Parse(json);
                var City = (string)obj["city"];
                // - For Country = (string)obj["region_name"];                    
                //- For  CountryCode = (string)obj["country_code"];
                return (City);
            }

        }
    }
}

