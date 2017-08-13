using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using Rg.Plugins.Popup.Extensions;

namespace GMX.Views
{
    public partial class VwOpcion : ContentView
    {
        private ListaOpciones lo;

        public bool ClickAuto
		{
			get { return (bool)GetValue(ClickAutoProperty); }
			set { base.SetValue(ClickAutoProperty, value); }
		}
        public static readonly BindableProperty ClickAutoProperty = BindableProperty.Create(propertyName: "ClickAuto", returnType: typeof(bool), declaringType: typeof(VwOpcion), defaultValue: false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: clickaauto);

		public Color TitleColor
		{
            get { return (Color)GetValue(TitleColorProperty); }
			set { base.SetValue(TitleColorProperty, value); }
		}
        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(propertyName: "TitleColor", returnType: typeof(Color), declaringType: typeof(VwOpcion), defaultValue: Color.Black, defaultBindingMode: BindingMode.TwoWay);

		public string Title
		{
			get { return GetValue(TitleProperty).ToString(); }
			set { base.SetValue(TitleProperty, value); }
		}
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(propertyName: "Title", returnType: typeof(string), declaringType: typeof(VwOpcion), defaultValue: "", defaultBindingMode: BindingMode.TwoWay);

		public string Detail
		{
			get { return GetValue(DetailProperty).ToString(); }
			set { base.SetValue(DetailProperty, value); }
		}
		public static readonly BindableProperty DetailProperty = BindableProperty.Create(propertyName: "Detail", returnType: typeof(string), declaringType: typeof(VwOpcion), defaultValue: "", defaultBindingMode: BindingMode.TwoWay);

		public string SubTxt
		{
			get { return GetValue(SubTxtProperty).ToString(); }
			set { base.SetValue(SubTxtProperty, value); }
		}
		public static readonly BindableProperty SubTxtProperty = BindableProperty.Create(propertyName: "SubTxt", returnType: typeof(string), declaringType: typeof(VwOpcion), defaultValue: "", defaultBindingMode: BindingMode.TwoWay);

		public string IdDetail
		{
			get { return GetValue(IdDetailProperty).ToString(); }
			set { base.SetValue(IdDetailProperty, value); }
		}
        public static readonly BindableProperty IdDetailProperty = BindableProperty.Create(propertyName: "IdDetail", returnType: typeof(string), declaringType: typeof(VwOpcion), defaultValue: "", defaultBindingMode: BindingMode.TwoWay, propertyChanged:LimpiaDetalle);
		
        public ObservableCollection<opciones> Lst
		{
			get { return GetValue(LstProperty) as ObservableCollection<opciones>; }
			set { base.SetValue(LstProperty, value); }
		}
        public static readonly BindableProperty LstProperty = BindableProperty.Create(propertyName: "Lst", returnType: typeof(ObservableCollection<opciones>), declaringType: typeof(VwOpcion), defaultValue: new ObservableCollection<opciones>{ new opciones(){ idopc = "-1", opc = "seleccione" } }, defaultBindingMode: BindingMode.TwoWay);

        private static void LimpiaDetalle(BindableObject bindable, object oldValue, object newValue)
        {
            var obj = bindable as VwOpcion;
            try
            {
                if (obj.IdDetail == null || String.IsNullOrEmpty(obj.IdDetail))
                {
                    obj.Detail = " ";
                    obj.TitleColor = Color.Red;
                }
                else
                {
                    obj.Detail = obj.Lst.Where(x => x.idopc == obj.IdDetail).FirstOrDefault().opc;
					obj.TitleColor = Color.Black;
                }
            }
            catch
            {

            }
        }

        private static async void clickaauto(BindableObject bindable, object oldValue, object newValue)
        {
            var obj = (bindable as VwOpcion);
			obj.lo = new ListaOpciones(obj.Lst, obj.Title, obj.SubTxt);
			obj.stack.BackgroundColor = Color.FromHex("#e5e5e5");
			await Task.Delay(100);
			obj.stack.BackgroundColor = Color.Transparent;
			obj.lo.OpcionSeleccionada += obj.muestraopciones;
			await obj.Navigation.PushPopupAsync(obj.lo, true);
        }

        private void muestraopciones(object sender, ListaOpciones.OpcionSeleccionadaEventArgs ea){
			Detail = ea.sel.opc;
			IdDetail = ea.sel.idopc;
			TitleColor = Color.Black;
			foreach (opciones o in Lst)
			{
				if (o.idopc == ea.sel.idopc)
					o.sel = true;
				else
					o.sel = false;
			}
		}

        private async Task Clicka(){
			lo = new ListaOpciones(Lst, Title, SubTxt);
			stack.BackgroundColor = Color.FromHex("#e5e5e5");
			await Task.Delay(100);
			stack.BackgroundColor = Color.Transparent;
            lo.OpcionSeleccionada += muestraopciones;
			//await Navigation.PushAsync(lo, true);
			await Navigation.PushPopupAsync(lo, true);
        }

        public VwOpcion()
        {
            InitializeComponent();
            Detail = " ";
			TapGestureRecognizer tap = new TapGestureRecognizer();
			tap.Tapped += async (s, e) =>
			{
                await Clicka();    
			};
			stack.GestureRecognizers.Add(tap);
            if (String.IsNullOrEmpty(Detail.Trim()))
            {
                TitleColor = Color.Red;
                Title = "*" + Title;
            }
            else
                TitleColor = Color.Black;
		}
    }
}
