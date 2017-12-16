using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace ZengZeng.Core
{
    public class ImageUtils
    {
        /// <summary>  
        /// 图片裁剪，生成新图，保存在同一目录下,名字加_new，格式1.png 新图1_new.png  
        /// </summary>  
        /// <param name="picPath">要修改图片完整路径</param>  
        /// <param name="x">修改起点x坐标</param>  
        /// <param name="y">修改起点y坐标</param>  
        /// <param name="width">新图宽度</param>  
        /// <param name="height">新图高度</param>  
        public static void caijianpic(String picPath, int x, int y, int width, int height)
        {
            //图片路径  
            String oldPath = picPath;
            //新图片路径  
            String newPath = System.IO.Path.GetExtension(oldPath);
            //计算新的文件名，在旧文件名后加_new  
            newPath = oldPath.Substring(0, oldPath.Length - newPath.Length) + "_new" + newPath;
            //定义截取矩形  
            System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(x, y, width, height);
            //要截取的区域大小  
            //加载图片  
            System.Drawing.Image img = System.Drawing.Image.FromStream(new System.IO.MemoryStream(System.IO.File.ReadAllBytes(oldPath)));
            //判断超出的位置否  
            if ((img.Width < x + width) || img.Height < y + height)
            {
                //MessageBox.Show("裁剪尺寸超出原有尺寸！");
                img.Dispose();
                return;
            }
            //定义Bitmap对象  
            System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(img);
            //进行裁剪  
            System.Drawing.Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            //保存成新文件  
            bmpCrop.Save(newPath);
            //释放对象  
            img.Dispose(); bmpCrop.Dispose();
        }


        /// <summary>  
        /// 调用此函数后使此两种图片合并，类似相册，有个  
        /// 背景图，中间贴自己的目标图片  
        /// </summary>  
        /// <param name="sourceImg">粘贴的源图片</param>  
        /// <param name="destImg">粘贴的目标图片</param>  
        public static Image CombinImage(string sourceImg, string destImg,string filepath)
        {
            Image imgBack = System.Drawing.Image.FromFile(sourceImg); //相框图片   
            Image img = System.Drawing.Image.FromFile(destImg); //照片图片  
                                                                //从指定的System.Drawing.Image创建新的System.Drawing.Graphics         
            Graphics g = Graphics.FromImage(imgBack);
            //g.DrawImage(imgBack, 0, 0, 148, 124); // g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);  
            //g.FillRectangle(System.Drawing.Brushes.Black, -50, -50, (int)212, ((int)203));//相片四周刷一层黑色边框，这里没有，需要调尺寸  
                                                                                          //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  
            g.DrawImage(img, 352, 177, 55, 55);
            GC.Collect();
            string saveImagePath = filepath;
            //save new image to file system.  
            imgBack.Save(saveImagePath, ImageFormat.Jpeg);
            return imgBack;
        }
    }
}