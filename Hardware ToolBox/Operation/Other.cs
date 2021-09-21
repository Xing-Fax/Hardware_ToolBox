using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Hardware_ToolBox.Operation
{
    class Other
    {
        /// <summary>
        /// 创建动画效果，起始值为对象当前值
        /// </summary>
        /// <param name="arrive">结束动画</param>
        /// <param name="bar">要创建的对象</param>
        public static void Animation(double arrive, ProgressBar bar)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = bar.Value;       //设置From属性
            doubleAnimation.To = arrive;            //设置To属性
            doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(App.Transition));   //设置Duration属性
            bar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);                       //为元素设置BeginAnimation方法
        }

        /// <summary>
        /// 修改主题颜色
        /// </summary>
        /// <param name="Sixteen">颜色值(十六进制)</param>
        public static void Theme(string Sixteen)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            Color color = (Color)ColorConverter.ConvertFromString(Sixteen);
            theme.SetPrimaryColor(color);
            paletteHelper.SetTheme(theme);
        }

        /// <summary>
        /// 文件签名校验
        /// 通过读取唯一指纹来确定文件完整
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>成功校验返回 true 失败或者出现错误返回 false</returns>
        public static bool Document_verification(string file)
        {
            try
            {
                X509Certificate cert = X509Certificate.CreateFromSignedFile(file);
                string f = cert.GetCertHashString();
                if (f == "36A888B9F2A505BF92AC6B2796C2188E639AB1D1")
                { return true; }
                else
                { return false; }
            }
            catch { return false; }
        }
    }
}
