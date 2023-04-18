using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace slide_show
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        List<Bitmap> images = new List<Bitmap>();
        int np;
        public Form1()
        {
            InitializeComponent();
            label1.Text = np.ToString() + "/" + images.Count().ToString();
            timer.Interval = 500;
            timer.Tick += nextClick;
            timer1.Tick += changeText;
        }

        private void nextClick(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                return;
            }
            np++;
            if (np >= images.Count)
            {
                np = 0;
            }
            pictureBox1.Image = images[np];
        }

        

        private void button_start_Click(object sender, EventArgs e)
        {
            if(images.Count!= 0)
            {
                timer.Start();
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Папка не выбрана");
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            timer.Stop();
            timer1.Stop();
        }

        private void button_previous_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                return;
            }
            np--;
            if (np < 0)
            {
                np = images.Count - 1;
            }
            pictureBox1.Image = images[np];
            label1.Text = (np + 1).ToString() + "/" + images.Count().ToString();
        }

        private void button_next_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                return;
            }
            np++;
            if (np >= images.Count)
            {
                np = 0;
            }
            pictureBox1.Image = images[np];
            label1.Text = (np+1).ToString() + "/" + images.Count().ToString();
        }

        private void button_open_Click(object sender, EventArgs e)
        {
            button_stop_Click(null, null);
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                timer.Stop();
                if (images.Count != 0)
                {
                    foreach (var item in images)
                    {
                        item.Dispose();
                    }
                    images.Clear();
                }
                DirectoryInfo direct = new DirectoryInfo(folder.SelectedPath);
                IEnumerable<FileInfo> all_file = direct.EnumerateFiles(); // 
                foreach (var item in all_file)
                {
                    Bitmap pi = new Bitmap(item.FullName);
                    Size pi_size = pictureBox1.Size;
                    images.Add(new Bitmap(pi,pi_size));
                }
            }
        }

        private void changeText(object sender, EventArgs e)
        {
            label1.Text = (np + 1).ToString() + "/" + images.Count().ToString();
        }
    }
}
