using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Angulos
{
    
    public partial class Form1 : Form
    {
        // Configurar el ángulo y la longitud
        double angulo = 45;
        double longitud = 1000;
        int Left = 17, Right = 17, Top = 182;

        public double Radianes(double angulo)
        {
            return angulo * (Math.PI / 180);
        }
        private void DibujarLineaConAngulo(Graphics g, Point origen, double anguloGrados, double longitud,Pen lapiz)
        {
            // Convertir ángulo a radianes
            double anguloRadianes = anguloGrados * (Math.PI / 180);

            // Calcular el punto final ajustando la orientación del eje Y
            int xFinal = origen.X + (int)(longitud * Math.Cos(anguloRadianes));
            int yFinal = origen.Y + (int)(longitud * Math.Sin(anguloRadianes)) * -1; // Ajuste del eje Y

            
            {
                // Dibujar la línea con el Pen personalizado
                g.DrawLine(lapiz, origen.X, origen.Y, xFinal, yFinal);
            }
        }
        private void DibujarPlanoCartesiano(Graphics g, int centroX, int centroY, int escala)
        {
            // Dibuja las líneas del eje
            g.DrawLine(Pens.Black, centroX, 0, centroX, panel1.Height); // Eje Y
            g.DrawLine(Pens.Black, 0, centroY, panel1.Width, centroY); // Eje X

            // Dibujar marcas de escala en el eje X
            for (int i = centroX; i < panel1.Width; i += escala)
            {
                g.DrawLine(Pens.Black, i, centroY - 5, i, centroY + 5);
            }
            for (int i = centroX; i > 0; i -= escala)
            {
                g.DrawLine(Pens.Black, i, centroY - 5, i, centroY + 5);
            }

            // Dibujar marcas de escala en el eje Y
            for (int i = centroY; i < panel1.Height; i += escala)
            {
                g.DrawLine(Pens.Black, centroX - 5, i, centroX + 5, i);
            }
            for (int i = centroY; i > 0; i -= escala)
            {
                g.DrawLine(Pens.Black, centroX - 5, i, centroX + 5, i);
            }
        }

        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Value = Convert.ToDecimal(angulo);
            MaximizeBox = false;

            this.BackColor = Color.DarkBlue;
            panel1.BackColor = Color.White;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {
                MessageBox.Show("Flecha derecha presionada!");
            }
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                numericUpDown1.Value = numericUpDown1.Value + 1;
            }
            else if(e.KeyCode == Keys.Left)
            {
                numericUpDown1.Value = numericUpDown1.Value - 1;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Centrar el Panel
            int centroX = panel1.Width / 2;
            int centroY = panel1.Height / 2;
            int escala = 20; // Espaciado de las líneas de la cuadrícula
            
            // Preparar para dibujar la línea
            Point origen = new Point(centroX, centroY);
            int grosor = 3;
            angulo = Convert.ToDouble(numericUpDown1.Value);

            using (Pen lapiz = new Pen(Color.Black, grosor)) // Define el lápiz con el que dibujarás la línea
            {
                DibujarLineaConAngulo(g, origen, angulo, longitud, lapiz);
            }
            // Llamar al método para dibujar el plano cartesiano
            DibujarPlanoCartesiano(g, centroX, centroY, escala);

            
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            panel1.Invalidate();
            if (numericUpDown1.Value < 90) {
                lbAngulo.Text = "Ángulo: Agudo";
            }
            else if(numericUpDown1.Value == 90)
            {
                 lbAngulo.Text = "Ángulo: Recto";
            }
            else if(numericUpDown1.Value > 90 && numericUpDown1.Value < 180)
            {
                lbAngulo.Text = "Ángulo: Obtuso";
            }
            else if(numericUpDown1.Value == 180)
            {
                lbAngulo.Text = "Ángulo: Llano";
            }
            else if(numericUpDown1.Value >180 && numericUpDown1.Value < 360)
            {
                lbAngulo.Text = "Ángulo: Concavo";
            }
            else if(numericUpDown1.Value == 360)
            {
                lbAngulo.Text = "Ángulo: Completo";
            }

        }
    }
}
