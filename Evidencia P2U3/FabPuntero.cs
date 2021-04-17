using System;
using System.Windows.Forms;
using Evidencia_P2U3.Caballos;


namespace Evidencia_P2U3
{
    public class FabPuntero
    {
        public Puntero Obtener(String Nombre, Label ApuestaMax, Label apuesta) 
        {
            Puntero puntero;
            switch (Nombre.ToLower())
            {
                case "antonio":
                    puntero = new Antonio(null, ApuestaMax, 50, apuesta);
                    break;
                
                case "jessy":
                    puntero = new Jessy(null, ApuestaMax, 50, apuesta);
                    break;
                
                case "megan":
                    puntero = new Megan(null, ApuestaMax, 50, apuesta);
                    break;

                case "elpepe":
                    puntero = new ElPepe(null, ApuestaMax, 50, apuesta);
                    break;

                default:
                    puntero = null;
                    break;
            }

            puntero.CambiarNombrePuntero();
            return puntero;
        }
    }
}
