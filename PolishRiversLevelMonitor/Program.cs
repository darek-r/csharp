using System;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Data;

namespace WinFormsApp1
{
    
    internal static class Program
    {
        static readonly HttpClient client = new HttpClient();

        public static List<StationsData>? riversStatus;

        public static List<StationsData>? stationsStatus;

        public static bool downloadComplete;

        public static string downloadException = "";

        public static async Task DownloadData()
        {
            downloadException = "OK.";

            string jsonStringFromWeb = "[]";
            try
            {
                jsonStringFromWeb = await client.GetStringAsync("http://danepubliczne.imgw.pl/api/data/hydro/");
            }
            catch (Exception ex)
            {
                downloadException = ex.Message;
            }

            riversStatus = JsonSerializer.Deserialize<List<StationsData>>(jsonStringFromWeb);

            downloadComplete = true;
        }



        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            downloadComplete = false;
            ApplicationConfiguration.Initialize();
            Application.Run(new ProgramForm());
        }
    }
}