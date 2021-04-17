using System;
using System.Windows.Forms;

namespace Evidencia_P2U3
{
    public abstract class Puntero
    {
        public string Nombre;
        public Apuesta apuesta;
        public int Dinero;
        public bool atrapado;
        public Label LabelEstado, ApuestaMax;
        public abstract void CambiarNombrePuntero();

        public Puntero(Apuesta apuesta, Label ApuestaMax, int Dinero, Label LabelEstado) 
        {
            this.apuesta = apuesta;
            this.Dinero = Dinero;
            this.ApuestaMax = ApuestaMax;
            this.LabelEstado = LabelEstado;
            if (this.LabelEstado != null)
                this.LabelEstado.ForeColor = System.Drawing.Color.Black;
        }

        public void ActualizarLabel() 
        {
            if (apuesta == null) 
                LabelEstado.Text = String.Format($"{Nombre} no tiene ninguna apuesta");
            else
            {
                LabelEstado.Text = apuesta.Descripcion();
            }
            if (Dinero == 0)
            {
                atrapado = true;
                LabelEstado.Text = string.Format("Ya no tienes dinero");
                LabelEstado.ForeColor = System.Drawing.Color.Red;
            }
            ApuestaMax.Text = string.Format($"Apuesta máxima {Dinero}");
        }

        public bool Apostar(int Cantidad, int caballo) 
        {
            if (Cantidad<= Dinero)
            {
                apuesta = new Apuesta(Cantidad, caballo, this);
                return true;
            }
            return false;
        }

        public void Juntar(int ganador) 
        {
            Dinero += apuesta.Pagar(ganador);
        }

    }
}
