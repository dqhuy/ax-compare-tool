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
using AX.AXSDK;
using OneAPI;

namespace ax_tool
{
    public partial class OCRFullText : Form
    {
        public OCRFullText()
        {
            InitializeComponent();
        }

        private string[] processedData = new string[3] { string.Empty, string.Empty, string.Empty };
        private DateTime startTime;
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorkerOCR_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorkerOCR.WorkerReportsProgress = true;
            runOCR(e);
        }

        /// <summary>
        /// Do OCR and compare result quality of orginal image vs pre-processed image
        /// Then write result to cvs file to output folder
        /// </summary>
        private void runOCR(DoWorkEventArgs e)
        {

            try
            {
                string imageFolder = txtImageFolder.Text;
                string preprocessedImageFolder = txtPreprocessedImageFolder.Text;
                string resultFolder = txtOutputFolder.Text;

                //loop all file => run ocr then compare result
                string resultFilePath = Path.Combine(resultFolder, "ocr-result.csv");
                if (File.Exists(resultFilePath))
                {
                    File.Delete(resultFilePath);
                }

                StreamWriter streamWriter = new StreamWriter(resultFilePath);
                streamWriter.AutoFlush = true;

                streamWriter.WriteLine(string.Join(",", new string[]{
                    "File",
                    "Total_word",
                    "Average_Confidence",
                    "Total_word_2",
                    "Average_Confidence_2",
                    "Delta_word",
                    "Delta_Confidence"
                }));

                int totalFile = Directory.EnumerateFiles(imageFolder).Count();
                int fileIndex = 0;
                if (totalFile == 0)
                {
                    MessageBox.Show("There is no image in folder: " + imageFolder);
                    return;
                }
                foreach (var f in Directory.EnumerateFiles(imageFolder).
                    Where(i => i.EndsWith(".jpg") ||
                    i.EndsWith(".png") ||
                    i.EndsWith("pdf")))
                {
                    FileInfo orginalfileInfo = new FileInfo(f);
                    FileInfo preprocessedFileInfo = new FileInfo(Path.Combine(preprocessedImageFolder, orginalfileInfo.Name));


                    fileIndex++;
                    int progressValue = 100 * fileIndex / totalFile;
                    processedData[0] = fileIndex.ToString();
                    processedData[1] = totalFile.ToString();
                    processedData[2] = orginalfileInfo.Name;
                    backgroundWorkerOCR.ReportProgress(progressValue);



                    //check pre-processed file exist by name
                    //if not exist preprocessed file then do nothing
                    if (!File.Exists(preprocessedFileInfo.FullName))
                    {
                        continue;
                    }



                    List<OCRResult> ocrResults = APIs.API.RecognizeVBHC(orginalfileInfo.FullName);
                    List<OCRResult> ocrResults2 = APIs.API.RecognizeVBHC(preprocessedFileInfo.FullName);

                    //output record format
                    // filename, totalword, averageConfidence
                    long totalWord, totalWord2, deltaWord = 0;
                    double averageConfi, averageConfi2, deltaConfi = 0.0;

                    averageOCRResult(ocrResults, out totalWord, out averageConfi);
                    averageOCRResult(ocrResults2, out totalWord2, out averageConfi2);

                    deltaWord = totalWord2 - totalWord;
                    deltaConfi = averageConfi2 - averageConfi;

                    string filename = new FileInfo(f).Name;
                    string resultText = string.Join(",", new string[] {
                        filename,
                        totalWord.ToString(),
                        averageConfi.ToString(),
                        totalWord2.ToString(),
                        averageConfi2.ToString(),
                        deltaWord.ToString(),
                        deltaConfi.ToString()
                    });

                    streamWriter.WriteLine(resultText);
                }
                //close file
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                e.Cancel = true;
            }
        }

        private void averageOCRResult(List<OCRResult> ocrResults, out long totalWord, out double averageConfidence)
        {
            totalWord = 0;
            averageConfidence = 0;
            double totalConfi = 0;

            foreach (var r in ocrResults)
            {
                if (r.WordInfos.Count() > 0)
                {
                    totalWord += r.WordInfos.Count();
                    totalConfi += r.WordInfos.Sum(x => x.Confidence);
                }

            }
            if (totalWord != 0)
            {
                averageConfidence = totalConfi / totalWord;
            }
        }

        private void backgroundWorkerOCR_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int totalprocessed = int.Parse(processedData[0]);
            DateTime endTime = DateTime.Now;
            Double elapsed = ((TimeSpan)(endTime - startTime)).TotalSeconds;
            double speed = elapsed/totalprocessed;

            progressBarOCR.Value = e.ProgressPercentage;
            lbStatus.Text = processedData[0] + " / " + processedData[1] + "- File: " + processedData[2];
            if (totalprocessed > 1)
            {
                lbStatus.Text += " - Everage time(s): "+ String.Format("{0:0.0}", speed);
            }
            else
            {
                lbStatus.Text += " - Everage time(s): NaN";
            }
        }

        private void backgroundWorkerOCR_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lbStatus.Text = lbStatus.Text + " DONE ";
            btnRunOCR.Enabled = true; ;
        }

        private void btnOpenImageFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtImageFolder.Text = dlg.SelectedPath;
            }
        }

        private void btnOpenOutputFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtOutputFolder.Text = dlg.SelectedPath;
            }
        }

        private void btnRunOCR_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(txtImageFolder.Text))
            {
                MessageBox.Show("Image folder is not exist!");
                return;
            }
            if (!System.IO.Directory.Exists(txtOutputFolder.Text))
            {
                MessageBox.Show("Output folder is not exist!");
                return;
            }

            if (!string.IsNullOrEmpty(txtOCRServer.Text))
            {
                APIs.SetServerAddress("192.168.6.90");
            }
            //Everything is ready
            lbStatus.Text = string.Empty;
            progressBarOCR.Value = 0;
            btnRunOCR.Enabled = false;
            startTime = DateTime.Now;

            backgroundWorkerOCR.RunWorkerAsync();
        }

        private void btnSelectPreprocessedFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtPreprocessedImageFolder.Text = dlg.SelectedPath;
            }
        }

        private void OCRFullText_Load(object sender, EventArgs e)
        {
            setDefaultValeForDebug();
        }

        private void setDefaultValeForDebug()
        {
            txtOCRServer.Text = "192.168.6.90";
            txtImageFolder.Text = "D:\\Google-drive-huy-work\\imagedata\\skew\\train\\images";
            txtPreprocessedImageFolder.Text = "D:\\Google-drive-huy-work\\imagedata\\skew\\train\\images\\out";
            txtOutputFolder.Text = "D:\\Google-drive-huy-work\\imagedata\\skew\\train\\ocr-compare";

            txtOCRServer.Text = string.Empty;// "192.168.6.90";
            txtImageFolder.Text = "\\\\huydq-pc\\imagedata\\skew\\train\\images";
            txtPreprocessedImageFolder.Text = "\\\\huydq-pc\\imagedata\\skew\\train\\images\\out";
            txtOutputFolder.Text = "\\\\huydq-pc\\imagedata\\skew\\train\\ocr-compare";


        }
    }
}
