using System;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Security.Policy;

namespace WindowsFormsApp2
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void button1_Click(object sender, EventArgs e)
        {

            OutputDiskCUsage();
            CheckUptime();
            CheckRamUsage();
            CheckTemperature();
            CheckResourceUsage();
            RunProgramAndDeleteFile("C:\\Program Files (x86)\\VirtualDJ\\virtualdj8.exe", "C:\\Users\\Elite\\Desktop\\VNCHELPER.txt");

        }

        class FileWriting
        {
            public void WriteToFile(string textToWrite)
            {
                try
                {
                    // Open the file to write to
                    using (StreamWriter sw = File.AppendText("C:\\Users\\Elite\\Desktop\\VNCHELPER.txt"))
                    {
                        sw.WriteLine(textToWrite);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error writing to file: " + ex.Message);
                }
            }
        
        }


        public void OutputDiskCUsage()
        {
            FileWriting fileWriting = new FileWriting();
            DriveInfo di = new DriveInfo("C:/");
            double totalSize = di.TotalSize / 1000000000;
            double freeSpace = di.TotalFreeSpace / 1000000000;
            double usedSpace = totalSize - freeSpace;
            string freediskspace = "\n Disk C: Usage: " + usedSpace + " GB out of " + totalSize + " GB";
            fileWriting.WriteToFile(freediskspace);
           
        }


            public void CheckTemperature()
        {
            FileWriting fileWriting = new FileWriting();
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
                foreach (ManagementObject obj in searcher.Get())
                {
                    string temp = obj["CurrentTemperature"].ToString();
                    string temperature = "Temperature: " + (Convert.ToInt32(temp) - 2732) + "°C";
                    fileWriting.WriteToFile(temperature);
                }
            }
            catch (ManagementException)
            {
                fileWriting.WriteToFile("Error: Unable to retrieve temperature data.");
            }
        }


       
            public void RunProgramAndDeleteFile(string programPath, string filePath)
            {
                try
                {
                    // Start the program
                    Process process = new Process();
                    process.StartInfo.FileName = programPath;
                    process.Start();
                    process.WaitForExit();

                    // Delete the file
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error running program or deleting file: " + ex.Message);
                }
            }
        


        public void CheckResourceUsage()
        {
            FileWriting fileWriting = new FileWriting();
            try
            {
                var processes = Process.GetProcesses();
                foreach (var process in processes)
                {
                    if (process.Threads.Count > 0)
                    {
                        var cpuUsage = process.TotalProcessorTime.TotalMilliseconds / Environment.ProcessorCount / Stopwatch.Frequency;
                        if (cpuUsage > 0.5)
                        {
                            string resourceusage = "Process: " + process.ProcessName + " is using too much CPU resources.";
                            fileWriting.WriteToFile(resourceusage);
                        }
                        else if (process.WorkingSet64 > 8000000)
                        { //8000000 bytes = 8mb
                            fileWriting.WriteToFile("Process: " + process.ProcessName + " is using too much Memory resources.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                fileWriting.WriteToFile ("Error: Unable to retrieve process data." + ex.Message);
            }
        }
        /* This code snippet uses the Process class in the System.Diagnostics namespace to retrieve a list of all running processes and their resource usage. It checks the total processor time consumed by each process, and if the process is consuming more than 50% of the available CPU resources, it will output the process name in the Output textbox.
        It also checks the memory usage of each process and if the process is using more than 8MB of memory it will output the process name in the Output textbox.
        Please note that this code is checking for CPU and Memory usage only, if you need to monitor other resources you can add them to the code snippet.
        It also assumes that you have a textbox called Output in your form, otherwise you will need to reference the textbox by its name.*/
        public void CheckUptime()
        {
            FileWriting fileWriting = new FileWriting();
            try
            {
                var uptime = new TimeSpan(0, 0, 0, 0, (int)GetTickCount64());
                string uptimestring = "Uptime: " + uptime.Days + " days " + uptime.Hours + " hours " + uptime.Minutes + " minutes " + uptime.Seconds + " seconds";
                fileWriting.WriteToFile(uptimestring);
            }
            catch (Exception ex)
            {
               
            }
        }

        [DllImport("kernel32.dll")]
        private static extern ulong GetTickCount64();

        public void CheckRamUsage()
        {
            FileWriting fileWriting = new FileWriting();
            try
            {
                var performanceCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
                var ramUsagePercentage = performanceCounter.NextValue();
                string ramusage = "RAM Usage: " + ramUsagePercentage + "%";
                fileWriting.WriteToFile(ramusage);
            }
            catch (Exception ex)
            {
                fileWriting.WriteToFile("Error: Unable to retrieve RAM usage data." + ex.Message);
            }
        }


        //You can call this function like this
       // OpenProgram("C:\\Users\\gonca\\OneDrive\\Pictures\\linkedin\\program.exe"); asda



    }   




}
   



/*
            ProcessStartInfo ps = new ProcessStartInfo();
            ps.FileName = "cmd.exe";
            ps.WindowStyle = ProcessWindowStyle.Normal;
            ps.Arguments = @"ping 8.8.8.8";
            Process.Start(ps);
            https://kodify.net/csharp/computer-drive/drive-free-space/
            
            espaço de disco
 */
