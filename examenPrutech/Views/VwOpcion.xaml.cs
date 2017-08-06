using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GMX.Views
{
    public partial class VwOpcion : ContentView
    {
        private ListaOpciones lo;

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

        private static void LimpiaDetalle(BindableObject bindable, object oldValue, object newValue){
            var obj = bindable as VwOpcion;
            try
            {
                if (obj.IdDetail == null || String.IsNullOrEmpty(obj.IdDetail))
                    obj.Detail = " ";
            }catch{
                
            }
        }

        public VwOpcion()
        {
            InitializeComponent();
            Detail = " ";
			TapGestureRecognizer tap = new TapGestureRecognizer();
			tap.Tapped += async (s, e) =>
			{
                lo = new ListaOpciones(Lst, Title, SubTxt);
				stack.BackgroundColor = Color.FromHex("#e5e5e5");
				await Task.Delay(100);
				stack.BackgroundColor = Color.Transparent;
				lo.OpcionSeleccionada += (sender, ea) =>
				{
                    Detail = ea.sel.opc;
                    IdDetail = ea.sel.idopc;
				};
				await Navigation.PushAsync(lo, true);
			};
			stack.GestureRecognizers.Add(tap);

		}
    }
}
