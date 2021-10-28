using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace PKAD_EV_REJECTION_ANALYSIS
{
    public class EV_REJECTION_Renderer
    {
        private int width = 0, height = 0;
        private double totHeight = 750;
        private Bitmap bmp = null;
        private Graphics gfx = null;

        private List<EV_REJECTION_MODEL> data = null;
        Image logoImg = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "assets", "logo.png"));
        Image Img14 = Image.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "assets", "14.png"));
        public EV_REJECTION_Renderer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int getDataCount()
        {
            if (this.data == null) return 0;
            else return this.data.Count;
        }

        public List<EV_REJECTION_MODEL> getData()
        {
            return this.data;
        }

        public void setData( List<EV_REJECTION_MODEL> data)
        {
            this.data = data;
        }
        public void setRenderSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public Point convertCoord(Point a)
        {
            double px = height / totHeight;

            Point res = new Point();
            res.X = (int)(a.X * px);
            res.Y = (int)((totHeight - a.Y) * px);
            return res;
        }
        public PointF convertCoord(PointF p)
        {
            double px = height / totHeight;
            PointF res = new PointF();
            res.X = (int)(p.X * px);
            res.Y = (int)((totHeight - p.Y) * px);
            return res;
        }
        public Bitmap getBmp()
        {
            return this.bmp;
        }

        public void drawFilledCircle(Brush brush, Point o, Size size)
        {
            double px = height / totHeight;
            size.Width = (int)(size.Width * px);
            size.Height = (int)(size.Height * px);

            Rectangle rect = new Rectangle(convertCoord(o), size);

            gfx.FillEllipse(brush, rect);
        }
        public void fillRectangle(Color color, Rectangle rect)
        {
            rect.Location = convertCoord(rect.Location);
            double px = height / totHeight;
            rect.Width = (int)(rect.Width * px);
            rect.Height = (int)(rect.Height * px);

            Brush brush = new SolidBrush(color);
            gfx.FillRectangle(brush, rect);
            brush.Dispose();

        }
        public void drawRectangle(Pen pen, Rectangle rect)
        {
            rect.Location = convertCoord(rect.Location);
            double px = height / totHeight;
            rect.Width = (int)(rect.Width * px);
            rect.Height = (int)(rect.Height * px);
            gfx.DrawRectangle(pen, rect);
        }

        public void drawImg(Image img, Point o, Size size)
        {
            double px = height / totHeight;
            o = convertCoord(o);
            Rectangle rect = new Rectangle(o, new Size((int)(size.Width * px), (int)(size.Height * px)));
            gfx.DrawImage(img, rect);

        }
        public void drawString(Color color, Point o, string content, int font = 15)
        {

            o = convertCoord(o);

            // Create font and brush.
            Font drawFont = new Font("Arial", font);
            SolidBrush drawBrush = new SolidBrush(color);

            gfx.DrawString(content, drawFont, drawBrush, o.X, o.Y);

            drawFont.Dispose();
            drawBrush.Dispose();

        }

        public void drawCenteredString_withBorder(string content, Rectangle rect, Brush brush, Font font, Color borderColor)
        {

            //using (Font font1 = new Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Point))

            // Create a StringFormat object with the each line of text, and the block
            // of text centered on the page.
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            gfx.DrawString(content, font, brush, rect, stringFormat);

            Pen borderPen = new Pen(new SolidBrush(borderColor), 2);
            gfx.DrawRectangle(borderPen, rect);
            borderPen.Dispose();
        }
        public void drawCenteredImg_withBorder(Image img, Rectangle rect, Brush brush, Font font, Color borderColor)
        {
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            //gfx.DrawString(content, font, brush, rect, stringFormat);
            //drawImg(logoImg, new Point(20, 60), new Size(150, 50));
            gfx.DrawImage(img, rect);
            Pen borderPen = new Pen(new SolidBrush(borderColor), 2);
            gfx.DrawRectangle(borderPen, rect);
            borderPen.Dispose();
        }
        public void drawCenteredString(string content, Rectangle rect, Brush brush, Font font)
        {

            //using (Font font1 = new Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Point))

            // Create a StringFormat object with the each line of text, and the block
            // of text centered on the page.
            double px = height / totHeight;
            rect.Location = convertCoord(rect.Location);
            rect.Width = (int)(px * rect.Width);
            rect.Height = (int)(px * rect.Height);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            // Draw the text and the surrounding rectangle.
            gfx.DrawString(content, font, brush, rect, stringFormat);
            //gfx.DrawRectangle(Pens.Black, rect);

        }
        private void fillPolygon(Brush brush, PointF[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = convertCoord(points[i]);
            }
            gfx.FillPolygon(brush, points);
        }
        public void drawLine(Point p1, Point p2, Color color, int linethickness = 1)
        {
            if (color == null)
                color = Color.Gray;

            p1 = convertCoord(p1);
            p2 = convertCoord(p2);
            gfx.DrawLine(new Pen(color, linethickness), p1, p2);

        }
        public void drawString(Font font, Color brushColor, string content, Point o)
        {
            o = convertCoord(o);
            SolidBrush drawBrush = new SolidBrush(brushColor);
            gfx.DrawString(content, font, drawBrush, o.X, o.Y);
        }
        public void drawString(Point o, string content, int font = 15)
        {

            o = convertCoord(o);

            // Create font and brush.
            Font drawFont = new Font("Arial", font);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            gfx.DrawString(content, drawFont, drawBrush, o.X, o.Y);

        }

        public void drawPie(Color color, Point o, Size size, float startAngle, float sweepAngle)
        {
            // Create location and size of ellipse.
            double px = height / totHeight;
            size.Width = (int)(size.Width * px);
            size.Height = (int)(size.Height * px);

            Rectangle rect = new Rectangle(convertCoord(o), size);
            // Draw pie to screen.            
            Brush grayBrush = new SolidBrush(color);
            gfx.FillPie(grayBrush, rect, startAngle, sweepAngle);
        }

        public void draw (int pageID = 1)
        {
            if (bmp == null)
                bmp = new Bitmap(width, height);
            else
            {
                if (bmp.Width != width || bmp.Height != height)
                {
                    bmp.Dispose();
                    bmp = new Bitmap(width, height);

                    gfx.Dispose();
                    gfx = Graphics.FromImage(bmp);
                    gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                }
            }

            if (gfx == null)
            {
                gfx = Graphics.FromImage(bmp);
                gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            }
            else
            {
                gfx.Clear(Color.Transparent);
            }

            if (data == null) return;
            Pen blackBorderPen3 = new Pen(Color.Black, 3);

            Font totCountFont = new Font("Arial", 40, FontStyle.Bold, GraphicsUnit.Point);
            Font percentFont = new Font("Arial", 35, FontStyle.Bold, GraphicsUnit.Point);
            Font headertitle = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            Font headerText = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont10 = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont8 = new Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont7 = new Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont6 = new Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Point);
            Font textFont5 = new Font("Arial", 5, FontStyle.Regular, GraphicsUnit.Point);


            int rectHeight = 700, rectWidth = 700;
            int baseIndex = (pageID - 1) * 2;
            for (int col = 0; col < 2; col ++)
            {
                int baseLeft = 30 + col * (rectHeight + 40);
                int baseTop = 730;
                int index = baseIndex + col;

                if (index > data.Count - 1) break;
                //DRAW Headers
                drawRectangle(blackBorderPen3, new Rectangle(baseLeft, baseTop, rectWidth, rectHeight));
                fillRectangle(Color.CornflowerBlue, new Rectangle(baseLeft, baseTop, rectWidth / 2, 70));
                drawCenteredString("PKAD-EV-REJECTION ANALYSIS", new Rectangle(baseLeft, baseTop, rectWidth / 2, 70), Brushes.White, headertitle);

                fillRectangle(Color.Gray, new Rectangle(baseLeft + rectWidth / 2, baseTop, rectWidth / 2, 70));
                drawCenteredString(data[index].precinct + " " + data[index].name, new Rectangle(baseLeft + rectWidth / 2, baseTop, rectWidth / 2, 70), Brushes.White, headertitle);

                drawCenteredString("NO SIGNATURE", new Rectangle(baseLeft, baseTop - 70, rectWidth / 2, 50), Brushes.DimGray, headertitle);
                drawCenteredString("BAD SIGNATURE", new Rectangle(baseLeft + rectWidth / 2, baseTop - 70, rectWidth / 2, 50), Brushes.Red, headertitle);
                drawCenteredString("RETURNED LATE", new Rectangle(baseLeft , baseTop - rectHeight + 180, rectWidth / 2, 50), Brushes.LimeGreen, headertitle);

                if (data[index].total != 0)
                {
                    //Draw Pie
                    int pieRadius = 200;
                    Point o = new Point(baseLeft + rectWidth / 2, baseTop - rectHeight / 2);
                    Point redo = new Point(o.X + 50, o.Y);
                    float startAngle = 270, sweepAngle = 0;
                    sweepAngle = data[index].bs * 360 / (float)data[index].total; 
                    drawPie(Color.Red, new Point(o.X - pieRadius, o.Y + pieRadius), new Size(2 * pieRadius, 2 * pieRadius), startAngle, sweepAngle);

                    startAngle = startAngle + sweepAngle;
                    sweepAngle = data[index].L * 360 / (float)data[index].total;
                    Point greeno = new Point(o.X - 20, o.Y - 50);
                    drawPie(Color.LimeGreen, new Point(o.X - pieRadius, o.Y + pieRadius), new Size(2 * pieRadius, 2 * pieRadius), startAngle, sweepAngle);

                    startAngle = startAngle + sweepAngle;
                    sweepAngle = data[index].ns * 360 / (float)data[index].total;
                    Point grayo = new Point(o.X - 50, o.Y + 50);
                    drawPie(Color.Gray, new Point(o.X - pieRadius, o.Y + pieRadius), new Size(2 * pieRadius, 2 * pieRadius), startAngle, sweepAngle);


                    drawCenteredString(data[index].total.ToString(), new Rectangle(baseLeft + rectWidth - 150, baseTop - rectHeight +  200, 150, 150), Brushes.Black, totCountFont);



                    double percent = Math.Round( data[index].ns * 100/ (double)data[index].total, 2);
                    if (percent > 0) drawImg(Img14, new Point(baseLeft, baseTop - 100), new Size(80, 80));
                    drawCenteredString(percent.ToString() + "%", new Rectangle(baseLeft, baseTop - 110, rectWidth / 2, 70), Brushes.DimGray, percentFont);
                    
                    percent = Math.Round(data[index].bs * 100 / (double)data[index].total, 2);
                    if (percent > 0) drawImg(Img14, new Point(baseLeft + rectWidth - 80, baseTop - 100), new Size(80, 80));
                    drawCenteredString(percent.ToString() + "%", new Rectangle(baseLeft + rectWidth / 2, baseTop - 110, rectWidth / 2, 70), Brushes.Red, percentFont);

                    percent = Math.Round(data[index].L * 100 / (double)data[index].total, 2);
                    if (percent > 0) drawImg(Img14, new Point(baseLeft, baseTop - rectHeight + 140), new Size(80, 80));
                    drawCenteredString(percent.ToString() + "%", new Rectangle(baseLeft, baseTop - rectHeight + 140, rectWidth / 2, 70), Brushes.LimeGreen, percentFont);

                }

                //Draw Logo and Copyright
                drawImg(logoImg, new Point(baseLeft + rectWidth - 100, baseTop - rectHeight + 80), new Size(100, 50));
                string copyright = "©2021 Tesla Laboratories, llc & JHP";
                drawCenteredString(copyright, new Rectangle(baseLeft, baseTop - rectHeight + 50, rectWidth / 2, 50), Brushes.Black, textFont10);
            }
            blackBorderPen3.Dispose();
            headertitle.Dispose();
            headerText.Dispose();
            textFont10.Dispose();
            textFont8.Dispose();
            textFont7.Dispose();
            textFont6.Dispose();
            textFont5.Dispose();
            totCountFont.Dispose();

        }


    }
}
