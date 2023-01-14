using System;
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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process ps = new Process();
            ps.StartInfo.FileName = "powershell.exe";
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            ps.StartInfo.Arguments = "Systeminfo | Select-String KB";
            ps.StartInfo.RedirectStandardOutput = true;
            ps.Start();
            Output.Text = ps.StandardOutput.ReadToEnd();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
