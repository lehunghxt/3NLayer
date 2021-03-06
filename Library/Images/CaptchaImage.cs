using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace Library.Images
{
    public class CaptchaImage
    {
        private string text;
        private int width;
        private int height;
        private string familyName;
        private HatchBrush hatchBrushAnh = new HatchBrush(HatchStyle.OutlinedDiamond, Color.LightGray, Color.White);
        private HatchBrush hatchBrushText = new HatchBrush(HatchStyle.LargeConfetti, Color.LightGray, Color.DarkGray);
        private Bitmap image;

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }
        public Bitmap Image
        {
            get { return this.image; }
        }
        public int Width
        {
            get { return this.width; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("width", value, "Đối số ra khỏi phạm vi, phải lớn hơn số không");
                this.width = value;
            }
        }
        public int Height
        {
            get { return this.height; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("height", height, "Đối số ra khỏi phạm vi, phải lớn hơn số không");
                this.height = value;
            }
        }
        public string FontName
        {
            set
            {
                try
                {
                    Font font = new Font(this.familyName, 12F);
                    this.familyName = value;
                    font.Dispose();
                }
                catch
                {
                    this.familyName = System.Drawing.FontFamily.GenericSerif.Name;
                }
            }
        }
        public HatchBrush HatchBrushAnh
        {
            set { this.hatchBrushAnh = value; }
        }
        public HatchBrush HatchBrusText
        {
            set { this.hatchBrushText = value; }
        }

        //private Random random = new Random();

        public CaptchaImage()
        {
            this.Text = "";
            this.Width = 1;
            this.Height = 1;
        }
        public CaptchaImage(int width, int height)
        {
            this.Text = "";
            this.Width = width;
            this.Height = height;
        }
        public CaptchaImage(string s, int width, int height)
        {
            this.Text = s;
            this.Width = width;
            this.Height = height;
        }
        public CaptchaImage(string text, int width, int height, string fontName)
        {
            this.Text = text;
            this.Width = width;
            this.Height = height;
            this.FontName = fontName;
        }

        ~CaptchaImage()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                this.image.Dispose();
        }

        public void GenerateImage()
        {
            // Tạo mới 32-bit bitmap hình ảnh.
            Bitmap bitmap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);

            // Tạo một đối tượng đồ hoạ cho bản vẽ.
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.width, this.height);

            // Loc tren nen cua hinh
            g.FillRectangle(hatchBrushAnh, rect);

            // Thiết lập font chữ.
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            // Điều chỉnh cỡ chữ cho đến khi văn bản phù hợp trong hình ảnh.
            do
            {
                fontSize--;
                font = new Font(this.familyName, fontSize, FontStyle.Bold);
                size = g.MeasureString(this.text, font);
            } while (size.Width > rect.Width);

            // Thiết lập các định dạng văn bản.
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Tạo một đường dẫn bằng văn bản và dọc nó ngẫu nhiên.
            GraphicsPath path = new GraphicsPath();
            path.AddString(this.text, font.FontFamily, (int)font.Style, font.Size, rect, format);

            // tao hieu ung nghieng ngau nhien cho text
            //float v = 4F;
            //PointF[] points =
            //{
            //    new PointF(this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
            //    new PointF(rect.Width - this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
            //    new PointF(this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v),
            //    new PointF(rect.Width - this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v)
            //};
            //Matrix matrix = new Matrix();
            //matrix.Translate(0F, 0F);
            //path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

            // Phan hinh cua text
            g.FillPath(hatchBrushText, path);

            // Clean up.
            font.Dispose();
            hatchBrushAnh.Dispose();
            hatchBrushText.Dispose();
            g.Dispose();

            // Set the image.
            this.image = bitmap;
        }
    }
}
