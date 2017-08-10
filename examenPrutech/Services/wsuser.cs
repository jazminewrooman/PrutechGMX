// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace GMX.wsUser {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="SecuritySoap", Namespace="http://www.gmx.com.mx/")]
    public partial class Security : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback AuthenticateUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback ValidateAuthCookieOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback UserExistsByExtIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback UserExistsByNameOperationCompleted;
        
        private System.Threading.SendOrPostCallback NewUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddRoleToUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAppAdminOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendPwdRecoveryMailOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetChildsOperationCompleted;
        
        /// CodeRemarks
        public Security() {
            this.Url = "http://148.243.230.134/wsUserAdmin/security.asmx";
        }
        
        public Security(string url) {
            this.Url = url;
        }
        
        /// CodeRemarks
        public event AuthenticateUserCompletedEventHandler AuthenticateUserCompleted;
        
        /// CodeRemarks
        public event ValidateAuthCookieCompletedEventHandler ValidateAuthCookieCompleted;
        
        /// CodeRemarks
        public event GetUserCompletedEventHandler GetUserCompleted;
        
        /// CodeRemarks
        public event UserExistsByExtIdCompletedEventHandler UserExistsByExtIdCompleted;
        
        /// CodeRemarks
        public event UserExistsByNameCompletedEventHandler UserExistsByNameCompleted;
        
        /// CodeRemarks
        public event NewUserCompletedEventHandler NewUserCompleted;
        
        /// CodeRemarks
        public event UpdateUserCompletedEventHandler UpdateUserCompleted;
        
        /// CodeRemarks
        public event AddRoleToUserCompletedEventHandler AddRoleToUserCompleted;
        
        /// CodeRemarks
        public event GetAppAdminCompletedEventHandler GetAppAdminCompleted;
        
        /// CodeRemarks
        public event SendPwdRecoveryMailCompletedEventHandler SendPwdRecoveryMailCompleted;
        
        /// CodeRemarks
        public event GetChildsCompletedEventHandler GetChildsCompleted;
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gmx.com.mx/AuthenticateUser", RequestNamespace="http://www.gmx.com.mx/", ResponseNamespace="http://www.gmx.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bUsers AuthenticateUser(string User, string Password, int ApplicationId) {
            object[] results = this.Invoke("AuthenticateUser", new object[] {
                        User,
                        Password,
                        ApplicationId});
            return ((bUsers)(results[0]));
        }
        
        /// CodeRemarks
        public void AuthenticateUserAsync(string User, string Password, int ApplicationId) {
            this.AuthenticateUserAsync(User, Password, ApplicationId, null);
        }
        
        /// CodeRemarks
        public void AuthenticateUserAsync(string User, string Password, int ApplicationId, object userState) {
            if ((this.AuthenticateUserOperationCompleted == null)) {
                this.AuthenticateUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAuthenticateUserOperationCompleted);
            }
            this.InvokeAsync("AuthenticateUser", new object[] {
                        User,
                        Password,
                        ApplicationId}, this.AuthenticateUserOperationCompleted, userState);
        }
        
        private void OnAuthenticateUserOperationCompleted(object arg) {
            if ((this.AuthenticateUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AuthenticateUserCompleted(this, new AuthenticateUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gmx.com.mx/ValidateAuthCookie", RequestNamespace="http://www.gmx.com.mx/", ResponseNamespace="http://www.gmx.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bUsers ValidateAuthCookie(int User, string CryptPwd, int ApplicationId) {
            object[] results = this.Invoke("ValidateAuthCookie", new object[] {
                        User,
                        CryptPwd,
                        ApplicationId});
            return ((bUsers)(results[0]));
        }
        
        /// CodeRemarks
        public void ValidateAuthCookieAsync(int User, string CryptPwd, int ApplicationId) {
            this.ValidateAuthCookieAsync(User, CryptPwd, ApplicationId, null);
        }
        
        /// CodeRemarks
        public void ValidateAuthCookieAsync(int User, string CryptPwd, int ApplicationId, object userState) {
            if ((this.ValidateAuthCookieOperationCompleted == null)) {
                this.ValidateAuthCookieOperationCompleted = new System.Threading.SendOrPostCallback(this.OnValidateAuthCookieOperationCompleted);
            }
            this.InvokeAsync("ValidateAuthCookie", new object[] {
                        User,
                        CryptPwd,
                        ApplicationId}, this.ValidateAuthCookieOperationCompleted, userState);
        }
        
        private void OnValidateAuthCookieOperationCompleted(object arg) {
            if ((this.ValidateAuthCookieCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ValidateAuthCookieCompleted(this, new ValidateAuthCookieCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gmx.com.mx/GetUser", RequestNamespace="http://www.gmx.com.mx/", ResponseNamespace="http://www.gmx.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bUsers GetUser(int User, int ApplicationId) {
            object[] results = this.Invoke("GetUser", new object[] {
                        User,
                        ApplicationId});
            return ((bUsers)(results[0]));
        }
        
        /// CodeRemarks
        public void GetUserAsync(int User, int ApplicationId) {
            this.GetUserAsync(User, ApplicationId, null);
        }
        
        /// CodeRemarks
        public void GetUserAsync(int User, int ApplicationId, object userState) {
            if ((this.GetUserOperationCompleted == null)) {
                this.GetUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetUserOperationCompleted);
            }
            this.InvokeAsync("GetUser", new object[] {
                        User,
                        ApplicationId}, this.GetUserOperationCompleted, userState);
        }
        
        private void OnGetUserOperationCompleted(object arg) {
            if ((this.GetUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetUserCompleted(this, new GetUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gmx.com.mx/UserExistsByExtId", RequestNamespace="http://www.gmx.com.mx/", ResponseNamespace="http://www.gmx.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UserExistsByExtId(int ExtId, int Source) {
            object[] results = this.Invoke("UserExistsByExtId", new object[] {
                        ExtId,
                        Source});
            return ((bool)(results[0]));
        }
        
        /// CodeRemarks
        public void UserExistsByExtIdAsync(int ExtId, int Source) {
            this.UserExistsByExtIdAsync(ExtId, Source, null);
        }
        
        /// CodeRemarks
        public void UserExistsByExtIdAsync(int ExtId, int Source, object userState) {
            if ((this.UserExistsByExtIdOperationCompleted == null)) {
                this.UserExistsByExtIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUserExistsByExtIdOperationCompleted);
            }
            this.InvokeAsync("UserExistsByExtId", new object[] {
                        ExtId,
                        Source}, this.UserExistsByExtIdOperationCompleted, userState);
        }
        
        private void OnUserExistsByExtIdOperationCompleted(object arg) {
            if ((this.UserExistsByExtIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UserExistsByExtIdCompleted(this, new UserExistsByExtIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gmx.com.mx/UserExistsByName", RequestNamespace="http://www.gmx.com.mx/", ResponseNamespace="http://www.gmx.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UserExistsByName(string User, int Source) {
            object[] results = this.Invoke("UserExistsByName", new object[] {
                        User,
                        Source});
            return ((bool)(results[0]));
        }
        
        /// CodeRemarks
        public void UserExistsByNameAsync(string User, int Source) {
            this.UserExistsByNameAsync(User, Source, null);
        }
        
        /// CodeRemarks
        public void UserExistsByNameAsync(string User, int Source, object userState) {
            if ((this.UserExistsByNameOperationCompleted == null)) {
                this.UserExistsByNameOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUserExistsByNameOperationCompleted);
            }
            this.InvokeAsync("UserExistsByName", new object[] {
                        User,
                        Source}, this.UserExistsByNameOperationCompleted, userState);
        }
        
        private void OnUserExistsByNameOperationCompleted(object arg) {
            if ((this.UserExistsByNameCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UserExistsByNameCompleted(this, new UserExistsByNameCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gmx.com.mx/NewUser", RequestNamespace="http://www.gmx.com.mx/", ResponseNamespace="http://www.gmx.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool NewUser(string UserName, int ExtId, int Source, string Password, string Email, System.DateTime ExpiryDte, int AppId) {
            object[] results = this.Invoke("NewUser", new object[] {
                        UserName,
                        ExtId,
                        Source,
                        Password,
                        Email,
                        ExpiryDte,
                        AppId});
            return ((bool)(results[0]));
        }
        
        /// CodeRemarks
        public void NewUserAsync(string UserName, int ExtId, int Source, string Password, string Email, System.DateTime ExpiryDte, int AppId) {
            this.NewUserAsync(UserName, ExtId, Source, Password, Email, ExpiryDte, AppId, null);
        }
        
        /// CodeRemarks
        public void NewUserAsync(string UserName, int ExtId, int Source, string Password, string Email, System.DateTime ExpiryDte, int AppId, object userState) {
            if ((this.NewUserOperationCompleted == null)) {
                this.NewUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnNewUserOperationCompleted);
            }
            this.InvokeAsync("NewUser", new object[] {
                        UserName,
                        ExtId,
                        Source,
                        Password,
                        Email,
                        ExpiryDte,
                        AppId}, this.NewUserOperationCompleted, userState);
        }
        
        private void OnNewUserOperationCompleted(object arg) {
            if ((this.NewUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.NewUserCompleted(this, new NewUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gmx.com.mx/UpdateUser", RequestNamespace="http://www.gmx.com.mx/", ResponseNamespace="http://www.gmx.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UpdateUser(int UserId, string UserName, string Email, int ExtId, int Source, int ParentId, System.DateTime ExpiryDte) {
            object[] results = this.Invoke("UpdateUser", new object[] {
                        UserId,
                        UserName,
                        Email,
                        ExtId,
                        Source,
                        ParentId,
                        ExpiryDte});
            return ((bool)(results[0]));
        }
        
        /// CodeRemarks
        public void UpdateUserAsync(int UserId, string UserName, string Email, int ExtId, int Source, int ParentId, System.DateTime ExpiryDte) {
            this.UpdateUserAsync(UserId, UserName, Email, ExtId, Source, ParentId, ExpiryDte, null);
        }
        
        /// CodeRemarks
        public void UpdateUserAsync(int UserId, string UserName, string Email, int ExtId, int Source, int ParentId, System.DateTime ExpiryDte, object userState) {
            if ((this.UpdateUserOperationCompleted == null)) {
                this.UpdateUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateUserOperationCompleted);
            }
            this.InvokeAsync("UpdateUser", new object[] {
                        UserId,
                        UserName,
                        Email,
                        ExtId,
                        Source,
                        ParentId,
                        ExpiryDte}, this.UpdateUserOperationCompleted, userState);
        }
        
        private void OnUpdateUserOperationCompleted(object arg) {
            if ((this.UpdateUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateUserCompleted(this, new UpdateUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gmx.com.mx/AddRoleToUser", RequestNamespace="http://www.gmx.com.mx/", ResponseNamespace="http://www.gmx.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool AddRoleToUser(string UserName, int Source, string Role, int AppId) {
            object[] results = this.Invoke("AddRoleToUser", new object[] {
                        UserName,
                        Source,
                        Role,
                        AppId});
            return ((bool)(results[0]));
        }
        
        /// CodeRemarks
        public void AddRoleToUserAsync(string UserName, int Source, string Role, int AppId) {
            this.AddRoleToUserAsync(UserName, Source, Role, AppId, null);
        }
        
        /// CodeRemarks
        public void AddRoleToUserAsync(string UserName, int Source, string Role, int AppId, object userState) {
            if ((this.AddRoleToUserOperationCompleted == null)) {
                this.AddRoleToUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddRoleToUserOperationCompleted);
            }
            this.InvokeAsync("AddRoleToUser", new object[] {
                        UserName,
                        Source,
                        Role,
                        AppId}, this.AddRoleToUserOperationCompleted, userState);
        }
        
        private void OnAddRoleToUserOperationCompleted(object arg) {
            if ((this.AddRoleToUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddRoleToUserCompleted(this, new AddRoleToUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gmx.com.mx/GetAppAdmin", RequestNamespace="http://www.gmx.com.mx/", ResponseNamespace="http://www.gmx.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetAppAdmin(int AppId) {
            object[] results = this.Invoke("GetAppAdmin", new object[] {
                        AppId});
            return ((int)(results[0]));
        }
        
        /// CodeRemarks
        public void GetAppAdminAsync(int AppId) {
            this.GetAppAdminAsync(AppId, null);
        }
        
        /// CodeRemarks
        public void GetAppAdminAsync(int AppId, object userState) {
            if ((this.GetAppAdminOperationCompleted == null)) {
                this.GetAppAdminOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAppAdminOperationCompleted);
            }
            this.InvokeAsync("GetAppAdmin", new object[] {
                        AppId}, this.GetAppAdminOperationCompleted, userState);
        }
        
        private void OnGetAppAdminOperationCompleted(object arg) {
            if ((this.GetAppAdminCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAppAdminCompleted(this, new GetAppAdminCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gmx.com.mx/SendPwdRecoveryMail", RequestNamespace="http://www.gmx.com.mx/", ResponseNamespace="http://www.gmx.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SendPwdRecoveryMail(string UserName, int Source) {
            object[] results = this.Invoke("SendPwdRecoveryMail", new object[] {
                        UserName,
                        Source});
            return ((bool)(results[0]));
        }
        
        /// CodeRemarks
        public void SendPwdRecoveryMailAsync(string UserName, int Source) {
            this.SendPwdRecoveryMailAsync(UserName, Source, null);
        }
        
        /// CodeRemarks
        public void SendPwdRecoveryMailAsync(string UserName, int Source, object userState) {
            if ((this.SendPwdRecoveryMailOperationCompleted == null)) {
                this.SendPwdRecoveryMailOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendPwdRecoveryMailOperationCompleted);
            }
            this.InvokeAsync("SendPwdRecoveryMail", new object[] {
                        UserName,
                        Source}, this.SendPwdRecoveryMailOperationCompleted, userState);
        }
        
        private void OnSendPwdRecoveryMailOperationCompleted(object arg) {
            if ((this.SendPwdRecoveryMailCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendPwdRecoveryMailCompleted(this, new SendPwdRecoveryMailCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.gmx.com.mx/GetChilds", RequestNamespace="http://www.gmx.com.mx/", ResponseNamespace="http://www.gmx.com.mx/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bUsers[] GetChilds(int UserId, int ApplicationId) {
            object[] results = this.Invoke("GetChilds", new object[] {
                        UserId,
                        ApplicationId});
            return ((bUsers[])(results[0]));
        }
        
        /// CodeRemarks
        public void GetChildsAsync(int UserId, int ApplicationId) {
            this.GetChildsAsync(UserId, ApplicationId, null);
        }
        
        /// CodeRemarks
        public void GetChildsAsync(int UserId, int ApplicationId, object userState) {
            if ((this.GetChildsOperationCompleted == null)) {
                this.GetChildsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetChildsOperationCompleted);
            }
            this.InvokeAsync("GetChilds", new object[] {
                        UserId,
                        ApplicationId}, this.GetChildsOperationCompleted, userState);
        }
        
        private void OnGetChildsOperationCompleted(object arg) {
            if ((this.GetChildsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetChildsCompleted(this, new GetChildsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// CodeRemarks
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.gmx.com.mx/")]
    public partial class bUsers {
        
        /// <remarks/>
        public int UserId;
        
        /// <remarks/>
        public string CryptPwd;
        
        /// <remarks/>
        public int ExtId;
        
        /// <remarks/>
        public string Source;
        
        /// <remarks/>
        public string UserName;
        
        /// <remarks/>
        public string DisplayName;
        
        /// <remarks/>
        public string Email;
        
        /// <remarks/>
        public System.DateTime ExpDte;
        
        /// <remarks/>
        public int ParentId;
        
        /// <remarks/>
        public string[] Roles;
    }
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    public delegate void AuthenticateUserCompletedEventHandler(object sender, AuthenticateUserCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AuthenticateUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AuthenticateUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// CodeRemarks
        public bUsers Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bUsers)(this.results[0]));
            }
        }
    }
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    public delegate void ValidateAuthCookieCompletedEventHandler(object sender, ValidateAuthCookieCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ValidateAuthCookieCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ValidateAuthCookieCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// CodeRemarks
        public bUsers Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bUsers)(this.results[0]));
            }
        }
    }
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    public delegate void GetUserCompletedEventHandler(object sender, GetUserCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// CodeRemarks
        public bUsers Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bUsers)(this.results[0]));
            }
        }
    }
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    public delegate void UserExistsByExtIdCompletedEventHandler(object sender, UserExistsByExtIdCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UserExistsByExtIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UserExistsByExtIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void UserExistsByNameCompletedEventHandler(object sender, UserExistsByNameCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UserExistsByNameCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UserExistsByNameCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void NewUserCompletedEventHandler(object sender, NewUserCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class NewUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal NewUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void UpdateUserCompletedEventHandler(object sender, UpdateUserCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void AddRoleToUserCompletedEventHandler(object sender, AddRoleToUserCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddRoleToUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddRoleToUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetAppAdminCompletedEventHandler(object sender, GetAppAdminCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAppAdminCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAppAdminCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// CodeRemarks
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    public delegate void SendPwdRecoveryMailCompletedEventHandler(object sender, SendPwdRecoveryMailCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendPwdRecoveryMailCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendPwdRecoveryMailCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void GetChildsCompletedEventHandler(object sender, GetChildsCompletedEventArgs e);
    
    /// CodeRemarks
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamarinStudio", "7.0.1.24")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetChildsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetChildsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// CodeRemarks
        public bUsers[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bUsers[])(this.results[0]));
            }
        }
    }
}