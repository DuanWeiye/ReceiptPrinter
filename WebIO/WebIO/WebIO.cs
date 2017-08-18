using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace wxWebIO
{
    public class WebIO : IHttpHandler
    {
        public static readonly string logFileWebIO = "/home/WebIO.log";
        public static readonly string WebIOFile = "/home/WebIO.xml";
        public static readonly string XMLElementRoot = "WebIO";
        public static readonly string XMLElementLastUpdate = "LastUpdate";
        public static readonly string XMLElementUpdateTime = "UpdateTime";

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod.ToLower() == "get")
            {
                try
                {
                    string para = context.Request.QueryString.ToString();
                    string[] splitedPara = para.Split(',');

                    if (splitedPara.Length == 2 && splitedPara[0] == "Printer")
                    {
                        context.Response.Write(SyncPrinterJobs(splitedPara[1]));
                    }
                    else
                    {
                        WriteLog("ProcessRequest: Error: Invalid Parameter.");
                        context.Response.Write("NG");
                    }
                }
                catch (Exception e)
                {
                    WriteLog("ProcessRequest: Error Msg: " + e.Message);
                }
            }
            else if (context.Request.HttpMethod.ToLower() == "post")
            {
                string para = context.Request.QueryString.ToString();
                string[] splitedPara = para.Split(',');

                if (splitedPara.Length == 3 && splitedPara[0] == "Printer")
                {
                    WriteLog("ProcessRequest: Parameter OK - Add Printer Job For Node: " + splitedPara[1]);

                    SavePostedPrinterJob(splitedPara[1], splitedPara[2], context);
                }
                else if (splitedPara.Length == 2 && splitedPara[0] =="upload")
                {
                    SavePostedPicFile(splitedPara[1], context);
                }
            }

            context.Response.End();
        }

        private bool SavePostedPrinterJob(string thisNode, string targetTimestamp, HttpContext postContext)
        {
            bool ret = false;

            try
            {
                WriteLog("SavePostedPrinterJob: thisNode:" + thisNode + "targetTimestamp:" + targetTimestamp);

                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");

                if (!targetTimestamp.Equals("NOW", StringComparison.CurrentCultureIgnoreCase) &&
                    string.Compare(targetTimestamp, timeStamp) > 0)
                {
                    timeStamp = targetTimestamp;
                }

                int timestampOffset = 0;
                string offsetMark = "_" + timestampOffset.ToString().PadLeft(4, '0');

                string targetFolder = "<put your job path here>" + thisNode + "/";
                string fullPath = targetFolder + timeStamp + offsetMark + ".pp";

                while (File.Exists(fullPath))
                {
                    timestampOffset++;
                    offsetMark = offsetMark = "_" + timestampOffset.ToString().PadLeft(4, '0');

                    fullPath = targetFolder + timeStamp + offsetMark + ".pp";
                }

                WriteLog("SavePostedPrinterJob: fullPath:" + fullPath);

                using (var file = File.Create(fullPath))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;

                    while ((bytesRead = postContext.Request.InputStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        file.Write(buffer, 0, bytesRead);
                    }
                }

                postContext.Response.Write("OK");

                ret = true;
            }
            catch (Exception e)
            {
                WriteLog("SavePostedPrinterJob: Error:" + e.Message);
            }

            return ret;
        }

        private bool SavePostedPicFile(string targetType, HttpContext postContext)
        {
            bool ret = false;
            FileStream fs = null;

            try
            {
                int timestampOffset = 0;
                string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string offsetMark = "_" + timestampOffset.ToString().PadLeft(4, '0');

                string localFolder = "<put your local path here>";
                string httpPath = "<put your http path here>";
                string fileName = timeStamp + offsetMark + ".jpg";

                while (File.Exists(localFolder + fileName))
                {
                    timestampOffset++;
                    offsetMark = offsetMark = "_" + timestampOffset.ToString().PadLeft(4, '0');

                    fileName = timeStamp + offsetMark + ".jpg";
                }

                WriteLog("SavePostedPicFile: Local Path:" + localFolder + fileName);
                WriteLog("SavePostedPicFile: Http Path:" + httpPath + fileName);

                int bytesRead = 0;
                byte[] buffer = new byte[4096];

                if (targetType.Equals("FILE", StringComparison.CurrentCultureIgnoreCase))
                {
                    fs = System.IO.File.Create(localFolder + fileName);
                    while ((bytesRead = postContext.Request.InputStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        fs.Write(buffer, 0, bytesRead);
                    }
                    fs.Close();
                }
                else if (targetType.Equals("URL", StringComparison.CurrentCultureIgnoreCase))
                {
                    bytesRead = postContext.Request.InputStream.Read(buffer, 0, buffer.Length);
                    string requestURL = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    string finalPara = "'" + requestURL + "'" + " -O " + localFolder + fileName;

                    WriteLog("SavePostedPicFile: Call [/usr/bin/wget " + finalPara + "]");

                    ExecuteCMD("/usr/bin/wget", finalPara);
                }

                if (File.Exists(localFolder + fileName))
                {
                    ret = true;

                    WriteLog("SavePostedPicFile: OK");

                    postContext.Response.Write(httpPath + fileName);
                }
                else
                {
                    ret = false;
                    WriteLog("SavePostedPicFile: NG");

                    postContext.Response.Write("NG");
                }
            }
            catch (Exception e)
            {
                WriteLog("SavePostedPicFile: Error:" + e.Message);
            }
            finally
            {
                if(fs != null)
                {
                    fs.Close();
                    fs = null;
                }
            }
            return ret;
        }
        
        private ArrayList ExecuteCMD(string cmdLine, string parameter)
        {
            ArrayList ret = new ArrayList();

            if (string.IsNullOrWhiteSpace(cmdLine) == true)
            {
                return ret;
            }

            Process p = new Process();

            p.StartInfo.FileName = cmdLine;
            p.StartInfo.Arguments = parameter;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            try
            {
                p.Start();

                StreamReader outputReader = p.StandardOutput;

                string strLine = null;
                while ((strLine = outputReader.ReadLine()) != null)
                {
                    if (strLine.Length > 0)
                    {
                        ret.Add(strLine);
                    }
                }

                outputReader.Close();

                p.WaitForExit();
            }
            catch (Exception ex)
            {
                WriteLog("Err: " + ex.Message);
            }
            finally
            {
                p.Close();
            }

            return ret;
        }

        private string SyncPrinterJobs(string thisNode)
        {
            string ret = "NG";
            bool isAvaliable = false;
            string targetFolder = "/home/printer/" + thisNode;
            string headFile = targetFolder + "/_head.pp";
            string endFile = targetFolder + "/_end.pp";
            string thisTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            XmlDocument arXML = new XmlDocument();

            if (System.IO.File.Exists(WebIOFile) == false)
            {
                WriteLog("SyncPrinterJobs: XML File Not Found");
                XmlTextWriter xmlWriter;

                xmlWriter = new XmlTextWriter(WebIOFile, Encoding.Default);
                xmlWriter.Formatting = Formatting.Indented;

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(XMLElementRoot);
                xmlWriter.WriteEndElement();

                xmlWriter.Close();
                WriteLog("SyncPrinterJobs: XML File Created");
            }

            try
            {
                arXML.Load(WebIOFile);
                XmlNode nodeRoot = arXML.SelectSingleNode(XMLElementRoot);

                foreach (XmlNode xn in nodeRoot.ChildNodes)
                {
                    XmlElement xe = (XmlElement)xn;

                    if (xe.Name == thisNode)
                    {
                        string textLastSync = xe.GetAttribute(XMLElementLastUpdate);

                        if (string.IsNullOrWhiteSpace(textLastSync))
                        {
                            WriteLog("SyncPrinterJobs: Invalid Printer Node - Empty");
                            return ret;
                        }

                        foreach (char eachChar in textLastSync.ToCharArray())
                        {
                            if (!char.IsDigit(eachChar))
                            {
                                WriteLog("SyncPrinterJobs: Invalid Node - Not Timestamp");
                                return ret;
                            }
                        }

                        DirectoryInfo di = new DirectoryInfo(targetFolder);

                        if (!di.Exists)
                        {
                            WriteLog("SyncPrinterJobs: Target Printer Node Not Exist");
                            return ret;
                        }

                        ret = "OK" + Environment.NewLine;
                        ret += "_BEGIN_" + Environment.NewLine;

                        foreach (FileInfo eachFile in di.GetFiles().OrderBy(f => f.Name))
                        {
                            if (eachFile.Extension != ".pp" || eachFile.Name.Length != 22)
                            {
                                continue;
                            }

                            string eachTT = eachFile.Name.Substring(0, 13);
                            foreach (char eachChar in eachTT.ToCharArray())
                            {
                                if (!char.IsDigit(eachChar))
                                {
                                    WriteLog("SyncPrinterJobs: Invalid file Name - Not Timestamp, skip");
                                    continue;
                                }
                            }

                            if (string.Compare(textLastSync, eachTT) > 0 ||
                                string.Compare(eachTT, thisTimeStamp) > 0 ||
                                string.Compare(textLastSync, thisTimeStamp) > 0)
                            {
                                continue;
                            }

                            // Add Head
                            if (File.Exists(headFile))
                            {
                                foreach (string eachLine in File.ReadLines(headFile))
                                {
                                    ret += eachLine.Replace("<ReservedTime>", "\"" + eachTT + "\"") + Environment.NewLine;
                                }
                            }

                            // Add Content
                            foreach (string eachLine in File.ReadLines(eachFile.FullName))
                            {
                                ret += eachLine + Environment.NewLine;

                                if (!isAvaliable) isAvaliable = true;
                            }

                            // Add End
                            if (File.Exists(endFile))
                            {
                                foreach (string eachLine in File.ReadLines(endFile))
                                {
                                    ret += eachLine.Replace("<ReservedTime>", "\"" + eachTT + "\"") + Environment.NewLine;
                                }
                            }

                        }

                        ret += "_END_";

                        if (!isAvaliable)
                        {
                            ret = "IDLE";
                        }
                        else
                        {
                            xe.SetAttribute(XMLElementLastUpdate, thisTimeStamp);
                            nodeRoot.ReplaceChild(xe, xn);

                            arXML.Save(WebIOFile);
                        }

                        break;
                    }
                }
            }
            catch
            {
                ret = "NG";
            }

            return ret;
        }

        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        private void WriteLog(string strMemo)
        {
            System.IO.StreamWriter sr = null;

            try
            {
                sr = System.IO.File.AppendText(logFileWebIO);

                sr.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ": " + strMemo);
            }
            catch
            {

            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        #endregion
    }
}
