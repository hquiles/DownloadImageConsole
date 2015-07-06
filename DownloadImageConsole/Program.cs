using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web;
using Newtonsoft.Json.Linq;

namespace DownloadImageConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            JArray ja;
            Console.WriteLine("Starting....");
            string access_token = "Token";
            FacebookClient fbclient = new FacebookClient(access_token);
            dynamic Albumlist = fbclient.Get("/me/albums");

            int count = (int)Albumlist.data.Count;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(i + " " + Albumlist.data[i].id + " " + Albumlist.data[i].name);

            }
            
            Console.WriteLine("");
            Console.WriteLine("Enter the Number of the Album to download");
            string input = Console.ReadLine();
            int number;
            Int32.TryParse(input, out number);

            String strrr = "/" + Albumlist.data[number].id + "/photos";
            dynamic pics = fbclient.Get(strrr);
            count = (int)pics.data.Count;
            String ssss = Convert.ToString(pics.data);
            //JObject ob = JObject.Parse(ssss);
            ja = JArray.Parse(ssss);
            WebClient wc = new WebClient();

            int j = 0;
            try
            {
                string folderpath = String.Format(@"{0}\Album", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                while (ja[j]["images"][0]["source"].ToString() != null)
                {
                    wc.DownloadFile(ja[j]["images"][0]["source"].ToString(), folderpath + "\\" + j.ToString() + ".jpg");
                    Console.WriteLine("Downloading : " + folderpath + "\\" + j.ToString() + ".jpg");
                    j++;
                }
                Console.WriteLine("Download Complete");
            }
            catch (Exception esss)
            {

            }

            Console.WriteLine("Download Complete");
            Console.ReadKey();


        }

    }
}

