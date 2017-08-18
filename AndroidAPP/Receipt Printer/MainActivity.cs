using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReceiptPrinter
{
    [Activity(Label = "Receipt Printer", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private static bool isFinished = false;
        private static DateTime targetTime = DateTime.Now;

        private static readonly string[] _TextAlign = new string[] { "左侧", "中央", "右侧" };
        private static readonly string[] _TextSize = new string[] { "18", "20", "22", "24", "26", "28", "30", "32", "34", "36", "38", "40" };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            // Bind Function 
            RadioButton radioButtonNow = FindViewById<RadioButton>(Resource.Id.radioButtonNow);
            radioButtonNow.CheckedChange += RadioDateTimeChanged;

            RadioButton radioButtonReserve = FindViewById<RadioButton>(Resource.Id.radioButtonReserve);

            TextView textboxDate = FindViewById<TextView>(Resource.Id.textViewDate);
            textboxDate.Text = targetTime.ToString("yyyy/MM/dd");
            textboxDate.Click += OnClickDateEditText;

            TextView textboxTime = FindViewById<TextView>(Resource.Id.textViewTime);
            textboxTime.Text = targetTime.ToString("HH:mm");
            textboxTime.Click += OnClickTimeEditText;

            EditText textboxTitle = FindViewById<EditText>(Resource.Id.editTextTitle);
            textboxTitle.SetSingleLine();

            EditText textboxText = FindViewById<EditText>(Resource.Id.editTextText);

            CheckBox checkBoxTitle = FindViewById<CheckBox>(Resource.Id.checkBoxTitle);
            checkBoxTitle.CheckedChange += checkBoxCheckedTitle;
            checkBoxTitle.Checked = true;

            CheckBox checkBoxTitleBold = FindViewById<CheckBox>(Resource.Id.checkBoxTitleBold);
            checkBoxTitleBold.Checked = true;

            Spinner spinnerTitleAlign = FindViewById<Spinner>(Resource.Id.spinnerTitleAlign);
            ArrayAdapter adapterAlign = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, _TextAlign);
            spinnerTitleAlign.Adapter = adapterAlign;

            Spinner spinnerTitleSize = FindViewById<Spinner>(Resource.Id.spinnerTitleSize);
            ArrayAdapter adapterSize = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, _TextSize);
            spinnerTitleSize.Adapter = adapterSize;

            CheckBox checkBoxText = FindViewById<CheckBox>(Resource.Id.checkBoxText);
            checkBoxText.CheckedChange += checkBoxCheckedText;
            checkBoxText.Checked = true;

            CheckBox checkBoxTextBold = FindViewById<CheckBox>(Resource.Id.checkBoxTextBold);
            checkBoxTextBold.Checked = false;

            Spinner spinnerTextAlign = FindViewById<Spinner>(Resource.Id.spinnerTextAlign);
            spinnerTextAlign.Adapter = adapterAlign;

            Spinner spinnerTextSize = FindViewById<Spinner>(Resource.Id.spinnerTextSize);
            spinnerTextSize.Adapter = adapterSize;

            Button buttonSend = FindViewById<Button>(Resource.Id.buttonSend);
            buttonSend.Click += Send;

            Button buttonReset = FindViewById<Button>(Resource.Id.buttonReset);
            buttonReset.Click += InitUI;


            InitUI(null, null);

            isFinished = true;
        }

        private void InitUI(object sender, EventArgs e)
        {
            RadioButton radioButtonNow = FindViewById<RadioButton>(Resource.Id.radioButtonNow);
            RadioButton radioButtonReserve = FindViewById<RadioButton>(Resource.Id.radioButtonReserve);
            radioButtonReserve.Checked = true;
            radioButtonNow.Checked = true;

            DateTime targetTime = DateTime.Now;

            TextView textboxDate = FindViewById<TextView>(Resource.Id.textViewDate);
            textboxDate.Text = targetTime.ToString("yyyy/MM/dd");

            TextView textboxTime = FindViewById<TextView>(Resource.Id.textViewTime);
            textboxTime.Text = targetTime.ToString("HH:mm");

            EditText textboxTitle = FindViewById<EditText>(Resource.Id.editTextTitle);
            textboxTitle.Text = "标题";

            EditText textboxText = FindViewById<EditText>(Resource.Id.editTextText);
            textboxText.Text = "正文";

            CheckBox checkBoxTitle = FindViewById<CheckBox>(Resource.Id.checkBoxTitle);
            checkBoxTitle.Checked = true;

            CheckBox checkBoxTitleBold = FindViewById<CheckBox>(Resource.Id.checkBoxTitleBold);
            checkBoxTitleBold.Checked = true;

            Spinner spinnerTitleAlign = FindViewById<Spinner>(Resource.Id.spinnerTitleAlign);
            spinnerTitleAlign.SetSelection(1);

            Spinner spinnerTitleSize = FindViewById<Spinner>(Resource.Id.spinnerTitleSize);
            spinnerTitleSize.SetSelection(9);

            CheckBox checkBoxText = FindViewById<CheckBox>(Resource.Id.checkBoxText);
            checkBoxText.Checked = true;

            CheckBox checkBoxTextBold = FindViewById<CheckBox>(Resource.Id.checkBoxTextBold);
            checkBoxTextBold.Checked = false;

            Spinner spinnerTextAlign = FindViewById<Spinner>(Resource.Id.spinnerTextAlign);
            spinnerTextAlign.SetSelection(0);

            Spinner spinnerTextSize = FindViewById<Spinner>(Resource.Id.spinnerTextSize);
            spinnerTextSize.SetSelection(3);
        }

        private void RadioDateTimeChanged(object sender, EventArgs e)
        {
            RadioButton radioButtonNow = FindViewById<RadioButton>(Resource.Id.radioButtonNow);
            TextView textboxDate = FindViewById<TextView>(Resource.Id.textViewDate);
            TextView textboxTime = FindViewById<TextView>(Resource.Id.textViewTime);

            if (radioButtonNow.Checked)
            {
                textboxDate.Enabled = false;
                textboxTime.Enabled = false;
            }
            else
            {
                DateTime targetTime = DateTime.Now;

                textboxDate.Enabled = true;
                textboxDate.Text = targetTime.ToString("yyyy/MM/dd");

                textboxTime.Enabled = true;
                textboxTime.Text = targetTime.ToString("HH:mm");
            }
        }

        private void OnClickDateEditText(object sender, EventArgs e)
        {
            DatePickerDialog datePicker = new DatePickerDialog(this, OnDateTimeSet, targetTime.Year, targetTime.Month - 1, targetTime.Day);
            datePicker.Show();
        }

        private void OnClickTimeEditText(object sender, EventArgs e)
        {
            TimePickerDialog timePicker = new TimePickerDialog(this, OnDateTimeSet, targetTime.Hour, targetTime.Minute, true);
            timePicker.Show();
        }

        private void OnDateTimeSet(object sender, object eventArgs)
        {
            DateTime dtTemp = targetTime;
            TextView textboxDate = FindViewById<TextView>(Resource.Id.textViewDate);
            TextView textboxTime = FindViewById<TextView>(Resource.Id.textViewTime);

            if (eventArgs is DatePickerDialog.DateSetEventArgs)
            {
                targetTime = (eventArgs as DatePickerDialog.DateSetEventArgs).Date;
                targetTime = targetTime.AddHours(dtTemp.Hour).AddMinutes(dtTemp.Minute);
            }
            else if (eventArgs is TimePickerDialog.TimeSetEventArgs)
            {
                targetTime = dtTemp.Date;
                targetTime = targetTime.AddHours((eventArgs as TimePickerDialog.TimeSetEventArgs).HourOfDay).
                                        AddMinutes((eventArgs as TimePickerDialog.TimeSetEventArgs).Minute);
            }
            else
            {
                return;
            }

            textboxDate.Text = targetTime.ToString("yyyy/MM/dd");
            textboxTime.Text = targetTime.ToString("HH:mm");
        }

        private void checkBoxCheckedTitle(object sender, EventArgs e)
        {
            CheckBox checkBoxTitle = FindViewById<CheckBox>(Resource.Id.checkBoxTitle);
            CheckBox checkBoxTitleBold = FindViewById<CheckBox>(Resource.Id.checkBoxTitleBold);
            Spinner spinnerTitleAlign = FindViewById<Spinner>(Resource.Id.spinnerTitleAlign);
            Spinner spinnerTitleSize = FindViewById<Spinner>(Resource.Id.spinnerTitleSize);
            EditText textboxTitle = FindViewById<EditText>(Resource.Id.editTextTitle);

            if (checkBoxTitle.Checked)
            {
                checkBoxTitleBold.Enabled = true;
                textboxTitle.Enabled = true;
                spinnerTitleAlign.Enabled = true;
                spinnerTitleSize.Enabled = true;
            }
            else
            {
                checkBoxTitleBold.Enabled = false;
                textboxTitle.Enabled = false;
                spinnerTitleAlign.Enabled = false;
                spinnerTitleSize.Enabled = false;
            }
        }

        private void checkBoxCheckedText(object sender, EventArgs e)
        {
            CheckBox checkBoxText = FindViewById<CheckBox>(Resource.Id.checkBoxText);
            CheckBox checkBoxTextBold = FindViewById<CheckBox>(Resource.Id.checkBoxTextBold);
            Spinner spinnerTextAlign = FindViewById<Spinner>(Resource.Id.spinnerTextAlign);
            Spinner spinnerTextSize = FindViewById<Spinner>(Resource.Id.spinnerTextSize);
            EditText textboxText = FindViewById<EditText>(Resource.Id.editTextText);

            if (checkBoxText.Checked)
            {
                checkBoxTextBold.Enabled = true;
                textboxText.Enabled = true;
                spinnerTextAlign.Enabled = true;
                spinnerTextSize.Enabled = true;
            }
            else
            {
                checkBoxTextBold.Enabled = false;
                textboxText.Enabled = false;
                spinnerTextAlign.Enabled = false;
                spinnerTextSize.Enabled = false;

            }
        }

        private void Send(object sender, EventArgs e)
        {
            if (!isFinished) return;
            isFinished = false;

            Button thisButton = FindViewById<Button>(Resource.Id.buttonSend);
            RadioButton radioButtonNow = FindViewById<RadioButton>(Resource.Id.radioButtonNow);
            TextView textboxDate = FindViewById<TextView>(Resource.Id.textViewDate);
            TextView textboxTime = FindViewById<TextView>(Resource.Id.textViewTime);

            var dlgAlert = (new AlertDialog.Builder(this)).Create();

            Task.Run(() => 
            {
                RunOnUiThread((() => thisButton.Text = "等待"));
                RunOnUiThread((() => thisButton.Enabled = false));

                string timeStamp = string.Empty;

                if (radioButtonNow.Checked)
                {
                    timeStamp = "NOW";
                }
                else
                {
                    timeStamp = targetTime.ToString("yyyyMMddHHmmss");
                }

                string finalURL = @"Put your upload url here" + timeStamp;
                string finalPost = GenerateReceiptPrinterJobText();

                if (!string.IsNullOrWhiteSpace(finalPost))
                {
                    KeyValuePair<bool, string> response = CreatePostHttpResponse(finalURL, finalPost);

                    dlgAlert.SetTitle("发送完成");

                    if (response.Key)
                    {
                        dlgAlert.SetMessage("发送成功!" + System.Environment.NewLine + "服务器返回: [" + response.Value + "]");
                    }
                    else
                    {
                        dlgAlert.SetMessage("发送失败!" + System.Environment.NewLine + "服务器返回: [" + response.Value + "]");
                    }
                }
                else
                {
                    dlgAlert.SetTitle("未发送");
                    dlgAlert.SetMessage("生成打印脚本失败，或没有可以打印的内容。");
                }
                dlgAlert.SetButton("关闭", handllerNotingButton);

                RunOnUiThread((() => thisButton.Text = "发送"));
                RunOnUiThread((() => thisButton.Enabled = true));

                isFinished = true;
            });

            while (!isFinished)
            {
                Thread.Sleep(10);
            }

            dlgAlert.Show();
            InitUI(null, null);
        }

        private void handllerNotingButton(object sender, DialogClickEventArgs e)
        {
            //AlertDialog objAlertDialog = sender as AlertDialog;
            //Button btnClicked = objAlertDialog.GetButton(e.Which);
            //Toast.MakeText(this, "you clicked on " + btnClicked.Text, ToastLength.Long).Show();
        }

        private string GenerateReceiptPrinterJobText()
        {
            string ret = string.Empty;
            FileStream fs = null;

            try
            {
                CheckBox checkBoxTitle = FindViewById<CheckBox>(Resource.Id.checkBoxTitle);
                CheckBox checkBoxTitleBold = FindViewById<CheckBox>(Resource.Id.checkBoxTitleBold);
                Spinner spinnerTitleAlign = FindViewById<Spinner>(Resource.Id.spinnerTitleAlign);
                Spinner spinnerTitleSize = FindViewById<Spinner>(Resource.Id.spinnerTitleSize);
                EditText textboxTitle = FindViewById<EditText>(Resource.Id.editTextTitle);

                CheckBox checkBoxText = FindViewById<CheckBox>(Resource.Id.checkBoxText);
                CheckBox checkBoxTextBold = FindViewById<CheckBox>(Resource.Id.checkBoxTextBold);
                EditText textboxText = FindViewById<EditText>(Resource.Id.editTextText);
                Spinner spinnerTextAlign = FindViewById<Spinner>(Resource.Id.spinnerTextAlign);
                Spinner spinnerTextSize = FindViewById<Spinner>(Resource.Id.spinnerTextSize);

                if (checkBoxTitle.Checked)
                {
                    ret += "PrintAlign(" + spinnerTitleAlign.SelectedItemPosition.ToString() + ")" + System.Environment.NewLine;
                    ret += "PrintText(u'" + textboxTitle.Text + "'";
                    ret += ", inputBold=" + checkBoxTitle.Checked.ToString();
                    ret += ", inputSize=" + spinnerTitleSize.SelectedItem.ToString();
                    ret += ", inputFade=False";
                    ret += ")" + System.Environment.NewLine;

                    ret += "PrintSpace(20)" + System.Environment.NewLine;
                }
                
                if (checkBoxText.Checked)
                {
                    ret += "PrintAlign(" + spinnerTextAlign.SelectedItemPosition.ToString() + ")" + System.Environment.NewLine;
                    ret += "PrintText(u'" + textboxText.Text.Replace(System.Environment.NewLine, "\\n") + "'";
                    ret += ", inputBold=" + checkBoxTextBold.Checked.ToString();
                    ret += ", inputSize=" + spinnerTextSize.SelectedItem.ToString();
                    ret += ", inputFade=False";
                    ret += ")" + System.Environment.NewLine;
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

        public static KeyValuePair<bool, string> CreatePostHttpResponse(string url, object paramObject)
        {
            KeyValuePair<bool, string> ret = new KeyValuePair<bool, string>();

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

    }


}

