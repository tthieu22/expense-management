using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CircularPictureBox : PictureBox
{
    protected override void OnPaint(PaintEventArgs pe)
    {
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(0, 0, this.Width, this.Height);
        this.Region = new Region(path);
        base.OnPaint(pe);
    }
}
