using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;

namespace Evidencia_P2U3
{
    public class Caballo
    {
        private int PosicionInicio = 12;
        private int LargoCarrera = 802 - 120;
        public PictureBox ImagenCaballo = null;
        public int posicion = 0;
        public static Random RdmNum = new Random();

        public int PosicionInicio1 { get => PosicionInicio; set => PosicionInicio = value; }
        public int LargoCarrera1 { get => LargoCarrera; set => LargoCarrera = value; }

        public bool Correr(Caballo obj)
        {
            int distancia = RdmNum.Next(2, 6);
            if (obj.ImagenCaballo != null)
            { obj.MoverCaballo(distancia); }

            obj.posicion += distancia;

            if (obj.posicion >= (LargoCarrera1 - PosicionInicio1))
            { return true; }

            return false;
        }

        public void TomarPosicionIncial()
        {
            MoverCaballo(-posicion);
            posicion = 0;

        }

        public void MoverCaballo(int distancia)
        {
            Point p = ImagenCaballo.Location;
            p.X += distancia;
            if (ImagenCaballo.InvokeRequired)
            {
                ImagenCaballo.Location = p;
            }
        }
    }
}
