﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace GMX.Views
{
    public partial class ListaOpciones : PopupPage
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

        public ListaOpciones()
        {
            InitializeComponent();
			//this.IsBackgroundAnimating = true;
			//this.IsCloseOnBackgroundClick = true;
			this.IsAnimating = true;
		}

        public ListaOpciones(ObservableCollection<opciones> ls, string title, string leyenda = "")
        {
            InitializeComponent();
            //Title = title;
            lvOpciones.IsGroupingEnabled = false;
            lvOpciones.ItemsSource = ls;
            lvOpciones.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;
                OnOpcionSeleccionada(new OpcionSeleccionadaEventArgs() { sel = (e.SelectedItem as opciones) });
                //await Navigation.PopAsync();
                await PopupNavigation.PopAsync();
                ((ListView)sender).SelectedItem = null;
            };
            lblLeyenda.Text = leyenda;
            var adjust = Device.OS != TargetPlatform.Android ? 1 : -ls.Count + 1;
            lvOpciones.HeightRequest = (ls.Count * (lvOpciones.RowHeight)) - adjust;
        }
    }


    public class opciones
    {
        public string idopc { get; set; }
        public string opc { get; set; }
        public bool sel { get; set; }
    }
}
