using System;
using System.Drawing;
using System.Windows.Forms;

namespace T2_Trasformaciones2D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Point[] Poligono;
        private Point[] PoligonoEs;
        private Point[] PoligonoTr;
        private Graphics g;
        private int cX, cY;

        

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            Pen pluma = new Pen(Color.Black);
            Font f = new Font("Arial", 10);
            Brush b = new SolidBrush(Color.Black);
            g = e.Graphics;

            cX = pictureBox1.Width / 2;
            cY = pictureBox1.Height / 2;

            g.DrawLine(pluma, cX, 0, cX, pictureBox1.Height);
            g.DrawLine(pluma, 0, cY, pictureBox1.Width, cY);

            for (int i = -15; i <= 15; i++)
            {
                int x = cX + i * 20;
                int y = cY - i * 20;

                g.DrawString(i.ToString(), f, b, new PointF(x, cY));
                g.DrawString(i.ToString(), f, b, new PointF(cX, y));
            }

            //inicia Poligono orininal 
            if(Poligono != null)
            {
                Point[] puntosPantalla = new Point[Poligono.Length]; 

                for (int i = 0; i < Poligono.Length; i++)
                {
                    puntosPantalla[i].X = Poligono[i].X * 20 + cX;
                    puntosPantalla[i].Y = cY - Poligono[i].Y * 20;
                }

                g.DrawPolygon(Pens.Red, puntosPantalla);
            }
            //termina Poligono original

            //inicia escalacion
            if(checkBox1.Checked) 
            {
                try
                {
                    int x1 = int.Parse(textBox1.Text);
                    int y1 = int.Parse(textBox4.Text);
                    int x2 = int.Parse(textBox2.Text);
                    int y2 = int.Parse(textBox5.Text);
                    int x3 = int.Parse(textBox3.Text);
                    int y3 = int.Parse(textBox6.Text);
                    int Sx = int.Parse(textBox7.Text);
                    int Sy = int.Parse(textBox11.Text);

                    Point[] PoligonoEs = new Point[]
                    {
                        new Point(x1 * Sx, y1 * Sy),
                        new Point(x2 * Sx, y2 * Sy),
                        new Point(x3 * Sx, y3 * Sy)
                    };

                    for (int i = 0; i < PoligonoEs.Length; i++)
                    {
                        PoligonoEs[i].X = PoligonoEs[i].X * 20 + cX;
                        PoligonoEs[i].Y = cY - PoligonoEs[i].Y * 20;
                    }
                    g.DrawPolygon(Pens.Blue, PoligonoEs);
                }
                catch (FormatException)
                {

                }
            }
            //Termina escalacion

            //inicia traslacion
            if (checkBox2.Checked)
            {
                try
                {
                    int x1 = int.Parse(textBox1.Text);
                    int y1 = int.Parse(textBox4.Text);
                    int x2 = int.Parse(textBox2.Text);
                    int y2 = int.Parse(textBox5.Text);
                    int x3 = int.Parse(textBox3.Text);
                    int y3 = int.Parse(textBox6.Text);
                    int Tx = int.Parse(textBox9.Text);
                    int Ty = int.Parse(textBox12.Text);

                    Point[] PoligonoTr = new Point[]
                    {
                        new Point(x1 + Tx, y1 + Ty),
                        new Point(x2 + Tx, y2 + Ty),
                        new Point(x3 + Tx, y3 + Ty)
                    };

                    for (int i = 0; i < PoligonoTr.Length; i++)
                    {
                        PoligonoTr[i].X = PoligonoTr[i].X * 20 + cX;
                        PoligonoTr[i].Y = cY - PoligonoTr[i].Y * 20;
                    }
                    g.DrawPolygon(Pens.Green, PoligonoTr);
                }
                catch (FormatException)
                {

                }
            }
            //Termina traslacion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Poligono = null;
            
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();

            checkBox1.Checked = false;
            checkBox2.Checked = false;

            pictureBox1.Invalidate();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //Escalamiento
            if (checkBox1.Checked)
            {
                bool cordenadas = 
                    string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text) ||
                    string.IsNullOrWhiteSpace(textBox6.Text);

                bool escala = 
                    string.IsNullOrWhiteSpace(textBox7.Text) ||
                    string.IsNullOrWhiteSpace(textBox11.Text);

                if (cordenadas || escala)
                {
                    MessageBox.Show("Por favor, llena las coordenadas del polígono y los valores de escala antes de marcar esta opción.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    checkBox1.Checked = false;
                    return;
                }

                if (Poligono == null)
                {
                    MessageBox.Show("Primero debes hacer clic en 'Dibujar Poligono' antes de aplicarle transformaciones.", "Acción requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    checkBox1.Checked = false;
                    return;
                }
            }
            pictureBox1.Invalidate();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //Traslacion
            if (checkBox2.Checked)
            {
                bool cordenadas =
                    string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text) ||
                    string.IsNullOrWhiteSpace(textBox6.Text);

                bool escala =
                    string.IsNullOrWhiteSpace(textBox9.Text) ||
                    string.IsNullOrWhiteSpace(textBox12.Text);

                if (cordenadas || escala)
                {
                    MessageBox.Show("Por favor, llena las coordenadas del polígono y los valores de traslado antes de marcar esta opción.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    checkBox1.Checked = false;
                    return;
                }

                if (Poligono == null)
                {
                    MessageBox.Show("Primero debes hacer clic en 'Dibujar Poligono' antes de aplicarle transformaciones.", "Acción requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    checkBox2.Checked = false;
                    return;
                }
            }
            pictureBox1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Poligono original
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Faltan las coordenadas");
                return;
            }

            int x1 = int.Parse(textBox1.Text);
            int y1 = int.Parse(textBox4.Text);
            int x2 = int.Parse(textBox2.Text);
            int y2 = int.Parse(textBox5.Text);
            int x3 = int.Parse(textBox3.Text);
            int y3 = int.Parse(textBox6.Text);

            Poligono = new Point[]
            {
                new Point(x1, y1),
                new Point(x2, y2),
                new Point(x3, y3)
            };

            pictureBox1.Invalidate();
        }
    }
}