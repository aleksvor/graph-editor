﻿using System;
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



namespace GraphEditor
{

    
    public partial class Form1 : Form
    {

        int nNumCurrentTextBox = 0;
        int nCurrentSize = 100;
        int nReSize = 10;
        TextBox[] txtBox = new TextBox[100];

        //для рисование рёбер
        TextBox tBoxEdge;
        bool selected1 = false;
        List<Edge> E = new List<Edge>();

        public int newTextBox()
        {
            
            try
            {
                txtBox[nNumCurrentTextBox] = new TextBox();
            }
            catch
            {
                Array.Resize<TextBox>(ref txtBox, nCurrentSize + nReSize);
                nCurrentSize = nCurrentSize + nReSize;
                //To Do не ресайзится
            }
            int CursorX = Cursor.Position.X;
            int CursorY = Cursor.Position.Y;

           
            //.Location.X не работает??? а так как сделал не правильно
            //txtBox[nNumCurrentTextBox].Left = CursorX;
            //txtBox[nNumCurrentTextBox].Top = CursorY;
            txtBox[nNumCurrentTextBox].Location = new System.Drawing.Point(Form1.MousePosition.X, Form1.MousePosition.Y); 

            txtBox[nNumCurrentTextBox].Multiline = true;
            txtBox[nNumCurrentTextBox].Width = 60;
            txtBox[nNumCurrentTextBox].Height = 60;
            txtBox[nNumCurrentTextBox].BackColor = Color.Silver;
            txtBox[nNumCurrentTextBox].TextAlign = HorizontalAlignment.Center; //To Do по центру надо а тут сверху
            System.Drawing.Drawing2D.GraphicsPath myPath =
                new System.Drawing.Drawing2D.GraphicsPath();
            myPath.AddEllipse(0, 0, txtBox[nNumCurrentTextBox].Width - 1, txtBox[nNumCurrentTextBox].Height - 1);
            Region myRegion = new Region(myPath);
            txtBox[nNumCurrentTextBox].Region = myRegion;
            txtBox[nNumCurrentTextBox].Text = Convert.ToString(nNumCurrentTextBox);

            // Пропушим себя ещё чтоб знать чо кликать
            txtBox[nNumCurrentTextBox].Click += delegate(object sender, EventArgs e)
            { clickVertex(sender, e); };

            txtBox[nNumCurrentTextBox].Parent = sheet;

            nNumCurrentTextBox++;

            return 0; 
        }
        public void clickVertex(object sender, EventArgs e)
        {
            for (int i = 0; i<nNumCurrentTextBox; i++)
            {
                txtBox[i].BackColor = Color.Silver;
            }
            TextBox tBox = (TextBox)sender; 
            tBox.BackColor = Color.Red;

            //Рисуем ребро
            if(drawEdgeButton.Enabled == false)
            {
                if(selected1)
                {

                    int x1 = tBoxEdge.Left + tBoxEdge.Width/2;
                    int y1 = tBoxEdge.Top + tBoxEdge.Height/2;
                    int x2 = tBox.Left + tBox.Width/2;
                    int y2 = tBox.Top + tBox.Height/2;
                    int k = (y2 - y1) / (x2 - x1);
                    int b = y1 - ((y2 - y1) * x1) / (x2 - x1);
                    System.Drawing.Pen koord; 
                    koord = new System.Drawing.Pen(System.Drawing.Color.Black);
                    System.Drawing.Graphics MyFormGrap = sheet.CreateGraphics(); 
                    MyFormGrap.DrawLine(koord, x1, y1, x2, y2); 
                    koord.Dispose(); 
                    MyFormGrap.Dispose();
                    

                    E.Add(new Edge(k, b, tBoxEdge, tBox));
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
                tBox.Parent = null;
                for (int i = 0; i < E.Count; i++)
                {
                    if ((E[i].txtBoxFrom.Text == tBox.Text) || (E[i].txtBoxTo.Text == tBox.Text))
                    {
                        E.RemoveAt(i);
                        Graphics g = Graphics.FromImage(sheet.Image);
                        g.FillRectangle(Brushes.White, 0, 0, 1000, 1000);
                        g.Dispose();
                        sheet.Invalidate();
                        for(int j = 0; j < E.Count; j++)
                        {
                            int x1 = E[j].txtBoxFrom.Left + E[j].txtBoxFrom.Width / 2;
                            int y1 = E[j].txtBoxFrom.Top + E[j].txtBoxFrom.Height / 2;
                            int x2 = E[j].txtBoxTo.Left + E[j].txtBoxTo.Width / 2;
                            int y2 = E[j].txtBoxTo.Top + E[j].txtBoxTo.Height / 2;
                            int k = (y2 - y1) / (x2 - x1);
                            int b = y1 - ((y2 - y1) * x1) / (x2 - x1);
                            System.Drawing.Pen koord;
                            koord = new System.Drawing.Pen(System.Drawing.Color.Black);
                            System.Drawing.Graphics MyFormGrap = sheet.CreateGraphics();
                            MyFormGrap.DrawLine(koord, x1, y1, x2, y2);
                            koord.Dispose();
                            MyFormGrap.Dispose();
                            sheet.Invalidate();
                        }
                    }
                }
            }
        }

        public int deleteSomething()
        {
            if (deleteButton.Enabled = false)
            {

            }
            return 0;
        }
        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(sheet.Width,sheet.Height);
            sheet.Image = bitmap;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //newTextBox();
        }


        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = false;
            selectButton.Enabled = true;
            deleteButton.Enabled = true;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = true;
            selectButton.Enabled = false;
            deleteButton.Enabled = true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = true;
            selectButton.Enabled = true;
            deleteButton.Enabled = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sheet_Click(object sender, EventArgs e)
        {
            newTextBox();
        }

    }
    class Edge
    {
        public int k, b;
        public TextBox txtBoxFrom, txtBoxTo;

        public Edge(int k, int b, TextBox txtBoxFrom, TextBox txtBoxTo)
        {
            this.k = k;
            this.b = b;
            this.txtBoxFrom = txtBoxFrom;
            this.txtBoxTo = txtBoxTo;
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
