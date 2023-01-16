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

            OpenProgram("C:\\Program Files (x86)\\Steam\\steam.exe");



        }

        public void OutputDiskCUsage()
        {
            DriveInfo di = new DriveInfo("C:/");
            double totalSize = di.TotalSize / 1000000000;
            double freeSpace = di.TotalFreeSpace / 1000000000;
            double usedSpace = totalSize - freeSpace;
            Output.Text = "Disk C: Usage: " + usedSpace + " GB out of " + totalSize + " GB";
        }


            public void CheckTemperature()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
                foreach (ManagementObject obj in searcher.Get())
                {
                    string temp = obj["CurrentTemperature"].ToString();
                    Output.Text = "Temperature: " + (Convert.ToInt32(temp) - 2732) + "°C";
                }
            }
            catch (ManagementException)
            {
                Output.Text = "Error: Unable to retrieve temperature data.";
            }
        }

        public void CheckResourceUsage()
        {
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
                            Output.Text = "Process: " + process.ProcessName + " is using too much CPU resources.";
                        }
                        else if (process.WorkingSet64 > 8000000)
                        { //8000000 bytes = 8mb
                            Output.Text = "Process: " + process.ProcessName + " is using too much Memory resources.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Output.Text = "Error: Unable to retrieve process data." + ex.Message;
            }
        }
        /* This code snippet uses the Process class in the System.Diagnostics namespace to retrieve a list of all running processes and their resource usage. It checks the total processor time consumed by each process, and if the process is consuming more than 50% of the available CPU resources, it will output the process name in the Output textbox.
        It also checks the memory usage of each process and if the process is using more than 8MB of memory it will output the process name in the Output textbox.
        Please note that this code is checking for CPU and Memory usage only, if you need to monitor other resources you can add them to the code snippet.
        It also assumes that you have a textbox called Output in your form, otherwise you will need to reference the textbox by its name.*/
        public void CheckUptime()
        {
            try
            {
                var uptime = new TimeSpan(0, 0, 0, 0, (int)GetTickCount64());
                Output.Text = "Uptime: " + uptime.Days + " days " + uptime.Hours + " hours " + uptime.Minutes + " minutes " + uptime.Seconds + " seconds";
            }
            catch (Exception ex)
            {
                Output.Text = "Error: Unable to retrieve uptime data." + ex.Message;
            }
        }

        [DllImport("kernel32.dll")]
        private static extern ulong GetTickCount64();

        public void CheckRamUsage()
        {
            try
            {
                var performanceCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
                var ramUsagePercentage = performanceCounter.NextValue();
                Output.Text = "RAM Usage: " + ramUsagePercentage + "%";
            }
            catch (Exception ex)
            {
                Output.Text = "Error: Unable to retrieve RAM usage data." + ex.Message;
            }
        }
        public void OpenProgram(string directory)
        {
            try
            {
                Process.Start(directory);
                Output.Text = "The program located in: " + directory + " was opened successfully";
            }
            catch (Exception ex)
            {
                Output.Text = "Error: Unable to open the program. " + ex.Message;
            }
        }

        //You can call this function like this
       // OpenProgram("C:\\Users\\gonca\\OneDrive\\Pictures\\linkedin\\program.exe");



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
