using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.QrCode;

namespace ZengZeng.Core
{
    public class QRUtils
    {
        /// <summary>
        /// 生成二维码,保存成图片
        /// </summary>
        public static void GenerateQR(string text, string tempFilePath)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            //设置内容编码
            options.CharacterSet = "UTF-8";
            //设置二维码的宽度和高度
            options.Width = 500;
            options.Height = 500;
            //设置二维码的边距,单位不是固定像素
            options.Margin = 1;
            writer.Options = options;

            Bitmap map = writer.Write(text);
            string filename = tempFilePath;
            map.Save(filename, ImageFormat.Jpeg);
            map.Dispose();
        }
    }
}