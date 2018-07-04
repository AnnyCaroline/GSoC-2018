using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Management;
using System.Reflection;

namespace SelectDevice
{
    public partial class Form1 : Form
    {

        private int selectedrowindex = -1;
        private int counter = 0;

        public Form1()
        {
            InitializeComponent();
        }

        // Helper function to handle regex search
        private static string regex(string pattern, string text)
        {
            Regex re = new Regex(pattern);
            Match m = re.Match(text);
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return null;
            }
        }

        private void loadBoards()
        {
            string port = "", vid = "", pid = "", sn = "";

            // Use WMI to get info
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\"");

            // Read file
            try
            {
                #if DEBUG
                    string text = System.IO.File.ReadAllText(@"C:\Users\Anny\Desktop\ceu-maker\ceu-maker\arduino-1.8.3\hardware\arduino\avr\boards.txt");
                #else
                    // get path of the executing assembly
                    string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    string text = System.IO.File.ReadAllText(currentPath + @"\..\arduino-1.8.3\hardware\arduino\avr\boards.txt");
                #endif

                this.dataGridView1.Rows.Clear();
                this.txtBoard.Clear();
                this.txtPort.Clear();
                this.comboCPU.Items.Clear();

                // Search all serial ports
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    // Parse the data
                    if (null != queryObj["Name"])
                    {
                        //https://stackoverflow.com/questions/24135006/regex-that-match-any-character-inside-a-parenthesis
                        port = regex("(?<=" + Regex.Escape("(") + ")[^)]*(?=" + Regex.Escape(")") + ")", queryObj["Name"].ToString());
                    }

                    if (null != queryObj["PNPDeviceID"])
                    {
                        //https://stackoverflow.com/questions/3926451/how-to-match-but-not-capture-part-of-a-regex?rq=1
                        vid = regex("(?<=VID_)([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString());
                        pid = regex("(?<=PID_)([0-9a-fA-F]+)", queryObj["PNPDeviceID"].ToString());
                        sn = regex("([0-9a-fA-F]{5,})", queryObj["PNPDeviceID"].ToString());

                        if (!string.IsNullOrEmpty(port) && !string.IsNullOrEmpty(vid) && !string.IsNullOrEmpty(pid) && !string.IsNullOrEmpty(sn))
                        {
                            counter++;

                            //Get the VID and PID lines
                            string a = "";
                            a = regex("[a-zA-Z]+" + Regex.Escape(".") + "vid" + Regex.Escape(".") + "[0-9]=0x" + vid + "(\r)(\n)[a-zA-Z]+" + Regex.Escape(".") + "pid" + Regex.Escape(".") + "[0-9]=0x" + pid, text.ToString());

                            //Get first word from a
                            string b = "";
                            b = regex("[a-zA-Z]+(?=" + Regex.Escape(".") + "vid)", a.ToString());

                            //Get b.name
                            string name = "";
                            name = regex("(?<=" + b + Regex.Escape(".") + "name=).+", text.ToString());

                            //Get b.menu.cpu.*.build.mcu 
                            Regex r = new Regex("(?<=" + b + Regex.Escape(".") + "menu" + Regex.Escape(".") + "cpu" + Regex.Escape(".") + "[0-9a-zA-Z]+" + Regex.Escape(".") + "build" + Regex.Escape(".") + "mcu=).+");

                            string mcu = "";
                            bool firstFlag = true;
                            foreach (Match match in r.Matches(text.ToString()))
                            {
                                if (firstFlag)
                                    firstFlag = false;
                                else
                                    mcu += ",";

                                mcu += match.Value;
                            }

                            //Add to DataGrid
                            this.dataGridView1.Rows.Add(port, b, name, pid, vid, sn, mcu);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error: Cannot open boards.txt");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadBoards();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPort.Text=="" || txtBoard.Text=="" || selectedrowindex == -1)
            {
                MessageBox.Show("Select a device in the above list");
            }else
            {
                Console.WriteLine(txtBoard.Text + " " + txtPort.Text + " " + comboCPU.Text);
                this.Close();
            }
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try{
                //https://stackoverflow.com/questions/7657137/datagridview-full-row-selection-but-get-single-cell-value
                this.selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                txtPort.Text = Convert.ToString(selectedRow.Cells["port"].Value);
                txtBoard.Text = Convert.ToString(selectedRow.Cells["board"].Value);

                this.comboCPU.Items.Clear();
                string mcus = Convert.ToString(selectedRow.Cells["mcu"].Value);
                string[] arr = mcus.Split(',');
                foreach (string mcu in arr)
                    comboCPU.Items.Add(mcu);
                comboCPU.SelectedIndex = 0;

                //Hide comboCPU if no cpu selection is required
                if (string.IsNullOrEmpty(mcus))
                {
                    comboCPU.Visible = false;
                    lblCpu.Visible = false;
                }
                else
                {
                    comboCPU.Visible = true;
                    lblCpu.Visible = true;
                }
            }
            catch
            {

            }
        }

        private void btnIDE_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadBoards();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void comboCPU_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
