using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;


namespace Inteldev.Core.Presentacion.ClienteServicios
{
    /// <summary>
    /// Fabrica de conectores de servicios para la aplicacion cliente
    /// </summary>
    public class FabricaClienteServicio : Core.Patrones.Singleton<FabricaClienteServicio>
    {
        public int puertoTcp { get; set; }
        public string ServerIp { get; set; }

        public NetTcpBinding CrearNetTcpBinding()
        {
            NetTcpBinding tcpBinding = new NetTcpBinding();
            tcpBinding.TransactionFlow = false;
            tcpBinding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            tcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

            tcpBinding.CloseTimeout = TimeSpan.FromSeconds(120);
            tcpBinding.OpenTimeout = TimeSpan.FromSeconds(120);
            tcpBinding.SendTimeout = TimeSpan.FromSeconds(120);
            tcpBinding.ReceiveTimeout = TimeSpan.FromSeconds(120);
            tcpBinding.Security.Mode = SecurityMode.None;
            //tcpBinding.MaxBufferSize = 2147483647;
            tcpBinding.MaxReceivedMessageSize = 2147483647;
            return tcpBinding;
        }

        /// <summary>
        /// Crea la ruta del servicio
        /// </summary>
        /// <param name="ip">ip del servidor</param>
        /// <param name="puerto">puerto del servidor</param>
        /// <param name="servicio">nombre del servicio</param>
        /// <returns>URi del servicio</returns>
        public EndpointAddress CrearEndPointAddress(string ip, string puerto, string servicio)
        {
            string endPointAddr = "net.tcp://" + ip + ":" + puerto + "/" + servicio;
            EndpointAddress endpointAddress = new EndpointAddress(endPointAddr);
            return endpointAddress;
        }

        /// <summary>
        /// Crea una conexion a un servicio
        /// </summary>
        /// <typeparam name="TContrato"></typeparam>
        /// <param name="servicio">nombre del servicio</param>
        /// <returns>canal del servicio</returns>
        public TContrato CrearCliente<TContrato>(string servicio)
        {
            var ntb = this.CrearNetTcpBinding();
            //var epa = this.CrearEndPointAddress(Servidor.Instancia.DireccionIP, Servidor.Instancia.PuertoTCP.ToString(), servicio);
            //Hacer algo con esto, porque no me toma el puerto TCP
            var epa = this.CrearEndPointAddress(this.ServerIp, this.puertoTcp.ToString(), servicio);
            var cn = new ChannelFactory<TContrato>(ntb);
            return cn.CreateChannel(epa);
        }

        /// <summary>
        /// Crea un cliente a partir de un contrato, pero no necesita el nombre ya que lo obtiene del contrato
        /// </summary>
        /// <typeparam name="TContrato">tipo de contrato</typeparam>
        /// <returns>Contrato</returns>
        public TContrato CrearCliente<TContrato>()
        {
            var typecontrato = typeof(TContrato);
            var nombre = typecontrato.GetGenericArguments().FirstOrDefault().Name;
            return CrearCliente<TContrato>("Servicio" + nombre);
        }
    }
}
