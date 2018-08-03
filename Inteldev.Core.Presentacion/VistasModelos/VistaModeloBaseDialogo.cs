using Inteldev.Core.DTO;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Comandos;
using Inteldev.Core.Presentacion.Presentadores.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Inteldev.Core.Presentacion.VistasModelos
{
    public class VistaModeloBaseDialogo<TDto> : VistaModeloBase<TDto>, IPresentadorBaseDialogo<TDto>, Inteldev.Core.Presentacion.VistasModelos.IVistaModeloBaseDialogo
        where TDto : DTOMaestro, new()
    {
        public VistaModeloBaseDialogo()
            : base()
        {
            this.CmdAceptar = new RelayCommand(p => TryCatch.Intentar(i => this.Ventana.Close()), p => this.puedegrabar());
            this.CmdCancelar = new RelayCommand(c => TryCatch.Intentar(delegate(object o)
            {
                this.EntidadActual = null;
                this.Ventana.Close();
            }));
        }
        public event Action<bool> DialogoCerrado;
        protected virtual bool puedegrabar()
        {
            return this.EntidadActual != null;
        }

        public VistaModeloBaseDialogo(TDto dto)
            : base(dto)
        {

        }

        public ICommand CmdAceptar { get; set; }


        public ICommand CmdCancelar { get; set; }


        public void Ejecutar()
        {
            throw new Exception("no implementado");
        }


        public TDto EntidadActual
        {
            get { return (TDto)GetValue(EntidadActualProperty); }
            set { SetValue(EntidadActualProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EntidadActual.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EntidadActualProperty =
            DependencyProperty.Register("EntidadActual", typeof(TDto), typeof(VistaModeloBaseDialogo<TDto>));



        public ObservableCollection<TDto> Entidades { get; set; }

        public System.Windows.Window Ventana { get; set; }

        public System.Windows.FrameworkElement Vista { get; set; }


        public bool SeleccionOk
        {
            get
            {
                return this.puedegrabar();
            }

        }

        public int ObtenerID()
        {
            //aca cambia esto
            return EntidadActual.Id;
        }

        public virtual int[] ObtenerIds()
        {
            int i = 0;
            var properties = this.EntidadActual.GetType().GetProperties();
            int[] args = new int[properties.Length];
            foreach (var property in properties)
            {
                var type = property.PropertyType;
                if (type != typeof(int) && type != typeof(string))
                {
                    var propVal = property.GetValue(this.EntidadActual, null);
                    var prop = type.GetProperty("Id");
                    if (prop != null && propVal != null)
                    {
                        var id = (int)prop.GetValue(propVal, null);
                        args[i] = id;
                        i++;
                    }
                }
            }
            return args;
        }
    }

}
