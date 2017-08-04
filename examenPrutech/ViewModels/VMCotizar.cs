using System;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Collections.Generic;
using GMX.Views;

namespace GMX
{
    public class VMCotizar : VMGmx
    {
        INavigation nav;

		public VMCotizar(IUserDialogs diag, INavigation n) : base(diag)
		{
			Title = "Cotizar";
            nav = n;

            TxtPlan = "Plan";
			List<opciones> lst = new List<opciones>();
			lst.Add(new opciones() { idopc = "1", opc = "Médicos Tradicional" });
            lst.Add(new opciones() { idopc = "2", opc = "Médicos Angeles" });
            LstPlan = lst;
            TxtTipo = "Tipo de Póliza";
            List<opciones> lst2 = new List<opciones>();
			lst2.Add(new opciones() { idopc = "1", opc = "Póliza nueva" });
			lst2.Add(new opciones() { idopc = "2", opc = "Renovación" });
            LstTipo = lst2;
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
		List<opciones> lstplan;
        public List<opciones> LstPlan
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
		List<opciones> lsttipo;
		public List<opciones> LstTipo
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
    }
}
