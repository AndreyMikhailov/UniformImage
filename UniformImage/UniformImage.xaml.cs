using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UniformImageNamespace
{
    public partial class UniformImage : UserControl
    {
        public static readonly DependencyProperty StaticAreaProperty = DependencyProperty.Register(
            "StaticArea",
            typeof(Thickness),
            typeof(UniformImage),
            new PropertyMetadata(InvalidateImage)
            );

        public Thickness StaticArea
        {
            get { return (Thickness)GetValue(StaticAreaProperty); }
            set { SetValue(StaticAreaProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source",
            typeof(ImageSource),
            typeof(UniformImage),
            new PropertyMetadata(InvalidateImage)
            );

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public UniformImage()
        {
            InitializeComponent();
        }

        protected static readonly DependencyProperty TopLeftImageProperty = DependencyProperty.Register(
            "TopLeftImage",
            typeof(ImageSource),
            typeof(UniformImage)
            );

        protected ImageSource TopLeftImage
        {
            get { return (ImageSource)GetValue(TopLeftImageProperty); }
            set { SetValue(TopLeftImageProperty, value); }
        }

        protected static readonly DependencyProperty TopImageProperty = DependencyProperty.Register(
            "TopImage",
            typeof(ImageSource),
            typeof(UniformImage)
            );

        protected ImageSource TopImage
        {
            get { return (ImageSource)GetValue(TopImageProperty); }
            set { SetValue(TopImageProperty, value); }
        }

        protected static readonly DependencyProperty TopRightImageProperty = DependencyProperty.Register(
            "TopRightImage",
            typeof(ImageSource),
            typeof(UniformImage)
            );

        protected ImageSource TopRightImage
        {
            get { return (ImageSource)GetValue(TopRightImageProperty); }
            set { SetValue(TopRightImageProperty, value); }
        }

        protected static readonly DependencyProperty RightImageProperty = DependencyProperty.Register(
            "RightImage",
            typeof(ImageSource),
            typeof(UniformImage)
            );

        protected ImageSource RightImage
        {
            get { return (ImageSource)GetValue(RightImageProperty); }
            set { SetValue(RightImageProperty, value); }
        }

        protected static readonly DependencyProperty BottomRightImageProperty = DependencyProperty.Register(
            "BottomRightImage",
            typeof(ImageSource),
            typeof(UniformImage)
            );

        protected ImageSource BottomRightImage
        {
            get { return (ImageSource)GetValue(BottomRightImageProperty); }
            set { SetValue(BottomRightImageProperty, value); }
        }

        protected static readonly DependencyProperty BottomImageProperty = DependencyProperty.Register(
            "BottomImage",
            typeof(ImageSource),
            typeof(UniformImage)
            );

        protected ImageSource BottomImage
        {
            get { return (ImageSource)GetValue(BottomImageProperty); }
            set { SetValue(BottomImageProperty, value); }
        }

        protected static readonly DependencyProperty BottomLeftImageProperty = DependencyProperty.Register(
            "BottomLeftImage",
            typeof(ImageSource),
            typeof(UniformImage)
            );

        protected ImageSource BottomLeftImage
        {
            get { return (ImageSource)GetValue(BottomLeftImageProperty); }
            set { SetValue(BottomLeftImageProperty, value); }
        }

        protected static readonly DependencyProperty LeftImageProperty = DependencyProperty.Register(
            "LeftImage",
            typeof(ImageSource),
            typeof(UniformImage)
            );

        protected ImageSource LeftImage
        {
            get { return (ImageSource)GetValue(LeftImageProperty); }
            set { SetValue(LeftImageProperty, value); }
        }

        protected static readonly DependencyProperty CenterImageProperty = DependencyProperty.Register(
            "CenterImage",
            typeof(ImageSource),
            typeof(UniformImage)
            );

        protected ImageSource CenterImage
        {
            get { return (ImageSource)GetValue(CenterImageProperty); }
            set { SetValue(CenterImageProperty, value); }
        }

        private static void InvalidateImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uniformImage = (UniformImage)d;

            uniformImage.TopLeftImage = GetCroppedPart(AlignmentX.Left, AlignmentY.Top, uniformImage);
            uniformImage.TopImage = GetCroppedPart(AlignmentX.Center, AlignmentY.Top, uniformImage);
            uniformImage.TopRightImage = GetCroppedPart(AlignmentX.Right, AlignmentY.Top, uniformImage);
            uniformImage.LeftImage = GetCroppedPart(AlignmentX.Left, AlignmentY.Center, uniformImage);
            uniformImage.CenterImage = GetCroppedPart(AlignmentX.Center, AlignmentY.Center, uniformImage);
            uniformImage.RightImage = GetCroppedPart(AlignmentX.Right, AlignmentY.Center, uniformImage);
            uniformImage.BottomLeftImage = GetCroppedPart(AlignmentX.Left, AlignmentY.Bottom, uniformImage);
            uniformImage.BottomImage = GetCroppedPart(AlignmentX.Center, AlignmentY.Bottom, uniformImage);
            uniformImage.BottomRightImage = GetCroppedPart(AlignmentX.Right, AlignmentY.Bottom, uniformImage);
        }

        private static ImageSource GetCroppedPart(AlignmentX alignmentX, AlignmentY alignmentY, UniformImage uniformImage)
        {
            ImageSource imageSource = uniformImage.Source;
            if (imageSource == null) return null;

            double totalWidth = imageSource.Width;
            double totalHeight = imageSource.Height;
            Thickness staticArea = uniformImage.StaticArea;

            if (totalWidth <= 0 || totalHeight <= 0) return null;

            // Предопределенные размеры

            double topHeight = staticArea.Top;
            double centerHeight = totalHeight - (staticArea.Top + staticArea.Bottom);
            double bottomHeight = staticArea.Bottom;

            double leftWidth = staticArea.Left;
            double centerWidth = totalWidth - (staticArea.Left + staticArea.Right);
            double rightWidth = staticArea.Right;

            // Предопределенные отступы

            double centerTopMargin = staticArea.Top;
            double bottomTopMargin = totalHeight - staticArea.Bottom;

            double centerLeftMargin = staticArea.Left;
            double rightLeftMargin = totalWidth - staticArea.Right;

            // Расчет позиции и размера региона в зависимости от его выравнивания

            double topMargin = 0;
            double leftMargin = 0;

            double width = 0;
            double height = 0;

            switch (alignmentX)
            {
                case AlignmentX.Left:
                    width = leftWidth;
                    break;

                case AlignmentX.Center:
                    leftMargin = centerLeftMargin;
                    width = centerWidth;
                    break;

                case AlignmentX.Right:
                    leftMargin = rightLeftMargin;
                    width = rightWidth;
                    break;
            }

            switch (alignmentY)
            {
                case AlignmentY.Top:
                    height = topHeight;
                    break;

                case AlignmentY.Center:
                    topMargin = centerTopMargin;
                    height = centerHeight;
                    break;

                case AlignmentY.Bottom:
                    topMargin = bottomTopMargin;
                    height = bottomHeight;
                    break;
            }
            
            if (height <= 0 || width <= 0) return null;
            if (topMargin < 0) topMargin = 0;
            if (leftMargin < 0) leftMargin = 0;
            if (height + topMargin > totalHeight) height = totalHeight - topMargin;
            if (width + leftMargin > totalWidth) width = totalWidth - leftMargin;

            return new CroppedBitmap(
                (BitmapSource)imageSource, 
                new Int32Rect((int)leftMargin, (int)topMargin, (int)width, (int)height));
        }
    }
}
