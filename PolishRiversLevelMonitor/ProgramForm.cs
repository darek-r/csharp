using System.Runtime.CompilerServices;

namespace WinFormsApp1
{
    public partial class ProgramForm : Form
    {
        public ProgramForm()
        {
            InitializeComponent();

        }

        // Select River comboBox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.riversStatus is not null)
            {
                if(Program.stationsStatus is null) 
                { 
                    Program.stationsStatus = new List<StationsData>(); 
                }

                Program.stationsStatus.Clear();

                foreach (StationsData stationStatus in Program.riversStatus)
                    if (stationStatus.rzeka.Equals(comboBox1.GetItemText(comboBox1.SelectedItem)))
                        Program.stationsStatus.Add(stationStatus);



                this.comboBox2.Items.Clear();
                if (Program.stationsStatus is not null)
                {
                    // Add first item as operation sugestion for user
                    this.comboBox2.Items.Add("-- Select Station --");
                    this.comboBox2.SelectedItem = this.comboBox2.Items[0];

                    // Sort data by river name
                    Program.stationsStatus = Program.stationsStatus.OrderBy(x => x.stacja).ToList();

                    foreach (StationsData station in Program.stationsStatus)
                        if (station.stacja is not null && !this.comboBox2.Items.Contains(station.stacja))
                        {
                            this.comboBox2.Items.Add(station.stacja);
                        }

                }

                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";


            }
        }

        private async void ProgramForm_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "Data downloading...";

            // Download data from internet
            await Program.DownloadData();

            // Read data from file
            Program.ReadFromFileData();

            if (Program.downloadComplete)
            {
                this.toolStripStatusLabel1.Text = "OK. Data downloaded.";

                this.comboBox1.Items.Clear();
                if (Program.riversStatus is not null)
                {
                    // Add first item as operation sugestion for user
                    this.comboBox1.Items.Add("-- Select River --");
                    this.comboBox1.SelectedItem = this.comboBox1.Items[0];

                    // Sort data by river name
                    Program.riversStatus = Program.riversStatus.OrderBy(x => x.rzeka).ToList();

                    foreach (StationsData river in Program.riversStatus)
                        if (river.rzeka is not null && !this.comboBox1.Items.Contains(river.rzeka) )
                        {
                            this.comboBox1.Items.Add(river.rzeka);
                        }

                }
            }
            else
            {
                this.toolStripStatusLabel1.Text = "Data unavailable. Check internet connection.";
            }
        }

        
        // Refresh Data button
        private async void button3_Click(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "Data downloading...";

            // Re-Download data from internet
            await Program.DownloadData();

            if (Program.downloadComplete)
            {
                this.toolStripStatusLabel1.Text = "OK. Data downloaded.";

                this.comboBox1.Items.Clear();
                if (Program.riversStatus is not null)
                {
                    // Add first item as operation sugestion for user
                    this.comboBox1.Items.Add("-- Select River --");
                    this.comboBox1.SelectedItem = this.comboBox1.Items[0];

                    // Sort data by river name
                    Program.riversStatus = Program.riversStatus.OrderBy(x => x.rzeka).ToList();

                    foreach (StationsData river in Program.riversStatus)
                        if (river.rzeka is not null && !this.comboBox1.Items.Contains(river.rzeka))
                        {
                            this.comboBox1.Items.Add(river.rzeka);
                        }

                }
            }
            else
            {
                this.toolStripStatusLabel1.Text = "Data unavailable. Check internet connection.";
            }
        }

        // Exit button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0) this.textBox1.Text = "";

            if (Program.stationsStatus is not null
                && Program.stationsWarningData is not null
                && this.comboBox2.SelectedIndex > 0
                && this.comboBox2.SelectedIndex-1 < Program.stationsStatus.Count
                && Program.stationsStatus[this.comboBox2.SelectedIndex-1] is not null)
            {
                // Water height
                this.textBox1.Text = Program.stationsStatus[this.comboBox2.SelectedIndex - 1].stan_wody == null ? " - cm" : Program.stationsStatus[this.comboBox2.SelectedIndex - 1].stan_wody + " cm";
                // Water temperature
                this.textBox2.Text = Program.stationsStatus[this.comboBox2.SelectedIndex - 1].temperatura_wody == null ? " - C" : Program.stationsStatus[this.comboBox2.SelectedIndex - 1].temperatura_wody + " C";
                // Date
                this.textBox3.Text = Program.stationsStatus[this.comboBox2.SelectedIndex - 1].stan_wody_data_pomiaru == null ? " - " : Program.stationsStatus[this.comboBox2.SelectedIndex - 1].stan_wody_data_pomiaru;
                
                // Monitor and Danger stages
                string tx1 = "";
                string tx2 = "";
                foreach (StationsWarningData sdw in Program.stationsWarningData)
                {
                    if(sdw.id_stacji is not null && sdw.id_stacji == Program.stationsStatus[this.comboBox2.SelectedIndex - 1].id_stacji)
                    {
                        tx1 = sdw.warning is null ? "" : sdw.warning + " cm";
                        tx2 = sdw.alarm is null ? "" : sdw.alarm + " cm";
                        break;
                    }
                }

                if (tx1 == "nul cm") tx1 = "";
                if (tx2 == "nul cm") tx2 = "";
                this.textBox4.Text = tx1;
                this.textBox5.Text = tx2;
            }
                
        }
    }
}