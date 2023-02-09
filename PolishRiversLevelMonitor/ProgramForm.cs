using System.Runtime.CompilerServices;

namespace WinFormsApp1
{
    public partial class ProgramForm : Form
    {
        public ProgramForm()
        {
            InitializeComponent();

        }

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

                    // Sort data by river name
                    Program.stationsStatus = Program.stationsStatus.OrderBy(x => x.stacja).ToList();

                    foreach (StationsData station in Program.stationsStatus)
                        if (station.stacja is not null && !this.comboBox2.Items.Contains(station.stacja))
                        {
                            this.comboBox2.Items.Add(station.stacja);
                        }

                }

                comboBox2.SelectedItem = 0;
            }
        }

        private async void ProgramForm_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "Data downloading...";

            // Download data from internet
            await Program.DownloadData();

            if (Program.downloadComplete)
            {
                this.toolStripStatusLabel1.Text = "OK. Data downloaded.";

                this.comboBox1.Items.Clear();
                if (Program.riversStatus is not null)
                {
                    // Add first item as operation sugestion for user
                    this.comboBox1.Items.Add("-- Select River --");

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

        

        private async void button3_Click(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "Data downloading...";

            // Download data from internet
            await Program.DownloadData();

            if (Program.downloadComplete)
            {
                this.toolStripStatusLabel1.Text = "OK. Data downloaded.";

                this.comboBox1.Items.Clear();
                if (Program.riversStatus is not null)
                {
                    // Add first item as operation sugestion for user
                    this.comboBox1.Items.Add("-- Select River --");

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
    }
}