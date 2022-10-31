using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace WindowsFormsApplication1
{
    internal unsafe class VisionTransform
    {
        private BitmapData BmpData_In = new BitmapData();
        private BitmapData BmpData_Out = new BitmapData();
        private byte* ImgPtr_In { get; set; }
        private byte* ImgPtr_Out { get; set; }
        private int StepSize_In, ByteOfSkip_In, StepSize_Out, ByteOfSkip_Out;

        internal void RGB2Gray(ref Bitmap _imgin, ref Bitmap _imgout)//RGB To Y
        {
            byte temp;
            BmpData_In = _imgin.LockBits(new Rectangle(0, 0, _imgin.Width, _imgin.Height), ImageLockMode.ReadOnly, _imgin.PixelFormat);
            switch (BmpData_In.PixelFormat)
            {
                case PixelFormat.Format8bppIndexed:
                    StepSize_In = 1;
                    ByteOfSkip_In = BmpData_In.Stride - _imgin.Width;
                    break;
                case PixelFormat.Format24bppRgb:
                    StepSize_In = 3;
                    ByteOfSkip_In = BmpData_In.Stride - _imgin.Width * 3;
                    break;
                case PixelFormat.Format32bppArgb:
                    StepSize_In = 4;
                    ByteOfSkip_In = BmpData_In.Stride - _imgin.Width * 4;
                    break;
            }
            BmpData_Out = _imgout.LockBits(new Rectangle(0, 0, _imgout.Width, _imgout.Height), ImageLockMode.WriteOnly, _imgout.PixelFormat);
            StepSize_Out = 1;
            ByteOfSkip_Out = BmpData_Out.Stride - _imgout.Width;
            ImgPtr_In = (byte*)BmpData_In.Scan0.ToPointer();
            ImgPtr_Out = (byte*)BmpData_Out.Scan0.ToPointer();
            for (int i = 0; i < _imgin.Height; i++)
            {
                for (int j = 0; j < _imgin.Width; j++)//Grayscale = 0.3*R + 0.59*G + 0.11*B
                {
                    temp = Convert.ToByte(Convert.ToSingle(*ImgPtr_In) * 0.114f);
                    ++ImgPtr_In;
                    temp += Convert.ToByte(Convert.ToSingle(*ImgPtr_In) * 0.587f);
                    ++ImgPtr_In;
                    temp += Convert.ToByte(Convert.ToSingle(*ImgPtr_In) * 0.299f);
                    ++ImgPtr_In;
                    *ImgPtr_Out = temp;
                    ++ImgPtr_Out;
                }
                ImgPtr_In += ByteOfSkip_In;
                ImgPtr_Out += ByteOfSkip_Out;
            }
            _imgin.UnlockBits(BmpData_In);
            _imgout.UnlockBits(BmpData_Out);
        }

        internal void RGB2Laplace(ref Bitmap _imgin, ref Bitmap _imgout)//RGB To Y
        {
            int i_temp = 0;
            int bb = 0;
            double d_temp = 0;
            int maskskip = 1;
            BmpData_In = _imgin.LockBits(new Rectangle(0, 0, _imgin.Width, _imgin.Height), ImageLockMode.ReadOnly, _imgin.PixelFormat);
            ByteOfSkip_In = BmpData_In.Stride - _imgin.Width;
            BmpData_Out = _imgout.LockBits(new Rectangle(0, 0, _imgout.Width, _imgout.Height), ImageLockMode.WriteOnly, _imgout.PixelFormat);
            ByteOfSkip_Out = BmpData_Out.Stride - _imgout.Width;
            ImgPtr_In = (byte*)BmpData_In.Scan0.ToPointer();
            ImgPtr_Out = (byte*)BmpData_Out.Scan0.ToPointer();
            ImgPtr_In += (maskskip * BmpData_In.Stride);
            ImgPtr_Out += (maskskip * BmpData_Out.Stride);
            bb += (maskskip * BmpData_Out.Stride);
            for (int i = maskskip; i < _imgin.Height - maskskip; i++)
            {
                ImgPtr_In = ImgPtr_In + maskskip;
                ImgPtr_Out = ImgPtr_Out + maskskip;
                bb = bb + maskskip;
                for (int j = maskskip; j < _imgin.Width - maskskip; j++)
                {
                    i_temp = Convert.ToInt32(*ImgPtr_In);
                   
                    if (i_temp <= 35 ) { i_temp = 255; }
                    
                    else { i_temp = 0; }
                    
                    
                    *ImgPtr_Out = Convert.ToByte(i_temp);
                    ++ImgPtr_In;
                    ++ImgPtr_Out;
                    bb++;
                }
                ImgPtr_In += ByteOfSkip_In + maskskip;
                ImgPtr_Out += ByteOfSkip_Out + maskskip;
                bb += ByteOfSkip_Out + maskskip;
            }
            _imgin.UnlockBits(BmpData_In);
            _imgout.UnlockBits(BmpData_Out);
        }

        internal void RGB2Gray_2(ref Bitmap _imgin, ref Bitmap _imgout)//RGB To Y
        {
            byte temp;
            BmpData_In = _imgin.LockBits(new Rectangle(0, 0, _imgin.Width, _imgin.Height), ImageLockMode.ReadOnly, _imgin.PixelFormat);
            switch (BmpData_In.PixelFormat)
            {
                case PixelFormat.Format8bppIndexed:
                    StepSize_In = 1;
                    ByteOfSkip_In = BmpData_In.Stride - _imgin.Width;
                    break;
                case PixelFormat.Format24bppRgb:
                    StepSize_In = 3;
                    ByteOfSkip_In = BmpData_In.Stride - _imgin.Width * 3;
                    break;
                case PixelFormat.Format32bppArgb:
                    StepSize_In = 4;
                    ByteOfSkip_In = BmpData_In.Stride - _imgin.Width * 4;
                    break;
            }
            BmpData_Out = _imgout.LockBits(new Rectangle(0, 0, _imgout.Width, _imgout.Height), ImageLockMode.WriteOnly, _imgout.PixelFormat);
            StepSize_Out = 1;
            ByteOfSkip_Out = BmpData_Out.Stride - _imgout.Width;
            ImgPtr_In = (byte*)BmpData_In.Scan0.ToPointer();
            ImgPtr_Out = (byte*)BmpData_Out.Scan0.ToPointer();
            for (int i = 0; i < _imgin.Height; i++)
            {
                for (int j = 0; j < _imgin.Width; j++)//Grayscale = 0.3*R + 0.59*G + 0.11*B
                {
                    temp = Convert.ToByte(Convert.ToSingle(*ImgPtr_In) * 0.114f);
                    ++ImgPtr_In;
                    temp += Convert.ToByte(Convert.ToSingle(*ImgPtr_In) * 0.587f);
                    ++ImgPtr_In;
                    temp += Convert.ToByte(Convert.ToSingle(*ImgPtr_In) * 0.299f);
                    ++ImgPtr_In;
                    *ImgPtr_Out = temp;
                    ++ImgPtr_Out;
                }
                ImgPtr_In += ByteOfSkip_In;
                ImgPtr_Out += ByteOfSkip_Out;
            }
            _imgin.UnlockBits(BmpData_In);
            _imgout.UnlockBits(BmpData_Out);
        }
    }
}
