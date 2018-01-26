using System;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Collections.Generic;
using GMX.Views;
using System.Collections.ObjectModel;
using GMX.Services.DTOs;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Linq;
using System.Threading.Tasks;
using CreditCardValidator;
using Rg.Plugins.Popup.Extensions;
using GMX.Services;
using GMXHelper;

namespace GMX
{
    public class VMDetallePoliza : VMGmx
    {
		INavigation nav;

        public ICommand NextCommand { get; private set; }
        public ICommand CaratulaCommand { get; set; }
        public ICommand GeneralesCommand { get; set; }
        public ICommand ControlCommand { get; set; }
        public ICommand ParticularesCommand { get; set; }
        public ICommand DetalleCommand { get; set; }
        public ICommand ReenviarCommand { get; set; }

        public VMDetallePoliza(polizaemitida res, IUserDialogs diag, INavigation n) : base(diag)
        {
            //public VMDetallePoliza(IUserDialogs diag, INavigation n, resultado res) : base(diag)
            nav = n;
            Title = "Pólizas Emitidas";
            Email = res.Email_Cliente;

            cargaDatos(res);
            bindings b = new bindings();
            try
            {
                CaratulaCommand = new Command(async () =>
                {
                    Ocupado = true;
                    b.IniciaWS();
                    var caratula = await b.getPolicy(res.Poliza, "caratula");
                    await DependencyService.Get<ISaveAndOpen>().OpenFile("caratula.pdf", caratula.Result);
                    Ocupado = false;
                });
                GeneralesCommand = new Command(async () =>
                {
                    Ocupado = true;
                    b.IniciaWS(apidoc: config.Config["APIDocs"]);
                    var condgen = await b.ReturnDocument("W_RCMed_Ind_01.07.17.pdf", false);
                    if (condgen.Result != null)
                        await DependencyService.Get<ISaveAndOpen>().OpenFile("W_RCMed_Ind_01.07.17.pdf", condgen.Result);
                    Ocupado = false;
                });
                ControlCommand = new Command(async () =>
                {
                    Ocupado = true;
                    b.IniciaWS();
                    byte[] doc = null;
                    if (!String.IsNullOrEmpty(res.Tarjeta))
                    {
                        /*b.IniciaWS(api: config.Config["APIGMXIT"]);
                        GMXHelper.EmisionPago datosMit = new EmisionPago();
                        datosMit.Poliza = $"{res.Poliza}-Voucher";
                        var recibo = await b.GenerateRecibo(datosMit);
                        doc = recibo.Result;*/
                        var recibo = await b.getPolicy(res.Poliza, $"{res.Poliza}-Voucher");
                        doc = recibo.Result;
                    }
                    else
                    {
                        var recibo = await b.getPolicy(res.Poliza, "recibo");
                        doc = recibo.Result;
                    }
                    await DependencyService.Get<ISaveAndOpen>().OpenFile("recibo.pdf", doc);
                    Ocupado = false;
                });
                ParticularesCommand = new Command(async () =>
                {
                    string[] polant = new string[] { "", "", "" };
                    if (!String.IsNullOrEmpty(res.PolAnt1))
                        polant = res.PolAnt1.Split(',');

                    Ocupado = true;
                    string polizagenerada = "";
                    string[] numpol = res.Poliza.Split('_');
                    if (numpol.Length > 0 && numpol.Length >= 3)
                        polizagenerada = numpol[2];
                    string numpoliza = $"{App.agent.cod_suc.PadLeft(3, '0')}-66-{polizagenerada.PadLeft(8, '0')}-0000-01";

                    try
                    {
                        b.IniciaWS(apidoc: config.Config["APIDocs"]);
                        var waterm = await b.ReturnDocument("Watermark.jpg", false);

                        if (res.Tipo_Negocio == 1) //tradicional
                        {
                            if (res.PolizasAnt == "NO")//nueva
                                GMX.ViewModels.Emails.SlipTradicional(numpoliza, res.Arrendatario, res.Nombre_Cliente, res.Especialidad, res.SubEspecialidad, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, $"{res.Suma_Asegurada.ToString("c")} M.N.");
                            if (!String.IsNullOrEmpty(res.PolizasAnt) && res.PolizasAnt == "SI")//renovacion
                                GMX.ViewModels.Emails.SlipTradicionalRenov(numpoliza, res.Arrendatario, res.Nombre_Cliente, res.Especialidad, res.SubEspecialidad, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, $"{res.Suma_Asegurada.ToString("c")} M.N.", res.fecRetroactiva.ToString("dd/MM/yyyy"), (polant.Length > 0 ? polant[0] : ""), (polant.Length > 1 ? polant[1] : ""), (polant.Length > 2 ? polant[2] : ""));
                        }
                        else //Angeles
                        {
                            if (res.PolizasAnt == "NO")//nueva
                                GMX.ViewModels.Emails.SlipAngeles(numpoliza, res.Nombre_Cliente, res.Especialidad, res.SubEspecialidad, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, (decimal)res.Suma_Asegurada);
                            if (!String.IsNullOrEmpty(res.PolizasAnt) && res.PolizasAnt == "SI")//renovacion
                                GMX.ViewModels.Emails.SlipAngelesReov(numpoliza, res.Nombre_Cliente, res.Especialidad, res.SubEspecialidad, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, (decimal)res.Suma_Asegurada, res.fecRetroactiva.ToString("dd/MM/yyyy"), (polant.Length > 0 ? polant[0] : ""), (polant.Length > 1 ? polant[1] : ""), (polant.Length > 2 ? polant[2] : ""));
                        }

                        GMX.ViewModels.Emails.docPDF.Watermark = waterm.Result;
                        b.IniciaWS(api: config.Config["APIGMXIT"]);
                        var condpart = await b.GenerateDocument(GMX.ViewModels.Emails.Sections.ToArray(), GMX.ViewModels.Emails.docPDF);
                        await DependencyService.Get<ISaveAndOpen>().OpenFile("Particulares.pdf", condpart.Result);
                    }
                    catch (Exception ex)
                    {
                        await diag.AlertAsync(ex.InnerException.ToString(), "Error", "Ok");
                        Ocupado = false;
                    }
                    Ocupado = false;
                });
                DetalleCommand = new Command(async () =>
                {
                    Ocupado = true;
                    b.IniciaWS(apidoc: config.Config["APIDocs"]);
                    var folleto = await b.ReturnDocument("PLAN_LEGAL_MEDICOS.pdf", false);
                    await DependencyService.Get<ISaveAndOpen>().OpenFile("PLAN_LEGAL_MEDICOS.pdf", folleto.Result);
                    Ocupado = false;
                });
                ReenviarCommand = new Command(async () =>
                {
                    string[] polant = new string[] { "", "", "" };
                    if (!String.IsNullOrEmpty(res.PolAnt1))
                        polant = res.PolAnt1.Split(',');

                    FilePropertiesManager file_condi_gral = null;
                    List<FilePropertiesManager> fileAttach = new List<FilePropertiesManager>();
                    Ocupado = true;
                    string polizagenerada = "";
                    string[] numpol = res.Poliza.Split('_');
                    if (numpol.Length > 0 && numpol.Length >= 3)
                        polizagenerada = numpol[2];
                    string numpoliza = $"{App.agent.cod_suc.PadLeft(3, '0')}-66-{polizagenerada.PadLeft(8, '0')}-0000-01";

                    b.IniciaWS(apidoc: config.Config["APIDocs"]);
                    var waterm = await b.ReturnDocument("Watermark.jpg", false);

                    var condgen = await b.ReturnDocument("W_RCMed_Ind_01.07.17.pdf", false);
                    if (condgen.Result != null)
                    {
                        file_condi_gral = new FilePropertiesManager
                        {
                            stream = condgen.Result,
                            fileName = "W_RCMed_Ind_01.07.17.pdf",
                            length = condgen.Result.Length
                        };
                    }

                    var folleto = await b.ReturnDocument("PLAN_LEGAL_MEDICOS.pdf", false);
                    var filefolleto = new FilePropertiesManager
                    {
                        stream = folleto.Result,
                        fileName = "Legal Medico.pdf",
                        length = folleto.Result.Length
                    };

                    var caratula = await b.getPolicy(res.Poliza, "caratula");
                    var file_caratula = new FilePropertiesManager
                    {
                        stream = caratula.Result,
                        fileName = "Poliza.pdf",
                        length = caratula.Result.Length
                    };

                    getPolicyCompletedEventArgs recibo = null;
                    if (!String.IsNullOrEmpty(res.Tarjeta))
                        recibo = await b.getPolicy(res.Poliza, $"{res.Poliza}-Voucher");
                    else
                        recibo = await b.getPolicy(res.Poliza, "recibo");
                    var file_recibo = new FilePropertiesManager
                    {
                        stream = recibo.Result,
                        fileName = "Recibo.pdf",
                        length = recibo.Result.Length
                    };

                    if (res.Tipo_Negocio == 1) //tradicional
                    {
                        if (res.PolizasAnt == "NO")//nueva
                            GMX.ViewModels.Emails.SlipTradicional(numpoliza, res.Arrendatario, res.Nombre_Cliente, res.Especialidad, res.SubEspecialidad, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, $"{res.Suma_Asegurada.ToString("c")} M.N.");
                        if (!String.IsNullOrEmpty(res.PolizasAnt) && res.PolizasAnt == "SI")//renovacion
                            GMX.ViewModels.Emails.SlipTradicionalRenov(numpoliza, res.Arrendatario, res.Nombre_Cliente, res.Especialidad, res.SubEspecialidad, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, $"{res.Suma_Asegurada.ToString("c")} M.N.", res.fecRetroactiva.ToString("dd/MM/yyyy"), (polant.Length > 0 ? polant[0] : ""), (polant.Length > 1 ? polant[1] : ""), (polant.Length > 2 ? polant[2] : ""));
                    }
                    else //Angeles
                    {
                        if (res.PolizasAnt == "NO")//nueva
                            GMX.ViewModels.Emails.SlipAngeles(numpoliza, res.Nombre_Cliente, res.Especialidad, res.SubEspecialidad, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, (decimal)res.Suma_Asegurada);
                        if (!String.IsNullOrEmpty(res.PolizasAnt) && res.PolizasAnt == "SI")//renovacion
                            GMX.ViewModels.Emails.SlipAngelesReov(numpoliza, res.Nombre_Cliente, res.Especialidad, res.SubEspecialidad, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, (decimal)res.Suma_Asegurada, res.fecRetroactiva.ToString("dd/MM/yyyy"), (polant.Length > 0 ? polant[0] : ""), (polant.Length > 1 ? polant[1] : ""), (polant.Length > 2 ? polant[2] : ""));
                    }

                    GMX.ViewModels.Emails.docPDF.Watermark = waterm.Result;
                    b.IniciaWS(api: config.Config["APIGMXIT"]);
                    var condpart = await b.GenerateDocument(GMX.ViewModels.Emails.Sections.ToArray(), GMX.ViewModels.Emails.docPDF);
                    var slip_condpart = new FilePropertiesManager { stream = condpart.Result, fileName = "CondicionesParticulares.pdf", length = condpart.Result.Length };

                    var r = await b.DistribuirDocumentacionReenvio(GMX.ViewModels.Emails.Reenvio(), res.Nombre_Cliente, Email, res.Poliza, file_caratula, slip_condpart, file_recibo, file_condi_gral, new FilePropertiesManager[] { filefolleto });


                    Ocupado = false;
                });

            }
            catch (Exception ex)
            {

            }
        }

        public void cargaDatos(polizaemitida res)
        {
            Emision = res.Emision.ToString("dd/MM/yyyy");
            PrimaNeta = res.PrimaNeta;
            Derechos = res.Derechos;
            IVA = res.Iva;
            PrimaTotal = res.PrimaTotal;
            Email = res.Email_Cliente;
        }


        string email;
        public string Email{
            get => email;
            set {
                if (email != value){
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        string emision;
        public string Emision
        {
            get => emision;
            set 
            {
                if (emision != value)
                {
                    emision = value;
                    OnPropertyChanged("Emision");
                }
            }
        }

		double primaneta;
		public double PrimaNeta
		{
			get => primaneta;
			set
			{
				if (primaneta != value)
				{
					primaneta = value;
					OnPropertyChanged("PrimaNeta");
				}
			}
		}

		double derechos;
		public double Derechos
		{
            get => derechos;
			set
			{
				if (derechos != value)
				{
					derechos = value;
					OnPropertyChanged("Derechos");
				}
			}
		}

		double iva;
		public double IVA
		{
			get => iva;
			set
			{
				if (iva != value)
				{
					iva = value;
					OnPropertyChanged("IVA");
				}
			}
		}

		double primatotal;
		public double PrimaTotal
		{
			get => primatotal;
			set
			{
				if (primatotal != value)
				{
					primatotal = value;
					OnPropertyChanged("PrimaTotal");
				}
			}
		}


    }

}
