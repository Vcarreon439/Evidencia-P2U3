using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Evidencia_P2U3
{
    class Caballo
    {
        private static int PosicionInicio;
        private static int LargoCarrera;
        public PictureBox ImagenCaballo;
        public int posicion = 0;
        public static Random RdmNum = new Random();

        public static int PosicionInicio1 { get => PosicionInicio; set => PosicionInicio = value; }
        public static int LargoCarrera1 { get => LargoCarrera; set => LargoCarrera = value; }

        public static bool Correr(Caballo obj) 
        {
            int distancia = RdmNum.Next(2,6);
            if (obj.ImagenCaballo!=null)
                obj.MoverCaballo(distancia);

            obj.posicion += distancia;
            if (obj.posicion>=(distancia-PosicionInicio))
                return true;

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
            ImagenCaballo.Location = p;
        }

    }
}
