using System;
using System.Linq.Expressions;
using System.Linq;
using Inteldev.Core.Datos;
using Inteldev.Core.Modelo;
using System.Collections.Generic;
using Inteldev.Core.Negocios.Busquedas;
using System.Diagnostics;
using Microsoft.Practices.Unity;
using System.Text.RegularExpressions;



namespace Inteldev.Core.Negocios
{

    public class BuscadorGenerico<TEntidad> : LogicaDeNegociosBase<TEntidad>, IBuscador<TEntidad>
        where TEntidad : EntidadBase
    {
        public ParameterOverride[] parameters { get; set; }
        ///public object Numerador { get; set; }
        public CargarRelaciones CargarEntidadesRelacionadas { get; set; }

        public BuscadorGenerico(string empresa, string entidad)
            : base(empresa, entidad)
        {
            this.CargarEntidadesRelacionadas = CargarRelaciones.CargarTodo;
            this.parameters = new ParameterOverride[2];
            this.parameters[0] = new ParameterOverride("empresa", empresa);
            this.parameters[1] = new ParameterOverride("entidad", entidad);
        }

        public virtual List<TEntidad> BuscarLista(Expression<Func<TEntidad, bool>> busqueda, CargarRelaciones cargarEntidades)
        {
            return this.Contexto.Consultar<TEntidad>(cargarEntidades).Where(busqueda).ToList();
        }

        //public virtual List<TEntidad> BuscarLista(CargarRelaciones cargarentidades)
        //{
        //    return this.Contexto.Consultar<TEntidad>(cargarentidades).ToList();
        //}

        public virtual List<TEntidad> BuscarLista(CargarRelaciones cargarentidades)
        {
            var q = this.Contexto.Consultar<TEntidad>(cargarentidades);
            List<TEntidad> devuelve = new List<TEntidad>();
            List<TEntidad> listaLote = new List<TEntidad>(); //agregar de a 100 una listaLote de datos con take(100)
            int cantidadRegistros = q.Count(); //cantidad total de registros
            int lote = 0; //indice de busqueda
            while (lote < cantidadRegistros) //el indice esta dentro de la cantidada de registros de la lista
            {
                listaLote = q.OrderBy(d => d.Id).Skip(lote).Take(100).ToList(); //salteamos los registros que ya ingresamos. (la primera vez salteamos 0 registros, i=0)
                devuelve.AddRange(listaLote); //agregamos los 100 registros a la lista
                lote += 100; //incrementamos el skip
                listaLote.Clear();
            }
            var resto = (cantidadRegistros - lote); //esto es por si quedan registros sin ingresar de q.
            ///Ej:
            ///Agregamos 150;
            ///Los primeros 100 entran.
            ///Hacemos i+=100, se va a 200.
            ///200>150, sale del bucle while.
            ///(150 - 200) = -50
            ///-50 > 0 => NO
            if (devuelve.Count < cantidadRegistros)
            {
                listaLote.Clear();
                listaLote = q.OrderBy(d => d.Id).Skip(cantidadRegistros - resto).Take(resto).ToList(); //salteamos el total menos el resto y tomamos el resto.
                devuelve.AddRange(q);
            }
            return devuelve;
        }

        public virtual List<TMaestro> BuscarDiferencia<TMaestro>(List<string[]> codigosImportados) where TMaestro : EntidadMaestro, new()
        {
            var listaFiltrada = new List<TMaestro>();
            var listaCodigosLocal = this.Contexto.Consultar<TMaestro>(CargarRelaciones.NoCargarNada).Select(p => p.Codigo).ToList();
            //comparar
            foreach (var item in listaCodigosLocal)
            {
                if (!codigosImportados.Any(p => p[0] == item))
                    listaFiltrada.Add(this.Contexto.Consultar<TMaestro>(CargarRelaciones.NoCargarNada).FirstOrDefault(p => p.Codigo == item));
            }
            //si encuentra
            //recorrer y buscarla en codigos. si no esta buscar la entidad completa y agregarla a una lista y luego devolver la lista
            return listaFiltrada;
        }


        /*public virtual List<TMaestro> BuscarDiferencia<TMaestro>(List<string> codigosImportados) where TMaestro : EntidadMaestro, new()
        {
            var listaFiltrada = new List<TMaestro>();
            var listaCodigosLocal = this.Contexto.Consultar<TMaestro>(CargarRelaciones.NoCargarNada).Select(p => p.Codigo).ToList();
            //comparar
            foreach (var item in listaCodigosLocal)
            {
                if (!codigosImportados.Contains(item))
                    listaFiltrada.Add(this.Contexto.Consultar<TMaestro>(CargarRelaciones.NoCargarNada).FirstOrDefault(p => p.Codigo == item));
            }
            //si encuentra
            //recorrer y buscarla en codigos. si no esta buscar la entidad completa y agregarla a una lista y luego devolver la lista
            return listaFiltrada;
        }*/

        public virtual List<TEntidad> BuscarLista(MethodCallExpression busqueda, CargarRelaciones cargarEntidades)
        {
            if (busqueda != null)
            {
                return this.Contexto.Consultar<TEntidad>(cargarEntidades).Provider.CreateQuery<TEntidad>(busqueda).ToList();
            }
            else
                return null;
        }

        /// <summary>
        /// Consulta toda una entidad sin cargar las entidades relacionadas.
        /// </summary>
        /// <returns>la consulta tipo iquereable</returns>
        public virtual IQueryable<TEntidad> ConsultaSimple(CargarRelaciones cargarEntidades)
        {
            return this.Contexto.Consultar<TEntidad>(cargarEntidades);
        }

        public virtual TEntidad BuscarSimple(object busqueda)
        {
            return this.Contexto.BuscarPorId(Convert.ToInt32(busqueda), this.CargarEntidadesRelacionadas);
        }

        public virtual TMaestro BuscarPorCodigo<TMaestro>(object busqueda, List<ParametrosMiniBusca> parametros = null) where TMaestro : EntidadMaestro
        {
            if (parametros != null && parametros.Count != 0)
            {
                var lista = new List<ParteBusqueda<TMaestro>>();
                foreach (var item in parametros)
                {
                    var parteDeBusqueda = new ParteBusqueda<TMaestro>();
                    parteDeBusqueda.PuedeBuscar = (p => true);
                    parteDeBusqueda.SetearParteIzquierda(item.Nombre);
                    parteDeBusqueda.SetearParteDerecha(item.Valor, item.TipoObjeto);
                    parteDeBusqueda.JuntaExpressionIgual();
                    lista.Add(parteDeBusqueda);
                }
                var parte = new ParteBusqueda<TMaestro>() { PuedeBuscar = (p => true) };
                if (lista.Count > 1)
                {
                    for (int i = 1; i < lista.Count; i = i + 2)
                    {
                        parte.AnidarCondicionAnd(lista.ElementAt(i - 1).GetResult(), lista.ElementAt(i).GetResult());
                    }
                }
                else
                    parte = lista.FirstOrDefault();

                var listu = this.BuscarLista(parte.ArmaConsulta(this.ConsultaSimple(CargarRelaciones.NoCargarNada)), CargarRelaciones.NoCargarNada);
                if (listu != null)
                {
                    var entidad = listu.FirstOrDefault() as TMaestro;
                    return entidad;
                }
                else
                    return null;
            }
            else
            {
                var codigo = busqueda.ToString();
                var entidad = this.Contexto.BuscarPorCodigo<TMaestro>(busqueda.ToString(), this.CargarEntidadesRelacionadas);
                return entidad as TMaestro;
            }
        }

        public bool ExisteCodigo<TMaestro>(string Codigo) where TMaestro : EntidadMaestro
        {
            return this.Contexto.Consultar<TMaestro>(Core.CargarRelaciones.NoCargarNada).Any(c => c.Codigo == Codigo);
        }


        public List<string> ObtenerListaCodigos<TMaestro>(long desde, int tamañoMaximo) where TMaestro : EntidadMaestro
        {
            //var Numerador = (INumerador<TMaestro>)FabricaNegocios.Instancia.Resolver(typeof(INumerador<TMaestro>), this.parameters);
            //string Desde = desde.ToString().PadLeft(Numerador.TamañoMaximo, '0');
            string Desde = desde.ToString().PadLeft(tamañoMaximo, '0');
            var resultado = this.Contexto.Consultar<TMaestro>(CargarRelaciones.NoCargarNada)
                .Where(p => p.Codigo.CompareTo(Desde) >= 0)
                .OrderBy(p => p.Codigo)
                .Select(c => c.Codigo)
                .Take(1000)
                .ToList();

            var NonAlphas = resultado.Where(n => !Regex.IsMatch(n, @"[a-zA-Z]")).ToList();
            return NonAlphas;
        }

        public List<string> ObtenerListaCodigos<TMaestro>(string prefijo, long desde, int tamañoMaximo) where TMaestro : EntidadMaestro
        {
            string Desde = desde.ToString().PadLeft(tamañoMaximo, '0');
            var resultado = this.Contexto.Consultar<TMaestro>(CargarRelaciones.NoCargarNada)
                .Where(p => p.Codigo.StartsWith(prefijo) && p.Codigo.CompareTo(prefijo + Desde) >= 0)
                .OrderBy(p => p.Codigo)
                .Select(c => c.Codigo)
                .Take(1000)
                .ToList();
            var NonAlphas = resultado.Where(n => !Regex.IsMatch(n.Remove(0, 1), @"[a-zA-Z]")).ToList();//n.substring(0,1)
            return NonAlphas;
        }
    }
}
