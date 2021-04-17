using System.Windows.Forms;

namespace Evidencia_P2U3.Caballos
{
    class Jessy : Puntero
    {
        public Jessy(Apuesta MiApuesta, Label ApuestaMax, int Dinero, Label MyLabel): base (MiApuesta, ApuestaMax, Dinero, MyLabel) 
        {
        
        }

        public override void CambiarNombrePuntero() 
        {
            Nombre = "Jessy";
        }
    }
}
