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

            Che


        }

        public void OutputDiskCUsage()
        {
            DriveInfo di = new DriveInfo("C:/");
            double totalSize = di.TotalSize / 1000000000;
            double freeSpace = di.TotalFreeSpace / 1000000000;
            double usedSpace = totalSize - freeSpace;
            Output.Text = "Disk C: Usage: " + usedSpace + " GB out of " + totalSize + " GB";
        }



        public void checkupdates()
        {
            Process ps = new Process();
            ps.StartInfo.FileName = "powershell.exe";
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ps.StartInfo.Arguments = "Systeminfo | Select-String [01KB";
            ps.StartInfo.RedirectStandardOutput = true;
            ps.Start();
            Output.Text = ps.StandardOutput.ReadToEnd();
        }

        public void CheckCPUTemp()
        {
            // Get the temperature reported by the CPU
            float temp = System.Convert.ToSingle(System.Diagnostics.PerformanceCounter("Processor Information", "Processor Temperature", true).NextValue());

            // Write the temperature to the Output textbox
            Output.Text = "CPU Temperature: " + temp.ToString() + "°C";
        }
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
