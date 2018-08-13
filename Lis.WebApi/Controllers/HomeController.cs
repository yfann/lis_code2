using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Lis.Service.Dtos;
using System.Net.Http;

namespace CSH.HCIS.Ucc.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("~/app/index.html");
        }

        public void DownloadPdf(string reportId)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=检验报告单.pdf");
            Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/pdf";
            //文件临时存储路径
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "/PDF/" + System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/PDF/"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "/PDF/");
            }

            var requestHost = Request.Url.ToString().Substring(0, Request.Url.ToString().ToLower().IndexOf("home/downloadpdf") - 1);
            BaoGaoHtml(filePath, reportId, requestHost);
            //读取已建好的文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            while (true)
            {
                byte[] buffer = new byte[fs.Length];
                //将文件读取成byte字节
                int len = fs.Read(buffer, 0, (int)fs.Length);
                if (len == 0) break;
                if (len == 1024)
                {
                    Response.BinaryWrite(buffer);
                    break;
                }
                else
                {
                    //读出文件数据比缓冲区小，重新定义缓冲区大小，只用于读取文件的最后一个数据块  
                    byte[] b = new byte[len];
                    for (int i = 0; i < len; i++)
                    {
                        b[i] = buffer[i];
                    }
                    Response.BinaryWrite(b);
                    break;
                }
            }
            fs.Flush();
            fs.Close();
            //删除临时文件
            if (System.IO.File.Exists(filePath))
            {
                //System.IO.File.Delete(filePath);
            }
            Response.End();
        }

        private static ReportDto GetReportDetail(string reportId, string uri)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(uri);
            var response = httpClient.GetAsync("api/lis/reportsdetail?id=" + reportId).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<ReportDto>().Result;
                return result;
            }
            return new ReportDto();
        }

        /// <summary>
        /// html转pdf
        /// </summary>
        /// <param name="filePath">文件存储路径</param>
        /// <param name="reportId">报告ID</param>
        /// <param name="uri"></param>
        public static void BaoGaoHtml(string filePath, string reportId, string uri)
        {
            //注册字体(pdf上显示中文必须注册字体)
            FontFactory.RegisterFamily("宋体", "simsun", @"c:\windows\fonts\SIMSUN.TTC,0");
            // FontFactory.RegisterFamily("宋体", "simsun bold", @"c:\windows\fonts\SIMSUN_bold.TTC");

            var data = GetReportDetail(reportId, uri);
            StringBuilder sb = PdfHtml(data);
            Document document = new Document();
            //设置页面大小是A4纸大小
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            //创建文档
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();
            HTMLWorker html = new HTMLWorker(document);
            //将html转为pdf
            html.Parse(new StringReader(sb.ToString()));
            document.Close();
        }

        public static StringBuilder PdfHtml(ReportDto data)
        {
            StringBuilder sb = new StringBuilder();
            //标题
            sb.Append("<table border=\"0\" width=\"100%\" style=\"margin:0 auto;\">");
            sb.Append("<tr>");
            //sb.Append("<td width=\"25%\"></td>");
            sb.Append("<td width=\"100%\" style=\"font-family: 宋体; font-size: 18pt; height: 50px; line-height: 22px; text-align:center;\" encoding=\"Identity-H\" >检验报告单(" + data.SetName + ")</td>");
            //sb.Append("<td width=\"25%\" style=\"font-family: 宋体; font-size: 12pt; height: 50px; line-height: 22px; text-align:right;\" encoding=\"Identity-H\">打印日期：" + DateTime.Now.ToString("yyyy.MM.dd") + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<br/>");

            sb.Append("<table border=\"0.5\" width=\"100%\" style=\"margin:0 auto; border-color:#aaa;\">");
            sb.Append("<tr>");
            sb.Append("<td>&nbsp;</td>");
            sb.Append("</tr>");
            sb.Append("</table>");

            sb.Append("<br/>");
            //中间部分的表格
            sb.Append("<table border=\"0\" width=\"100%\" style=\"margin:0 auto;\">");
            sb.Append("<tr>");
            sb.Append("<td width=\"11%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">姓名:&nbsp;" + data.PatientName + "</td>");
            sb.Append("<td width=\"11%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">年龄:&nbsp;" + data.Age + "</td>");
            sb.Append("<td width=\"11%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">性别:&nbsp;" + data.Gender + "</td>");
            sb.Append("<td width=\"11%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">床号:&nbsp;" + data.BedNo + "</td>");
            sb.Append("<td width=\"11%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">申请医生:&nbsp;" + data.ApplicationDoctor + "</td>");
            sb.Append("<td width=\"45%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">申请时间:" + (data.ApplicationTime.HasValue ? data.ApplicationTime.Value.ToString("yyyy-MM-dd") : "") + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td width=\"11%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">科室:&nbsp;" + data.Dept + "</td>");
            sb.Append("<td width=\"11%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">类别:&nbsp;" + data.Category + "</td>");
            sb.Append("<td width=\"78%\" colspan=\"4\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">备注:&nbsp;" + data.Comment + "</td>");
            sb.Append("</tr>");
            sb.Append("</table><br />");

            //sb.Append("<hr />");
            //检验项表格
            //表格标题
            sb.Append("<table width=\"100%\" border=\"0.5\" style=\"margin:0 auto;\">");
            sb.Append("<tr>");
            sb.Append("<th width=\"5%\" style=\"font-family: 宋体; font-size: 14pt;text-align:center;\" encoding=\"Identity-H\" bgcolor=\"rgb(196,196,196)\">序号</th>");
            sb.Append("<th width=\"15%\" style=\"font-family: 宋体; font-size: 14pt;text-align:center;\" encoding=\"Identity-H\" bgcolor=\"rgb(196,196,196)\">代码</th>");
            sb.Append("<th width=\"30%\" style=\"font-family: 宋体; font-size: 14pt;text-align:center;\" encoding=\"Identity-H\" bgcolor=\"rgb(196,196,196)\">项目名称</th>");
            sb.Append("<th width=\"18%\" style=\"font-family: 宋体; font-size: 14pt;text-align:center;\" encoding=\"Identity-H\" bgcolor=\"rgb(196,196,196)\">检验结果值</th>");
            sb.Append("<th width=\"18%\" style=\"font-family: 宋体; font-size: 14pt;text-align:center;\" encoding=\"Identity-H\" bgcolor=\"rgb(196,196,196)\">结果范围值</th>");
            sb.Append("<th width=\"14%\" style=\"font-family: 宋体; font-size: 14pt;text-align:center;\" encoding=\"Identity-H\" bgcolor=\"rgb(196,196,196)\">检验结果单位</th>");
            sb.Append("</tr>");
            //表格内容
            var index = 0;
            foreach (var dr in data.Details)
            {
                index++;

                decimal resultValue, refLo, refHi;
                var isRed = false;
                string rangeStr = "";
                var resultValueIsNum = decimal.TryParse(dr.LabResult.ResultValue, out resultValue);
                var refLoIsNum = decimal.TryParse(dr.LabResult.RefLo, out refLo);
                var refHiIsNum = decimal.TryParse(dr.LabResult.RefHi, out refHi);
                if (resultValueIsNum && refLoIsNum && refHiIsNum)
                {
                    if (resultValue < refLo || resultValue > refHi)
                    {
                        isRed = true;
                    }
                    rangeStr = string.Format("{0} - {1}", refLo, refHi);
                }
                else
                {
                    rangeStr = dr.LabResult.RefRange;
                    if (dr.LabResult.ResultValue != dr.LabResult.RefRange)
                    {
                        isRed = true;
                    }
                }

                sb.Append("<tr>");
                sb.Append("<td width=\"5%\" style=\"font-family: 宋体; font-size: 12pt;text-align:center;\" encoding=\"Identity-H\">" + index + "</td>");
                sb.Append("<td width=\"15%\" style=\"font-family: 宋体; font-size: 12pt;text-align:center;\" encoding=\"Identity-H\">" + dr.LabItem.ItemCode + "</td>");
                sb.Append("<td width=\"30%\" style=\"font-family: 宋体; font-size: 12pt;text-align:center;\" encoding=\"Identity-H\">" + dr.LabItem.ItemName + "</td>");
                sb.Append("<td width=\"18%\" style=\"font-family: 宋体; font-size: 12pt;text-align:center; color:" + (isRed ? "red" : "black") + ";\" encoding=\"Identity-H\">" + dr.LabResult.ResultValue + "</td>");
                sb.Append("<td width=\"18%\" style=\"font-family: 宋体; font-size: 12pt;text-align:center;\" encoding=\"Identity-H\">" + rangeStr + "</td>");
                sb.Append("<td width=\"14%\" style=\"font-family: 宋体; font-size: 12pt;text-align:center;\" encoding=\"Identity-H\">" + dr.LabResult.Unit + "</td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            sb.Append("<br />");
            sb.Append("<br />");
            sb.Append("<br />");

            // 页脚
            sb.Append("<table border=\"0\" width=\"100%\" style=\"margin:0 auto;\">");
            sb.Append("<tr>");
            sb.Append("<td width=\"30%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">送检时间:&nbsp;" + (data.SendTime.HasValue ? data.SendTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "") + "</td>");
            sb.Append("<td width=\"30%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">报告时间:&nbsp;" + (data.ReportTime.HasValue ? data.ReportTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "") + "</td>");
            sb.Append("<td width=\"20%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">送检人:&nbsp;" + data.Approvaler + "</td>");
            sb.Append("<td width=\"20%\" style=\"font-family: 宋体; font-size: 12pt; text-align:left; \" encoding=\"Identity-H\">审核人:&nbsp;" + data.Inspector + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            return sb;
        }
    }
}
