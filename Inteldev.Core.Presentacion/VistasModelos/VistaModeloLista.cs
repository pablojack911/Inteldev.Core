using ExportToExcel;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Inteldev.Core.Presentacion.VistasModelos
{
    public class VistaModeloLista<TDto> : VistaModeloBase<TDto> where TDto : DTO.DTOBase
    {
        public DataGrid dataGrid { get; set; }
        public ICommand CmdExcel { get; set; }
        public ObservableCollection<TDto> Items
        {
            get { return (ObservableCollection<TDto>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<TDto>), typeof(VistaModeloLista<TDto>));



        public object columnas
        {
            get { return (object)GetValue(columnasProperty); }
            set { SetValue(columnasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for columnas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty columnasProperty =
            DependencyProperty.Register("columnas", typeof(object), typeof(VistaModeloLista<TDto>));

        

        public VistaModeloLista()
        {

        }
        public VistaModeloLista(IList<TDto> dto)
            : base()
        {
            //this.Items = (ObservableCollection<TDto>)dto;
            this.Items = new ObservableCollection<TDto>(dto);
            this.CmdExcel = new RelayCommand(x => TryCatch.Intentar(f => this.ExportarAExcel(), n => Mensajes.Error((Exception)n)));
        }

        private void ExportarAExcel()
        {
            var a = this.columnas;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Libro de Excel|.xlsx";
            saveFileDialog1.Title = "Exportar a Excel";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                CreateExcelFile.CreateExcelDocument(Items.ToList(), saveFileDialog1.FileName);
            }
        }
    }
}
