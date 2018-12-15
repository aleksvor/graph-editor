using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor
{
    namespace Elements
    {
        class Vertex
        {
            public TextBox txtBoxVertex;

            public Vertex(TextBox txtBoxVertex)
            {
                this.txtBoxVertex = txtBoxVertex;
            }
        }
        class Edge
        {
            public float k, b, x1, x2, y1, y2;
            public TextBox txtBoxFrom, txtBoxTo;

            public Edge(float k, float b,
                float x1, float x2,
                float y1, float y2,
                TextBox txtBoxFrom, TextBox txtBoxTo)
            {
                this.k = k;
                this.b = b;
                this.x1 = x1;
                this.x2 = x2;
                this.y1 = y1;
                this.y2 = y2;
                this.txtBoxFrom = txtBoxFrom;
                this.txtBoxTo = txtBoxTo;
            }
        }

    }

}
