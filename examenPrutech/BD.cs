using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using GMX.Services;
using SQLite;
using System.Globalization;

namespace GMX
{
    public class BD
    {
        static object locker = new object();
        SQLiteConnection database;

        string DatabasePath
        {
            get
            {
                var sqliteFilename = "gmx.db3";
#if __IOS__
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
                var path = Path.Combine(libraryPath, sqliteFilename);
#else
#if __ANDROID__
                string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
                var path = Path.Combine (documentsPath, sqliteFilename);
#else
                    // WinPhone
                    var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);;
#endif
#endif
                return path;
            }
        }

        public BD(VMCotizar vm)
        {
            database = new SQLiteConnection(DatabasePath, true);
            // create the tables
            database.CreateTable<cobertura>();
            InsertaCoberturas(vm);
        }

        public void InsertaCoberturas(VMCotizar vm)
        {
            decimal sumaasegdec = decimal.Parse(vm.SumaAseg, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
            string sumaaseg = sumaasegdec.ToString("##.00");
            try
            {
                database.DeleteAll<cobertura>();

                // idplan 1 tradicional RCArrendatario = 1
                string sql = $"insert into cobertura values (1, 1, -1, -1, 911, 1, 1, 159, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0,0,0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.5M).ToString()}, {(vm.PrimaNeta * 0.5M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (1, 1, 0, -1, 911, 1, 2, 326, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0,0,0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.2M).ToString()}, {(vm.PrimaNeta * 0.2M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (1, 1, 0, -1, 909, 1, 3, 139, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0,0,0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.1M).ToString()}, {(vm.PrimaNeta * 0.1M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (1, 1, 0, -1, 909, 1, 4, 140, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0,0,0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.1M).ToString()}, {(vm.PrimaNeta * 0.1M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (1, 1, 0, -1, 909, 1, 5, 227, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0,0,0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.1M).ToString()}, {(vm.PrimaNeta * 0.1M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);

                // idplan 1 tradicional RCArrendatario = 0
                sql = $"insert into cobertura values (1, 0, -1, -1, 911, 1, 1, 159, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0,0,0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.6M).ToString()}, {(vm.PrimaNeta * 0.6M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (1, 0, 0, -1, 911, 1, 2, 326, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0,0,0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.2M).ToString()}, {(vm.PrimaNeta * 0.2M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (1, 0, 0, -1, 909, 1, 3, 139, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0,0,0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.1M).ToString()}, {(vm.PrimaNeta * 0.1M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (1, 0, 0, -1, 909, 1, 4, 227, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0,0,0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.1M).ToString()}, {(vm.PrimaNeta * 0.1M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);

                // idplan 2 angeles
                sql = $"insert into cobertura values (2, 0, -1, -1, 911, 1, 1, 159, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0, 0, 0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.4M).ToString()}, {(vm.PrimaNeta * 0.4M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (2, 0, 0, -1, 911, 1, 2, 326, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0, 0, 0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.3M).ToString()}, {(vm.PrimaNeta * 0.3M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (2, 0, 0, -1, 909, 1, 3, 139, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0, 0, 0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.1M).ToString()}, {(vm.PrimaNeta * 0.1M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (2, 0, 0, -1, 909, 1, 4, 140, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0, 0, 0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.05M).ToString()}, {(vm.PrimaNeta * 0.05M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (2, 0, 0, -1, 909, 1, 5, 146, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0, 0, 0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.05M).ToString()}, {(vm.PrimaNeta * 0.05M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (2, 0, 0, -1, 909, 1, 6, 149, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0, 0, 0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.05M).ToString()}, {(vm.PrimaNeta * 0.05M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
                sql = $"insert into cobertura values (2, 0, 0, -1, 909, 1, 7, 227, 0, 1, 1, {sumaaseg}, {sumaaseg}, 0, 0, 0, 0, 0, 0, 0, '', {(vm.PrimaNeta * 0.05M).ToString()}, {(vm.PrimaNeta * 0.05M).ToString()}, 0, 0, 0, 0, 0, 0, {sumaaseg})";
                database.Execute(sql);
            }
            catch { }
        }

        public List<cobertura> SelCoberturas()
        {
            List<cobertura> lp;
            lock (locker)
            {
                lp = database.Table<cobertura>().ToList();
            }
            return (lp);
        }
    }
}