using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using SkiaSharp;
using System.Windows;
using System.Windows.Controls;
using TestApp.Core;
using TestApp.StaticObjects;

namespace TestApp.Services
{
    internal interface IImageService
    {
        void InitCoordinateSystem(WriteableBitmap writeableBitmap);
        WriteableBitmap InitFrame();
        void CreatePoint(WriteableBitmap
            writeableBitmap,
            float x,
            float y,
            float radius,
            SKPaint sKPaint);
        void CreatePolygon(WriteableBitmap writeableBitmap, SKPaint color, int pointSize = 2, params float[] pairPoints);
        void Update(WriteableBitmap writeableBitmap);
    }

    /// <summary>
    /// Сервис для работы с изображением 
    /// </summary>
    internal class ImageService : BaseViewModel, IImageService
    {
        #region Конструкторы
        public ImageService(int width, int height, int dPI)
        {
            Width = width;
            Height = height;
            DPI = dPI;
        }

        public ImageService()
        {
            Width = 900;
            Height = 900;
            DPI = 96;
        }
        #endregion

        /// <summary>
        /// Ширина в PX
        /// </summary>
        private int Width { get; set; }

        /// <summary>
        /// Высота в PX
        /// </summary>
        private int Height { get; set; }

        /// <summary>
        /// Плотность пикселей
        /// </summary>
        private int DPI { get; set; }

        /// <summary>
        /// Инициализация рисунка
        /// </summary>
        /// <returns></returns>
        public WriteableBitmap InitFrame()
        {
            return new(
                pixelWidth: this.Width,
                pixelHeight: this.Height,
                dpiX: DPI,
                dpiY: DPI,
                pixelFormat: PixelFormats.Bgra32,
                palette: BitmapPalettes.Halftone256Transparent);
        }

        /// <summary>
        /// Создание координатной сетки
        /// </summary>
        /// <param name="writeableBitmap">Наш рисунок</param>
        public void InitCoordinateSystem(WriteableBitmap writeableBitmap)
        {
            writeableBitmap.Lock();
            int pixelindent = 100;
            using (var surface = SKSurface.Create(new SKImageInfo(Width, Height), pixels: writeableBitmap.BackBuffer))
            {
                SKCanvas canvas = surface.Canvas;
                int x_offset = 30;
                int y_offset = 10;

                //Разлиновка по X
                for (int i = 0; i < Width; i += pixelindent)
                {
                    canvas.DrawLine(
                        new SKPoint(i, pixelindent),
                        new SKPoint(i, Height - pixelindent),
                        Palette.GetBrushes(AppColor.Gray));
                }

                //Разлиновка по Y
                for (int i = pixelindent; i < Height; i += pixelindent)
                {
                    canvas.DrawLine(
                        new SKPoint(pixelindent, i),
                        new SKPoint(Width - pixelindent, i),
                        Palette.GetBrushes(AppColor.Gray));
                }

                //Ось абсцис (X)
                canvas.DrawLine(
                    new SKPoint(pixelindent, pixelindent),
                    new SKPoint(Width - pixelindent, pixelindent),
                    Palette.GetBrushes(AppColor.Black));

                //Ось ординат Y
                canvas.DrawLine(
                    new SKPoint(pixelindent, pixelindent),
                    new SKPoint(pixelindent, Width - pixelindent),
                    Palette.GetBrushes(AppColor.Black));

                //100 or 0
                canvas.DrawText(
                    pixelindent.ToString(),
                    pixelindent - x_offset,
                    pixelindent - y_offset,
                    Palette.GetBrushes(AppColor.Black, 15));
                canvas.DrawLine(
                        new SKPoint(pixelindent, pixelindent - 5),
                        new SKPoint(pixelindent, pixelindent + 5),
                        Palette.GetBrushes(AppColor.Black));
                canvas.DrawLine(
                        new SKPoint(pixelindent - 5, pixelindent),
                        new SKPoint(pixelindent + 5, pixelindent),
                        Palette.GetBrushes(AppColor.Black));


                //200...Width
                for (int i = pixelindent * 2; i < Width; i += pixelindent)
                {
                    canvas.DrawText(
                        $"{i}",
                        i - 12,
                        pixelindent - 10,
                        Palette.GetBrushes(AppColor.Black, 15));

                    canvas.DrawLine(
                        new SKPoint(i, pixelindent - 5),
                        new SKPoint(i, pixelindent + 5),
                        Palette.GetBrushes(AppColor.Black));
                }

                //200...Height
                for (int i = pixelindent * 2; i < Height; i += pixelindent)
                {
                    canvas.DrawText(
                        $"{i}",
                        pixelindent - x_offset,
                        i - y_offset,
                        Palette.GetBrushes(AppColor.Black, 15));

                    canvas.DrawLine(
                        new SKPoint(pixelindent + 5, i),
                        new SKPoint(pixelindent - 5, i),
                        Palette.GetBrushes(AppColor.Black));
                }
            };

            writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, Width, Height));
            writeableBitmap.Unlock();
        }

        /// <summary>
        /// Создание круглого заполненого цветом объекта на <paramref name="writeableBitmap"/>
        /// </summary>
        /// <param name="writeableBitmap">Наш рисунок</param>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координита Y</param>
        /// <param name="r">Радиус круглого объекта</param>
        /// <param name="sKPaint">Параметры цвета</param>
        public void CreatePoint(WriteableBitmap 
            writeableBitmap, 
            float x, 
            float y, 
            float r,
            SKPaint sKPaint)
        {
            writeableBitmap.Lock(); 

            using (var surface = SKSurface.Create(new SKImageInfo(Width, Height), pixels: writeableBitmap.BackBuffer))
            {
                SKCanvas canvas = surface.Canvas;
                canvas.DrawCircle(new SKPoint(x, y), r, sKPaint);
            }

            writeableBitmap.Unlock();
        }

        /// <summary>
        /// Создание объекта многоугольника на <paramref name="writeableBitmap"/>
        /// </summary>
        /// <param name="writeableBitmap">Наш рисунок</param>
        /// <param name="color">Параметры цвета</param>
        /// <param name="pointSize">Радиус точки обозначение вершины многоугольника</param>
        /// <param name="pairPoints">Массив массив точек типа [x1, y1, x2, y2 ... xn, yn] </param>
        public void CreatePolygon(WriteableBitmap
            writeableBitmap,
            SKPaint color,
            int pointSize,
            params float[] pairPoints) 
        {
            if (pairPoints.Length == 0)
                throw new Exception("Массив не может быть пустым");

            if (pairPoints.Length % 2 != 0)
                throw new Exception("Для создания фигуры необходим массив точек типа [x1, y1, x2, y2 ... xn, yn]");

            if (pairPoints.Length < 3)
                throw new Exception("У фигуры не может быть меньше 3 вершин");

            writeableBitmap.Lock();

            using (var surface = SKSurface.Create(new SKImageInfo(Width, Height), pixels: writeableBitmap.BackBuffer))
            {
                SKCanvas canvas = surface.Canvas;

                for (int i = 0; i < pairPoints.Length - 2; i += 2) 
                {
                    canvas.DrawLine(
                        new SKPoint(pairPoints[i], pairPoints[i+1]),
                        new SKPoint(pairPoints[i + 2], pairPoints[i+3]),
                        color);

                    canvas.DrawCircle(new SKPoint(pairPoints[i], pairPoints[i + 1]), pointSize, color);
                    canvas.DrawCircle(new SKPoint(pairPoints[i + 2], pairPoints[i + 3]), pointSize, color);
                }

                canvas.DrawLine(
                        new SKPoint(pairPoints[0], pairPoints[1]),
                        new SKPoint(pairPoints[pairPoints.Length - 2], pairPoints[pairPoints.Length - 1]),
                        color);

                canvas.DrawCircle(new SKPoint(pairPoints[0], pairPoints[1]), pointSize, color);
                canvas.DrawCircle(new SKPoint(pairPoints[pairPoints.Length - 2], pairPoints[pairPoints.Length - 1]), pointSize, color);
            }
            writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, Width, Height));
            writeableBitmap.Unlock();
        }

        /// <summary>
        /// Событие обновления <paramref name="writeableBitmap"/>
        /// </summary>
        /// <param name="writeableBitmap">Наш рисунок</param>
        public void Update(WriteableBitmap writeableBitmap)
        {

        }
    }
}
