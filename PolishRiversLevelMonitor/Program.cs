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

        // Contains all data from web
        public static List<StationsData>? riversStatus;

        // Contains data of one selected river
        public static List<StationsData>? stationsStatus;

        // Contains local data like alarm and warning levels
        public static List<StationsWarningData>? stationsWarningData;

        // Indicates download status
        public static bool downloadComplete;

        // Indicates read from file status
        public static bool readComplete;

        // For future purpose
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

        public static void ReadFromFileData()
        {
            readComplete = false;

            bool fileOK = true;
            // Read additional data from file
            string jsonStringFromFile = "[]";
            try
            {
                jsonStringFromFile = System.IO.File.ReadAllText(@"stations_alarm_warning.json");
            }
            catch (FileNotFoundException)
            {
                fileOK = false;
            }
            catch (Exception ex)
            {
                fileOK = false;
            }

            if (fileOK)
            {
                try
                {
                    stationsWarningData = JsonSerializer.Deserialize<List<StationsWarningData>>(jsonStringFromFile);
                }
                catch (Exception ex)
                {
                    fileOK = false;
                }
                if (fileOK)
                    readComplete = true;
            }
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
            readComplete = false;
            ApplicationConfiguration.Initialize();
            Application.Run(new ProgramForm());
        }
    }
}