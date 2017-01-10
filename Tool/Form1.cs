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

namespace Tool
{
    public partial class MainWindow : Form
    {
        private List<Tuple<Config.EdgeDetectionMethodEnum, string>> edgeOptions;
        private List<Tuple<Config.MergeStrategyEnum, string>> mergeOptions;

        public MainWindow()
        {
            InitializeComponent();
            bRun.Enabled = false;
            bSave.Enabled = false;

            edgeOptions = new List<Tuple<Config.EdgeDetectionMethodEnum, string>>();
            foreach (Config.EdgeDetectionMethodEnum value in Enum.GetValues(typeof(Config.EdgeDetectionMethodEnum)))
            {
                edgeOptions.Add(new Tuple<Config.EdgeDetectionMethodEnum, string>(value, value.ToString()));
            }

            cbEdgeDetector.DataSource = edgeOptions;
                
            cbEdgeDetector.ValueMember = "Item1";
            cbEdgeDetector.DisplayMember = "Item2";
            cbEdgeDetector.SelectedIndex = 0;

            mergeOptions = new List<Tuple<Config.MergeStrategyEnum, string>>();
            foreach (Config.MergeStrategyEnum value in Enum.GetValues(typeof(Config.MergeStrategyEnum)))
            {
                mergeOptions.Add(new Tuple<Config.MergeStrategyEnum, string>(value, value.ToString()));
            }

            cbMergeStrategy.DataSource = mergeOptions;

            cbMergeStrategy.ValueMember = "Item1";
            cbMergeStrategy.DisplayMember = "Item2";
            cbMergeStrategy.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    clbInputImagesList.Items.Clear();
                    var images = Directory.EnumerateFiles(fbd.SelectedPath, "*.png");
                    foreach (var img in images)
                    {
                        clbInputImagesList.Items.Add(img, CheckState.Checked);
                    }

                    bRun.Enabled = clbInputImagesList.CheckedItems.Count > 1;
                }
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            var input = e.Argument as Tuple<List<string>, Config>;
            var inputImages = input.Item1;
            var config      = input.Item2;

            FocusStacking fs = new FocusStacking();
            fs.SetReporter(bw.ReportProgress);
            fs.setConfig(config);

            double steps = inputImages.Count;
            double step = 0;
            foreach (var img in inputImages)
            {
                fs.AddImage(new Bitmap(img));
                step += 1.0;
                bw.ReportProgress((int)(step * 100.0 / steps));
            }

            fs.ValidateImages();
            fs.Process();

            e.Result = new Tuple<Bitmap, Bitmap> (fs.Result, fs.DepthMap);
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bRun.Enabled = true;
            progressBar.Value = 0;

            Exception ex = e.Error;

            if (ex != null)
            {
                lOutput.Text = "Error: " + ex.ToString();
                return;
            }

            if (e.Cancelled)
            {
                lOutput.Text = "Processing cancelled by user.";
                return;
            }

            if (e.Result != null)
            {
                lOutput.Text = "Processing done.";
                var results = e.Result as Tuple<Bitmap, Bitmap>;
                pbOutput.Image = results.Item1;
                pbDepth.Image = results.Item2;
                bSave.Enabled = true;
            }
        }

        private Config getConfig()
        {
            Config config = new Config();

            config.DepthThreshold = decimal.ToInt32(nudDepth.Value);
            config.EdgeThreshold  = decimal.ToInt32(nudEdge.Value);

            config.EdgeDetMethod = (Config.EdgeDetectionMethodEnum)cbEdgeDetector.SelectedValue;
            config.MergeStrategy = (Config.MergeStrategyEnum)cbMergeStrategy.SelectedValue;

            return config;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (bgWorker.IsBusy)
            {
                //bgWorker.CancelAsync();
            }
            else if (bRun.Enabled)
            {
                bRun.Enabled = false;
                bSave.Enabled = false;
                lOutput.Text = "";
                var inputImages = clbInputImagesList.CheckedItems.Cast<string>().ToList();
                var cfg = getConfig();
                Tuple<List<string>, Config> input = new Tuple<List<string>, Config>(inputImages, cfg);
                bgWorker.RunWorkerAsync(input);
            }
        }

        private string MakeImageDescription(Image img)
        {
            StringBuilder sb = new StringBuilder("Image format: ");
            sb.Append(img.PixelFormat);
            sb.AppendFormat(" Width: {0} Height: {1}", img.Width, img.Height);
            return sb.ToString();
        }

        private void clbInputImagesList_SelectedValueChanged(object sender, EventArgs e)
        {
            var clb = sender as CheckedListBox;
            
            try
            {
                int i = clb.SelectedIndex;
                var msg = clb.GetItemText(clb.Items[i]);
                pbPreview.Image = Image.FromFile(msg);

                lPreview.Text = MakeImageDescription(pbPreview.Image);
            }
            catch (Exception ex)
            {
                lPreview.Text = "Failed to open image - " + ex.ToString();
            }
        }

        private void clbInputImagesList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var clb = sender as CheckedListBox;

            var count = clb.CheckedItems.Count;

            if (e.NewValue == CheckState.Checked && count >= 1)
            {
                bRun.Enabled = true;
            }
            else if (e.NewValue == CheckState.Unchecked && count <= 2)
            {
                bRun.Enabled = false;
            }
        }

        private void pbDepth_Click(object sender, EventArgs e)
        {
            if (pbDepth.Image != null)
            {
                DepthViewWindow wnd = new DepthViewWindow(pbDepth.Image);
                wnd.ShowDialog();
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".png";
            sfd.Filter = "PNG file|*.png";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var name = sfd.FileName;
                pbOutput.Image.Save(name, System.Drawing.Imaging.ImageFormat.Png);
                var name2 = Path.GetDirectoryName(name) + "\\" + Path.GetFileNameWithoutExtension(name);
                name2 += "_depth.png";
                pbDepth.Image.Save(name2, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
