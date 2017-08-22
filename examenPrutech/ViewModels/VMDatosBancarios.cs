﻿using System;
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

namespace GMX
{
	public class VMDatosBancarios : VMGmx
	{
        VMCotizar vmcotizar;
		INavigation nav;
        GMX.wspago.KeyValue[] lstformaspago;
        public ICommand NextCommand { get; set; }
        public ICommand VerCotizaCommand { get; set; }

        public VMDatosBancarios(IUserDialogs diag, INavigation n, VMCotizar vmc) : base(diag)
        {
            nav = n;
            vmcotizar = vmc;
            TipoTarj = CardIssuer.Unknown;
            Meses = new List<string>();
            for (int i = 1; i <= 12; i++)
                Meses.Add(i.ToString().PadLeft(2, '0'));
            OnPropertyChanged("Meses");
            Anios = new List<string>();
            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + 6; i++)
                Anios.Add(i.ToString());
            OnPropertyChanged("Anios");
            CargaFormaPago();
            NextCommand = new Command(async () =>
            {
                if (!Validar())
                    await diag.AlertAsync(Resources.FaltanOb, "Error", "Ok");
                else
                {
                    FormatText();
                    await nav.PushAsync(new ResumenDatos());
                }
            });
			VerCotizaCommand = new Command(async () =>
			{
                await nav.PushPopupAsync(new VerCotiza(vmcotizar), true);
			});
            if (vmc.DatosBank != null)
                CargaBancarios(vmc.DatosBank);
        }

        public List<string> Meses { get; set; }
        public List<string> Anios { get; set; }
		public string Mes { get; set; }
		public string Anio { get; set; }

        private string codigoseg;
        public string CodigoSeg
        {
            get => codigoseg;
            set
            {
                if (codigoseg != value)
                {
                    codigoseg = value;
                    OnPropertyChanged("CodigoSeg");
                }
            }
        }
		private string nombre;
        public string Nombre
        {
            get => nombre;
            set
            {
                if (nombre != value)
                {
                    nombre = value;
                    OnPropertyChanged("Nombre");
                }
            }
        }
        private List<string> formaspago;
		public List<string> FormasPago
		{
			get => formaspago;
			set
			{
				if (formaspago != value)
				{
					formaspago = value;
					OnPropertyChanged("FormasPago");
				}
			}
		}
        private string formapago;
        public string FormaPago
        {
            get => formapago;
            set
            {
                if (formapago != value){
                    formapago = value;
                    OnPropertyChanged("FormaPago");
                }
            }
        }
        private bool numtarjetavalido;
        public bool NumTarjetaValido
		{
			get => numtarjetavalido;
			set
			{
				if (numtarjetavalido != value)
				{
					numtarjetavalido = value;
					OnPropertyChanged("NumTarjetaValido");
				}
			}
		}
		private string numtarjeta;
        public string NumTarjeta
        {
            get => numtarjeta;
            set
            {
                if (numtarjeta != value)
                {
                    numtarjeta = value;
                    numtarjeta = ValidaFormatCard();
                    OnPropertyChanged("NumTarjeta");
                }
            }
        }
        private ImageSource imgcard;
		public ImageSource ImgCard
		{
			get => imgcard;
			set
			{
				if (imgcard != value)
				{
					imgcard = value;
					OnPropertyChanged("ImgCard");
				}
			}
		}
        private CardIssuer tipotarj;
        public CardIssuer TipoTarj
        {
            get => tipotarj;
            set
            {
                if (tipotarj != value)
                {
                    tipotarj = value;
                    if (tipotarj == CardIssuer.Visa)
                        ImgCard = ImageSource.FromFile("visa.png");
                    if (tipotarj == CardIssuer.MasterCard)
                        ImgCard = ImageSource.FromFile("master.png");
                    if (tipotarj == CardIssuer.Unknown)
                        ImgCard = ImageSource.FromFile("genericcard.png");
                    OnPropertyChanged("TipoTarj");
                }
            }
        }

        private void CargaBancarios(GMX.Models.DatosBancarios db){
            if (db != null){
                Nombre = db.Nombre;
                TipoTarj = db.TipoTarj;
                FormaPago = db.FormaPago;
                NumTarjeta = db.NumTarjeta;
                Mes = db.Mes;
                Anio = db.Anio;
                CodigoSeg = db.CodigoSeg;
                NumTarjetaValido = true;
            }
        }

        private bool Validar()
        {
            if (String.IsNullOrEmpty(Nombre) || TipoTarj == CardIssuer.Unknown || String.IsNullOrEmpty(FormaPago)
                || String.IsNullOrEmpty(NumTarjeta) || String.IsNullOrEmpty(Mes) || String.IsNullOrEmpty(Anio)
                || String.IsNullOrEmpty(CodigoSeg) || !NumTarjetaValido)
                return false;
            else
            {
                GMX.Models.DatosBancarios db = new Models.DatosBancarios()
                {
                    Nombre = Nombre,
                    TipoTarj = TipoTarj,
                    FormaPago = FormaPago,
                    NumTarjeta = NumTarjeta,
                    Mes = Mes,
                    Anio = Anio,
                    CodigoSeg = CodigoSeg
                };
                vmcotizar.DatosBank = db;
                return true;
            }
        }

        private void CargaFormaPago(){
            List<string> tmp = new List<string>();
            Ocupado = true;
			GMX.wspago.PaymentCenter ws = new GMX.wspago.PaymentCenter();
            lstformaspago = ws.GetMerchantIds();
            foreach (GMX.wspago.KeyValue k in lstformaspago)
                tmp.Add(k.Key.Replace("MERCHANT_", ""));
            FormasPago = tmp;
            Ocupado = false;
		}


		private FormattedString FormatText()
		{
			var fs = new FormattedString();
			fs.Spans.Add(new Span { Text = "Nombre Tarjetahabiente: ", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
			fs.Spans.Add(new Span { Text = Nombre + "\\n", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
			fs.Spans.Add(new Span { Text = "Forma de Pago: ", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
            fs.Spans.Add(new Span { Text = FormaPago + "\\n", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
			fs.Spans.Add(new Span { Text = "Número de Tarjeta: ", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
            fs.Spans.Add(new Span { Text = NumTarjeta + "\\n", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
			fs.Spans.Add(new Span { Text = "Fecha Vencimiento: ", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
            fs.Spans.Add(new Span { Text = Mes + "/" + Anio + "\\n", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
			fs.Spans.Add(new Span { Text = "Código Seguridad: ", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
            fs.Spans.Add(new Span { Text = CodigoSeg + "\\n", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
			
			return fs;
		}
        private string ValidaFormatCard(){
			string numberMasked = "";
			int count = numtarjeta.Length;
			string val = numtarjeta;
			if (count < 25)
			{
				switch (count)
				{
					case 5:
						var a = val.Substring(0, 4);
						var b = val.Substring(val.Length - 1);
						numberMasked = (a + " - " + b);
						break;
					case 7:
						numberMasked = val.Substring(0, 4);
						break;
					case 12:
						var c = val.Substring(0, 11);
						var d = val.Substring(val.Length - 1);
						numberMasked = (c + " - " + d);
						break;
					case 14:
						numberMasked = val.Substring(0, 11);
						break;
					case 19:
						var e = val.Substring(0, 18);
						var f = val.Substring(val.Length - 1);
						numberMasked = (e + " - " + f);
						break;
					case 21:
						numberMasked = val.Substring(0, 18);
						break;
					case 25:
						numberMasked = val.Substring(0, 25);
						break;
					default:
						numberMasked = val;
						break;
				}
			}
			else
				numberMasked = val.Substring(0, 25);
            return numberMasked;
        }
	}
}
