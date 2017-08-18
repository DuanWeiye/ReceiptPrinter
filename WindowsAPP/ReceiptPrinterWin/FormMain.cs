using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ReceiptPrinterWin
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void checkBoxTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTitle.Checked)
            {
                textBoxTitle.Enabled = true;
                checkBoxTitleBold.Enabled = true;
                comboBoxTitlePos.Enabled = true;
                comboBoxTitleSize.Enabled = true;
            }
            else
            {
                textBoxTitle.Text = string.Empty;
                textBoxTitle.Enabled = false;
                checkBoxTitleBold.Enabled = false;
                comboBoxTitlePos.Enabled = false;
                comboBoxTitleSize.Enabled = false;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            dateTimePickerDate.Enabled = false;
            dateTimePickerDate.Value = DateTime.Now;
            dateTimePickerDate.Format = DateTimePickerFormat.Short;
            dateTimePickerDate.CustomFormat = "yyyy/MM/dd";

            dateTimePickerTime.Enabled = false;
            dateTimePickerTime.Value = DateTime.Now;
            dateTimePickerTime.Format = DateTimePickerFormat.Time;
            dateTimePickerTime.ShowUpDown = true;

            radioButtonNow.Checked = true;


            checkBoxTitle.Checked = true;

            textBoxTitle.Text = "标题";
            checkBoxTitleBold.Checked = true;

            comboBoxTitlePos.Items.Clear();
            comboBoxTitlePos.Items.Add("左侧");
            comboBoxTitlePos.Items.Add("中央");
            comboBoxTitlePos.Items.Add("右侧");
            comboBoxTitlePos.SelectedIndex = 1;

            comboBoxTitleSize.Items.Clear();
            comboBoxTitleSize.Items.Add("24");
            comboBoxTitleSize.Items.Add("26");
            comboBoxTitleSize.Items.Add("28");
            comboBoxTitleSize.Items.Add("30");
            comboBoxTitleSize.Items.Add("32");
            comboBoxTitleSize.Items.Add("34");
            comboBoxTitleSize.Items.Add("36");
            comboBoxTitleSize.Items.Add("38");
            comboBoxTitleSize.Items.Add("40");
            comboBoxTitleSize.SelectedIndex = 5;

            checkBoxText.Checked = true;

            textBoxText.Text = "正文";
            checkBoxTextBold.Checked = false;

            comboBoxTextPos.Items.Clear();
            comboBoxTextPos.Items.Add("左侧");
            comboBoxTextPos.Items.Add("中央");
            comboBoxTextPos.Items.Add("右侧");
            comboBoxTextPos.SelectedIndex = 0;

            comboBoxTextSize.Items.Clear();
            comboBoxTextSize.Items.Add("18");
            comboBoxTextSize.Items.Add("20");
            comboBoxTextSize.Items.Add("22");
            comboBoxTextSize.Items.Add("24");
            comboBoxTextSize.Items.Add("26");
            comboBoxTextSize.Items.Add("28");
            comboBoxTextSize.Items.Add("30");
            comboBoxTextSize.Items.Add("32");
            comboBoxTextSize.Items.Add("34");
            comboBoxTextSize.SelectedIndex = 3;


            checkBoxPic.Checked = false;
            radioButtonPicFile.Enabled = false;
            radioButtonPicFile.Checked = true;
            radioButtonPicURL.Enabled = false;
            radioButtonPicURL.Checked = false;
            textBoxPic.Enabled = false;
            textBoxPic.Text = string.Empty;
            textBoxPic.ReadOnly = true;
        }

        private void checkBoxText_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxText.Checked)
            {
                textBoxText.Enabled = true;
                checkBoxTextBold.Enabled = true;
                comboBoxTextPos.Enabled = true;
                comboBoxTextSize.Enabled = true;
            }
            else
            {
                textBoxText.Text = string.Empty;
                textBoxText.Enabled = false;
                checkBoxTextBold.Enabled = false;
                comboBoxTextPos.Enabled = false;
                comboBoxTextSize.Enabled = false;
            }
        }
        private string GenerateReceiptPrinterJobText()
        {
            string ret = string.Empty;
            FileStream fs = null;

            try
            {
                if (checkBoxTitle.Checked)
                {
                    ret += "PrintAlign(" + comboBoxTitlePos.SelectedIndex.ToString() + ")" + Environment.NewLine;
                    ret += "PrintText(u'" + textBoxTitle.Text + "'";
                    ret += ", inputBold=" + checkBoxTitleBold.Checked.ToString();
                    ret += ", inputSize=" + comboBoxTitleSize.SelectedItem.ToString();
                    ret += ", inputFade=False";
                    ret += ")" + Environment.NewLine;
                    ret += "PrintSpace(20)" + Environment.NewLine;
                }

                if (checkBoxText.Checked)
                {
                    ret += "PrintAlign(" + comboBoxTextPos.SelectedIndex.ToString() + ")" + Environment.NewLine;
                    ret += "PrintText(u'" + textBoxText.Text.Replace(Environment.NewLine, "\\n") + "'";
                    ret += ", inputBold=" + checkBoxTextBold.Checked.ToString();
                    ret += ", inputSize=" + comboBoxTextSize.SelectedItem.ToString();
                    ret += ", inputFade=False";
                    ret += ")" + Environment.NewLine;
                }

                if (checkBoxPic.Checked)
                {
                    KeyValuePair<bool, string> uploadRet;
                    string uploadURL = @"<Put your upload pic url here>" + (radioButtonPicFile.Checked ? "FILE" : "URL");

                    ret += "PrintSpace(20)" + Environment.NewLine;
                    ret += "PrintAlign(1)" + Environment.NewLine;

                    if (radioButtonPicFile.Checked)
                    {
                        fs = new FileStream(textBoxPic.Text, FileMode.Open);

                        byte[] imgData = new byte[fs.Length];
                        fs.Read(imgData, 0, imgData.Length);
                        fs.Close();

                        uploadRet = CreatePostHttpResponse(uploadURL, imgData);
                    }
                    else
                    {
                        uploadRet = CreatePostHttpResponse(uploadURL, textBoxPic.Text);
                    }

                    if (uploadRet.Key && !uploadRet.Value.Trim().StartsWith("NG"))
                    {
                        ret += "PrintImage(\"" + uploadRet.Value + "\")" + Environment.NewLine;
                    }
                    else
                    {
                        MessageBox.Show("Picture Upload Failed" + Environment.NewLine + "Response: [" + uploadRet.Value + "]");

                        ret = string.Empty;
                    }
                }
            }
            catch
            {
                ret = string.Empty;

                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }

            return ret;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            buttonOK.Enabled = false;

            string timeStamp = string.Empty;
            if (radioButtonNow.Checked)
            {
                timeStamp = "NOW";
            }
            else
            {
                timeStamp = dateTimePickerDate.Value.ToString("yyyyMMdd") + 
                            dateTimePickerTime.Value.ToString("HHmmss");
            }

            string finalURL = @"<Put your upload url here>" + timeStamp;
            string finalPost = GenerateReceiptPrinterJobText();

            if (!string.IsNullOrWhiteSpace(finalPost))
            {
                KeyValuePair<bool, string> ret = CreatePostHttpResponse(finalURL, finalPost);
                if (ret.Key)
                {
                    MessageBox.Show("Successed" + Environment.NewLine + "Response: [" + ret.Value + "]");
                }
                else
                {
                    MessageBox.Show("Failed" + Environment.NewLine + "Response: [" + ret.Value + "]");
                }
            }

            buttonOK.Enabled = true;
        }

        public static KeyValuePair<bool, string> CreatePostHttpResponse(string url, object paramObject)
        {
            KeyValuePair<bool, string> ret = new KeyValuePair<bool,string>();

            for (int retryCount = 0; retryCount < 10; retryCount++)
            {
                StreamReader sr = null;
                HttpWebResponse response = null;

                try
                {
                    response = CreatePostHttpResponse(url, paramObject, null);

                    sr = new StreamReader(response.GetResponseStream());

                    ret = new KeyValuePair<bool, string>(true, sr.ReadToEnd());

                    break;
                }
                catch (Exception e)
                {
                    ret = new KeyValuePair<bool, string>(false, "Exception: " + e.Message);

                    Thread.Sleep(500);
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();
                        sr = null;
                    }

                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
            }

            return ret;
        }


        public static HttpWebResponse CreatePostHttpResponse(string url, object paramObject, int? timeout)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("CreatePostHttpResponse: URI Missing");
            }

            HttpWebRequest request = null;

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }

            request.Method = "POST";
            request.ContentType = "charset=UTF-8";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value * 1000;
            }
            else
            {
                request.Timeout = 30 * 1000;
            }

            byte[] data = null;

            if (paramObject is string)
            {
                data = Encoding.UTF8.GetBytes(paramObject.ToString());
            }
            else
            {
                data = paramObject as byte[];
            }

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            return request.GetResponse() as HttpWebResponse;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private void buttonPicUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "JPG File|*.jpg";
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPic.Text = openFileDialog1.FileName;
            }
        }

        private void checkBoxPic_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPic.Checked)
            {
                radioButtonPicFile.Enabled = true;
                radioButtonPicURL.Enabled = true;
                textBoxPic.Enabled = true;
                buttonPicUpload.Enabled = true;
            }
            else
            {
                radioButtonPicFile.Enabled = false;
                radioButtonPicURL.Enabled = false;
                textBoxPic.Enabled = false;
                buttonPicUpload.Enabled = false;
            }
        }

        private void radioButtonNow_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerDate.Value = DateTime.Now;
            dateTimePickerTime.Value = DateTime.Now;

            if (radioButtonNow.Checked)
            {
                dateTimePickerDate.Enabled = false;
                dateTimePickerTime.Enabled = false;
            }
            else
            {
                dateTimePickerDate.Enabled = true;
                dateTimePickerTime.Enabled = true;
            }
        }

        private void radioButtonPicFile_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPic.Text = string.Empty;

            if (radioButtonPicFile.Checked)
            {
                buttonPicUpload.Enabled = true;
                textBoxPic.ReadOnly = true;
            }
            else
            {
                buttonPicUpload.Enabled = false;
                textBoxPic.ReadOnly = false;
            }
        }
    }
}
