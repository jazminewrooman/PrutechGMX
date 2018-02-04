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
using System.Globalization;

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
            lst.Add(new resum { id = 1, opc = "Datos Generales" });
            lst.Add(new resum { id = 2, opc = "Datos Fiscales" });
            lst.Add(new resum { id = 3, opc = "Datos Profesionales" });
            lst.Add(new resum { id = 4, opc = "Datos Bancarios" });
            if (vmc.Antecedentes != null && vmc.Antecedentes.poliza1 != null)
                lst.Add(new resum { id = 5, opc = "Antecedentes de poliza" });

            ListaDatos = lst;

            VerCotizaCommand = new Command(async () =>
            {
                await nav.PushPopupAsync(new VerCotiza(vmcotizar), true);
            });
            EmisionCommand = new Command(async () =>
            {
                //string msg = $"Se mandara la emision de la siguiente poliza:{Environment.NewLine}Prima Neta: {vmcotizar.PrimaNeta.ToString("c")}{Environment.NewLine}Derechos: {vmcotizar.Derechos.ToString("c")}{Environment.NewLine}Subtotal:{vmcotizar.SubTotal.ToString("c")}{Environment.NewLine}IVA:{vmcotizar.Iva.ToString("c")}{Environment.NewLine}Total:{vmcotizar.PrimaTotal.ToString("c")}{Environment.NewLine}{Environment.NewLine}¿Desea continuar?";
                //var result = await diag.ConfirmAsync(msg, "Aviso", "Ok", "Cancelar");
                var confirma = new VerConfirma(vmcotizar);
                await nav.PushPopupAsync(confirma, true);
                var seleccion = await confirma.Regresa();
                if (seleccion)
                {
                    await vmcotizar.MandarEmision();
                    if (vmcotizar.PolizaGenerada != null && !String.IsNullOrEmpty(vmcotizar.PolizaGenerada.NumPoliza))
                    {
                        // default siempre va la referencia, los demas en blanco
                        vmcotizar.StrTransBanco = String.Empty;
                        vmcotizar.TransBanco = new wspago.MITResponse
                        {
                            reference = vmcotizar.PolizaGenerada.Referencia,
                            response = String.Empty,
                            foliocpagos = String.Empty,
                            auth = String.Empty,
                            cc_number = String.Empty
                        };
                        if (vmcotizar.DatosBank != null && vmcotizar.DatosBank.TipoTarj != CreditCardValidator.CardIssuer.Unknown)
                            vmcotizar.MandarPagar();

                        await EnviaConfirmacion();
                        await EnviaAvisoVenta();

                        vmcotizar.GuardarBD();

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

            SelectList = new Command((e) =>
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
                MsgOcupado = "Enviando correo electronico";
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

                /*var incendio = await b.getPolicy(vmcotizar.PolizaGenerada.PolizaGenerada, "master_incendio");
                var file_inc = new FilePropertiesManager
                {
                    stream = incendio.Result,
                    fileName = "DetalleCobertura.pdf",
                    length = incendio.Result.Length
                };*/

                decimal sumaasegdec = decimal.Parse(vmcotizar.SumaAseg, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
				if (vmcotizar.IdPlan == "1") //tradicional
				{
					if (vmcotizar.IdTipo == "1")//nueva
                        GMX.ViewModels.Emails.SlipTradicional(vmcotizar.PolizaGenerada.NumPoliza, vmcotizar.Adicional, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.StrEspecialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, $"{sumaasegdec.ToString("c")} M.N.");
					if (vmcotizar.IdTipo == "2")//renovacion
                        GMX.ViewModels.Emails.SlipTradicionalRenov(vmcotizar.PolizaGenerada.NumPoliza, vmcotizar.Adicional, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.StrEspecialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, $"{sumaasegdec.ToString("c")} M.N.", $"{vmcotizar.Antecedentes.dia}/{vmcotizar.Antecedentes.mes}/{vmcotizar.Antecedentes.anno}", (vmcotizar.Antecedentes.poliza1 != null ? vmcotizar.Antecedentes.poliza1.fullpoliza : String.Empty), (vmcotizar.Antecedentes.poliza2 != null ? vmcotizar.Antecedentes.poliza2.fullpoliza : String.Empty), (vmcotizar.Antecedentes.poliza3 != null ? vmcotizar.Antecedentes.poliza3.fullpoliza : String.Empty));
				}
				if (vmcotizar.IdPlan == "2") //angeles
				{
					if (vmcotizar.IdTipo == "1")//nueva
                        GMX.ViewModels.Emails.SlipAngeles(vmcotizar.PolizaGenerada.NumPoliza, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.StrEspecialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, sumaasegdec);
					if (vmcotizar.IdTipo == "2")//renovacion
                        GMX.ViewModels.Emails.SlipAngelesReov(vmcotizar.PolizaGenerada.NumPoliza, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.StrEspecialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, sumaasegdec, $"{vmcotizar.Antecedentes.dia}/{vmcotizar.Antecedentes.mes}/{vmcotizar.Antecedentes.anno}", (vmcotizar.Antecedentes.poliza1 != null ? vmcotizar.Antecedentes.poliza1.fullpoliza : String.Empty), (vmcotizar.Antecedentes.poliza2 != null ? vmcotizar.Antecedentes.poliza2.fullpoliza : String.Empty), (vmcotizar.Antecedentes.poliza3 != null ? vmcotizar.Antecedentes.poliza3.fullpoliza : String.Empty));
				}                

                GMX.ViewModels.Emails.docPDF.Watermark = waterm.Result;
                b.IniciaWS(api: config.Config["APIGMXIT"]);
                var condpart = await b.GenerateDocument(GMX.ViewModels.Emails.Sections.ToArray(), GMX.ViewModels.Emails.docPDF);
                var slip_condpart = new FilePropertiesManager
                {
                    stream = condpart.Result,
                    fileName = "CondicionesParticulares.pdf",
                    length = condpart.Result.Length
                };

                var res = await b.DistribuirDocumentacionAviso(vmcotizar.DatosGrales, vmcotizar.PolizaGenerada.NumPoliza, file_caratula, slip_condpart, null);
                //var res = await b.DistribuirDocumentacionAviso(vmcotizar.DatosGrales, vmcotizar.PolizaGenerada.NumPoliza, file_caratula, slip_condpart, new FilePropertiesManager[] { file_inc });
                //var res = await b.DistribuirDocumentacionAviso(vmcotizar.DatosGrales, vmcotizar.PolizaGenerada.NumPoliza, file_caratula, null, null);

                Ocupado = false;
                MsgOcupado = "";
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
                MsgOcupado = "Generando documentación";
                Ocupado = true;

                bindings b = new bindings();
                b.IniciaWS(apidoc: config.Config["APIDocs"]);
                var waterm = await b.ReturnDocument("Watermark.jpg", false);

                //var condgen = await b.ReturnDocument("W_RCMedML_Ind_20.07.2016.2.pdf", false);
                var condgen = await b.ReturnDocument("W_RCMed_Ind_01.07.17.pdf", false);
                var file_condi_gral = new FilePropertiesManager
                {
                    stream = condgen.Result,
                    fileName = "W_RCMed_Ind_01.07.17.pdf",
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
                for (int i = 0; i < 10; i++)
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
                if (vmcotizar.TransBanco.response != "approved")
                {
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            var recibo = await b.getPolicy(vmcotizar.PolizaGenerada.PolizaGenerada, "recibo");
                            if (recibo == null)
                                await Task.Delay(TimeSpan.FromSeconds(5));
                            else
                            {
                                file_recibo = new FilePropertiesManager
                                {
                                    stream = recibo.Result,
                                    fileName = "Recibo.pdf",
                                    length = recibo.Result.Length
                                };
                                break;
                            }
                        }
                        catch
                        {
                            await Task.Delay(TimeSpan.FromSeconds(5));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            b.IniciaWS(api: config.Config["APIGMXIT"]);
                            GMXHelper.EmisionPago datosMit = new EmisionPago();

                            datosMit.NombreTH = vmcotizar.TransBanco.cc_name;
                            datosMit.Referencia = vmcotizar.TransBanco.reference;
                            datosMit.Autorizacion = vmcotizar.TransBanco.auth;
                            datosMit.Tarjeta = vmcotizar.TransBanco.cc_number;
                            datosMit.ImportePago = Convert.ToDouble(vmcotizar.TransBanco.amount);
                            datosMit.FechaEmision = vmcotizar.IniVig;
                            datosMit.FolioCPagos = vmcotizar.TransBanco.foliocpagos;
                            datosMit.FriendlyResponse = vmcotizar.TransBanco.friendly_response;
                            datosMit.CCType = vmcotizar.TransBanco.cc_type;
                            datosMit.URL = "https://pvl.gmx.com.mx/Medicos/login.aspx";
                            datosMit.Poliza = vmcotizar.PolizaGenerada.PolizaGenerada;

                            var recibo = await b.GenerateRecibo(datosMit);
                            if (recibo == null)
                                await Task.Delay(TimeSpan.FromSeconds(5));
                            else
                            {
                                file_recibo = new FilePropertiesManager
                                {
                                    stream = recibo.Result,
                                    fileName = "ComprobandeDePago.pdf",
                                    length = recibo.Result.Length
                                };
                                break;
                            }
                        }
                        catch
                        {
                            await Task.Delay(TimeSpan.FromSeconds(5));
                        }
                    }
                }

                decimal sumaasegdec = decimal.Parse(vmcotizar.SumaAseg, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                if (vmcotizar.IdPlan == "1") //tradicional
                {
                    if (vmcotizar.IdTipo == "1")//nueva
                        GMX.ViewModels.Emails.SlipTradicional(vmcotizar.PolizaGenerada.NumPoliza, vmcotizar.Adicional, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.StrEspecialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, $"{sumaasegdec.ToString("c")} M.N.");
                    if (vmcotizar.IdTipo == "2")//renovacion
                        GMX.ViewModels.Emails.SlipTradicionalRenov(vmcotizar.PolizaGenerada.NumPoliza, vmcotizar.Adicional, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.StrEspecialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, $"{sumaasegdec.ToString("c")} M.N.", $"{vmcotizar.Antecedentes.dia}/{vmcotizar.Antecedentes.mes}/{vmcotizar.Antecedentes.anno}", (vmcotizar.Antecedentes.poliza1 != null ? vmcotizar.Antecedentes.poliza1.fullpoliza : String.Empty), (vmcotizar.Antecedentes.poliza2 != null ? vmcotizar.Antecedentes.poliza2.fullpoliza : String.Empty), (vmcotizar.Antecedentes.poliza3 != null ? vmcotizar.Antecedentes.poliza3.fullpoliza : String.Empty));
                }
				if (vmcotizar.IdPlan == "2") //angeles
				{
					if (vmcotizar.IdTipo == "1")//nueva
                        GMX.ViewModels.Emails.SlipAngeles(vmcotizar.PolizaGenerada.NumPoliza, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.StrEspecialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, sumaasegdec);
					if (vmcotizar.IdTipo == "2")//renovacion
                        GMX.ViewModels.Emails.SlipAngelesReov(vmcotizar.PolizaGenerada.NumPoliza, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.StrEspecialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, sumaasegdec, $"{vmcotizar.Antecedentes.dia}/{vmcotizar.Antecedentes.mes}/{vmcotizar.Antecedentes.anno}", (vmcotizar.Antecedentes.poliza1 != null ? vmcotizar.Antecedentes.poliza1.fullpoliza : String.Empty), (vmcotizar.Antecedentes.poliza2 != null ? vmcotizar.Antecedentes.poliza2.fullpoliza : String.Empty), (vmcotizar.Antecedentes.poliza3 != null ? vmcotizar.Antecedentes.poliza3.fullpoliza : String.Empty));
				}

				GMX.ViewModels.Emails.docPDF.Watermark = waterm.Result;
                b.IniciaWS(api: config.Config["APIGMXIT"]);
                var condpart = await b.GenerateDocument(GMX.ViewModels.Emails.Sections.ToArray(), GMX.ViewModels.Emails.docPDF);
                var slip_condpart = new FilePropertiesManager { stream = condpart.Result, fileName = "CondicionesParticulares.pdf", length = condpart.Result.Length };

                var res = await b.DistribuirDocumentacionConfirmacion(vmcotizar.DatosGrales, vmcotizar.PolizaGenerada.NumPoliza, file_caratula, slip_condpart, file_recibo, file_condi_gral, new FilePropertiesManager[] { filefolleto });

                Ocupado = false;
                MsgOcupado = "";
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
                case 5:
                    //Para antecedentes poliza
                    await nav.PushPopupAsync(new VerResumen(vmcotizar, vmcotizar.DatosAntecedentes, TipoResumen.Antecedentes, nav), true);
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
