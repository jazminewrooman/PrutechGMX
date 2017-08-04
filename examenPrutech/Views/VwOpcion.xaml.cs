using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

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

        public List<opciones> Lst
		{
			get { return GetValue(LstProperty) as List<opciones>; }
			set { base.SetValue(LstProperty, value); }
		}
        public static readonly BindableProperty LstProperty = BindableProperty.Create(propertyName: "Lst", returnType: typeof(List<opciones>), declaringType: typeof(VwOpcion), defaultValue: new List<opciones>{ new opciones(){ idopc = "-1", opc = "seleccione" } }, defaultBindingMode: BindingMode.TwoWay, propertyChanged: Carga);

        private static void Carga(BindableObject bindable, object oldValue, object newValue){
        /*    var obj = bindable as VwOpcion;
            if (obj.Lst != null && !String.IsNullOrEmpty(obj.Title)){
                lo = new ListaOpciones(obj.Lst, obj.Title);
			}*/
        }

        public VwOpcion()
        {
            InitializeComponent();

			TapGestureRecognizer tap = new TapGestureRecognizer();
			tap.Tapped += async (s, e) =>
			{
                lo = new ListaOpciones(Lst, Title);
				stack.BackgroundColor = Color.FromHex("#e5e5e5");
				await Task.Delay(100);
				stack.BackgroundColor = Color.Transparent;
				lo.OpcionSeleccionada += (sender, ea) =>
				{
                    Detail = ea.sel.opc;
				};
				await Navigation.PushAsync(lo, true);
			};
			stack.GestureRecognizers.Add(tap);

		}
    }
}
