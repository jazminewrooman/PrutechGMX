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
using GMX.Models;
using GMXHelper;
using System.Text.RegularExpressions;
using System.Globalization;

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
        IUserDialogs diag;
        private int intentosdepago = 0;

        public bool VerImg => !VerCuestionario;

        public VMCotizar(IUserDialogs d, INavigation n) : base(d)
        {
            nav = n;
            diag = d;
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
                await nav.PushAsync(new DatosGenerales(datosgrales, TipoDatos.Generales, this, Modo.Captura));
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
						bindings b = new bindings();
                        b.IniciaWS(apidoc: config.Config["APIDocs"]);
                        var waterm = await b.ReturnDocument("Watermark.jpg", false);
                        if (IdPlan == "1") //tradicional
                            GMX.ViewModels.Emails.GetSlipCotizacionTrad(this);
						if (IdPlan == "2") //angeles
                            GMX.ViewModels.Emails.GetSlipCotizacionAng(this);
						GMX.ViewModels.Emails.docPDF.Watermark = waterm.Result;
                        b.IniciaWS(api: config.Config["APIGMXIT"]);
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

        public void GuardarBD()
        {
            MsgOcupado = "Guardando en base de datos";
            Ocupado = true;
            wsbd.Service bd = new wsbd.Service();
            decimal suma = decimal.Parse(SumaAseg, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
            DatosGralesModel fisc = (DatosFiscales != null ? DatosFiscales : DatosGrales);
            string nombre = $"{App.agent.txt_nombre} {App.agent.txt_apellido1} {App.agent.txt_apellido2}";
            string nombrecliente = $"{DatosGrales.Nombre} {DatosGrales.APaterno} {DatosGrales.AMaterno}";

            var res = bd.insert_Emision(int.Parse(App.agent.cod_agente), nombre, DatosGrales.RFC, nombrecliente, DatosGrales.Direccion, "",
                              "", DatosGrales.Telefono, DatosGrales.CP, DatosGrales.ColoniaStr, DatosGrales.EstadoStr, DatosGrales.MunicipioStr,
                              DatosGrales.Correo, PolizaGenerada.NumPoliza, PolizaGenerada.Referencia, "", "", "", "", "", "",
                              DatosProf.Descripcion, DatosProf.Especialidad.ToString(), DatosProf.CedulaProf, DatosProf.CedulaEsp, DatosProf.Diplomados,
                              suma, (Adicional ? 1 : 0), IniVig, FinVig, DateTime.Now.Date, PrimaNeta, Derechos, Iva, PrimaTotal, App.usr.UserId,
                              0, "", "", "", $"{fisc.Nombre} {fisc.APaterno} {fisc.AMaterno}", fisc.RFC, fisc.Direccion,
                              0, "", "", "", "", "", "", "", "", "", IniVig, "", "", "", "", "", "");
            var resbit = bd.insert_emision_Bitacora(int.Parse(App.agent.cod_agente), nombre, DatosGrales.RFC, nombrecliente, DatosGrales.Direccion, "",
							  "", DatosGrales.Telefono, DatosGrales.CP, DatosGrales.ColoniaStr, DatosGrales.EstadoStr, DatosGrales.MunicipioStr,
							  DatosGrales.Correo, PolizaGenerada.NumPoliza, PolizaGenerada.Referencia, "", "", "", "", "", "",
							  DatosProf.Descripcion, DatosProf.Especialidad.ToString(), DatosProf.CedulaProf, DatosProf.CedulaEsp, DatosProf.Diplomados,
							  suma, (Adicional ? 1 : 0), IniVig, FinVig, DateTime.Now.Date, PrimaNeta, Derechos, Iva, PrimaTotal, App.usr.UserId,
							  0, "", "", "", $"{fisc.Nombre} {fisc.APaterno} {fisc.AMaterno}", fisc.RFC, fisc.Direccion,
							  0, "", "", "", "", "", "", "", "", "", IniVig, "", "", "");
			Ocupado = false;
            MsgOcupado = "";
        }

        public async Task MandarEmision()
        {
            string usuario = "";
            int agrupador = 0;
            decimal sumaasegurada = decimal.Parse(SumaAseg, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
            int tipoagente = int.Parse(App.agent.cod_tipo_agente);
            decimal ComisionRamo1 = 0M; decimal ComisionRamo2 = 0M;

            try
            {
                MsgOcupado = "Enviando a Emision";
                Ocupado = true;
                // coberturas
                BD db = new BD(this);
                List<cobertura> lscob = null;
                if (IdPlan == "1") //tradicional
                {
                    usuario = "GMX-MED"; agrupador = 0;
                    if (Adicional)
                    {
                        ComisionRamo1 = PrimaNeta * 0.3M;
                        ComisionRamo2 = PrimaNeta * 0.7M;
                        lscob = db.SelCoberturas().Where(x => x.IdPlan == 1 && x.RCArrendatario == 1).ToList();
                    }
                    else
                    {
                        ComisionRamo1 = PrimaNeta * 0.1M;
                        ComisionRamo2 = PrimaNeta * 0.9M;
                        lscob = db.SelCoberturas().Where(x => x.IdPlan == 1 && x.RCArrendatario == 0).ToList();
                    }
                }
                if (IdPlan == "2") //angeles
                {
                    ComisionRamo1 = PrimaNeta * 0.3M;
                    ComisionRamo2 = PrimaNeta * 0.7M;
                    usuario = "GMX-ANG"; agrupador = 2;
                    lscob = db.SelCoberturas().Where(x => x.IdPlan == 2).ToList();
                }
                string strcobertura = JsonConvert.SerializeObject(lscob);

                // poliza
                IntegrationServiceEntity.Poliza p = new IntegrationServiceEntity.Poliza()
                {
                    codGrupo = agrupador,
                    codPtoVenta = 2,
                    ramoComercial = 66,
                    fecVigDesde = IniVig,
                    fecVigHasta = FinVig,
                    codGrupoEndo = 1,
                    codTipoEndo = 1,
                    codTipoPoliza = 1,
                    impSumaAsegurada = sumaasegurada,
                    impPrimaMe = PrimaNeta,
                    impDesctoMe = 0,
                    impDecretoMe = 0,
                    impDeremiMe = Derechos,
                    impDerPolizaTur = 0,
                    impIvaMe = Iva,
                    impRecFinMe = 0,
                    impPremioMe = PrimaTotal,
                    codMoneda = 0,
                    pjeIva = 16M,
                    pjeRecargo = 0,
                    pjeDecreto = 0,
                    pjeDescuento = 0,
                    pjeGastosEmision = ((Derechos / PrimaNeta) * 100),
                    pjeDerPolizaTur = 0,
                    polEstado = 0,
                    codPeriodoPago = 5,
                    codUsuario = App.suscriptor.clave.ToString(), //App.suscriptor.Pv.ToString(),
                    fecTarifa = DateTime.Now,
                    codNivFact = 1,
                    codNivImpFact = 2,
                    codOperacion = 1
                };
                if (Antecedentes != null && Antecedentes.anno > 0)
                    p.fecRetroactiva = new DateTime(Antecedentes.anno, Antecedentes.mes, Antecedentes.dia);

                // inciso
                bindings b = new bindings();
                b.IniciaWS();
                var cod = new Dictionary<string, string>();
                cod.Add("codpost", datosgrales.CP);
                var crypdata = await b.getCatalog("GetZonasHuracanByCodpost", cod);
                var strdata = await b.decrypt(crypdata.Result);
                KeyValuePair<int, zonashuracan> zhuracan = JsonConvert.DeserializeObject<Dictionary<int, zonashuracan>>(strdata.Result).FirstOrDefault();
                crypdata = await b.getCatalog("GetZonasTerremotoByCodpost", cod);
                strdata = await b.decrypt(crypdata.Result);
                KeyValuePair<int, zonasterremoto> zterremoto = JsonConvert.DeserializeObject<Dictionary<int, zonasterremoto>>(strdata.Result).FirstOrDefault();

                IntegrationServiceEntity.Inciso inciso = new IntegrationServiceEntity.Inciso()
                {
                    codItem = 1,
                    codIndCob = 1,
                    codGiroNegocio = 895,
                    codPais = 10,
                    codEstado = DatosGrales.Estado,
                    codMuncp = DatosGrales.Municipio,
                    codCiudad = DatosGrales.Ciudad,
                    codColonia = DatosGrales.Colonia,
                    txtDireccion = DatosGrales.Direccion,
                    numExt = ".",
                    codPostal = DatosGrales.CP,
                    impDeremiMe = p.impDeremiMe,
                    impIvaMe = p.impIvaMe,
                    impPremioMe = p.impPremioMe,
                    codSIC = "1",
                    codCalifUw = "1",
                    codTipoBenef1 = 1,
                    txtNombreBenef1 = $"{DatosGrales.Nombre} {DatosGrales.APaterno} {DatosGrales.AMaterno}",
                    codProfe = "8",
                    codDescrip = DatosProf.IdDescripcion.ToString(),
                    codEspecialidad = DatosProf.Especialidad.ToString(),
                    codCedProf = DatosProf.CedulaProf,
                    codCedEspecialidad = DatosProf.CedulaEsp,
                    impParticipaMe = p.impPrimaMe
                };
                inciso.zonaAmisTerrem = zterremoto.Value.zona_amis;
                inciso.zonaCrestaTerrem = zterremoto.Value.zona_cresta;
                inciso.zonaAmisHuracan = zhuracan.Value.zona_amis;
                inciso.zonaCrestaHuracan = zhuracan.Value.zona_cresta;

                // agentes
                IntegrationServiceEntity.Agente ag1 = new IntegrationServiceEntity.Agente()
                {
                    ramoTecnico = 909,
                    codTipoAgente = tipoagente,
                    codAgente = int.Parse(App.agent.cod_agente),
                    pjeComisNormal = (tipoagente == 3 ? 0 : 23),
                    pjeComisExtra = 0,
                    impComisNormalMe = (tipoagente == 3 ? 0 : (ComisionRamo1 * 0.23M)),
                    impComisExtraMe = 0,
                    pjeTasaArt41 = 0,
                    impArt41 = 0
                };
                IntegrationServiceEntity.Agente ag2 = new IntegrationServiceEntity.Agente()
                {
                    ramoTecnico = 911,
                    codTipoAgente = tipoagente,
                    codAgente = int.Parse(App.agent.cod_agente),
                    pjeComisNormal = (tipoagente == 3 ? 0 : 23),
                    pjeComisExtra = 0,
                    impComisNormalMe = (tipoagente == 3 ? 0 : (ComisionRamo2 * 0.23M)),
                    impComisExtraMe = 0,
                    pjeTasaArt41 = 0,
                    impArt41 = 0
                };

                // cliente
                List<IntegrationServiceEntity.Cliente> clientes = new List<IntegrationServiceEntity.Cliente>();
                IntegrationServiceEntity.Cliente clientegrales = new IntegrationServiceEntity.Cliente()
                {
                    tipoPersona = "F",
                    codItem = 0,
                    apPat = DatosGrales.APaterno,
                    apMat = DatosGrales.AMaterno,
                    nombre = DatosGrales.Nombre,
                    tipoDoc = 6,
                    numDoc = "",
                    lugNac = "",
                    sexo = "M",
                    estCivil = 6,
                    nomFactura = "",
                    codTipoDir1 = 1,
                    codPais1 = 10,
                    codEstado1 = DatosGrales.Estado,
                    codMuncp1 = DatosGrales.Municipio,
                    codCiudad1 = DatosGrales.Ciudad,
                    codColonia1 = DatosGrales.Colonia,
                    calle1 = DatosGrales.Direccion,
                    numExt1 = ".",
                    codTipoTel1 = 2,
                    tel1 = DatosGrales.Telefono,
                    rfc = DatosGrales.RFC,
                    codCondPago = 1,
                    codBancEmisor = 1
                };
                clientes.Add(clientegrales);
                if (DatosFiscales != null && !String.IsNullOrEmpty(DatosFiscales.RFC))
                {
                    IntegrationServiceEntity.Cliente clientefiscal = new IntegrationServiceEntity.Cliente()
                    {
                        tipoPersona = (DatosFiscales.Persona == TipoPersona.Fisica ? "F" : "J"),
                        codItem = 1,
                        apPat = DatosFiscales.APaterno,
                        apMat = DatosFiscales.AMaterno,
                        nombre = DatosFiscales.Nombre,
                        tipoDoc = 6,
                        numDoc = "",
                        lugNac = "",
                        sexo = "M",
                        estCivil = 6,
                        nomFactura = "",
                        codTipoDir1 = 1,
                        codPais1 = 10,
                        codEstado1 = DatosFiscales.Estado,
                        codMuncp1 = DatosFiscales.Municipio,
                        codCiudad1 = DatosFiscales.Ciudad,
                        codColonia1 = DatosFiscales.Colonia,
                        calle1 = DatosFiscales.Direccion,
                        numExt1 = ".",
                        codTipoTel1 = 2,
                        tel1 = DatosFiscales.Telefono,
                        rfc = DatosFiscales.RFC,
                        codCondPago = 1,
                        codBancEmisor = 1
                    };
                    clientes.Add(clientefiscal);
                }

                // cuota
                IntegrationServiceEntity.Cuota cuota = new IntegrationServiceEntity.Cuota()
                {
                    codItem = 1,
                    numCuota = 1,
                    fecVenc = p.fecVigHasta,
                    impPrimaMe = p.impPrimaMe,
                    impIvaMe = p.impIvaMe,
                    impRecFinMe = 0,
                    impDerPoliza = p.impDeremiMe,
                    impPremio = p.impPremioMe,
                    impDerPolizaTur = 0,
                    numComprobante = 0,
                    numSerie = "0",
                    snNumerado = 0,
                    fecIniVigCuota = p.fecVigDesde,
                    fecFinVigCuota = p.fecVigHasta,
                    codEstado = 1,
                    fecPago = p.fecTarifa.ToString(),
                    fecEmiComprobante = p.fecTarifa
                };

                // enviar al servicio
                IntegrationServiceEntity.Emission emision = new IntegrationServiceEntity.Emission();
                emision.codSuc = App.suscriptor.Pv;
                emision.llave = config.Config["llave"];
                emision.producto = App.suscriptor.clave.ToString();
                emision.clave = App.suscriptor.clave.ToString();
                emision.cobertura = JsonConvert.DeserializeObject<IntegrationServiceEntity.Cobertura[]>(strcobertura);
                emision.inciso = new IntegrationServiceEntity.Inciso[] { inciso };
                emision.poliza = p;
                emision.cuota = new IntegrationServiceEntity.Cuota[] { cuota };
                emision.agente = new IntegrationServiceEntity.Agente[] { ag1, ag2 };
                emision.cliente = clientes.ToArray();

                var resp = await b.createPolicy(emision);
                var jsonpoliza = await b.decrypt(resp.Result);

                var resultados = JsonConvert.DeserializeObject(jsonpoliza.Result);
                IDictionary<string, Newtonsoft.Json.Linq.JToken> dctRes = (Newtonsoft.Json.Linq.JObject)resultados;
                resultadopolizaerror rperr; resultadopoliza polizanueva;
                if (dctRes.ContainsKey("Error"))
                {
                    rperr = JsonConvert.DeserializeObject<resultadopolizaerror>(jsonpoliza.Result);
                    Ocupado = false;
                    await diag.AlertAsync($"Ocurrieron errores:{Environment.NewLine}{Environment.NewLine}{rperr.Error}", "Error");
                }
                else
                {
                    intentosdepago = 0;
                    PolizaGenerada = JsonConvert.DeserializeObject<resultadopoliza>(jsonpoliza.Result);
                    PolizaGenerada.NumPoliza = $"01-{PolizaGenerada.Ramo.PadLeft(2, '0')}-{PolizaGenerada.NumPoliza.PadLeft(8, '0')}-{PolizaGenerada.Endoso.PadLeft(3, '0')}-{PolizaGenerada.Renovacion.PadLeft(2, '0')}";
                }
                Ocupado = false;
                MsgOcupado = "";
            }
            catch (Exception ex)
            {
                Ocupado = false;
                await diag.AlertAsync(ex.Message, "Error", "Ok");
            }
        }

        public void MandarPagar()
        {
            MsgOcupado = "Procesando pago";
            Ocupado = true;
            intentosdepago++;
            DateTime dtExp = new DateTime(int.Parse(DatosBank.Anio), int.Parse(DatosBank.Mes), 1);
            wspago.PaymentCenter wsp = new wspago.PaymentCenter();
            wsp.Timeout = 5000;

            transbanco = wsp.ExecuteDirectPayment(DatosBank.Merchant, String.Format("{0}{1:00}", PolizaGenerada.Referencia,
            intentosdepago), (double)PrimaTotal, config.Config["MIT_Currency"], (DatosBank.TipoTarj == CreditCardValidator.CardIssuer.Visa ? "V" : "MC"),
            DatosBank.Nombre, DatosBank.NumTarjeta.Replace("-", "").Replace(" ", ""), dtExp, DatosBank.CodigoSeg);

            // simulamos happy path
            /*transbanco = new wspago.MITResponse
            {
                reference = "0817100066191413526900",
                response = "approved",
                foliocpagos = "461555306",
                auth = "012256",
                cd_response = "00",
                nb_company = "GMX SEGUROS",
                nb_merchant = "7203635 GMX",
                nb_street = "INSURGENTES SUR 1605 COL. SAN JOSE INSURGENTES, DF",
                cc_type = "CREDITO/BANCO INVEX/Visa",
                tp_operation = "VENTA",
                cc_name = "LUIS AZUL CONTRERAS MORENO",
                cc_number = "5551",
                cc_expmonth = "04",
                cc_expyear = "22",
                amount = "9312.48",
                voucher = "A4A9293B0E3FEE5762F0DA7871BB74B4D74BAFB9EB1F1374618FCFE7E9ED4DA5DFB1399AE29EAE42A5AA26327B0AC461CC288DE233B44E4B2C842221236F8F4CC8C5039E63100A44F89547E1DF6DFBC1ABAE8F91B7BC7DCFFC1885D80DEE0FD53BB7BBDFC937A52BC7D143A41DC313BF2856D4DDB655E041B8450536656B9A1B315785029206E44F93EAE6B191DA7EE5A8E702E22B00D02385794B2BE0F1C98735CFF862CBA63AA9CB010BC590A3CF222DE4E50ED5776527A469FBE70FD6F41A32E90A057E98B428C3784DEC6C23AB41DF840D46F2CA4AAE09D139139E154659B1B8A5DDAAE408980B1D40FC5C6DB6B3287C54FAFFA1BCC252DC1E38856E5DC8B37D8E1928A4B4C30E6ED258AD671BE146F7AC4988261B51FA9DB20B6F6341835951855F217BE28059199D8CFF7DE6C1BCD41B986E95FCD2CAEE897EC0ED162916EC8348DD993B42683FEF382401D935CC48001FDD4633B39B020350E3D7365EF5ABC1A352405B2F2FF5ED4A1E12FE30F9869106556FC1FB35F3E3B6167E7269E298958EC2771403A41C5976D86E264E6AFDF75A6E68963E6595E27DA2079C3C1E88F80397CDF5EE569EFEC2731B64D7C0B78EF7BBCCAF2888DA89DF12B6E94699FB2ACC7CD68FF3A4F8E8A69606550DD9FCAC548E9F57AB5F2E217D67CA5689FE10B9A0C96C8EC9B186A5FF929D00149DF1F655A8F1CA1F4095F06DFCC845A15437",
                voucher_comercio = "A4A9293B0E3FEE5762F0DA7871BB74B4D74BAFB9EB0A047F7187BBA289857FE6BB9F7AA2C9F0BD4EDD8035145231B669D250A7F1159D75394BE73A1B4A2CA66FB0D211FC130674349C9B49F6AC47B1EDA6AEC1F8B0A17BC8E91A8EC21CF87CA63DB0C9CECE31A03EC7FB098810C35DDC2455B6A0D7488145D75C0F45690580003045920B830DE326E0E682B3F7F034C9A4FB02D85B56881EF21B2A2BF3CDBAD067A8BF7A87C214EAEB0005A1C1E493140ED08C1E8D053D559C73F1BF7DEEE00C69FC715B70C69536C77319DB336DB05C9DCC410CB892389613DB2C2CEB48083DD19799F6CEFB39B9473A3B822B0DA4C8462051C0A2D7DFF15F927A58A44E3886BA45AF584AACB7D00E1CCF53DB0263EE30BEFF0C88157700B98F967F342D6EFB3A32A2506E44EFCF3427DEA1F27BBBEBE3DA2BF838D1EDEDBFB7D420D68C4C3B35C0AC2CCEDC572C3E71A128557DBE4BAE2C7523AB1464E08F17105CFDD23E5CEBBFF3C373767B2F64C0FB1604389E6CA6DA837C132AB99563BDADB6167E665593C1CED5D1014740C63D0F38867A5B1B35FCFF4A7F7F962F739FE87CAF029D2C249EC472CC8EE1D525C9BED4406B3685EAFDA2FBA7CCE41D9E8693F572F5A708999444A111BCE68CF187BDD9C3790012EACD9D2BF1E002B462035E2878F967C7D416BFADCF2DF8BCC8E1AA8EF1E77E04EF899830CC8EAA6E29818C02A4AD01AE164F1778BE28CC4A5D4F3AAC7CF60048EB3337EAB2B7BBC7F9AF13FEBE808DE440F4129F1B2212FCE3837A37F97F3FE7D48DEB53E8E1963D2808076F8433CACC306ACE108394C15A534D0AC0E785EB06E20DE48F3FB2AE8039C7ADE7850AB2F00A6347B0B42A38FD40D7615276EC81A94CCDAC1B7262679C172ECB1756923B252683B7EF9E8ADE33FA06F22E0729B8CF034A039B85C6964635E80BC86708F0B00F28351B0EA8C12EAD7B170D1A33074EDE35CE7B65F9DC135B438C47594FFC42E844940E4B6945F6046015126022650DAF841D87D27A86AAAE9E8B0971867E210D7B8860C9B19DB2",
                voucher_cliente = "A4A9293B0E3FEE5762F0DA7871BB74B4D74BAFB9EB0A047F7187BBA289857FE6BB9F7AA2C9F0BD4EDD8035145231B669D250A7F1159D75394BE73A1B4A2CA66FB0D211FC130674349C9B49F6AC47B1EDA6AEC1F8B0A17BC8E91A8EC21CF87CA63DB0C9CECE31A03EC7FB098810C35DDC2455B6A0D7488145D75C0F45690580003045920B830DE326E0E682B3F7F034C9A4FB02D85B56881EF21B2A2BF3CDBAD067A8BF7A87C27483C50C57B1FB91C92937A59141835B7C2DB674F7FE4D8EF41A7FA4474C26EA8C3CD56E588F3C7CE82EA5D64B54CAAA2C8005835A4B9B3D6163DED4B5D9A0EE13F76D1157F7470DC093154B31A89095A79139BC7716853C5081DE5DCD3E25A8D4DD6C68AB58A067318104A5AC67E873590CEBEECC6D18014D9F5651855F2F67ACAA5038A3AD8D66D3AE8E994FDA7A87EDDED3E88B7CD8F9244937DAA34885BC3F1E72158F64725ECE4B8C1D4207D1572AA28F130453E0D43A5BF6BBFDC3346E596100A0CB01421CE420E8948313437FF38A6EACBDA6066864529BC5CFD1D1044356F54D5376E21A65103DF2841F2B718C2F738FF06DB8039C2E269DC463FF9FDFC41CDC86A215356885C09DC4D5AB9E8562AAC4C7BB7293E24BD1C54FB313BDFF9CC28885C9F86E2A63B099C13BEBF439D40A51740232D56BDBD45A9AADC922E2BCB590A5F18A840573919EFD4FDAF8D70D33E1F870E3C445A1271D7333BA05ED60176C36B07CDC0167A50F52E3A8BAB7D786B40981A48C86FE3FEF0283113008EDE8930562E642129881ED827DE9AF94720332115FD413F7F60F6A871FAFB2B315416F378E8ABCDC31F323B4F55FDA9CC241A7C4C98949B28979723DC7CD504F8440980F781A83F2A912",
                friendly_response = "Aprobado",
            };*/

			// simulamos denegado
			/*transbanco = new wspago.MITResponse
            {
                reference = "0817090045141379324600",
                response = "denied",
                foliocpagos = "449422737",
                cd_response = "05",
                cc_name = "PAULO RAFAEL MURILLO BLANCO",
                cc_number = "8291",
                cc_expmonth = "09",
                cc_expyear = "21",
                friendly_response = "TRANSACCION INVALIDA",
            };*/

			//  simulamos error
			transbanco = new wspago.MITResponse
            {
                reference = "0117100068001414225400",
                response = "error",
                cd_error = "07",
                nb_error = "No se puede cobrar a meses con ese banco",
            };

			Ocupado = false;
            MsgOcupado = "";
        }

        private bool Validar()
        {
            if (String.IsNullOrEmpty(IdTipo) || String.IsNullOrEmpty(IdPlan) || String.IsNullOrEmpty(IdSuma) || IniVig == DateTime.MinValue)
                return false;
            else
                return true;
        }

		private wspago.MITResponse transbanco;
		public wspago.MITResponse TransBanco
		{
			get => transbanco;
			set
			{
				if (transbanco != value)
				{
					transbanco = value;
					OnPropertyChanged("TransBanco");
				}
			}
		}
        private resultadopoliza polizagenerada;
        public resultadopoliza PolizaGenerada
        {
            get => polizagenerada;
            set
            {
                if (polizagenerada != value)
                {
                    polizagenerada = value;
                    OnPropertyChanged("PolizaGenerada");
                }
            }
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
					datosprofesionales = value;
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
