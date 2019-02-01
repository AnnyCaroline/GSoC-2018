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

            // Read file
            try
            {
                // Use WMI to get info
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_PnPEntity WHERE ClassGuid=\"{4d36e978-e325-11ce-bfc1-08002be10318}\"");

                #if DEBUG
                    string text = System.IO.File.ReadAllText(@"absolute-path-to\arduino-1.8.8\hardware\arduino\avr\boards.txt");
                #else
                    // get path of the executing assembly
                    string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
<<<<<<< HEAD

                    string text = System.IO.File.ReadAllText(currentPath + @"\..\arduino-1.8.8\hardware\arduino\avr\boards.txt");
=======
                    string text = System.IO.File.ReadAllText(currentPath + @"\..\arduino-1.8.3\hardware\arduino\avr\boards.txt");
>>>>>>> 713f1f6cc0d047a0c5c711af70dadde1b55c8f58
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
                            a = regex("[a-zA-Z]+" + Regex.Escape(".") + "vid" + Regex.Escape(".") + "[0-9]=0x" + vid + "((\r\n)|(\n))[a-zA-Z]+" + Regex.Escape(".") + "pid" + Regex.Escape(".") + "[0-9]=0x" + pid, text.ToString());

                            //Get first word from a
                            string b = "";
                            b = regex("[a-zA-Z]+(?=" + Regex.Escape(".") + "vid)", a.ToString());

                            //Get b.name
                            string name = "";
                            name = regex("(?<=" + b + Regex.Escape(".") + "name=).+", text.ToString());

                            //Get b.menu.cpu.*.build.mcu 
                            Regex r = new Regex("(?<=" + b + Regex.Escape(".") + "menu" + Regex.Escape(".") + "cpu" + Regex.Escape(".") + "[0-9a-zA-Z]+" + Regex.Escape(".") + "build" + Regex.Escape(".") + "mcu=).+");

                            string mcu = "";
                            string cpu = "";
                            bool firstFlag = true;
                            foreach (Match match in r.Matches(text.ToString()))
                            {

                                //Get cpu of that mcu
                                string c = "";
                                c = regex(b + Regex.Escape(".") + "menu" + Regex.Escape(".") + "cpu" + Regex.Escape(".") + "[0-9a-zA-Z]+" + Regex.Escape(".") + "build" + Regex.Escape(".") + "mcu=" + match.Value, text.ToString());

                                string[] arrC = c.Split('.');

                                if (firstFlag)
                                    firstFlag = false;
                                else
                                {
                                    mcu += ",";
                                    cpu += ",";
                                }
                                    
                                mcu += match.Value;
                                cpu += arrC[3];
                            }

                            //Add to DataGrid
                            this.dataGridView1.Rows.Add(port, b, name, pid, vid, sn, mcu, cpu);
                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("boards.txt not found: " + ex.ToString());
            }
            catch(System.IO.DirectoryNotFoundException ex)
            {
                MessageBox.Show("boards.txt not found: " + ex.ToString());
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Exception while trying to access serial ports. Check if your account have admin rights: " + ex.ToString());
                return;
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
                Console.WriteLine(txtBoard.Text + " " + txtPort.Text + " " + comboCPU.Text + " " + comboMCU.Text);
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
                this.comboMCU.Items.Clear();
                string cpus = Convert.ToString(selectedRow.Cells["cpu"].Value);
                string mcus = Convert.ToString(selectedRow.Cells["mcu"].Value);

                string[] arrCPUs = cpus.Split(',');
                foreach (string cpu in arrCPUs)
                    comboCPU.Items.Add(cpu);

                string[] arrMCUs = mcus.Split(',');
                foreach (string mcu in arrMCUs)
                    comboMCU.Items.Add(mcu);

                comboCPU.SelectedIndex = 0;
                comboMCU.SelectedIndex = 0;

                //Hide comboCPU if no cpu selection is required
                if (string.IsNullOrEmpty(cpus))
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
            Console.WriteLine("ide");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadBoards();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboCPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMCU.SelectedIndex = comboCPU.SelectedIndex;
        }
    }
}
