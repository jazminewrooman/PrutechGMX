// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace GMX.wsbd {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="http://tempuri.org/")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback get_catalogosOperationCompleted;
        
        private System.Threading.SendOrPostCallback insert_EmisionOperationCompleted;
        
        private System.Threading.SendOrPostCallback insert_emision_BitacoraOperationCompleted;
        
        /// CodeRemarks
        public Service() {
            this.Url = "http://desa.gmx.com.mx/WS_Database_Access_PVLs/Service.asmx";
        }
        
        public Service(string url) {
            this.Url = url;
        }
        
        /// CodeRemarks
        public event get_catalogosCompletedEventHandler get_catalogosCompleted;
        
        /// CodeRemarks
        public event insert_EmisionCompletedEventHandler insert_EmisionCompleted;
        
        /// CodeRemarks
        public event insert_emision_BitacoraCompletedEventHandler insert_emision_BitacoraCompleted;
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/get_catalogos", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string get_catalogos(string procedimiento, string parametros) {
            object[] results = this.Invoke("get_catalogos", new object[] {
                        procedimiento,
                        parametros});
            return ((string)(results[0]));
        }
        
        /// CodeRemarks
        public void get_catalogosAsync(string procedimiento, string parametros) {
            this.get_catalogosAsync(procedimiento, parametros, null);
        }
        
        /// CodeRemarks
        public void get_catalogosAsync(string procedimiento, string parametros, object userState) {
            if ((this.get_catalogosOperationCompleted == null)) {
                this.get_catalogosOperationCompleted = new System.Threading.SendOrPostCallback(this.Onget_catalogosOperationCompleted);
            }
            this.InvokeAsync("get_catalogos", new object[] {
                        procedimiento,
                        parametros}, this.get_catalogosOperationCompleted, userState);
        }
        
        private void Onget_catalogosOperationCompleted(object arg) {
            if ((this.get_catalogosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.get_catalogosCompleted(this, new get_catalogosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/insert_Emision", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool insert_Emision(
                    int Id_Agente, 
                    string txt_Agente, 
                    string Rfc_Cliente, 
                    string Nombre_Cliente, 
                    string Calle_Cliente, 
                    string No_Ext_Cliente, 
                    string No_Int_Cliente, 
                    string Telefono_Cliente, 
                    string Cp_Cliente, 
                    string Colonia_Cliente, 
                    string Estado_Cliente, 
                    string Municipio_Cliente, 
                    string Email_Cliente, 
                    string Poliza, 
                    string Referencia, 
                    string Estatus_Transaccion, 
                    string Operacion, 
                    string Autorizacion, 
                    string Tarjeta, 
                    string Respuesta_Xml, 
                    string Genero_Recibo, 
                    string Especialidad, 
                    string SubEspecialidad, 
                    string NoCedulaPro, 
                    string NoCedulaEsp, 
                    string Otros, 
                    decimal Suma_Asegurada, 
                    int Arrendatario, 
                    System.DateTime Vigencia_Ini, 
                    System.DateTime Vigencia_Fin, 
                    System.DateTime Emision, 
                    decimal PrimaNeta, 
                    decimal Derechos, 
                    decimal Iva, 
                    decimal PrimaTotal, 
                    int UserId, 
                    int Tipo_Negocio, 
                    string No_Empleado, 
                    string Centro_Work, 
                    string Puesto, 
                    string RazonSocial, 
                    string RfcFis, 
                    string DomicilioFis, 
                    int movimiento, 
                    string MSI, 
                    string Especialidad2, 
                    string NoCedulaEsp2, 
                    string PolizasAnt, 
                    string PolAnt1, 
                    string NoSocioEspanol, 
                    string NoCedulaEspecialidad2_ABC, 
                    string Especialidad2_ABC, 
                    string NoCredencializacionTecSalud, 
                    System.DateTime fecRetroactiva, 
                    string MatriculaImss, 
                    string CentroTrabajoImss, 
                    string TipoContratoImss, 
                    string NoEmpleadoEnf, 
                    string AdscripcionEnf, 
                    string PuestoEnf) {
            object[] results = this.Invoke("insert_Emision", new object[] {
                        Id_Agente,
                        txt_Agente,
                        Rfc_Cliente,
                        Nombre_Cliente,
                        Calle_Cliente,
                        No_Ext_Cliente,
                        No_Int_Cliente,
                        Telefono_Cliente,
                        Cp_Cliente,
                        Colonia_Cliente,
                        Estado_Cliente,
                        Municipio_Cliente,
                        Email_Cliente,
                        Poliza,
                        Referencia,
                        Estatus_Transaccion,
                        Operacion,
                        Autorizacion,
                        Tarjeta,
                        Respuesta_Xml,
                        Genero_Recibo,
                        Especialidad,
                        SubEspecialidad,
                        NoCedulaPro,
                        NoCedulaEsp,
                        Otros,
                        Suma_Asegurada,
                        Arrendatario,
                        Vigencia_Ini,
                        Vigencia_Fin,
                        Emision,
                        PrimaNeta,
                        Derechos,
                        Iva,
                        PrimaTotal,
                        UserId,
                        Tipo_Negocio,
                        No_Empleado,
                        Centro_Work,
                        Puesto,
                        RazonSocial,
                        RfcFis,
                        DomicilioFis,
                        movimiento,
                        MSI,
                        Especialidad2,
                        NoCedulaEsp2,
                        PolizasAnt,
                        PolAnt1,
                        NoSocioEspanol,
                        NoCedulaEspecialidad2_ABC,
                        Especialidad2_ABC,
                        NoCredencializacionTecSalud,
                        fecRetroactiva,
                        MatriculaImss,
                        CentroTrabajoImss,
                        TipoContratoImss,
                        NoEmpleadoEnf,
                        AdscripcionEnf,
                        PuestoEnf});
            return ((bool)(results[0]));
        }
        
        /// CodeRemarks
        public void insert_EmisionAsync(
                    int Id_Agente, 
                    string txt_Agente, 
                    string Rfc_Cliente, 
                    string Nombre_Cliente, 
                    string Calle_Cliente, 
                    string No_Ext_Cliente, 
                    string No_Int_Cliente, 
                    string Telefono_Cliente, 
                    string Cp_Cliente, 
                    string Colonia_Cliente, 
                    string Estado_Cliente, 
                    string Municipio_Cliente, 
                    string Email_Cliente, 
                    string Poliza, 
                    string Referencia, 
                    string Estatus_Transaccion, 
                    string Operacion, 
                    string Autorizacion, 
                    string Tarjeta, 
                    string Respuesta_Xml, 
                    string Genero_Recibo, 
                    string Especialidad, 
                    string SubEspecialidad, 
                    string NoCedulaPro, 
                    string NoCedulaEsp, 
                    string Otros, 
                    decimal Suma_Asegurada, 
                    int Arrendatario, 
                    System.DateTime Vigencia_Ini, 
                    System.DateTime Vigencia_Fin, 
                    System.DateTime Emision, 
                    decimal PrimaNeta, 
                    decimal Derechos, 
                    decimal Iva, 
                    decimal PrimaTotal, 
                    int UserId, 
                    int Tipo_Negocio, 
                    string No_Empleado, 
                    string Centro_Work, 
                    string Puesto, 
                    string RazonSocial, 
                    string RfcFis, 
                    string DomicilioFis, 
                    int movimiento, 
                    string MSI, 
                    string Especialidad2, 
                    string NoCedulaEsp2, 
                    string PolizasAnt, 
                    string PolAnt1, 
                    string NoSocioEspanol, 
                    string NoCedulaEspecialidad2_ABC, 
                    string Especialidad2_ABC, 
                    string NoCredencializacionTecSalud, 
                    System.DateTime fecRetroactiva, 
                    string MatriculaImss, 
                    string CentroTrabajoImss, 
                    string TipoContratoImss, 
                    string NoEmpleadoEnf, 
                    string AdscripcionEnf, 
                    string PuestoEnf) {
            this.insert_EmisionAsync(Id_Agente, txt_Agente, Rfc_Cliente, Nombre_Cliente, Calle_Cliente, No_Ext_Cliente, No_Int_Cliente, Telefono_Cliente, Cp_Cliente, Colonia_Cliente, Estado_Cliente, Municipio_Cliente, Email_Cliente, Poliza, Referencia, Estatus_Transaccion, Operacion, Autorizacion, Tarjeta, Respuesta_Xml, Genero_Recibo, Especialidad, SubEspecialidad, NoCedulaPro, NoCedulaEsp, Otros, Suma_Asegurada, Arrendatario, Vigencia_Ini, Vigencia_Fin, Emision, PrimaNeta, Derechos, Iva, PrimaTotal, UserId, Tipo_Negocio, No_Empleado, Centro_Work, Puesto, RazonSocial, RfcFis, DomicilioFis, movimiento, MSI, Especialidad2, NoCedulaEsp2, PolizasAnt, PolAnt1, NoSocioEspanol, NoCedulaEspecialidad2_ABC, Especialidad2_ABC, NoCredencializacionTecSalud, fecRetroactiva, MatriculaImss, CentroTrabajoImss, TipoContratoImss, NoEmpleadoEnf, AdscripcionEnf, PuestoEnf, null);
        }
        
        /// CodeRemarks
        public void insert_EmisionAsync(
                    int Id_Agente, 
                    string txt_Agente, 
                    string Rfc_Cliente, 
                    string Nombre_Cliente, 
                    string Calle_Cliente, 
                    string No_Ext_Cliente, 
                    string No_Int_Cliente, 
                    string Telefono_Cliente, 
                    string Cp_Cliente, 
                    string Colonia_Cliente, 
                    string Estado_Cliente, 
                    string Municipio_Cliente, 
                    string Email_Cliente, 
                    string Poliza, 
                    string Referencia, 
                    string Estatus_Transaccion, 
                    string Operacion, 
                    string Autorizacion, 
                    string Tarjeta, 
                    string Respuesta_Xml, 
                    string Genero_Recibo, 
                    string Especialidad, 
                    string SubEspecialidad, 
                    string NoCedulaPro, 
                    string NoCedulaEsp, 
                    string Otros, 
                    decimal Suma_Asegurada, 
                    int Arrendatario, 
                    System.DateTime Vigencia_Ini, 
                    System.DateTime Vigencia_Fin, 
                    System.DateTime Emision, 
                    decimal PrimaNeta, 
                    decimal Derechos, 
                    decimal Iva, 
                    decimal PrimaTotal, 
                    int UserId, 
                    int Tipo_Negocio, 
                    string No_Empleado, 
                    string Centro_Work, 
                    string Puesto, 
                    string RazonSocial, 
                    string RfcFis, 
                    string DomicilioFis, 
                    int movimiento, 
                    string MSI, 
                    string Especialidad2, 
                    string NoCedulaEsp2, 
                    string PolizasAnt, 
                    string PolAnt1, 
                    string NoSocioEspanol, 
                    string NoCedulaEspecialidad2_ABC, 
                    string Especialidad2_ABC, 
                    string NoCredencializacionTecSalud, 
                    System.DateTime fecRetroactiva, 
                    string MatriculaImss, 
                    string CentroTrabajoImss, 
                    string TipoContratoImss, 
                    string NoEmpleadoEnf, 
                    string AdscripcionEnf, 
                    string PuestoEnf, 
                    object userState) {
            if ((this.insert_EmisionOperationCompleted == null)) {
                this.insert_EmisionOperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsert_EmisionOperationCompleted);
            }
            this.InvokeAsync("insert_Emision", new object[] {
                        Id_Agente,
                        txt_Agente,
                        Rfc_Cliente,
                        Nombre_Cliente,
                        Calle_Cliente,
                        No_Ext_Cliente,
                        No_Int_Cliente,
                        Telefono_Cliente,
                        Cp_Cliente,
                        Colonia_Cliente,
                        Estado_Cliente,
                        Municipio_Cliente,
                        Email_Cliente,
                        Poliza,
                        Referencia,
                        Estatus_Transaccion,
                        Operacion,
                        Autorizacion,
                        Tarjeta,
                        Respuesta_Xml,
                        Genero_Recibo,
                        Especialidad,
                        SubEspecialidad,
                        NoCedulaPro,
                        NoCedulaEsp,
                        Otros,
                        Suma_Asegurada,
                        Arrendatario,
                        Vigencia_Ini,
                        Vigencia_Fin,
                        Emision,
                        PrimaNeta,
                        Derechos,
                        Iva,
                        PrimaTotal,
                        UserId,
                        Tipo_Negocio,
                        No_Empleado,
                        Centro_Work,
                        Puesto,
                        RazonSocial,
                        RfcFis,
                        DomicilioFis,
                        movimiento,
                        MSI,
                        Especialidad2,
                        NoCedulaEsp2,
                        PolizasAnt,
                        PolAnt1,
                        NoSocioEspanol,
                        NoCedulaEspecialidad2_ABC,
                        Especialidad2_ABC,
                        NoCredencializacionTecSalud,
                        fecRetroactiva,
                        MatriculaImss,
                        CentroTrabajoImss,
                        TipoContratoImss,
                        NoEmpleadoEnf,
                        AdscripcionEnf,
                        PuestoEnf}, this.insert_EmisionOperationCompleted, userState);
        }
        
        private void Oninsert_EmisionOperationCompleted(object arg) {
            if ((this.insert_EmisionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insert_EmisionCompleted(this, new insert_EmisionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/insert_emision_Bitacora", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string insert_emision_Bitacora(
                    int Id_Agente, 
                    string txt_Agente, 
                    string Rfc_Cliente, 
                    string Nombre_Cliente, 
                    string Calle_Cliente, 
                    string No_Ext_Cliente, 
                    string No_Int_Cliente, 
                    string Telefono_Cliente, 
                    string Cp_Cliente, 
                    string Colonia_Cliente, 
                    string Estado_Cliente, 
                    string Municipio_Cliente, 
                    string Email_Cliente, 
                    string Poliza, 
                    string Referencia, 
                    string Estatus_Transaccion, 
                    string Operacion, 
                    string Autorizacion, 
                    string Tarjeta, 
                    string Respuesta_Xml, 
                    string Genero_Recibo, 
                    string Especialidad, 
                    string SubEspecialidad, 
                    string NoCedulaPro, 
                    string NoCedulaEsp, 
                    string Otros, 
                    decimal Suma_Asegurada, 
                    int Arrendatario, 
                    System.DateTime Vigencia_Ini, 
                    System.DateTime Vigencia_Fin, 
                    System.DateTime Emision, 
                    decimal PrimaNeta, 
                    decimal Derechos, 
                    decimal Iva, 
                    decimal PrimaTotal, 
                    int UserId, 
                    int Tipo_Negocio, 
                    string No_Empleado, 
                    string Centro_Work, 
                    string Puesto, 
                    string RazonSocial, 
                    string RfcFis, 
                    string DomicilioFis, 
                    int movimiento, 
                    string MSI, 
                    string Especialidad2, 
                    string NoCedulaEsp2, 
                    string PolizasAnt, 
                    string PolAnt1, 
                    string NoSocioEspanol, 
                    string NoCedulaEspecialidad2_ABC, 
                    string Especialidad2_ABC, 
                    string NoCredencializacionTecSalud, 
                    System.DateTime fecRetroactiva, 
                    string MatriculaImss, 
                    string CentroTrabajoImss, 
                    string TipoContratoImss) {
            object[] results = this.Invoke("insert_emision_Bitacora", new object[] {
                        Id_Agente,
                        txt_Agente,
                        Rfc_Cliente,
                        Nombre_Cliente,
                        Calle_Cliente,
                        No_Ext_Cliente,
                        No_Int_Cliente,
                        Telefono_Cliente,
                        Cp_Cliente,
                        Colonia_Cliente,
                        Estado_Cliente,
                        Municipio_Cliente,
                        Email_Cliente,
                        Poliza,
                        Referencia,
                        Estatus_Transaccion,
                        Operacion,
                        Autorizacion,
                        Tarjeta,
                        Respuesta_Xml,
                        Genero_Recibo,
                        Especialidad,
                        SubEspecialidad,
                        NoCedulaPro,
                        NoCedulaEsp,
                        Otros,
                        Suma_Asegurada,
                        Arrendatario,
                        Vigencia_Ini,
                        Vigencia_Fin,
                        Emision,
                        PrimaNeta,
                        Derechos,
                        Iva,
                        PrimaTotal,
                        UserId,
                        Tipo_Negocio,
                        No_Empleado,
                        Centro_Work,
                        Puesto,
                        RazonSocial,
                        RfcFis,
                        DomicilioFis,
                        movimiento,
                        MSI,
                        Especialidad2,
                        NoCedulaEsp2,
                        PolizasAnt,
                        PolAnt1,
                        NoSocioEspanol,
                        NoCedulaEspecialidad2_ABC,
                        Especialidad2_ABC,
                        NoCredencializacionTecSalud,
                        fecRetroactiva,
                        MatriculaImss,
                        CentroTrabajoImss,
                        TipoContratoImss});
            return ((string)(results[0]));
        }
        
        /// CodeRemarks
        public void insert_emision_BitacoraAsync(
                    int Id_Agente, 
                    string txt_Agente, 
                    string Rfc_Cliente, 
                    string Nombre_Cliente, 
                    string Calle_Cliente, 
                    string No_Ext_Cliente, 
                    string No_Int_Cliente, 
                    string Telefono_Cliente, 
                    string Cp_Cliente, 
                    string Colonia_Cliente, 
                    string Estado_Cliente, 
                    string Municipio_Cliente, 
                    string Email_Cliente, 
                    string Poliza, 
                    string Referencia, 
                    string Estatus_Transaccion, 
                    string Operacion, 
                    string Autorizacion, 
                    string Tarjeta, 
                    string Respuesta_Xml, 
                    string Genero_Recibo, 
                    string Especialidad, 
                    string SubEspecialidad, 
                    string NoCedulaPro, 
                    string NoCedulaEsp, 
                    string Otros, 
                    decimal Suma_Asegurada, 
                    int Arrendatario, 
                    System.DateTime Vigencia_Ini, 
                    System.DateTime Vigencia_Fin, 
                    System.DateTime Emision, 
                    decimal PrimaNeta, 
                    decimal Derechos, 
                    decimal Iva, 
                    decimal PrimaTotal, 
                    int UserId, 
                    int Tipo_Negocio, 
                    string No_Empleado, 
                    string Centro_Work, 
                    string Puesto, 
                    string RazonSocial, 
                    string RfcFis, 
                    string DomicilioFis, 
                    int movimiento, 
                    string MSI, 
                    string Especialidad2, 
                    string NoCedulaEsp2, 
                    string PolizasAnt, 
                    string PolAnt1, 
                    string NoSocioEspanol, 
                    string NoCedulaEspecialidad2_ABC, 
                    string Especialidad2_ABC, 
                    string NoCredencializacionTecSalud, 
                    System.DateTime fecRetroactiva, 
                    string MatriculaImss, 
                    string CentroTrabajoImss, 
                    string TipoContratoImss) {
            this.insert_emision_BitacoraAsync(Id_Agente, txt_Agente, Rfc_Cliente, Nombre_Cliente, Calle_Cliente, No_Ext_Cliente, No_Int_Cliente, Telefono_Cliente, Cp_Cliente, Colonia_Cliente, Estado_Cliente, Municipio_Cliente, Email_Cliente, Poliza, Referencia, Estatus_Transaccion, Operacion, Autorizacion, Tarjeta, Respuesta_Xml, Genero_Recibo, Especialidad, SubEspecialidad, NoCedulaPro, NoCedulaEsp, Otros, Suma_Asegurada, Arrendatario, Vigencia_Ini, Vigencia_Fin, Emision, PrimaNeta, Derechos, Iva, PrimaTotal, UserId, Tipo_Negocio, No_Empleado, Centro_Work, Puesto, RazonSocial, RfcFis, DomicilioFis, movimiento, MSI, Especialidad2, NoCedulaEsp2, PolizasAnt, PolAnt1, NoSocioEspanol, NoCedulaEspecialidad2_ABC, Especialidad2_ABC, NoCredencializacionTecSalud, fecRetroactiva, MatriculaImss, CentroTrabajoImss, TipoContratoImss, null);
        }
        
        /// CodeRemarks
        public void insert_emision_BitacoraAsync(
                    int Id_Agente, 
                    string txt_Agente, 
                    string Rfc_Cliente, 
                    string Nombre_Cliente, 
                    string Calle_Cliente, 
                    string No_Ext_Cliente, 
                    string No_Int_Cliente, 
                    string Telefono_Cliente, 
                    string Cp_Cliente, 
                    string Colonia_Cliente, 
                    string Estado_Cliente, 
                    string Municipio_Cliente, 
                    string Email_Cliente, 
                    string Poliza, 
                    string Referencia, 
                    string Estatus_Transaccion, 
                    string Operacion, 
                    string Autorizacion, 
                    string Tarjeta, 
                    string Respuesta_Xml, 
                    string Genero_Recibo, 
                    string Especialidad, 
                    string SubEspecialidad, 
                    string NoCedulaPro, 
                    string NoCedulaEsp, 
                    string Otros, 
                    decimal Suma_Asegurada, 
                    int Arrendatario, 
                    System.DateTime Vigencia_Ini, 
                    System.DateTime Vigencia_Fin, 
                    System.DateTime Emision, 
                    decimal PrimaNeta, 
                    decimal Derechos, 
                    decimal Iva, 
                    decimal PrimaTotal, 
                    int UserId, 
                    int Tipo_Negocio, 
                    string No_Empleado, 
                    string Centro_Work, 
                    string Puesto, 
                    string RazonSocial, 
                    string RfcFis, 
                    string DomicilioFis, 
                    int movimiento, 
                    string MSI, 
                    string Especialidad2, 
                    string NoCedulaEsp2, 
                    string PolizasAnt, 
                    string PolAnt1, 
                    string NoSocioEspanol, 
                    string NoCedulaEspecialidad2_ABC, 
                    string Especialidad2_ABC, 
                    string NoCredencializacionTecSalud, 
                    System.DateTime fecRetroactiva, 
                    string MatriculaImss, 
                    string CentroTrabajoImss, 
                    string TipoContratoImss, 
                    object userState) {
            if ((this.insert_emision_BitacoraOperationCompleted == null)) {
                this.insert_emision_BitacoraOperationCompleted = new System.Threading.SendOrPostCallback(this.Oninsert_emision_BitacoraOperationCompleted);
            }
            this.InvokeAsync("insert_emision_Bitacora", new object[] {
                        Id_Agente,
                        txt_Agente,
                        Rfc_Cliente,
                        Nombre_Cliente,
                        Calle_Cliente,
                        No_Ext_Cliente,
                        No_Int_Cliente,
                        Telefono_Cliente,
                        Cp_Cliente,
                        Colonia_Cliente,
                        Estado_Cliente,
                        Municipio_Cliente,
                        Email_Cliente,
                        Poliza,
                        Referencia,
                        Estatus_Transaccion,
                        Operacion,
                        Autorizacion,
                        Tarjeta,
                        Respuesta_Xml,
                        Genero_Recibo,
                        Especialidad,
                        SubEspecialidad,
                        NoCedulaPro,
                        NoCedulaEsp,
                        Otros,
                        Suma_Asegurada,
                        Arrendatario,
                        Vigencia_Ini,
                        Vigencia_Fin,
                        Emision,
                        PrimaNeta,
                        Derechos,
                        Iva,
                        PrimaTotal,
                        UserId,
                        Tipo_Negocio,
                        No_Empleado,
                        Centro_Work,
                        Puesto,
                        RazonSocial,
                        RfcFis,
                        DomicilioFis,
                        movimiento,
                        MSI,
                        Especialidad2,
                        NoCedulaEsp2,
                        PolizasAnt,
                        PolAnt1,
                        NoSocioEspanol,
                        NoCedulaEspecialidad2_ABC,
                        Especialidad2_ABC,
                        NoCredencializacionTecSalud,
                        fecRetroactiva,
                        MatriculaImss,
                        CentroTrabajoImss,
                        TipoContratoImss}, this.insert_emision_BitacoraOperationCompleted, userState);
        }
        
        private void Oninsert_emision_BitacoraOperationCompleted(object arg) {
            if ((this.insert_emision_BitacoraCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.insert_emision_BitacoraCompleted(this, new insert_emision_BitacoraCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    public delegate void get_catalogosCompletedEventHandler(object sender, get_catalogosCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class get_catalogosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal get_catalogosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// CodeRemarks
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    public delegate void insert_EmisionCompletedEventHandler(object sender, insert_EmisionCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insert_EmisionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal insert_EmisionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// CodeRemarks
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    public delegate void insert_emision_BitacoraCompletedEventHandler(object sender, insert_emision_BitacoraCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class insert_emision_BitacoraCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal insert_emision_BitacoraCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// CodeRemarks
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}