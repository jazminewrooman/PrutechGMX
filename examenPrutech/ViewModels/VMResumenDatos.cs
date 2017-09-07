using System;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Collections.Generic;
using GMX.Views;
using System.Collections.ObjectModel;
using GMX.Services.DTOs;
using GMX.Services;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using GMXHelper;

namespace GMX
{
    public class VMResumenDatos : VMGmx
    {
        IUserDialogs diag;
        VMCotizar vmcotizar;
        INavigation nav;
        public ICommand EmisionCommand { get; private set; }
		public ICommand VerCotizaCommand { get; private set; }
        public ICommand SelectList { get; private set; }

        public VMResumenDatos(IUserDialogs diag, INavigation n, VMCotizar vmc) : base(diag)
        {
            nav = n;
            vmcotizar = vmc;
            this.diag = diag;
            ObservableCollection<resum> lst = new ObservableCollection<resum>();
            lst.Add(new resum { id=1, opc = "Datos Generales" });
            lst.Add(new resum { id=2, opc = "Datos Fiscales" });
            lst.Add(new resum { id=3, opc ="Datos Profesionales"});
            lst.Add(new resum { id=4, opc = "Datos Bancarios"});

			ListaDatos = lst;
			
            VerCotizaCommand = new Command(async () =>
			{
				await nav.PushPopupAsync(new VerCotiza(vmcotizar), true);
			});
			EmisionCommand = new Command(async () =>
			{
                string msg = $"Se mandara la emision de la siguiente poliza:{Environment.NewLine}Prima Neta: {vmcotizar.PrimaNeta.ToString("c")}{Environment.NewLine}Derechos: {vmcotizar.Derechos.ToString("c")}{Environment.NewLine}Subtotal:{vmcotizar.SubTotal.ToString("c")}{Environment.NewLine}IVA:{vmcotizar.Iva.ToString("c")}{Environment.NewLine}Total:{vmcotizar.PrimaTotal.ToString("c")}{Environment.NewLine}{Environment.NewLine}¿Desea continuar?";
                var result = await diag.ConfirmAsync(msg, "Aviso", "Ok", "Cancelar");
                if (result)
                {
                    await vmcotizar.MandarEmision();
                    if (vmcotizar.PolizaGenerada != null && !String.IsNullOrEmpty(vmcotizar.PolizaGenerada.NumPoliza))
                    {
                        if (vmcotizar.DatosBank != null && vmcotizar.DatosBank.TipoTarj != CreditCardValidator.CardIssuer.Unknown)
                            vmcotizar.MandarPagar();
                        wsbd.Service wsbd = new GMX.wsbd.Service(config.Config["APIBD"]);
                        //wsbd.insert_Emision();

                        await EnviaConfirmacion();
                        await EnviaAvisoVenta();

						var conf = new Confirmacion(vmcotizar);
						var MainP = new NavigationPage(conf)
						{
							BarTextColor = Color.FromHex("#04b5b5"),
							BarBackgroundColor = Color.White,
						};
						var md = new MasterDetailPage();
						md.Master = new menu();
						md.Detail = MainP;
						App.Current.MainPage = md;
						await nav.PopToRootAsync(true);
                    }
                }
			});

            SelectList = new Command( (e) =>
            {
                /*if (e.SelectedItem == null)
                    return;
                OnOpcionSeleccionada(new SelectedOptionEventArgs() {sel = (e.SelectedItem as resum)});
                SeleccionaLista(e.SelectedItem);*/
            });
		}

		private async Task EnviaAvisoVenta()
		{
			try
			{
				Ocupado = true;
				bindings b = new bindings();
				b.IniciaWS(apidoc: config.Config["APIDocs"]);
				var waterm = await b.ReturnDocument("Watermark.jpg", false);
				
                b.IniciaWS();
                var caratula = await b.getPolicy(vmcotizar.PolizaGenerada.PolizaGenerada, "caratula");
				var file_caratula = new FilePropertiesManager
				{
					stream = caratula.Result,
					fileName = "Poliza.pdf",
					length = caratula.Result.Length
				};
				var incendio = await b.getPolicy(vmcotizar.PolizaGenerada.PolizaGenerada, "master_incendio");
				var file_inc = new FilePropertiesManager
				{
					stream = incendio.Result,
					fileName = "DetalleCobertura.pdf",
					length = incendio.Result.Length
				};

				if (vmcotizar.IdTipo == "1")//nueva
					GMX.ViewModels.Emails.SlipTradicional(vmcotizar.PolizaGenerada.NumPoliza, vmcotizar.Adicional, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.Especialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, vmcotizar.SumaAseg);
				if (vmcotizar.IdTipo == "2")//renovacion
					GMX.ViewModels.Emails.SlipTradicionalRenov(vmcotizar.PolizaGenerada.NumPoliza, vmcotizar.Adicional, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.Especialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, vmcotizar.SumaAseg, $"{vmcotizar.Antecedentes.dia}/{vmcotizar.Antecedentes.mes}/{vmcotizar.Antecedentes.anno}", (vmcotizar.Antecedentes.poliza1 != null ? vmcotizar.Antecedentes.poliza1.poliza : String.Empty), (vmcotizar.Antecedentes.poliza2 != null ? vmcotizar.Antecedentes.poliza2.poliza : String.Empty), (vmcotizar.Antecedentes.poliza3 != null ? vmcotizar.Antecedentes.poliza3.poliza : String.Empty));
				GMX.ViewModels.Emails.docPDF.Watermark = waterm.Result;
				b.IniciaWS(api: config.Config["APIGMXIT"]);
				var condpart = await b.GenerateDocument(GMX.ViewModels.Emails.Sections.ToArray(), GMX.ViewModels.Emails.docPDF);
				var slip_condpart = new FilePropertiesManager { stream = condpart.Result, fileName = "CondicionesParticulares.pdf", length = condpart.Result.Length };

                var res = await b.DistribuirDocumentacionAviso(vmcotizar.DatosGrales, vmcotizar.PolizaGenerada.NumPoliza, file_caratula, slip_condpart, new FilePropertiesManager[] { file_inc });

				Ocupado = false;
				if (res.Result)
					await diag.AlertAsync("Correo enviado con exito", "Aviso", "OK");
				else
					await diag.AlertAsync("El correo no pudo ser enviado", "Error", "OK");
			}
			catch (Exception ex)
			{
				Ocupado = false;
				await diag.AlertAsync("El correo no pudo ser enviado", "Error", "OK");
			}
		}

        private async Task EnviaConfirmacion()
        {
            try
            {
                Ocupado = true;
                bindings b = new bindings();
				b.IniciaWS(apidoc: config.Config["APIDocs"]);
				var waterm = await b.ReturnDocument("Watermark.jpg", false);
                var condgen = await b.ReturnDocument("W_RCMedML_Ind_20.07.2016.2.pdf", false);
				var file_condi_gral = new FilePropertiesManager
				{
                    stream = condgen.Result,
					fileName = "W_RCMedML_Ind_20.07.2016.2.pdf",
                    length = condgen.Result.Length
				};
				var folleto = await b.ReturnDocument("PLAN_LEGAL_MEDICOS.pdf", false);
				var filefolleto = new FilePropertiesManager
				{
                    stream = folleto.Result,
					fileName = "Legal Medico.pdf",
                    length = folleto.Result.Length
				};

                FilePropertiesManager file_caratula = null;
				b.IniciaWS();
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        var caratula = await b.getPolicy(vmcotizar.PolizaGenerada.PolizaGenerada, "caratula");
                        if (caratula == null)
                            await Task.Delay(TimeSpan.FromSeconds(5));
                        else
                        {
                            file_caratula = new FilePropertiesManager
                            {
                                stream = caratula.Result,
                                fileName = "Poliza.pdf",
                                length = caratula.Result.Length
                            };
                            break;
                        }
                    }
                    catch
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                }


                FilePropertiesManager file_recibo = null;
                //revisar el resultado de pago en banco y si fue aprovada ...
                if (vmcotizar.DatosBank == null || vmcotizar.DatosBank.TipoTarj == CreditCardValidator.CardIssuer.Unknown)
                {
                    var recibo = await b.getPolicy(vmcotizar.PolizaGenerada.PolizaGenerada, "recibo");
                    file_recibo = new FilePropertiesManager
                    {
                        stream = recibo.Result,
                        fileName = "Recibo.pdf",
                        length = recibo.Result.Length
                    };
                }
                else
                {
                    //EmisionPago pago = new EmisionPago() { }
                    //b.GenerateRecibo()
                }

                if (vmcotizar.IdTipo == "1")//nueva
                    GMX.ViewModels.Emails.SlipTradicional(vmcotizar.PolizaGenerada.NumPoliza, vmcotizar.Adicional, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.Especialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, vmcotizar.SumaAseg);
				if (vmcotizar.IdTipo == "2")//renovacion
                    GMX.ViewModels.Emails.SlipTradicionalRenov(vmcotizar.PolizaGenerada.NumPoliza, vmcotizar.Adicional, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.Especialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, vmcotizar.SumaAseg, $"{vmcotizar.Antecedentes.dia}/{vmcotizar.Antecedentes.mes}/{vmcotizar.Antecedentes.anno}", (vmcotizar.Antecedentes.poliza1 != null ? vmcotizar.Antecedentes.poliza1.poliza : String.Empty), (vmcotizar.Antecedentes.poliza2 != null ? vmcotizar.Antecedentes.poliza2.poliza : String.Empty), (vmcotizar.Antecedentes.poliza3 != null ? vmcotizar.Antecedentes.poliza3.poliza : String.Empty));

				GMX.ViewModels.Emails.docPDF.Watermark = waterm.Result;
				b.IniciaWS(api: config.Config["APIGMXIT"]);
				var condpart = await b.GenerateDocument(GMX.ViewModels.Emails.Sections.ToArray(), GMX.ViewModels.Emails.docPDF);
                var slip_condpart = new FilePropertiesManager { stream = condpart.Result, fileName = "CondicionesParticulares.pdf", length = condpart.Result.Length };

                //var res = await b.DistribuirDocumentacionConfirmacion(vmcotizar.DatosGrales, vmcotizar.PolizaGenerada.PolizaGenerada, file_caratula, slip_condpart, file_recibo, file_condi_gral, new FilePropertiesManager[]{ filefolleto });
                var res = await b.DistribuirDocumentacionConfirmacion(vmcotizar.DatosGrales, vmcotizar.PolizaGenerada.NumPoliza, file_caratula, slip_condpart, file_recibo, file_condi_gral, new FilePropertiesManager[] { filefolleto });

                Ocupado = false;
				if (res.Result)
					await diag.AlertAsync("Correo enviado con exito", "Aviso", "OK");
				else
					await diag.AlertAsync("El correo no pudo ser enviado", "Error", "OK"); 
            }
            catch(Exception ex)
            {
                Ocupado = false;
                await diag.AlertAsync("El correo no pudo ser enviado", "Error", "OK");
            }
        }

        public class SelectedOptionEventArgs : EventArgs
        {
            public resum sel { set; get; }
        }

		#region Declaraciones

		ObservableCollection<resum> listadatos;
		public ObservableCollection<resum> ListaDatos
		{
			get { return listadatos; }
			set
			{
				if (listadatos != value)
				{
					listadatos = value;
					OnPropertyChanged("ListaDatos");
				}

			}
		}

		public event EventHandler<SelectedOptionEventArgs> OpcionSeleccionada;
		protected virtual void OnOpcionSeleccionada(SelectedOptionEventArgs e)
		{
			var handler = OpcionSeleccionada;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		private resum _ItemSelected;
		public resum objItemSelected
		{
			get
			{
				return _ItemSelected;
			}
			set
			{
				if (_ItemSelected != value)
				{
					//_ItemSelected = value;
                    SeleccionaLista(value.id); 
                    _ItemSelected = null;
					OnPropertyChanged("objItemSelected");
				}
			}
		}

		#endregion

		#region Eventos

		public async void SeleccionaLista(int id)
		{
			switch (id)
			{
				case 1:
					//Para Datos Generales
                    await nav.PushPopupAsync(new VerResumen(vmcotizar, vmcotizar.DatosGenerales, TipoResumen.Generales, nav), true);
					break;
				case 2:
					//Para Datos Fiscales
                    await nav.PushPopupAsync(new VerResumen(vmcotizar, vmcotizar.DatosFiskles, TipoResumen.Fiscales, nav), true);
					break;
				case 3:
					//Para Datos Profesionales
                    await nav.PushPopupAsync(new VerResumen(vmcotizar, vmcotizar.DatosProfesionales, TipoResumen.Profesionales, nav), true);
					break;
				case 4:
					//Para Datos Bancarios
                    await nav.PushPopupAsync(new VerResumen(vmcotizar, vmcotizar.DatosBancarios, TipoResumen.Bancarios, nav), true);
					break;
			}
		}

  #endregion

	}

    public class resum 
    {
        public string opc { get; set; }
        public int id { get; set; }
        public bool sel { get; set; }
    }
}
