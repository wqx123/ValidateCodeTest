using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// ValidateCode 的摘要说明
    /// </summary>
    public class ValidateCode : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            using (Bitmap map = new Bitmap(80, 30))
            {
                using (Graphics g = Graphics.FromImage(map))
                {
                    g.Clear(Color.White);//填充颜色
                    string str = "";
                    Random r = new Random();
                    for (int i = 0; i < 5; i++)
                    {
                        str = str + r.Next(0, 10);
                    }
                    context.Session["code"] = str;//添加session 
                    //验证码
                    string[] font = { "黑体", "隶书", "楷体", "宋体", "微软雅黑" };
                    Brush[] B = { Brushes.Black, Brushes.Blue, Brushes.Red, Brushes.Green, Brushes.OrangeRed };
                    for (int i = 0; i < 5; i++)
                    {
                        g.DrawString(str[i].ToString(), new Font(font[i], 20), B[i], new Point(i * 15, 0));
                    }
                    //划线                    
                    for (int i = 0; i < 15; i++)
                    {
                        Point p1 = new Point(r.Next(0, map.Width), r.Next(map.Height));
                        Point p2 = new Point(r.Next(0, map.Width), r.Next(map.Height));
                        g.DrawLine(new Pen(Brushes.Red), p1, p2);
                    }
                    //画点
                    for (int i = 0; i < 200; i++)
                    {
                        map.SetPixel(r.Next(0, map.Width), r.Next(map.Height), Color.Red);
                    }
                    string fileNewName = Guid.NewGuid().ToString();
                    //map.Save(context.Request.MapPath("/ImagePath/" + fileNewName + ".jpg"));
                    //保存图片数据
                    MemoryStream stream = new MemoryStream();
                    map.Save(stream, ImageFormat.Jpeg);
                    //输出图片流
                    context.Response.Clear();
                    context.Response.ContentType = "image/jpeg";
                    context.Response.BinaryWrite(stream.ToArray());
                    //string filePath= context.Request.MapPath("Verification.html");
                    //string fileContent= File.ReadAllText(filePath);
                    //fileContent= fileContent.Replace("$value", "/ImagePath/" + fileNewName + ".jpg");
                    //context.Response.Write(fileContent);

                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}