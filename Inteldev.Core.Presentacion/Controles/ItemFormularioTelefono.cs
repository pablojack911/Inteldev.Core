using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace Inteldev.Core.Presentacion.Controles
{
    /// <summary>
    /// Clase en Inteldev.Core.Presentacion.Controles
    /// </summary>
    public class ItemFormularioTelefono : ItemFormularioGrilla
    {
        /// <summary>
        /// Crea un grid en base al ItemFormularioGrilla para almacenar datos de teléfonos
        /// </summary>
        public ItemFormularioTelefono()
            : base()
        {
            this.Etiqueta = "Teléfonos";
            this.Columnas.Add(new DataGridTextColumn() { Header = "Tipo", Binding = new Binding("TipoTelefono") });
            //this.Columnas.Add(new DataGridTextColumn() { Header = "Código de País", Binding = new Binding("CodigoPais") });
            //this.Columnas.Add(new DataGridTextColumn() { Header = "Código de Área", Binding = new Binding("CodigoArea") });
            this.Columnas.Add(new DataGridTextColumn() { Header = "Número", Binding = new Binding("Numero") });
            this.Columnas.Add(new DataGridTextColumn() { Header = "Contacto", Binding = new Binding("Nombre") });
            this.Margin = new System.Windows.Thickness(0, 0, 0, 5);
            this.MaxHeight = 125;
            this.Width = 528;
            this.dataGrid1.MaxHeight = 100;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

        }
    }
}
