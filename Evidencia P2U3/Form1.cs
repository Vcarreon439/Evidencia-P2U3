using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evidencia_P2U3
{
    public partial class Form1 : Form
    {
        Caballo[] caballos = new Caballo[4];
        FabPuntero Factory = new FabPuntero();
        Puntero[] punteros = new Puntero[4];
        List<Thread> hilos = new List<Thread>();


        public Form1()
        {
            InitializeComponent();
            PrepararCarrera();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void PrepararCarrera()
        {
            caballos[0] = new Caballo() { ImagenCaballo = pctCaballo1 };
            caballos[1] = new Caballo() { ImagenCaballo = pctCaballo2 };
            caballos[2] = new Caballo() { ImagenCaballo = pctCaballo3 };
            caballos[3] = new Caballo() { ImagenCaballo = pctCaballo4 };

            punteros[0] = Factory.Obtener("Jessy", lblApuestaMax, JessyApuesta);
            punteros[1] = Factory.Obtener("Antonio", lblApuestaMax, AntonioApuesta);
            punteros[2] = Factory.Obtener("Megan", lblApuestaMax, MeganApuesta);
            punteros[3] = Factory.Obtener("ElPepe", lblApuestaMax, ElPepeApuesta);

            foreach (Puntero puntero in punteros)
            {
                puntero.ActualizarLabel();
            }
        }

        private void CrearHilos() 
        {
            hilos.Add(new Thread(() => Correr(0)));
            hilos.Add(new Thread(() => Correr(1)));
            hilos.Add(new Thread(() => Correr(2)));
            hilos.Add(new Thread(() => Correr(3)));
        }

        private void CambiarLabelApuestaMax(int Dinero)
        {
            lblApuestaMax.Text = string.Format($"Apuesta Maxima: ${Dinero}");
        }

        private void radioJessy_CheckedChanged(object sender, EventArgs e)
        {
            CambiarLabelApuestaMax(punteros[0].Dinero);
        }

        private void radioAntonio_CheckedChanged(object sender, EventArgs e)
        {
            CambiarLabelApuestaMax(punteros[1].Dinero);
        }

        private void radioMegan_CheckedChanged(object sender, EventArgs e)
        {
            CambiarLabelApuestaMax(punteros[2].Dinero);
        }

        private void radioElPepe_CheckedChanged(object sender, EventArgs e)
        {
            CambiarLabelApuestaMax(punteros[3].Dinero);
        }

        public bool Correr(int numCaballo)
        {
            while (true)
            {

                if (caballos[numCaballo].Correr(caballos[numCaballo]))
                {
                    foreach (var hilo in hilos.Where(hilo => hilo.ManagedThreadId != Thread.CurrentThread.ManagedThreadId))
                    {
                        hilo.Abort();
                    }
                    MessageBox.Show($" Gano el caballo numero {numCaballo + 1} ");
                    foreach (Puntero puntero in punteros)
                    {
                        if (puntero.apuesta != null)
                        {
                            puntero.Juntar(numCaballo); //Le damos el doble de cantidad a quien gano la apuesta
                            puntero.apuesta = null;
                            puntero.ActualizarLabel();
                        }
                    }
                    foreach (Caballo caballo in caballos)
                    {
                        caballo.TomarPosicionIncial();
                    }
                    return true;
                }
            }

        }

        private void btnCarrera_Click(object sender, EventArgs e)
        {
            try
            {
                CrearHilos();

                btnCarrera.Enabled = false; //DesactivarDesactivar el boton de carrera

                hilos[0].Start(); hilos[1].Start(); hilos[2].Start(); hilos[3].Start();
                hilos[0].Join(); hilos[1].Join(); hilos[2].Join(); hilos[3].Join();

                if (punteros[0].atrapado && punteros[1].atrapado && punteros[2].atrapado && punteros[3].atrapado)
                {
                    string mensaje = "Quieres Juegar de Nuevo?";
                    string title = "GAME OVER!";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show(mensaje, title, buttons);
                
                    if (result == DialogResult.Yes)
                        PrepararCarrera();
                    else
                        Close();
                }

                hilos.Clear();
                ReiniciarCaballos();

                btnCarrera.Enabled = true; //Activar de nuevo el boton
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReiniciarCaballos() 
        {
            Point[] p = new Point[]
            {
                new Point(12, pctCaballo1.Location.Y),
                new Point(12, pctCaballo2.Location.Y),
                new Point(12, pctCaballo3.Location.Y),
                new Point(12, pctCaballo4.Location.Y),
            };

            pctCaballo1.Location = p[0];
            pctCaballo2.Location = p[1];
            pctCaballo3.Location = p[2];
            pctCaballo4.Location = p[3];
        }

        // Establecer la apuesta para cada puntero y actualizando sus labels
        private void btnApuesta_Click(object sender, EventArgs e)
        {
            try
            {
                int PunterNum = 0;

                if (radioJessy.Checked)
                {
                    PunterNum = 0;
                }
                if (radioAntonio.Checked)
                {
                    PunterNum = 1;
                }
                if (radioMegan.Checked)
                {
                    PunterNum = 2;
                }
                if (radioElPepe.Checked)
                {
                    PunterNum = 3;
                }

                punteros[PunterNum].Apostar((int)CantidadApuesta.Value, (int)numCaballo.Value);
                punteros[PunterNum].ActualizarLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
