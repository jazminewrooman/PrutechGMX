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
                    var condgen = await b.ReturnDocument("W_RCMedML_Ind_20.07.2016.2.pdf", false);
                    await DependencyService.Get<ISaveAndOpen>().OpenFile("W_RCMedML_Ind_20.07.2016.2.pdf", condgen.Result);
                    Ocupado = false;
                });
                ControlCommand = new Command(async () =>
                {
                    Ocupado = true;
                    b.IniciaWS();
                    var recibo = await b.getPolicy(res.Poliza, "recibo");
                    await DependencyService.Get<ISaveAndOpen>().OpenFile("recibo.pdf", recibo.Result);
                    Ocupado = false;
                });
                ParticularesCommand = new Command(async () =>
                {
                    Ocupado = true;
                    try
                    {
                        b.IniciaWS(apidoc: config.Config["APIDocs"]);
                        var waterm = await b.ReturnDocument("Watermark.jpg", false);

                        if (!String.IsNullOrEmpty(res.PolizasAnt) && res.PolizasAnt == "NO")//nueva
                            GMX.ViewModels.Emails.SlipTradicional(res.Poliza, res.Arrendatario, res.Nombre_Cliente, res.Especialidad, res.Especialidad2, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, res.Suma_Asegurada.ToString("c"));
                        if (!String.IsNullOrEmpty(res.PolizasAnt) && res.PolizasAnt == "SI")//renovacion
                            GMX.ViewModels.Emails.SlipTradicionalRenov(res.Poliza, res.Arrendatario, res.Nombre_Cliente, res.Especialidad, res.Especialidad2, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, res.Suma_Asegurada.ToString("c"), res.fecRetroactiva.ToString("dd/MM/yyyy"), res.PolAnt1, res.PolAnt2, "");

                        /*if (vmcotizar.IdPlan == "1") //tradicional
                {
                    if (vmcotizar.IdTipo == "1")//nueva
                        GMX.ViewModels.Emails.SlipTradicional(vmcotizar.PolizaGenerada.NumPoliza, vmcotizar.Adicional, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.Especialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, vmcotizar.SumaAseg);
                    if (vmcotizar.IdTipo == "2")//renovacion
                        GMX.ViewModels.Emails.SlipTradicionalRenov(vmcotizar.PolizaGenerada.NumPoliza, vmcotizar.Adicional, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.Especialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, vmcotizar.SumaAseg, $"{vmcotizar.Antecedentes.dia}/{vmcotizar.Antecedentes.mes}/{vmcotizar.Antecedentes.anno}", (vmcotizar.Antecedentes.poliza1 != null ? vmcotizar.Antecedentes.poliza1.poliza : String.Empty), (vmcotizar.Antecedentes.poliza2 != null ? vmcotizar.Antecedentes.poliza2.poliza : String.Empty), (vmcotizar.Antecedentes.poliza3 != null ? vmcotizar.Antecedentes.poliza3.poliza : String.Empty));
                }
                if (vmcotizar.IdPlan == "2") //angeles
                {
                    if (vmcotizar.IdTipo == "1")//nueva
                        GMX.ViewModels.Emails.SlipAngeles(vmcotizar.PolizaGenerada.NumPoliza, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.Especialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, sumaasegdec);
                    if (vmcotizar.IdTipo == "2")//renovacion
                        GMX.ViewModels.Emails.SlipAngelesReov(vmcotizar.PolizaGenerada.NumPoliza, $"{vmcotizar.DatosGrales.Nombre} {vmcotizar.DatosGrales.APaterno} {vmcotizar.DatosGrales.AMaterno}", vmcotizar.DatosProf.Descripcion, vmcotizar.DatosProf.Especialidad.ToString(), vmcotizar.DatosProf.CedulaProf, vmcotizar.DatosProf.CedulaEsp, vmcotizar.DatosProf.Diplomados, sumaasegdec, $"{vmcotizar.Antecedentes.dia}/{vmcotizar.Antecedentes.mes}/{vmcotizar.Antecedentes.anno}", (vmcotizar.Antecedentes.poliza1 != null ? vmcotizar.Antecedentes.poliza1.poliza : String.Empty), (vmcotizar.Antecedentes.poliza2 != null ? vmcotizar.Antecedentes.poliza2.poliza : String.Empty), (vmcotizar.Antecedentes.poliza3 != null ? vmcotizar.Antecedentes.poliza3.poliza : String.Empty));
                }*/



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
                    List<FilePropertiesManager> fileAttach = new List<FilePropertiesManager>();
                    Ocupado = true;
                    string polizagenerada = "";
                    string[] numpol = res.Poliza.Split('_');
                    if (numpol.Length > 0 && numpol.Length >= 3)
                        polizagenerada = numpol[2];
                    string numpoliza = $"01-66-{polizagenerada.PadLeft(8, '0')}-0000-01";

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

                    var caratula = await b.getPolicy(res.Poliza, "caratula");
                    var file_caratula = new FilePropertiesManager
                    {
                        stream = caratula.Result,
                        fileName = "Poliza.pdf",
                        length = caratula.Result.Length
                    };

                    var recibo = await b.getPolicy(res.Poliza, "recibo");
                    var file_recibo = new FilePropertiesManager
                    {
                        stream = recibo.Result,
                        fileName = "Recibo.pdf",
                        length = recibo.Result.Length
                    };

                    if (!String.IsNullOrEmpty(res.PolizasAnt) && res.PolizasAnt == "NO")//nueva
                        GMX.ViewModels.Emails.SlipTradicional(numpoliza, res.Arrendatario, res.Nombre_Cliente, res.Especialidad, res.Especialidad2, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, res.Suma_Asegurada.ToString("c"));
                    if (!String.IsNullOrEmpty(res.PolizasAnt) && res.PolizasAnt == "SI")//renovacion
                        GMX.ViewModels.Emails.SlipTradicionalRenov(numpoliza, res.Arrendatario, res.Nombre_Cliente, res.Especialidad, res.Especialidad2, res.NoCedulaPro, res.NoCedulaEsp, res.Otros, res.Suma_Asegurada.ToString("c"), res.fecRetroactiva.ToString("dd/MM/yyyy"), res.PolAnt1, res.PolAnt2, "");
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
            Emision = res.Emision;
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

        DateTime emision;
        public DateTime Emision
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
