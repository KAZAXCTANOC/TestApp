using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.StaticObjects
{
    public enum AppColor
    {
        Red,
        Black,
        Gray
    }

    /// <summary>
    /// Класс получения <see cref="SKColor"/> и готовых <see cref="SKPaint"/>
    /// </summary>
    public static class Palette
    {
        private static Dictionary<AppColor, SKColor> Colors { get; set; } 
            = new Dictionary<AppColor, SKColor>();
        private static Dictionary<AppColor, SKPaint> Brushes { get; set; } 
            = new Dictionary<AppColor, SKPaint>();

        public static SKColor GetColor(AppColor color) 
        {
            SKColor res;
            bool getRes = Colors.TryGetValue(color, out res);

            if (getRes == false) 
                throw new Exception("Ошибка при получении цвета");

            return res;
        }

        /// <summary>
        /// Получение кисти
        /// </summary>
        /// <param name="color">обозначение цвета</param>
        /// <returns>Кисть</returns>
        public static SKPaint GetBrushes(AppColor color)
        {
            SKColor res = GetColor(color);

            return new SKPaint() { Color = res };
        }

        /// <summary>
        /// Получение кисти
        /// </summary>
        /// <param name="color">обозначение цвета</param>
        /// <param name="textSize">Размер текста</param>
        /// <returns>Кисть</returns>
        public static SKPaint GetBrushes(AppColor color, int textSize)
        {
            SKColor res = GetColor(color);

            return new SKPaint() { Color = res, TextSize = textSize};
        }

        /// <summary>
        /// Добавление цвета
        /// </summary>
        /// <param name="color">обозначение цвета</param>
        /// <param name="sKPaint">цвет</param>
        /// <exception cref="Exception"></exception>
        public static void AddColor(AppColor color, SKColor sKPaint) 
        {
            var res = Colors.TryAdd(color, sKPaint);
            if (res == false)
                throw new Exception("Ошибка при добавлении цвета");
        }


    }
}
