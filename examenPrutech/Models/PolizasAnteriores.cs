using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GMX.Models
{
    public class PolizasAnteriores
    {
        public PolizasAnteriores()
        {
        }

        public NumPoliza poliza1 { get; set; }
        public NumPoliza poliza2 { get; set; }
        public NumPoliza poliza3 { get; set; }
        public int dia { get; set; }
        public int mes { get; set; }
        public int anno { get; set; }
    }

    public class NumPoliza : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string oficina { get; set; }
        public string producto { get; set; }
        public string poliza { get; set; }
        public string endoso { get; set; }

        private string renov;
        public string renovacion
        {
            get => renov;
            set
            {
                if (renov != value)
                {
                    renov = value;
                    NotifyPropertyChanged("renovacion");
                }
            }
        }

        public string fullpoliza { get; set; }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

}
