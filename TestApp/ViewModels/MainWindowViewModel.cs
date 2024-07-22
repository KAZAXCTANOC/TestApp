using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using TestApp.Core;
using TestApp.Services;
using TestApp.StaticObjects;

namespace TestApp.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        public string Title { get => "Тестовое задание"; }

        private IImageService _imageService;
        private WriteableBitmap _writeableBitmap;

        #region Image
        private ImageSource _ImageSource;
        public ImageSource ImageSource
        {
            get { return _ImageSource; }
            set
            {
                if (_ImageSource != value)
                {
                    _ImageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }
        #endregion

        public MainWindowViewModel(IImageService imageService)
        {
            _imageService = imageService;
            ImageSource = _writeableBitmap = _imageService.InitFrame();
            _imageService.InitCoordinateSystem(_writeableBitmap);
            CompositionTarget.Rendering += (o, e) => _imageService.Update(_writeableBitmap);
        }
    }
}
