using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphEditor
{
    class Elements
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
            public float k, b;
            public TextBox txtBoxFrom, txtBoxTo;

            public Edge(float k, float b, TextBox txtBoxFrom, TextBox txtBoxTo)
            {
                this.k = k;
                this.b = b;
                this.txtBoxFrom = txtBoxFrom;
                this.txtBoxTo = txtBoxTo;
            }
        }
    }

}
