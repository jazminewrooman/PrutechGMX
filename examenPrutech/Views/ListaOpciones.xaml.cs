using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GMX.Views
{
    public partial class ListaOpciones : ContentPage
    {
        public event EventHandler<OpcionSeleccionadaEventArgs> OpcionSeleccionada;
        protected virtual void OnOpcionSeleccionada(OpcionSeleccionadaEventArgs e)
        {
            var handler = OpcionSeleccionada;
            if (handler != null)
            {
                handler(this, e);
            }
        }

		public class OpcionSeleccionadaEventArgs : EventArgs
		{
            public opciones sel { get; set; }
		}

        public ListaOpciones(IEnumerable<opciones> ls, string title)
        {
            try
            {
                InitializeComponent();
                Title = title;
                lvOpciones.IsGroupingEnabled = false;
                lvOpciones.ItemsSource = ls;
                lvOpciones.ItemSelected += async (sender, e) =>
                {
                    if (e.SelectedItem == null)
                        return;
                    OnOpcionSeleccionada(new OpcionSeleccionadaEventArgs() { sel = (e.SelectedItem as opciones) });
                    await Navigation.PopAsync();
                    ((ListView)sender).SelectedItem = null;
                };
            }catch(Exception ex){
                
            }
        }
    }


    public class opciones{
        public string idopc { get; set; }
        public string opc { get; set; }
    }
}
