using Inteldev.Core.DTO;
using Inteldev.Core.Estructuras;
using Inteldev.Core.Presentacion.Controles;
using Inteldev.Core.Presentacion.Presentadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Inteldev.Core.Presentacion.VistasModelos
{
    /// <summary>
    /// Vista Modelo Base. Usada para que todos los View Model deriven.
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    public class VistaModeloBase<TDto> : DependencyObject where TDto : DTO.DTOBase
    {

        /// <summary>
        /// DP. A esta propiedad bindean los campos de las entidades en la vista. 
        /// </summary>
        public TDto Modelo
        {
            get { return (TDto)GetValue(ModeloProperty); }
            set { SetValue(ModeloProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Modelo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModeloProperty =
            DependencyProperty.Register("Modelo", typeof(TDto), typeof(VistaModeloBase<TDto>));



        #region Constructores
        /// <summary>
        /// Constructor vacío de la clase 
        /// </summary>
        public VistaModeloBase() { }

        /// <summary>
        /// Constructor de la clase con parametro
        /// </summary>
        /// <param name="dto"></param>
        public VistaModeloBase(TDto dto)
        {
            //id = 0 significa que se el dto es nuevo.
            if (dto.Id == 0)
            {
                this.CodigoHabilitado = true; //habilitado para edicion
            }
            else
            {
                this.CodigoHabilitado = false;
            }
            this.Modelo = dto;
            // this.InstanciarPresentadores();
        }
        #endregion

        /// <summary>
        /// Llama a fabrica de presentadores para instanciar presentador
        /// </summary>
        /// <param name="presentador"></param>
        /// <returns></returns>
        protected dynamic InstanciarPresentador(string presentador)
        {
            var type = this.GetType().GetProperty(presentador);
            dynamic instancia = FabricaPresentadores.Instancia.Resolver(type.PropertyType);
            type.SetValue(this, instancia, null);
            return instancia;
        }

        /// <summary>
        /// Setea el presentador para una DP. Se llama normal porque setea en propiedades ActualizarDTO y Entidad
        /// Para los otros presentadores que usan PMD, o PMB, usar el override.
        /// Se usa mas que nada para los Mini Busca.
        /// </summary>
        /// <param name="presentador">Nombre del Presentador a instanciar</param>
        /// <param name="actualizarDTO">Action que contiene comparacion para actualizar DTO</param>
        /// <param name="DTO"></param>
        protected void SetPresentador<TEntidad>(string presentador, Action<TEntidad> actualizarDTO, object DTO)
            where TEntidad : DTOBase
        {
            var instancia = this.InstanciarPresentador(presentador);
            instancia.ActualizarDto = actualizarDTO;
            instancia.Entidad = (TEntidad)DTO;
        }

        /// <summary>
        /// Override. Setea presentador y DTO. Trabaja con presentador.DTO nada mas
        /// Grilla
        /// </summary>
        /// <param name="presentador">string del presentador</param>
        /// <param name="DTO"></param>
        protected void SetPresentador<TEntidad>(string presentador, IEnumerable<TEntidad> DTO)
        {
            var instancia = this.InstanciarPresentador(presentador);
            instancia.DTO = DTO;
            instancia.Maestro = this.Modelo;
        }

        /// <summary>
        /// Setea presentador y DTO. Trabaja con presentador.PMD.DTO nada mas
        /// Mini Busca List nada mas
        /// </summary>
        /// <param name="presentador">string del presentador</param>
        /// <param name="DTO"></param>
        protected void SetPresentadorEspecial<TEntidad>(string presentador, IEnumerable<TEntidad> DTO)
        {
            var instancia = this.InstanciarPresentador(presentador);
            instancia.PMD.DTO = DTO;
        }

        protected void InstanciarPresentadores()
        {
            var props = this.GetType().GetProperties();
            foreach (var prop in props)
            {
                try
                {
                    this.InstanciarPresentador(prop.Name);
                }
                catch (Exception exc)
                {
                }
            }

        }
        public static DependencyProperty RegistrarDp(string prop, Type owner)
        {
            var pinfo = owner.GetProperty(prop);
            var propertyType = pinfo.PropertyType;
            var dp = DependencyProperty.Register(prop, propertyType, owner);
            return dp;

        }

        public static DependencyProperty RegistrarDp<TViewModel>(Expression<Func<TViewModel, object>> property)
        {
            PropertyInfo propertyInfo = null;
            if (property.Body is MemberExpression)
            {
                propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;
            }
            else
            {
                propertyInfo = (((UnaryExpression)property.Body).Operand as MemberExpression).Member as PropertyInfo;
            }

            return RegistrarDp(propertyInfo.Name, typeof(TViewModel));
        }

        /// <summary>
        /// Propiedad que se utiliza en la vista para identificar un campo como solo lectura
        /// </summary>
        public bool SoloLectura { get; set; }

        /// <summary>
        /// Propiedad que bindea a PermisoManager en la vista para permitir o denegar acceso a los campos de la vista
        /// </summary>
        public bool Editable { get; set; }

        #region DP's
        /// <summary>
        /// Propiedad de dependencia utilizada para bloquear el acceso al control si se abrió en modo edición.
        /// Bindeado al ItemFormulario. Propiedad que se pisa: IsEnabled
        /// </summary>
        public bool CodigoHabilitado
        {
            get { return (bool)GetValue(CodigoHabilitadoProperty); }
            set { SetValue(CodigoHabilitadoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Habilitado.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CodigoHabilitadoProperty =
            DependencyProperty.Register("CodigoHabilitado", typeof(bool), typeof(VistaModeloBase<TDto>));

        /// <summary>
        /// Propiedad de dependencia utilizada para cambiar la visibilidad del control en el caso que sea generado por la base de datos
        /// Bindeado al ItemFormulario. Propiedad que se pisa: Visibility
        /// </summary>
        public Visibility CodigoVisible
        {
            get { return (Visibility)GetValue(CodigoVisibleProperty); }
            set { SetValue(CodigoVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CodigoVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CodigoVisibleProperty =
            DependencyProperty.Register("CodigoVisible", typeof(Visibility), typeof(VistaModeloBase<TDto>), new PropertyMetadata(Visibility.Visible));

        #endregion

        protected bool PuedeBuscarCodigoDisponible(object p)
        {
            if (p != null)
                if (p.ToString().Trim() != "")
                    if (char.IsLetter(p.ToString().First()))
                        if (!Regex.IsMatch(p.ToString().Substring(1), @"[a-zA-Z]"))
                            return true;
                        else
                            return false;
                    else
                        if (!Regex.IsMatch(p.ToString(), @"[a-zA-Z]"))
                            return true;
            return false;
        }
    }
}
