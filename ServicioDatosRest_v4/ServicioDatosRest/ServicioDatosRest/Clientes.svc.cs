using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace ServicioDatosRest
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Clientes" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Clientes.svc o Clientes.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Clientes : IClientes
    {
        private readonly List<Cliente> repo;
        private const string CacheKey = "Clientes";
        public Clientes()
        {
            repo = new List<Cliente>();
            repo.Add(new Cliente() { Id = 1, Mail = "mail1@google.com", Nombre = "Cliente1", Telefono = "1111" });
            repo.Add(new Cliente() { Id = 2, Mail = "mail2@google.com", Nombre = "Cliente2", Telefono = "2222" });
            repo.Add(new Cliente() { Id = 3, Mail = "mail3@google.com", Nombre = "Cliente3", Telefono = "3333" });
            repo.Add(new Cliente() { Id = 4, Mail = "mail4@google.com", Nombre = "Cliente4", Telefono = "4444" });
        }

        public Cliente Create(Cliente cliente)
        {
            if (cliente != null)
            {
                if (cliente.Id == 0)
                {
                    int maxCliente = repo.Max(c => c.Id);
                    cliente.Id = maxCliente + 1;
                }
                repo.Add(cliente);
            }
            return cliente;
        }

        public void Delete(int idCliente)
        {
            if (idCliente > 0)
            {
                Cliente cli = ObtenerCliente(idCliente);
                if (cli != null)
                {
                    repo.Remove(cli);
                }
            }
        }

        public Cliente Get(string idCliente)
        {
            int id = 0;
            int.TryParse(idCliente, out id);
            return ObtenerCliente(id);
        }

        private Cliente ObtenerCliente(int idCliente)
        {
            Cliente cli = repo.Where(c => c.Id == idCliente).Select(c => c).SingleOrDefault();
            return cli;
        }

        public List<Cliente> GetAll()
        {
            return repo;
        }

        public List<Cliente> GetAllCache()
        {
            ObjectCache cache = MemoryCache.Default;

            if (cache.Contains(CacheKey))
                return (List<Cliente>)cache.Get(CacheKey);
            else
            {
                IEnumerable availablePizzas = repo;
                // Store data in the cache
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
                cache.Add(CacheKey, repo, cacheItemPolicy);
                return repo;
            }
        }

        public Cliente Update(Cliente cliente)
        {
            Cliente cli = null;
            if (cliente !=null)
            {
                cli = ObtenerCliente(cliente.Id);
                if (cli != null)
                {
                    cli.Nombre = cliente.Nombre;
                    cli.Mail= cliente.Mail;
                    cli.Telefono = cliente.Telefono;
                }
            }
            return cli;
        }
    }
}
