﻿using System;
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
using GMX.Models;
using GMXHelper;
using System.Text.RegularExpressions;

namespace GMX
{
    public class VMCotizar : VMGmx
    {
        INavigation nav;
        ListaSumasAseg lstsumas;
        ListaSumaAngeles lstangeles;
        public ICommand NextCommand { get; private set; }
        public ICommand EmailCommand { get; private set; }
        public ICommand ShopCommand { get; private set; }

        public bool VerImg => !VerCuestionario;

        public VMCotizar(IUserDialogs diag, INavigation n) : base(diag)
        {
            nav = n;
            SumaAseg = "";
            TxtPlan = "Plan";
            ObservableCollection<opciones> lst = new ObservableCollection<opciones>();
            lst.Add(new opciones() { idopc = "1", opc = "Médicos Tradicional", sel = true });
            lst.Add(new opciones() { idopc = "2", opc = "Médicos Angeles", sel = false });
            LstPlan = lst;
            TxtTipo = "Tipo de Póliza";
            ObservableCollection<opciones> lst2 = new ObservableCollection<opciones>();
            lst2.Add(new opciones() { idopc = "1", opc = "Póliza nueva" });
            lst2.Add(new opciones() { idopc = "2", opc = "Renovación" });
            LstTipo = lst2;
            TxtSuma = Resources.Sumaasegurada;
            TxtCirugias = Resources.RealizaCirugias;
            TxtEjerce = Resources.Ejerce;
            TxtContratar = Resources.Contratarcobertura;
            LySumaAseg = Resources.Montosexpresados;
            TxtVig = Resources.IniVig;

            NextCommand = new Command(async () =>
            {
                if (!Validar())
                    await Diag.AlertAsync(Resources.FaltanOb, "Error", "Ok");
                else
                    await nav.PushAsync(new Cotizacion(this));
            });
            ShopCommand = new Command(async () =>
            {
                await nav.PushAsync(new DatosGenerales(datosgrales, TipoDatos.Generales, this));
            });
            EmailCommand = new Command(async () => 
			{
                PromptConfig cfg = new PromptConfig()
                {
                    InputType = InputType.Email,

                    Message = "Introduzca el correo al cual enviar la cotización",
                    OkText = "OK",
                    OnTextChanged = args => args.IsValid = ValEmail(args.Value)
                };
                var email = await diag.PromptAsync(cfg);
                if (!String.IsNullOrEmpty(email.Value))
                {
                    try
                    {
                        Ocupado = true;
                        GMX.ViewModels.Emails.GetSlipCotizacion(this);
                        bindings b = new bindings();
                        b.IniciaWS(config.Config["APIGMXIT"]);
                        var doc = await b.GenerateDocument(GMX.ViewModels.Emails.Sections.ToArray(), GMX.ViewModels.Emails.docPDF);
                        var slip_cotizacion = new FilePropertiesManager { stream = doc.Result, fileName = "Cotizacion.pdf", length = doc.Result.Length };
                        var res = await b.DistribuirDocumentacionPoliza(email.Value, slip_cotizacion);
                        Ocupado = false;
                        if (res.Result)
                            await diag.AlertAsync("Correo enviado con exito", "Aviso", "OK");
                        else
                            await diag.AlertAsync("El correo no pudo ser enviado", "Error", "OK");
                    }
                    catch
                    {
                        Ocupado = false;
                        await diag.AlertAsync("El correo no pudo ser enviado", "Error", "OK");
                    }
                }
			});
        }

        private bool ValEmail(string email)
        {
            bool IsValid = false;
			const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
	@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
			IsValid = (Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            return IsValid;
        }

        public void IniciaCarga()
        {
            Ocupado = true;
            IdPlan = "1";
            IniVig = DateTime.Now;
            Ocupado = false;
        }

        private bool Validar()
        {
            if (String.IsNullOrEmpty(IdTipo) || String.IsNullOrEmpty(IdPlan) || String.IsNullOrEmpty(IdSuma) || IniVig == DateTime.MinValue)
                return false;
            else
                return true;
        }

        GMX.Models.DatosBancarios datosbank;
        public GMX.Models.DatosBancarios DatosBank
        {
            get => datosbank;
            set
            {
                if (datosbank != value)
                {
                    datosbank = value;
                    OnPropertyChanged("DatosBank");
                }
            }
        }
        PolizasAnteriores antecedentes;
		public PolizasAnteriores Antecedentes
		{
			get { return antecedentes; }
			set
			{
				if (antecedentes != value)
				{
					antecedentes = value;
					OnPropertyChanged("Antecedentes");
				}
			}
		}
        DatosProfesionales datosprof;
		public DatosProfesionales DatosProf
		{
			get { return datosprof; }
			set
			{
				if (datosprof != value)
				{
					datosprof = value;
					OnPropertyChanged("DatosProf");
				}
			}
		}
        DatosGralesModel datosfiscales;
		public DatosGralesModel DatosFiscales
		{
			get { return datosfiscales; }
			set
			{
				if (datosfiscales != value)
				{
					datosfiscales = value;
					OnPropertyChanged("DatosFiscales");
				}
			}
		}
		DatosGralesModel datosgrales;
		public DatosGralesModel DatosGrales
		{
			get { return datosgrales; }
			set
			{
				if (datosgrales != value)
				{
					datosgrales = value;
					OnPropertyChanged("DatosGrales");
				}
			}
		}
		string txtcontratar;
		public string TxtContratar
		{
			get { return txtcontratar; }
			set
			{
				if (txtcontratar != value)
				{
					txtcontratar = value;
					OnPropertyChanged("TxtContratar");
				}
			}
		}
		string lysumaAseg;
		public string LySumaAseg
		{
			get { return lysumaAseg; }
			set
			{
				if (lysumaAseg != value)
				{
					lysumaAseg = value;
					OnPropertyChanged("LySumaAseg");
				}
			}
		}
		string txtcirugias;
		public string TxtCirugias
		{
			get { return txtcirugias; }
			set
			{
				if (txtcirugias != value)
				{
					txtcirugias = value;
					OnPropertyChanged("TxtCirugias");
				}
			}
		}
		string txtejerce;
		public string TxtEjerce
		{
			get { return txtejerce; }
			set
			{
				if (txtejerce != value)
				{
					txtejerce = value;
					OnPropertyChanged("TxtEjerce");
				}
			}
		}

        private void ValidarMuestroCuestionario(){
            if (!String.IsNullOrEmpty(IdPlan) && !String.IsNullOrEmpty(IdTipo))
                VerCuestionario = true;
            else
                VerCuestionario = false;
        }

		bool vercuestionario;
		public bool VerCuestionario
		{
			get { return vercuestionario; }
			set
			{
				if (vercuestionario != value)
				{
					vercuestionario = value;
					OnPropertyChanged("VerCuestionario");
                    OnPropertyChanged("VerImg");
				}
			}
		}
		string idplan;
		public string IdPlan
		{
			get { return idplan; }
			set
			{
				if (idplan != value)
				{
                    idplan = value;
                    CargaSumas();
					OnPropertyChanged("IdPlan");
				}
			}
		}
        private DateTime fretroactiva;
        public DateTime FRetroactiva
        {
            get => fretroactiva;
            set
            {
                if (fretroactiva != value)
                {
                    fretroactiva = value;
                    OnPropertyChanged("FRetroactiva");
                }
            }
        }
        private DateTime fposterior;
		public DateTime FPosterior
		{
			get => fposterior;
			set
			{
				if (fposterior != value)
				{
					fposterior = value;
					OnPropertyChanged("FPosterior");
				}
			}
		}

        private void CargaFechas(ListaFechaCotiz fechas)
        {
            FechaCotizacion f = fechas.Table.FirstOrDefault();
            if (f.numMesesPost > -1 && f.numMesesRetro > -1)
            {
                FRetroactiva = DateTime.Now.AddMonths(f.numMesesRetro * -1);
                FPosterior = DateTime.Now.AddMonths(f.numMesesPost);
            }
            else if (f.diasPosteriores > -1 && f.diasRetroactivos > -1)
            {
                FRetroactiva = DateTime.Now.AddDays(f.diasRetroactivos * -1).Date;
                FPosterior = DateTime.Now.AddDays(f.diasPosteriores).Date;
            }
            else
            {
                FRetroactiva = (f.fecRetroactiva == null ? DateTime.MinValue : (DateTime)f.fecRetroactiva);
                FPosterior = (f.fecPosterior == null ? DateTime.MinValue : (DateTime)f.fecPosterior);
            }
        }

        private async Task CargaSumas(){
            ObservableCollection<opciones> lst = null;
			Ocupado = true;
			await System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(100));
			ValidarMuestroCuestionario();
			wsbd.Service ws = new wsbd.Service(config.Config["APIBD"]);
			string json = "";
            if (idplan == "1") //tradicional
			{
                json = ws.get_catalogos("GetAllSumaAseg_Categoria", "");
				lstsumas = JsonConvert.DeserializeObject<ListaSumasAseg>(json);
				lst = new ObservableCollection<opciones>();
				foreach (SumaAsegXPlan s in lstsumas.Table)
					lst.Add(new opciones() { idopc = s.idSumAse_Categoria.ToString(), opc = s.SumaAsegurada.ToString("c") });
                json = ws.get_catalogos("getConfigFecCotizacion", "@nomPlan = 'Tradicional'");
                ListaFechaCotiz fechas = JsonConvert.DeserializeObject<ListaFechaCotiz>(json);
                CargaFechas(fechas);
			}
            if (idplan == "2") //angeles
            {
                json = ws.get_catalogos("GetAllSumaAseg_Angeles", "");
                lstangeles = JsonConvert.DeserializeObject<ListaSumaAngeles>(json);
				lst = new ObservableCollection<opciones>();
                foreach (SumaAsegAngeles s in lstangeles.Table)
                    lst.Add(new opciones() { idopc = s.idSumAseg_Angeles.ToString(), opc = s.SumaAsegurada.ToString("c") });
				json = ws.get_catalogos("getConfigFecCotizacion", "@nomPlan = 'Angeles'");
				ListaFechaCotiz fechas = JsonConvert.DeserializeObject<ListaFechaCotiz>(json);
				CargaFechas(fechas);
			}
			LstSuma = lst;
			Ocupado = false;
			IdSuma = String.Empty;
            SumaAseg = " ";
            IniVig = DateTime.Now.Date;
		}
        private void EvaluaPrimaNeta(){
            string categoria = "";
            if (!String.IsNullOrEmpty(IdSuma))
            {
                if (idplan == "1") //tradicional
                {
                    if (Cirugias)
                    {
                        categoria = "D";
                        PrimaNeta = (decimal)lstsumas.Table.Where(x => x.idSumAse_Categoria == int.Parse(idsuma)).FirstOrDefault().D;
                    }
                    else
                    {
                        categoria = "A";
                        PrimaNeta = (decimal)lstsumas.Table.Where(x => x.idSumAse_Categoria == int.Parse(idsuma)).FirstOrDefault().A;
                    }
					Derechos = (decimal)lstsumas.Table.Where(x => x.idSumAse_Categoria == int.Parse(idsuma)).FirstOrDefault().derechos;
				}
                if (idplan == "2") //angeles
                {   // ??? PrimaTotal o PrimaUnica ???
                    PrimaNeta = (decimal)lstangeles.Table.Where(x => x.idSumAseg_Angeles == int.Parse(idsuma)).FirstOrDefault().PrimaUnica;
					Derechos = (decimal)lstangeles.Table.Where(x => x.idSumAseg_Angeles == int.Parse(idsuma)).FirstOrDefault().derechos;
				}
                Cobertura = "RC Profesional\nRC Actividades e Inmuebles";
                if (Adicional)
                {
                    PrimaNeta = PrimaNeta + (0.10M * PrimaNeta);
                    Cobertura += "\nRC Arrendatario";
                }
                SubTotal = PrimaNeta + Derechos;
                Iva = SubTotal * 0.16M;
                PrimaTotal = SubTotal + Iva;
            }
		}

		string cobertura;
		public string Cobertura
		{
			get { return cobertura; }
			set
			{
				if (cobertura != value)
				{
					cobertura = value;
					OnPropertyChanged("Cobertura");
				}
			}
		}
		string idtipo;
		public string IdTipo
		{
			get { return idtipo; }
			set
			{
				if (idtipo != value)
				{
					idtipo = value;
                    ValidarMuestroCuestionario();
					OnPropertyChanged("IdTipo");
				}
			}
		}
        string txtplan;
		public string TxtPlan
		{
			get { return txtplan; }
			set
			{
				if (txtplan != value)
				{
					txtplan = value;
					OnPropertyChanged("TxtPlan");
				}
			}
		}
		ObservableCollection<opciones> lstplan;
        public ObservableCollection<opciones> LstPlan
		{
			get { return lstplan; }
			set
			{
				if (lstplan != value)
				{
					lstplan = value;
					OnPropertyChanged("LstPlan");
				}
			}
		}
		string txttipo;
		public string TxtTipo
		{
			get { return txttipo; }
			set
			{
				if (txttipo != value)
				{
					txttipo = value;
					OnPropertyChanged("TxtTipo");
				}
			}
		}
		ObservableCollection<opciones> lsttipo;
		public ObservableCollection<opciones> LstTipo
		{
			get { return lsttipo; }
			set
			{
				if (lsttipo != value)
				{
					lsttipo = value;
					OnPropertyChanged("LstTipo");
				}
			}
		}
		string idsuma;
		public string IdSuma
		{
			get { return idsuma; }
			set
			{
				if (idsuma != value)
				{
					idsuma = value;
					EvaluaPrimaNeta();
					OnPropertyChanged("IdSuma");
				}
			}
		}
		string txtsuma;
		public string TxtSuma
		{
			get { return txtsuma; }
			set
			{
				if (txtsuma != value)
				{
					txtsuma = value;
					OnPropertyChanged("TxtSuma");
				}
			}
		}
		ObservableCollection<opciones> lstsuma;
		public ObservableCollection<opciones> LstSuma
		{
			get { return lstsuma; }
			set
			{
				if (lstsuma != value)
				{
					lstsuma = value;
					OnPropertyChanged("LstSuma");
				}
			}
		}
        DateTime finvig;
        public DateTime FinVig
        {
            get { return finvig; }
            set
            {
                if (finvig != value)
                {
                    finvig = value;
                    OnPropertyChanged("FinVig");
                }
            }
        }
        DateTime inivig;
		public DateTime IniVig
		{
			get { return inivig; }
			set
			{
				if (inivig != value)
				{
					inivig = value;
                    Inicia = value.ToString("dd/MM/yyyy");
                    FinVig = inivig.AddYears(1);
                    Vence = FinVig.ToString("dd/MM/yyyy");
					OnPropertyChanged("IniVig");
				}
			}
		}
		string txtvig;
		public string TxtVig
		{
			get { return txtvig; }
			set
			{
				if (txtvig != value)
				{
					txtvig = value;
					OnPropertyChanged("TxtVig");
				}
			}
		}
		bool cirugias;
        public bool Cirugias
		{
			get { return cirugias; }
			set
			{
				if (cirugias != value)
				{
					cirugias = value;
					EvaluaPrimaNeta();
					OnPropertyChanged("Cirugias");
				}
			}
		}
		bool ejerce;
		public bool Ejerce
		{
			get { return ejerce; }
			set
			{
				if (ejerce != value)
				{
					ejerce = value;
                    EvaluaPrimaNeta();
					OnPropertyChanged("Ejerce");
				}
			}
		}
		bool adicional;
		public bool Adicional
		{
			get { return adicional; }
			set
			{
				if (adicional != value)
				{
					adicional = value;
					EvaluaPrimaNeta();
					OnPropertyChanged("Adicional");
				}
			}
		}
		bool clickauto;
		public bool ClickAuto
		{
			get { return clickauto; }
			set
			{
				if (clickauto != value)
				{
					clickauto = value;
					OnPropertyChanged("ClickAuto");
				}
			}
		}

		//-----------------  para resumen de cotizacion  ---------------------

		decimal primaneta;
		public decimal PrimaNeta
		{
			get { return primaneta; }
			set
			{
				if (primaneta != value)
				{
					primaneta = value;
					OnPropertyChanged("PrimaNeta");
				}
			}
		}
		string inicia;
		public string Inicia
		{
			get { return inicia; }
			set
			{
				if (inicia != value)
				{
					inicia = value;
					OnPropertyChanged("Inicia");
				}
			}
		}
		string vence;
		public string Vence
		{
			get { return vence; }
			set
			{
				if (vence != value)
				{
					vence = value;
					OnPropertyChanged("Vence");
				}
			}
		}
        string sumaaseg;
        public string SumaAseg
		{
			get { return sumaaseg; }
			set
			{
				if (sumaaseg != value)
				{
					sumaaseg = value;
					OnPropertyChanged("SumaAseg");
				}
			}
		}
		decimal derechos;
		public decimal Derechos
		{
			get { return derechos; }
			set
			{
				if (derechos != value)
				{
					derechos = value;
					OnPropertyChanged("Derechos");
				}
			}
		}
		decimal subtotal;
		public decimal SubTotal
		{
			get { return subtotal; }
			set
			{
				if (subtotal != value)
				{
					subtotal = value;
					OnPropertyChanged("SubTotal");
				}
			}
		}
		decimal iva;
		public decimal Iva
		{
			get { return iva; }
			set
			{
				if (iva != value)
				{
					iva = value;
					OnPropertyChanged("Iva");
				}
			}
		}
        decimal primatotal;
		public decimal PrimaTotal
		{
			get { return primatotal; }
			set
			{
				if (primatotal != value)
				{
					primatotal = value;
					OnPropertyChanged("PrimaTotal");
				}
			}
		}

        //Propiedades FormattedStrings
        FormattedString datosbancarios;
        public FormattedString DatosBancarios
        {
            get { return datosbancarios; }
            set 
            {
                if (datosbancarios != value)
                {
                    datosbancarios = value;
                    OnPropertyChanged("DatosBancarios");
                }
                
            }
        }

        FormattedString datosfiskles;

		public FormattedString DatosFiskles
		{
			get { return datosfiskles; }
			set
			{
				if (datosfiskles != value)
				{
					datosfiskles = value;
					OnPropertyChanged("DatosFiskles");
				}

			}
		}

        FormattedString datosprofesionales;


        public FormattedString DatosProfesionales
		{
			get { return datosprofesionales; }
			set
			{
				if (datosprofesionales != value)
				{
					datosbancarios = value;
					OnPropertyChanged("DatosProfesionales");
				}

			}
		}

		FormattedString datosgenerales;


		public FormattedString DatosGenerales
		{
			get { return datosgenerales; }
			set
			{
				if (datosgenerales != value)
				{
					datosgenerales = value;
					OnPropertyChanged("DatosGenerales");
				}

			}
		}

        FormattedString resumen;
        public FormattedString Resumen1
        {
            get { return resumen; }
            set 
            {
                if (resumen != value)
                {
                    resumen = value;
                    OnPropertyChanged("Resumen1");
                }
            }
        }

    }
}
