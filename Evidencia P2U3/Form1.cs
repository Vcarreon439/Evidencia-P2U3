using System;
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

        //Parte de hilos
        Thread[] hilos = new Thread[4];
        delegate bool Delegado(Caballo[] caballos1);

        public Form1()
        {
            InitializeComponent();
            PrepararCarrera();
        }
        
        private void PrepararCarrera()
        {

            Caballo.PosicionInicio1 = pctCaballo1.Right;
            Caballo.LargoCarrera1 = racetrack.Size.Width; //Estableciendo lo largo de la carrera

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

        private void btnCarrera_Click(object sender, EventArgs e)
        {
            bool NoWinner = true;
            int caballoGanador;
            btnCarrera.Enabled = false; //DesactivarDesactivar el boton de carrera
            while (NoWinner)
            { // loop hasta tenehasta tener un ganador
                Application.DoEvents();
                for (int i = 0; i < caballos.Length; i++)
                {
                    if (Caballo.Correr(caballos[i]))
                    {
                        caballoGanador = i + 1;
                        NoWinner = false;
                        MessageBox.Show($"Tenemos un ganador - Horse #{caballoGanador}");
                        foreach (Puntero puntero in punteros)
                        {
                            if (puntero.apuesta != null)
                            {
                                puntero.Juntar(caballoGanador); //Le damos el doble de cantidad a quien gano la apuesta
                                puntero.apuesta = null;
                                puntero.ActualizarLabel();
                            }
                        }
                        foreach (Caballo caballo in caballos)
                        {
                            caballo.TomarPosicionIncial();
                        }
                        break;
                    }
                }
            }
            if (punteros[0].atrapado && punteros[1].atrapado && punteros[2].atrapado && punteros[3].atrapado)
            {
                string mensaje = "Quieres Juegar de Nuevo?";
                string title = "GAME OVER!";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(mensaje, title, buttons);
                if (result == DialogResult.Yes)
                {
                    PrepararCarrera();
                }
                else
                {
                    Close();
                }
            }
            btnCarrera.Enabled = true; //Activar de nuevo el boton
        }

        // Establecer la apuesta para cada puntero y actualizando sus labels
        private void btnApuesta_Click(object sender, EventArgs e)
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
    }
}
