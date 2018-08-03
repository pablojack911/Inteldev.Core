using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inteldev.Core.Modelo;
using Inteldev.Core.DTO;
using System.Reflection;

namespace Inteldev.Core.Negocios.Busquedas
{
	/// <summary>
	/// Buscador de bloques generico. Implementa BlockdeBusqueda.
	/// </summary>
	/// <see cref="BlockDeBusqueda"/>
	/// <typeparam name="TEntidad">Entidad. Tiene que derivar de EntidadBase</typeparam>
    public class BlockDeBusquedaGenerico<TEntidad> : BlockDeBusqueda<TEntidad> 
		where TEntidad: EntidadBase
    {

		public override void AgregarPartes( )
		{
			
		}

        public override void AgregarPartes(List<object> listaPropiedades, List<Inteldev.Core.Modelo.ParametrosMiniBusca> Parametros)
        {
			//aca tendria que tener la lista de propiedades con el atributo del buscador.

			foreach (PropertyInfo prop in listaPropiedades)
			{
				Type type = prop.PropertyType;
				if (type == typeof(string))
				{
                    if(prop.Name == "Codigo")
                    {
                        var busquedaPorCodigo = new BusquedaCodigo<TEntidad>();
                        busquedaPorCodigo.Cargar(Busqueda, prop.Name);
                        foreach (var item in Parametros)
                        {
                            busquedaPorCodigo.AgregaCondicionAnd(item.Nombre,item.Valor,item.TipoObjeto);
                        }
                        this.Partes.Add(busquedaPorCodigo);
                    }
                    else
                    {
                        var busquedaPorString = new BusquedaString<TEntidad>();
                        busquedaPorString.Cargar(Busqueda, prop.Name);
                        foreach (var item in Parametros)
                        {
                            busquedaPorString.AgregaCondicionAnd(item.Nombre, item.Valor, item.TipoObjeto);
                        }
                        this.Partes.Add(busquedaPorString);
                    }
				}
				else
				{
					if (type == typeof(int))
					{
						var busquedaPorId = new BusquedaPorInt<TEntidad>();
						busquedaPorId.Cargar(Busqueda,prop.Name);
						this.Partes.Add(busquedaPorId);
					}
				}
			}
        }

    }

}
