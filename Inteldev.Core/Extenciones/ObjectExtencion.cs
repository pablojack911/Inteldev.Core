using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Inteldev.Core.Extenciones
{
	/// <summary>
	/// Provee funciones extras sobre objetos
	/// </summary>
	/// <exception cref="NullReferenceException">Si algun objeto es nulo</exception>
    public static class ObjectExtencion
    {
		/// <summary>
		/// Clona un objecto y te lo devuelve
		/// </summary>
		/// <typeparam name="TObjeto">Tipo de Objecto a cloar</typeparam>
		/// <param name="objeto">Objeto a clonar</param>
		/// <returns>Objeto clonado</returns>
        public static TObjeto Clonar<TObjeto>(this Object objeto) where TObjeto : new()
        {
            var objetoNuevo = new TObjeto();

            var propiedades = objeto.GetType().GetProperties();

            foreach (var propiedad in propiedades)            
            {           
                if( propiedad.GetSetMethod()!=null)
                    propiedad.SetValue(objetoNuevo, propiedad.GetValue(objeto, null), null);
            }

            return objetoNuevo;
        }

		/// <summary>
		/// Clona un objecto. Si este tiene una propiedad ID no la agrega al objeto clonado
		/// </summary>
		/// <typeparam name="TObjeto">tipo de objecto a clonar</typeparam>
		/// <param name="objeto">objecto a clonar</param>
		/// <returns>objeto clonado</returns>
        public static TObjeto ClonarSinID<TObjeto>(this Object objeto) where TObjeto : new()
        {
            var objetoNuevo = new TObjeto();

            var propiedades = objeto.GetType().GetProperties();

            foreach (var propiedad in propiedades)
            {
                if (propiedad.Name != "Id")
                    propiedad.SetValue(objetoNuevo, propiedad.GetValue(objeto, null), null);
            }

            return objetoNuevo;
        }

		/// <summary>
		/// Pre Auto-Mapper funcion
		/// </summary>
		/// <param name="objeto">objeto concreto</param>
		/// <param name="objetoDestino">objeto abstracto</param>
		/// <returns>objeto abstracto</returns>
        public static object Mapear(this Object objeto, Object objetoDestino)
        {           
            var propiedades = objeto.GetType().GetProperties().Where(p=> p.CanWrite && p.CanRead );

            foreach (var propiedad in propiedades)
            {
                var valorOrigen = propiedad.GetValue(objeto, null);
                var valorDestino = propiedad.GetValue(objetoDestino,null);
				if (valorOrigen == null)
					propiedad.SetValue(objetoDestino, valorOrigen, null);
				else
				{
					if (valorOrigen.EsEntidad())
					{
						propiedad.SetValue(objetoDestino, valorOrigen, null);
					}
					else
					{
						if (valorOrigen.EsColeccion())
						{
							dynamic coleccionOrigen = valorOrigen;
							dynamic coleccionDestino = valorDestino;

							int itemsOrigen = Metadatos.MetaDatos.ObtenerValor<int>(coleccionOrigen, "Count");
							int itemsDestino = Metadatos.MetaDatos.ObtenerValor<int>(coleccionDestino, "Count");

							int index = 0;
							foreach (object itemOrigen in coleccionOrigen)
							{

								if (index + 1 <= itemsDestino)
								{
									object itemDestino = coleccionDestino[index];
									itemOrigen.Mapear(itemDestino);
								}
								else
								{
									var itemOrigenConvertido = Convert.ChangeType(itemOrigen, itemOrigen.GetType());


									Type tipocoleccion = coleccionDestino.GetType();
									var param = new object[1] { itemOrigen };

									tipocoleccion.GetMethod("Add").Invoke(coleccionDestino, param);

								}

								index++;

							}
						}
						else
						{
                            if (propiedad.CanWrite)
							    propiedad.SetValue(objetoDestino, valorOrigen, null);
						}
					}

				}
            }

            return objetoDestino;
        }

		/// <summary>
		/// Comprueba que un objeto sea entidad de datos
		/// </summary>
		/// <param name="objeto">objeto a comprobar</param>
		/// <returns>Boolean</returns>
        public static bool EsEntidad(this Object objeto)
        {
				return objeto.GetType().GetProperty("Id") != null;
        }
        
		/// <summary>
		/// Comprueba si un objeto sea una colleccion
		/// </summary>
		/// <param name="objeto">objeto a comprobar</param>
		/// <returns>Boolean</returns>
        public static bool EsColeccion(this Object objeto)
        {
				return objeto.GetType().GetProperty("Count") != null;
        }

    }
}
