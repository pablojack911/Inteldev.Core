using Inteldev.Core.DTO.Validaciones;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Inteldev.Core.DTO
{
    [DataContract(IsReference = true)]
    public class DTOBase : IDataErrorInfo, INotifyPropertyChanged
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [IncluirEnBuscador]
        [IncluirEnListado]
        public string Nombre { get; set; }


        public string Error
        {
            get { return ""; }
        }

        /// <summary>
        /// Cuando la capa de presentacion llama a esta propiedad para validar. Llama a 
        /// validador estatico que a su ves me da la instancia del validador y entonces 
        /// llamo a validar propiedad
        /// </summary>
        /// <param name="columnName">nombre de la propiedad a validar</param>
        /// <returns>mensaje de error</returns>
        public string this[string columnName]
        {
            get { return ValidadorEstatico.ValidarPropiedad(this, columnName); }
        }


        public DTOBase()
        {
            Inteldev.Core.Metadatos.MetaDatos.ForEachPropertys<DTOBase>(this, p => Inteldev.Core.Metadatos.MetaDatos.AsignarValor(this, p.Name,
                this.CrearInstanciaColleccion(p.PropertyType)));
        }

        private object CrearInstanciaColleccion(System.Type tipo)
        {
            object list = null;
            if (tipo.GetProperty("Count") != null)
            {
                //Type typeList = typeof(List<>);
                //Type typeList = tipo;
                //Type actualType = typeList.MakeGenericType(tipo.GetGenericArguments());
                //list = Activator.CreateInstance(actualType);
                list = Activator.CreateInstance(tipo);
            }
            return list;
        }

        public override string ToString()
        {
            return string.Format("({0})-{1}", Id.ToString(), Nombre);
        }

        #region Implementacion de INotifyPropertyChanged

        /// <summary>
        /// Utilizado para notificar cambios en propiedades del DTO. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                  new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }

}
