using System;
using System.Windows.Forms;

namespace Evidencia_P2U3.Caballos
{
    class ElPepe : Puntero
    {
        public ElPepe(Apuesta MiApuesta, Label ApuestaMax, int Dinero, Label MyLabel) : base(MiApuesta, ApuestaMax, Dinero, MyLabel)
        {

        }

        public override void CambiarNombrePuntero()
        {
            Nombre = "ElPepe";
        }
    }
}
