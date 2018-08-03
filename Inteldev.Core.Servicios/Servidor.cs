using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using Inteldev.Core.Contratos;
using Inteldev.Core.Patrones;
using System.Diagnostics;

namespace Inteldev.Core.Servicios
{
    public class Servidor : Singleton<Servidor>
    {
        List<ServiceHost> hosts;

        public int PuertoHttp { get; set; }
        public int PuertoTCP { get; set; }

        public string DireccionIP
        {
            get
            {
                var ips = Dns.GetHostEntry(Dns.GetHostName());
                var ip = ips.AddressList.Where(i => i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();

                return ip.ToString();
            }

        }
        NetTcpBinding tcpBinding;

        public Servidor()
        {
            this.hosts = new List<ServiceHost>();
            this.CrearNetTcpBinding();
        }

        private NetTcpBinding CrearNetTcpBinding()
        {
            tcpBinding = new NetTcpBinding();

            tcpBinding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            tcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            //FIJARSE
            tcpBinding.Security.Mode = SecurityMode.None; // <- Very crucial
            //tcpBinding.Security.Mode = SecurityMode.TransportWithMessageCredential;
            //tcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
            tcpBinding.MaxBufferPoolSize = 2147483647;
            tcpBinding.MaxBufferSize = 2147483647;
            tcpBinding.MaxReceivedMessageSize = 2147483647;
            tcpBinding.MaxConnections = 2147483647;
            tcpBinding.MaxReceivedMessageSize = 2147483647;
            tcpBinding.PortSharingEnabled = false;
            tcpBinding.TransactionFlow = false;
            tcpBinding.ListenBacklog = 2147483647;
            tcpBinding.CloseTimeout = new TimeSpan(8, 0, 0);
            tcpBinding.OpenTimeout = new TimeSpan(8, 0, 0);
            tcpBinding.ReceiveTimeout = new TimeSpan(8, 0, 0);
            tcpBinding.SendTimeout = new TimeSpan(8, 0, 0);
            return tcpBinding;
        }

        private ServiceMetadataBehavior CrearMetadataBehavior(ServiceHost host, string nombreDelServicio)
        {
            var metadataBehavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (metadataBehavior == null)
            {
                // This is how I create the proxy object that is generated via the svcutil.exe tool
                metadataBehavior = new ServiceMetadataBehavior();
                metadataBehavior.HttpGetUrl = new Uri("http://" + this.DireccionIP + ":" + this.PuertoHttp.ToString() + "/" + nombreDelServicio);
                metadataBehavior.HttpGetEnabled = true;

            }
            return metadataBehavior;
        }

        private void CrearServicios()
        {
            var servicios = this.GetType().GetProperties().Where(p => (p.PropertyType == typeof(IServicioABM<>)) ||
                                                                      p.PropertyType == typeof(IServicioMenu) ||
                                                                      p.PropertyType == typeof(IServicioUsuario));

            foreach (var servicio in servicios)
            {
                var oS = FabricaServicios._Resolver(servicio.PropertyType);
                servicio.SetValue(this, oS, null);
                this.CrearHost(oS.GetType(), this.ObtenerContrato(oS.GetType()), servicio.Name);
            }

        }

        private Type ObtenerContrato(Type servicio)
        {
            Type contrato = null;
            var interfaces = servicio.GetInterfaces();
            foreach (var interfaz in interfaces)
            {
                var atributos = interfaz.GetCustomAttributes(true);
                foreach (var atributo in atributos)
                {
                    if (atributo is ServiceContractAttribute)
                        contrato = interfaz;
                    break;
                }

                if (contrato != null)
                    break;
            }
            return contrato;
        }

        public void CrearHost(Type Servicio, Type Contrato, string Nombre)
        {
            var host = new ServiceHost(Servicio);
            string direccionTcp = "net.tcp://" + this.DireccionIP + ":" + this.PuertoTCP.ToString() + "/" + Nombre;
            host.AddServiceEndpoint(Contrato, this.tcpBinding, direccionTcp);
            host.Description.Behaviors.Add(CrearMetadataBehavior(host, Nombre));

            /// host.Faulted += new EventHandler(host_Faulted);

            ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();

            // if not found - add behavior with setting turned on 
            if (debug == null)
            {
                host.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                // make sure setting is turned ON
                if (!debug.IncludeExceptionDetailInFaults)
                {
                    debug.IncludeExceptionDetailInFaults = true;
                }
            }


            this.hosts.Add(host);

        }

        //esto es private. Quien lo define?
        // Action<object, EventArgs> Error;
        //void host_Faulted(object sender, EventArgs e)
        //{
        //	this.Error(sender, e);
        //}
        public void CrearHostGenerico(Type Servicio)
        {
            var contrato = ObtenerContrato(Servicio);
            Debug.WriteLine(contrato);
            var nombre = contrato.GetGenericArguments().FirstOrDefault().Name;
            Debug.WriteLine(nombre);
            this.CrearHost(Servicio, ObtenerContrato(Servicio), "Servicio" + nombre);
        }

        public void CrearHost(Type Servicio)
        {
            this.CrearHost(Servicio, ObtenerContrato(Servicio), Servicio.Name);
        }

        public void CrarHost<TServicio, TContrato>(string Nombre)
        {
            this.CrearHost(typeof(TServicio), typeof(TContrato), Nombre);
        }

        public void CrearHost<TServicio>() where TServicio : class
        {
            this.CrearHost(typeof(TServicio));
        }

        public void AbrirServicios()
        {

            this.hosts.ForEach(s => s.Open());
        }

        public void CerrarCervicios()
        {
            this.hosts.ForEach(s => s.Close());
        }
    }
}
