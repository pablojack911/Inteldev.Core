using System;
namespace Inteldev.Core.Presentacion.Controles
{
    public interface IBaseABM
    {
        System.Windows.Controls.Menu MenuSuperior { get; set; }
        System.Windows.Controls.ContentControl PanelIzquierdo { get; set; }
		System.Windows.Controls.TabControl TabControlDerecho { get; set; }
        System.Windows.Controls.Primitives.ToolBarPanel ToolBarDerecho { get; set; }
    }
}
