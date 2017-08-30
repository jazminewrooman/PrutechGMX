using System; using System.Collections.Generic; using Acr.UserDialogs; using Xamarin.Forms;
 namespace GMX.Views
{
	public partial class ResultadoBusqueda : ContentPage
	{
        public ResultadoBusqueda(DateTime FechaDesde, DateTime FechaHasta)
        {             try
            {

                InitializeComponent();

                var vm = new VMResultadoBusqueda(FechaDesde, FechaHasta, UserDialogs.Instance, Navigation);
                BindingContext = vm;
                Title = "Póizas Emitidas";             }
            catch (Exception ex)
            {

            }
		}
	}
}
