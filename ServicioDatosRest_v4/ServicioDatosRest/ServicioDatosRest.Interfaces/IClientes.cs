using ServicioDatosRest.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Services;

namespace ServicioDatosRest.Interfaces
{
    [ServiceContract]
    public interface IClientes
    {
        [OperationContract]
        [WebGet(UriTemplate = "/getAllCache",ResponseFormat = WebMessageFormat.Json,BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Cliente> GetAllCache();

        [OperationContract]
        [WebGet(UriTemplate = "/getAll", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Cliente> GetAll();

        [OperationContract]
        [WebGet(UriTemplate = "/get/{idCliente}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Cliente Get(string idCliente);

        [OperationContract]
        [WebInvoke(UriTemplate = "/create", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method ="POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        Cliente Create(Cliente cliente);

        [OperationContract]
        [WebInvoke(UriTemplate = "/update", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        Cliente Update(Cliente cliente);

        [OperationContract]
        [WebInvoke(UriTemplate = "/delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "POST")]
        void Delete(int idCliente);


        [OperationContract]
        [WebGet(UriTemplate = "/CargaInicial", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void CargaInicial();
    }
}
