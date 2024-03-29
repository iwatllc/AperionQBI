﻿namespace AperionQB.Infrastructure.Logging
{
    public class Logger
    {
        public Logger()
        {

        }
        public void log(string message)
        {
            try
            {
                StreamWriter file;
                string path = Directory.GetCurrentDirectory() + "/logs/QuibbyLog" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Year;
                Console.WriteLine(message);
                file = File.AppendText(path);
                file.WriteLine(message);
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong when trying to write to log file: " + e.Message);
            }
        }
    }
}