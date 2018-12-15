using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuickGraph;
using QuickGraph.Algorithms;
using GraphSharp;
using GraphEditor;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Drawing.Drawing2D;
using GraphEditor.Elements;

namespace GraphEditor
{


    public partial class Form1 : Form
    {

        int nNumCurrentTextBox = 0;
        int nCurrentSize = 100;
        int nReSize = 10;
        TextBox[] txtBox = new TextBox[100];
        List<Vertex> V = new List<Vertex>();

        bool isDrag = false;
        int txOx = 0;
        int txOy = 0;
        int scrollValue = 0;

        //для рисование рёбер
        TextBox tBoxEdge;
        bool selected1 = false;
        List<Edge> E = new List<Edge>();

        // координаы мыши
        int Ox;
        int Oy;

        public int newTextBox()
        {
            TextBox txtBox = new TextBox();
            try
            {
                //txtBox[nNumCurrentTextBox] = new TextBox();

                V.Add(new Vertex(txtBox));
            }
            catch
            {
                //Array.Resize<TextBox>(ref txtBox, nCurrentSize + nReSize);
                //nCurrentSize = nCurrentSize + nReSize;
                //To Do не ресайзится
            }
            //int CursorX = Cursor.Position.X;
            //int CursorY = Cursor.Position.Y;


            //.Location.X не работает??? а так как сделал не правильно
            //txtBox[nNumCurrentTextBox].Left = CursorX;
            //txtBox[nNumCurrentTextBox].Top = CursorY;

            txtBox.Location = new System.Drawing.Point(Ox, Oy);

            txtBox.Multiline = true;
            txtBox.Width = 60;
            txtBox.Height = 60;
            txtBox.BackColor = Color.Silver;
            txtBox.TextAlign = HorizontalAlignment.Center; //To Do по центру надо а тут сверху
            System.Drawing.Drawing2D.GraphicsPath myPath =
                new System.Drawing.Drawing2D.GraphicsPath();
            myPath.AddEllipse(0, 0, txtBox.Width - 1, txtBox.Height - 1);
            Region myRegion = new Region(myPath);
            txtBox.Region = myRegion;
            txtBox.Text = Convert.ToString(nNumCurrentTextBox);

            // Пропушим себя ещё чтоб знать чо кликать
            txtBox.MouseDown += delegate(object sender, MouseEventArgs e)
            { clickVertex(sender, e); };
            txtBox.MouseMove += delegate(object sender, MouseEventArgs e)
            { setMouseMove(sender, e); };
            txtBox.MouseUp += delegate(object sender, MouseEventArgs e)
            { isDragOff(sender, e); };

            txtBox.Parent = sheet;

            nNumCurrentTextBox++;

            return 0;
        }
        public void setMouseMove(object sender, MouseEventArgs e)
        {
            TextBox tBox = (TextBox)sender;
            if (selectButton.Enabled == false)
            {
                if (isDrag == true)
                {
                    {
                        Ox = tBox.Left + e.X;
                        Oy = tBox.Top + e.Y;
                        tBox.Location = new System.Drawing.Point(Ox, Oy);
                        reDrawAll();
                    }
                }
            }
            
        }
        public void isDragOff(object sender, MouseEventArgs e)
        {
            isDrag = false;
        }
        public void clickVertex(object sender, MouseEventArgs e)
        {
            isDrag = true;
            for (int i = 0; i < V.Count; i++)
            {
                V[i].txtBoxVertex.BackColor = Color.Silver;
            }
            TextBox tBox = (TextBox)sender;
            tBox.BackColor = Color.Red;
            propertyGrid1.SelectedObject = tBox;

            //Рисуем ребро
            if (drawEdgeButton.Enabled == false)
            {
                if (selected1)
                {

                    float x1 = tBoxEdge.Left + tBoxEdge.Width / 2;
                    float y1 = tBoxEdge.Top + tBoxEdge.Height / 2;
                    float x2 = tBox.Left + 10;
                    float y2 = tBox.Top + 10;
                    float k = (y2 - y1) / (x2 - x1);
                    float b = y1 - ((y2 - y1) * x1) / (x2 - x1);
                    System.Drawing.Pen koord;
                    koord = new System.Drawing.Pen(System.Drawing.Color.Black, 3);
                    AdjustableArrowCap bigArrow = new AdjustableArrowCap(7, 9);
                    koord.CustomStartCap = bigArrow;
                    //koord.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                    System.Drawing.Graphics MyFormGrap = sheet.CreateGraphics();
                    MyFormGrap.DrawLine(koord, x2, y2, x1, y1);
                    koord.Dispose();
                    MyFormGrap.Dispose();


                    E.Add(new Edge(k, b, x1, x2, y1, y2, tBoxEdge, tBox));
                    selected1 = false;

                }
                else
                {
                    selected1 = true;
                    tBoxEdge = tBox;
                }
            }
            //удаляем вершину
            if (deleteButton.Enabled == false)
            {
                for (int i = 0; i < V.Count; i++)
                {
                    if (V[i].txtBoxVertex == tBox)
                    {
                        V.RemoveAt(i);
                    }
                }
                //Двойной цикл потому что при одинарном походу сбивается i
                for (int i = 0; i < E.Count; i++)
                {
                    for (int j = 0; j < E.Count; j++)
                    {
                        if ((E[j].txtBoxFrom == tBox) || (E[j].txtBoxTo == tBox))
                        {
                            E.RemoveAt(j);
                        }
                    }
                }
                tBox.Parent = null;
                tBox = null;
                reDrawAll();
            }
        }

        public void reDrawAll()
        {
            try
            {
                Graphics g = Graphics.FromImage(sheet.Image);
                //System.Drawing.Graphics g = sheet.CreateGraphics();
                g.FillRectangle(Brushes.White, 0, 0, 1000, 1000);
                //g.Dispose();
                //sheet.Invalidate();
                for (int j = 0; j < E.Count; j++)
                {
                    int x1 = E[j].txtBoxFrom.Left + E[j].txtBoxFrom.Width / 2;
                    int y1 = E[j].txtBoxFrom.Top + E[j].txtBoxFrom.Height / 2;
                    int x2 = E[j].txtBoxTo.Left + 10;//E[j].txtBoxTo.Height / 2; ;
                    int y2 = E[j].txtBoxTo.Top + 10;//E[j].txtBoxTo.Height / 2; ;
                    int r = 30;
                    //double k1 = (y2 - y1);
                    //double k2 = (x2 - x1);
                    //double k = k1 / k2;

                    //float x1 = From.Left + From.Width / 2;
                    //float y1 = From.Top + From.Height / 2;
                    // float x2 = To.Left + To.Width / 2; ;
                    //float y2 = To.Top + To.Width / 2;
                    //float r = E[j].txtBoxTo.Width / 2;
                    float k1 = (y2 - y1);
                    float k2 = (x2 - x1);
                    float k = k1 / k2;
                    float b = -(y1 - ((y2 - y1) * x1) / (x2 - x1));
                    double petr = 4 * k * k * b * b;
                    double petr1 = 4 * (1 + k * k) * (b * b - r * r);
                    double d = Math.Sqrt(petr - petr1);
                    double resx1 = (-2 * k * b + d) / (2 * (1 + k * k));
                    double resx2 = (-2 * k * b - d) / (2 * (1 + k * k));
                    double resy1 = k * resx1 + b;
                    float resx = (float)resx1;
                    float resy = (float)resy1;
                    //int k = (y2 - y1) / (x2 - x1);
                    //int b = y1 - ((y2 - y1) * x1) / (x2 - x1);
                    System.Drawing.Pen koord;
                    koord = new System.Drawing.Pen(System.Drawing.Color.Black, 3);
                    //koord.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                    AdjustableArrowCap bigArrow = new AdjustableArrowCap(7, 9);
                    koord.CustomStartCap = bigArrow;
                    g.DrawLine(koord, x2, y2, x1, y1);
                    koord.Dispose();
                }
                g.Dispose();
                sheet.Invalidate();
            }
            catch { }
        }

        //Смотрим как рисовать долбаную стрелочку
        public void drawArrowMda(TextBox From, TextBox To)
        {
            float x1 = From.Left + From.Width / 2;
            float y1 = From.Top + From.Height / 2;
            float x2 = To.Left + To.Width / 2; ;
            float y2 = To.Top + To.Width / 2;
            float r = To.Width / 2;
            float k = (y2 - y1) / (x2 - x1);
            float b = y1 - ((y2 - y1) * x1) / (x2 - x1);

            double resx1 = (-b + Math.Sqrt(4 * k * k * b * b - 4 * (1 + 2 * k + k * k) * (b * b - r * r))) / (2 * (1 + 2 * k + k * k));
            double resx2 = (-b - Math.Sqrt(4 * k * k * b * b - 4 * (1 + 2 * k + k * k) * (b * b - r * r))) / (2 * (1 + 2 * k + k * k));
            double resy1 = k * resx1 + b;

        }

        public void deleteAll()
        {
            E.Clear();
            //Двойной цикл потому что при одинарном походу сбивается i
            for (int i = 0; i < V.Count; i++)
            {
                V[i].txtBoxVertex.Parent = null;
                V[i].txtBoxVertex = null;
            }
            V.Clear();
            reDrawAll();
        }

        public void checkForDelete()
        {
            for (int i = 0; i < E.Count; i++)
            {
                //Если попадаем в диапозон полосы
                //В прямоугольнике в коором она нарисована то удаляем
                if ((Oy - 8 < E[i].k * Ox + E[i].b) && ((Oy + 8 > E[i].k * Ox + E[i].b)))
                {
                    E.RemoveAt(i);
                }
            }
            reDrawAll();
        }

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(sheet.Width, sheet.Height);
            sheet.Image = bitmap;
            //vScrollBar1.Parent = sheet;
            //vScrollBar1.Visible = true;
            //sheet.Controls. Add(vScrollBar1);
            sheet.Height = 10000;
            
            //TextBox ffd = new TextBox();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //newTextBox();
        }


        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = false;
            drawVertexButton.Enabled = true;
            selectButton.Enabled = true;
            deleteButton.Enabled = true;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = true;
            drawVertexButton.Enabled = true;
            selectButton.Enabled = false;
            deleteButton.Enabled = true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = true;
            drawVertexButton.Enabled = true;
            selectButton.Enabled = true;
            deleteButton.Enabled = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sheet_Click(object sender, EventArgs e)
        {
            if (drawVertexButton.Enabled == false)
            {
                newTextBox();
            }
            checkForDelete();
        }

        private void sheet_MouseMove(object sender, MouseEventArgs e)
        {
            Ox = e.X;
            Oy = e.Y;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Save(saveFileDialog.FileName);
            }
        }

        //Сохраняем в xml
        public void Save(string fileName)
        {
            XDocument xdoc = new XDocument();
            XElement document = new XElement("document");
            XElement xeRectangles = new XElement("vertexs");
            XElement xeLines = new XElement("lines");
            //XElement xeComments = new XElement("comments");
            for (int i = 0; i < V.Count; i++)
            {
                XElement rec = new XElement("vertex");
                XAttribute Left = new XAttribute("Left", V[i].txtBoxVertex.Left);
                rec.Add(Left);
                XAttribute Top = new XAttribute("Top", V[i].txtBoxVertex.Top);
                rec.Add(Top);
                XAttribute Width = new XAttribute("Width", V[i].txtBoxVertex.Width);
                rec.Add(Width);
                XAttribute Heihgt = new XAttribute("Height", V[i].txtBoxVertex.Height);
                rec.Add(Heihgt);
                XAttribute Text = new XAttribute("Text", V[i].txtBoxVertex.Text);
                rec.Add(Text);
                xeRectangles.Add(rec);
            }
            for (int i = 0; i < E.Count; i++)
            {
                XElement rec = new XElement("Line");
                XAttribute x1 = new XAttribute("x1", E[i].x1);
                rec.Add(x1);
                XAttribute x2 = new XAttribute("x2", E[i].x2);
                rec.Add(x2);
                XAttribute y1 = new XAttribute("y1", E[i].y1);
                rec.Add(y1);
                XAttribute y2 = new XAttribute("y2", E[i].y2);
                rec.Add(y2);
                XAttribute k = new XAttribute("k", E[i].k);
                rec.Add(k);
                XAttribute b = new XAttribute("b", E[i].b);
                rec.Add(b);
                for (int j = 0; j < V.Count; j++)
                {
                    if (E[i].txtBoxFrom == V[j].txtBoxVertex)
                    {
                        XAttribute From = new XAttribute("From", j);
                        rec.Add(From);
                    }
                }
                for (int j = 0; j < V.Count; j++)
                {
                    if (E[i].txtBoxTo == V[j].txtBoxVertex)
                    {
                        XAttribute To = new XAttribute("To", j);
                        rec.Add(To);
                    }
                }
                xeLines.Add(rec);
            }
            document.Add(xeRectangles);
            document.Add(xeLines);
            //document.Add(xeComments);
            xdoc.Add(document);

            xdoc.Save(fileName);
        }

        private void deleteALLButton_Click(object sender, EventArgs e)
        {
            deleteAll();
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadAll(openFileDialog.FileName);
            }
        }

        //Восстанавливаем из xml
        public void LoadAll(string fileName)
        {
            deleteAll();
            XDocument xdoc = XDocument.Load(fileName);

            var xElements = xdoc.Elements("vertex");

            foreach (XElement el in xdoc.Root.Elements())
            {
                if (el.Name == "vertexs")
                {
                    foreach (XElement ell in el.Elements())
                    {
                        if (ell.Name == "vertex")
                        {
                            TextBox loadTxtBox = new TextBox();
                            loadTxtBox.Multiline = true;
                            loadTxtBox.Parent = sheet;
                            loadTxtBox.BackColor = Color.Silver;
                            loadTxtBox.Left = Int32.Parse(ell.Attribute("Left").Value);
                            loadTxtBox.Top = Int32.Parse(ell.Attribute("Top").Value);
                            loadTxtBox.Width = Int32.Parse(ell.Attribute("Width").Value);
                            loadTxtBox.Height = Int32.Parse(ell.Attribute("Height").Value);
                            loadTxtBox.Text = ell.Attribute("Text").Value;
                            loadTxtBox.TextAlign = HorizontalAlignment.Center; //To Do по центру надо а тут сверху
                            System.Drawing.Drawing2D.GraphicsPath myPath =
                                new System.Drawing.Drawing2D.GraphicsPath();
                            myPath.AddEllipse(0, 0, loadTxtBox.Width - 1, loadTxtBox.Height - 1);
                            Region myRegion = new Region(myPath);
                            loadTxtBox.Region = myRegion;
                            // Пропушим себя ещё чтоб знать чо кликать
                            loadTxtBox.MouseDown += delegate(object sender, MouseEventArgs e)
                            { clickVertex(sender, e); };
                            V.Add(new Vertex(loadTxtBox));
                        }

                    }
                }
                if (el.Name == "lines")
                {
                    foreach (XElement ell in el.Elements())
                    {
                        if (ell.Name == "line")
                        {
                            float x1 = float.Parse(ell.Attribute("x1").Value);
                            float x2 = float.Parse(ell.Attribute("x2").Value);
                            float y1 = float.Parse(ell.Attribute("y1").Value);
                            float y2 = float.Parse(ell.Attribute("y2").Value);
                            float k = float.Parse(ell.Attribute("k").Value);
                            float b = float.Parse(ell.Attribute("b").Value);
                            int from = Int32.Parse(ell.Attribute("From").Value);
                            int To = Int32.Parse(ell.Attribute("To").Value);
                            E.Add(new Edge(k, b, x1, x2, y1, y2,
                                V[from].txtBoxVertex,
                                V[To].txtBoxVertex));
                        }
                    }
                }
            }
            reDrawAll();
        }

        private void selectButton_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void selectButton_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void sheet_Paint(object sender, PaintEventArgs e)
        {

        }

        private void drawVertexButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = true;
            drawVertexButton.Enabled = false;
            selectButton.Enabled = true;
            deleteButton.Enabled = true;
        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            if (scrollValue < vScrollBar1.Value)
            {
                for (int i=0; i<V.Count; i++)
                {
                    V[i].txtBoxVertex.Top = V[i].txtBoxVertex.Top - 10 * (vScrollBar1.Value-scrollValue);
                }
            }
            if (scrollValue > vScrollBar1.Value)
            {
                for (int i = 0; i < V.Count; i++)
                {
                    V[i].txtBoxVertex.Top = V[i].txtBoxVertex.Top + 10 * (-vScrollBar1.Value + scrollValue);
                }
            }
            scrollValue = vScrollBar1.Value;
            reDrawAll();
        }
    }
}

static class GraphicsExtension
{
    private static void DrawCubicCurve(this Graphics graphics, Pen pen, float beta, float step, PointF start, PointF end, float a3, float a2, float a1, float a0, float b3, float b2, float b1, float b0)
    {
        float xPrev, yPrev;
        float xNext, yNext;
        bool stop = false;

        xPrev = beta * a0 + (1 - beta) * start.X;
        yPrev = beta * b0 + (1 - beta) * start.Y;

        for (float t = step; ; t += step)
        {
            if (stop)
                break;

            if (t >= 1)
            {
                stop = true;
                t = 1;
            }

            xNext = beta * (a3 * t * t * t + a2 * t * t + a1 * t + a0) + (1 - beta) * (start.X + (end.X - start.X) * t);
            yNext = beta * (b3 * t * t * t + b2 * t * t + b1 * t + b0) + (1 - beta) * (start.Y + (end.Y - start.Y) * t);

            graphics.DrawLine(pen, xPrev, yPrev, xNext, yNext);

            xPrev = xNext;
            yPrev = yNext;
        }
    }

    /// <summary>
    /// Draws a B-spline curve through a specified array of Point structures.
    /// </summary>
    /// <param name="pen">Pen for line drawing.</param>
    /// <param name="points">Array of control points that define the spline.</param>
    /// <param name="beta">Bundling strength, 0 <= beta <= 1.</param>
    /// <param name="step">Step of drawing curve, defines the quality of drawing, 0 < step <= 1</param>
    internal static void DrawBSpline(this Graphics graphics, Pen pen, PointF[] points, float beta, float step)
    {
        if (points == null)
            throw new ArgumentNullException("The point array must not be null.");

        if (beta < 0 || beta > 1)
            throw new ArgumentException("The bundling strength must be >= 0 and <= 1.");

        if (step <= 0 || step > 1)
            throw new ArgumentException("The step must be > 0 and <= 1.");

        if (points.Length <= 1)
            return;

        if (points.Length == 2)
        {
            graphics.DrawLine(pen, points[0], points[1]);
            return;
        }

        float a3, a2, a1, a0, b3, b2, b1, b0;
        float deltaX = (points[points.Length - 1].X - points[0].X) / (points.Length - 1);
        float deltaY = (points[points.Length - 1].Y - points[0].Y) / (points.Length - 1);
        PointF start, end;

        {
            a0 = points[0].X;
            b0 = points[0].Y;

            a1 = points[1].X - points[0].X;
            b1 = points[1].Y - points[0].Y;

            a2 = 0;
            b2 = 0;

            a3 = (points[0].X - 2 * points[1].X + points[2].X) / 6;
            b3 = (points[0].Y - 2 * points[1].Y + points[2].Y) / 6;

            start = points[0];
            end = new PointF
            (
              points[0].X + deltaX,
              points[0].Y + deltaY
            );

            graphics.DrawCubicCurve(pen, beta, step, start, end, a3, a2, a1, a0, b3, b2, b1, b0);
        }

        for (int i = 1; i < points.Length - 2; i++)
        {
            a0 = (points[i - 1].X + 4 * points[i].X + points[i + 1].X) / 6;
            b0 = (points[i - 1].Y + 4 * points[i].Y + points[i + 1].Y) / 6;

            a1 = (points[i + 1].X - points[i - 1].X) / 2;
            b1 = (points[i + 1].Y - points[i - 1].Y) / 2;

            a2 = (points[i - 1].X - 2 * points[i].X + points[i + 1].X) / 2;
            b2 = (points[i - 1].Y - 2 * points[i].Y + points[i + 1].Y) / 2;

            a3 = (-points[i - 1].X + 3 * points[i].X - 3 * points[i + 1].X + points[i + 2].X) / 6;
            b3 = (-points[i - 1].Y + 3 * points[i].Y - 3 * points[i + 1].Y + points[i + 2].Y) / 6;

            start = new PointF
            (
              points[0].X + deltaX * i,
              points[0].Y + deltaY * i
            );

            end = new PointF
            (
              points[0].X + deltaX * (i + 1),
              points[0].Y + deltaY * (i + 1)
            );

            graphics.DrawCubicCurve(pen, beta, step, start, end, a3, a2, a1, a0, b3, b2, b1, b0);
        }

        {
            a0 = points[points.Length - 1].X;
            b0 = points[points.Length - 1].Y;

            a1 = points[points.Length - 2].X - points[points.Length - 1].X;
            b1 = points[points.Length - 2].Y - points[points.Length - 1].Y;

            a2 = 0;
            b2 = 0;

            a3 = (points[points.Length - 1].X - 2 * points[points.Length - 2].X + points[points.Length - 3].X) / 6;
            b3 = (points[points.Length - 1].Y - 2 * points[points.Length - 2].Y + points[points.Length - 3].Y) / 6;

            start = points[points.Length - 1];

            end = new PointF
            (
              points[0].X + deltaX * (points.Length - 2),
              points[0].Y + deltaY * (points.Length - 2)
            );

            graphics.DrawCubicCurve(pen, beta, step, start, end, a3, a2, a1, a0, b3, b2, b1, b0);
        }
    }
}
