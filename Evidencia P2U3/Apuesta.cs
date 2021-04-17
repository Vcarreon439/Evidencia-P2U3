using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidencia_P2U3
{
    public class Apuesta
    {
        public int Cantidad;
        public int numCaballo;
        public Puntero apostador;

        public Apuesta(int cantidad, int numCaballo, Puntero Apostador)
        {
            this.Cantidad = cantidad;
            this.numCaballo = numCaballo;
            this.apostador = Apostador;
        }

        public string Descripcion() 
        {
            string desc = "";

            if (Cantidad>0)
                desc = string.Format($"{apostador.Nombre} apostó {Cantidad} al caballo {numCaballo}");
            else
                desc = string.Format($"{apostador.Nombre} no ha hecho ninguna apuesta");

            return desc;
        }

        public int Pagar(int Ganador) 
        {
            if (numCaballo==Ganador)
                return Cantidad;

            return -Cantidad;
        }


    }
}
