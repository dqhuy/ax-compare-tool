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
using Newtonsoft.Json;
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
        private bool isRuningOCR { get; set; }


        private void backgroundWorkerOCR_DoWork(object sender, DoWorkEventArgs e)
        {
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
                string resultFilePath = Path.Combine(resultFolder, "ax-compare.csv");
                if (File.Exists(resultFilePath))
                {
                    File.Delete(resultFilePath);
                }

                StreamWriter streamWriter = new StreamWriter(resultFilePath);
                streamWriter.AutoFlush = true;

                streamWriter.WriteLine(string.Join(",", new string[]{
                    "File",

                    "Total word",
                    "Word Confidence",
                    "Total line",
                    "Line confiden",
                    "Total page",
                    "Page confidence",

                    "Total word 2",
                    "Word Confidence 2",
                    "Total line 2",
                    "Line confiden 2",
                    "Total page 2",
                    "Page confidence 2",

                    "Delta Total word",
                    "Detlta Word Confidence",
                    "Delta Total line",
                    "Delta Line confiden",
                    "Delta Total page",
                    "Delta Page confidence",
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

                    if (backgroundWorkerOCR.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
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

                    //write to debug 
                    System.Diagnostics.Debug.WriteLine("OCR RESULT - " + orginalfileInfo.Name);
                    System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(ocrResults));
                    System.Diagnostics.Debug.WriteLine("OCR RESULT 2 - " + preprocessedFileInfo.Name);
                    System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(ocrResults2));
                    System.Diagnostics.Debug.WriteLine("-------------------------");
                    //end write to debug

                    //output record format
                    // filename, total_word, word_confidence, total_line,line_confidence, total_page, page_confidence
                    long totalWord, totalWord2, deltaWord = 0;
                    double wordConfidence, wordConfidence2, deltaWordConfidence = 0.0;

                    long totalLine, totalLine2, deltaLine = 0;
                    double LineConfidence, LineConfidence2, deltaLineConfidence = 0.0;

                    long totalPage, totalPage2, deltaPage = 0;
                    double PageConfidence, PageConfidence2, deltaPageConfidence = 0.0;

                    aggregateOCRResult(ocrResults, out totalWord, out wordConfidence, out totalLine, out LineConfidence, out totalPage, out PageConfidence);
                    aggregateOCRResult(ocrResults2, out totalWord2, out wordConfidence2, out totalLine2, out LineConfidence2, out totalPage2, out PageConfidence2);

                    deltaWord = totalWord2 - totalWord;
                    deltaWordConfidence = wordConfidence2 - wordConfidence;
                    deltaLine = totalLine2 - totalLine;
                    deltaLineConfidence = LineConfidence2 - LineConfidence;
                    deltaPage = totalPage2 - totalPage;
                    deltaPageConfidence = PageConfidence2 - PageConfidence;

                    string filename = new FileInfo(f).Name;
                    string resultText = string.Join(",", new string[] {
                        filename,

                        totalWord.ToString(),
                        wordConfidence.ToString(),
                        totalLine.ToString(),
                        LineConfidence.ToString(),
                        totalPage.ToString(),
                        PageConfidence.ToString(),

                        totalWord2.ToString(),
                        wordConfidence2.ToString(),
                        totalLine2.ToString(),
                        LineConfidence2.ToString(),
                        totalPage2.ToString(),
                        PageConfidence2.ToString(),

                        deltaWord.ToString(),
                        deltaWordConfidence.ToString(),
                        deltaLine.ToString(),
                        deltaLineConfidence.ToString(),
                        deltaPage.ToString(),
                        deltaPageConfidence.ToString(),
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

        private void aggregateOCRResult(List<OCRResult> ocrResults, out long totalWord, out double wordConfidence, out long totalLine, out double lineConfidence, out long totalPage, out double pageConfidence)
        {
            totalWord = totalLine = totalPage = 0;
            double totalwordConfidence = 0, totalLineConfidence = 0, totalPageConfidence = 0;
            wordConfidence = lineConfidence = pageConfidence = 1;
            foreach (var p in ocrResults.Where(x => x.LineInfos != null))
            {
                totalLine += p.LineInfos.Count();
                totalLineConfidence += p.LineInfos.Sum(x => x.LineConfidence);

                foreach (var l in p.LineInfos.Where(x => x.WordConfidences != null))
                {
                    totalwordConfidence += l.WordConfidences.Sum(x => x);
                    totalWord += l.WordConfidences.Count();


                }

                double tempPageConfidence = p.LineInfos.Sum(x => x.LineConfidence);
                totalPageConfidence += tempPageConfidence / p.LineInfos.Count();
            }

            totalPage = ocrResults.Count();
            if (totalWord > 0)
            {
                wordConfidence = totalwordConfidence / totalWord;
            }
            else
            {
                wordConfidence = 0;
            }
            if (totalLine > 0)
            {
                lineConfidence = totalLineConfidence / totalLine;
            }
            else
            {
                lineConfidence = 0;
            }
            if (totalPage > 0)
            {
                pageConfidence = totalPageConfidence / totalPage;
            }
            else
            {
                pageConfidence = 0;
            }
        }

        private void backgroundWorkerOCR_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int totalprocessed = int.Parse(processedData[0]);
            DateTime endTime = DateTime.Now;
            Double elapsed = ((TimeSpan)(endTime - startTime)).TotalSeconds;
            double speed = elapsed / totalprocessed;

            progressBarOCR.Value = e.ProgressPercentage;
            lbStatus.Text = processedData[0] + " / " + processedData[1] + "- File: " + processedData[2];
            if (totalprocessed > 1)
            {
                lbStatus.Text += " - Everage time(s): " + String.Format("{0:0.0}", speed);
            }
            else
            {
                lbStatus.Text += " - Everage time(s): NaN";
            }
        }

        private void backgroundWorkerOCR_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            if (e.Cancelled)
            {
                lbStatus.Text = lbStatus.Text + " - CANCELED ";
            }
            else
            {
                lbStatus.Text = lbStatus.Text + " - DONE ";
            }

            isRuningOCR = false;
            btnRunOCR.Text = "Run";
            btnRunOCR.Enabled = true;
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


            if (!isRuningOCR) // start runing ocr
            {
                if (backgroundWorkerOCR.IsBusy)
                {
                    MessageBox.Show("Background worker is busy. Please wait and try again later");
                    return;
                }

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
                btnRunOCR.Text = "Cancel";
                startTime = DateTime.Now;
                lbStatus.Text = string.Empty;
                progressBarOCR.Value = 0;

                isRuningOCR = !isRuningOCR;


                backgroundWorkerOCR.WorkerReportsProgress = true;
                backgroundWorkerOCR.WorkerSupportsCancellation = true;
                backgroundWorkerOCR.RunWorkerAsync();
            }
            else // stop runing ocr
            {
                backgroundWorkerOCR.CancelAsync();
                btnRunOCR.Text = "Canceling";
                btnRunOCR.Enabled = false;
                isRuningOCR = !isRuningOCR;
            }
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
            txtOCRServer.Text = string.Empty;
            txtImageFolder.Text = "D:\\Google-drive-huy-work\\imagedata\\skew\\train\\images";
            txtPreprocessedImageFolder.Text = "D:\\Google-drive-huy-work\\imagedata\\skew\\train\\images\\out";
            txtOutputFolder.Text = "D:\\Google-drive-huy-work\\imagedata\\skew\\train\\ocr-compare";

            //txtOCRServer.Text = "192.168.6.90";
            //txtImageFolder.Text = "\\\\huydq-pc\\imagedata\\skew\\train\\images";
            //txtPreprocessedImageFolder.Text = "\\\\huydq-pc\\imagedata\\skew\\train\\images\\out";
            //txtOutputFolder.Text = "\\\\huydq-pc\\imagedata\\skew\\train\\ocr-compare";


        }

    }
}
