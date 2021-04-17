using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evidencia_P2U3
{
    public partial class Form1 : Form
    {
        Caballo[] caballos = new Caballo[4];
        FabPuntero fPunteros = new FabPuntero();
        Puntero[] punteros = new Puntero[4];

        private void PrepararCarrera()
        {
            Caballo.PosicionInicio1 = 0;
            Caballo.LargoCarrera1 = this.Width-10;

            caballos[0] = new Caballo() { ImagenCaballo = pctCaballo1 };
            caballos[1] = new Caballo() { ImagenCaballo = pctCaballo2 };
            caballos[2] = new Caballo() { ImagenCaballo = pctCaballo3 };
            caballos[3] = new Caballo() { ImagenCaballo = pctCaballo4 };

            punteros[0] = fPunteros.Obtener("Jessy", lblApuestaMax, JessyApuesta);
            punteros[1] = fPunteros.Obtener("Antonio", lblApuestaMax, AntonioApuesta);
            punteros[2] = fPunteros.Obtener("Megan", lblApuestaMax, MeganApuesta);
            punteros[3] = fPunteros.Obtener("ElPepe", lblApuestaMax, ElPepeApuesta);

            foreach (Puntero item in punteros)
                item.ActualizarLabel(); 

        }

        public Form1()
        {
            InitializeComponent();
            PrepararCarrera();
        }
        private void radioJessy_CheckedChanged(object sender, EventArgs e)
        {
            establecerApuestaMax(punteros[0].Dinero);
        }

        private void establecerApuestaMax(int Dinero)
        {
            lblApuestaMax.Text = string.Format($"Apuesta Máxima {Dinero}");
        }

        private void radioAntonio_CheckedChanged(object sender, EventArgs e)
        {
            establecerApuestaMax(punteros[1].Dinero);
        }

        private void radioMegan_CheckedChanged(object sender, EventArgs e)
        {
            establecerApuestaMax(punteros[2].Dinero);
        }

        private void radioElPepe_CheckedChanged(object sender, EventArgs e)
        {
            establecerApuestaMax(punteros[3].Dinero);
        }

        private void btnApuesta_Click(object sender, EventArgs e)
        {
            int NumPuntero = 0;

            if (radioJessy.Checked)
                NumPuntero = 0; 

            if (radioAntonio.Checked)
                NumPuntero = 1; 

            if (radioMegan.Checked)
                NumPuntero = 2;

            if (radioElPepe.Checked)
                NumPuntero = 3;

            punteros[NumPuntero].Apostar((int)CantidadApuesta.Value, (int)numCaballo.Value);
            punteros[NumPuntero].ActualizarLabel();
        }

        private void btnCarrera_Click(object sender, EventArgs e)
        {
            bool SnGanador = true;
            int CaballoGanador;
            btnCarrera.Enabled = false; //Desactiva el boton de carrera

            while (SnGanador)
            { 
                Application.DoEvents();

                //Thread c1 = new Thread(() => Caballo.Correr(caballos[0]));
                //Thread c2 = new Thread(() => Caballo.Correr(caballos[1]));
                //Thread c3 = new Thread(() => Caballo.Correr(caballos[2]));
                //Thread c4 = new Thread(() => Caballo.Correr(caballos[3]));

                for (int i = 0; i < caballos.Length; i++)
                {
                    if (Caballo.Correr(caballos[i]))
                    {
                        CaballoGanador = i + 1;
                        SnGanador = false;
                        MessageBox.Show($"Tenemos un ganador - Caballo #{CaballoGanador}");
                        foreach (Puntero punter in punteros)
                        {
                            if (punter.apuesta != null)
                            {
                                punter.Juntar(CaballoGanador); //Da el doble de cantidad a todos quienes ganaron la apuesta
                                punter.apuesta = null;
                                punter.ActualizarLabel();
                            }
                        }

                        foreach (Caballo horse in caballos)
                        {
                            horse.TomarPosicionIncial();
                        }

                        break;
                    }
                }
            }
            if (punteros[0].atrapado && punteros[1].atrapado && punteros[2].atrapado&& punteros[3].atrapado)
            {
                string mesaje = "Quieres volver a jugar?";
                string titulo = "Fin del Juego";
                MessageBoxButtons botones = MessageBoxButtons.YesNo;
                DialogResult resultado = MessageBox.Show(mesaje, titulo, botones);

                if (resultado==DialogResult.Yes)
                    PrepararCarrera();
                else
                    Close();

            }

            btnCarrera.Enabled = true;

        }

    }
}
