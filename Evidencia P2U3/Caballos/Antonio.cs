using System.Windows.Forms;

namespace Evidencia_P2U3.Caballos
{
    public class Antonio : Puntero
    {
        public Antonio(Apuesta MiApuesta, Label ApuestaMax, int Dinero, Label MyLabel) : base(MiApuesta, ApuestaMax, Dinero, MyLabel)
        {

        }

        public override void CambiarNombrePuntero()
        {
            Nombre = "Antonio";
        }

    }
}
