//using System;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.Azure.WebJobs.Host;
//using Newtonsoft.Json;
//using Genso.Framework;
//using Path = Genso.DDNS.Syntax.Path;
//using Genso.DDNS.API;

//namespace API
//{
//    public static class EntryPoint
//    {

//        /** PUBLIC METHODS **/

//        [FunctionName("UpdateDomain")]
//        public static HttpResponseMessage updateDomain(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
//            [Blob(Path.API.Config, FileAccess.Read)] Stream configRead,
//            [Blob(Path.API.Config, FileAccess.Write)] Stream configWrite,
//            [Blob(Path.API.DomainList, FileAccess.Read)] Stream domainListRead,
//            [Blob(Path.API.DomainList, FileAccess.Write)] Stream domainListWrite,
//            [Blob(Path.API.DomainLog, FileAccess.Read)] Stream domainLogRead,
//            [Blob(Path.API.DomainLog, FileAccess.Write)] Stream domainLogWrite,
//            [Blob(Path.API.AccountList, FileAccess.Read)] Stream accountListRead,
//            [Blob(Path.API.AccountList, FileAccess.Write)] Stream accountListWrite,
//            [Blob(Path.API.AppLog, FileAccess.Read)] Stream appLogRead,
//            [Blob(Path.API.AppLog, FileAccess.Write)] Stream appLogWrite,
//            TraceWriter log) => runApi(ApiName.UpdateDomain, request, configRead, configWrite, domainListRead, domainListWrite,
//                domainLogRead, domainLogWrite, accountListRead, accountListWrite, appLogRead, appLogWrite);

//        [FunctionName("ListDomain")]
//        public static HttpResponseMessage listDomain(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
//            [Blob(Path.API.Config, FileAccess.Read)] Stream configRead,
//            [Blob(Path.API.Config, FileAccess.Write)] Stream configWrite,
//            [Blob(Path.API.DomainList, FileAccess.Read)] Stream domainListRead,
//            [Blob(Path.API.DomainList, FileAccess.Write)] Stream domainListWrite,
//            [Blob(Path.API.DomainLog, FileAccess.Read)] Stream domainLogRead,
//            [Blob(Path.API.DomainLog, FileAccess.Write)] Stream domainLogWrite,
//            [Blob(Path.API.AccountList, FileAccess.Read)] Stream accountListRead,
//            [Blob(Path.API.AccountList, FileAccess.Write)] Stream accountListWrite,
//            [Blob(Path.API.AppLog, FileAccess.Read)] Stream appLogRead,
//            [Blob(Path.API.AppLog, FileAccess.Write)] Stream appLogWrite,
//            TraceWriter log) => runApi(ApiName.ListDomain, request, configRead, configWrite, domainListRead, domainListWrite,
//                domainLogRead, domainLogWrite, accountListRead, accountListWrite, appLogRead, appLogWrite);

//        [FunctionName("AddDomain")]
//        public static HttpResponseMessage addDomain(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
//            [Blob(Path.API.Config, FileAccess.Read)] Stream configRead,
//            [Blob(Path.API.Config, FileAccess.Write)] Stream configWrite,
//            [Blob(Path.API.DomainList, FileAccess.Read)] Stream domainListRead,
//            [Blob(Path.API.DomainList, FileAccess.Write)] Stream domainListWrite,
//            [Blob(Path.API.DomainLog, FileAccess.Read)] Stream domainLogRead,
//            [Blob(Path.API.DomainLog, FileAccess.Write)] Stream domainLogWrite,
//            [Blob(Path.API.AccountList, FileAccess.Read)] Stream accountListRead,
//            [Blob(Path.API.AccountList, FileAccess.Write)] Stream accountListWrite,
//            [Blob(Path.API.AppLog, FileAccess.Read)] Stream appLogRead,
//            [Blob(Path.API.AppLog, FileAccess.Write)] Stream appLogWrite,
//            TraceWriter log) => runApi(ApiName.AddDomain, request, configRead, configWrite, domainListRead, domainListWrite,
//                domainLogRead, domainLogWrite, accountListRead, accountListWrite, appLogRead, appLogWrite);

//        [FunctionName("DeleteDomain")]
//        public static HttpResponseMessage deleteDomain(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
//            [Blob(Path.API.Config, FileAccess.Read)] Stream configRead,
//            [Blob(Path.API.Config, FileAccess.Write)] Stream configWrite,
//            [Blob(Path.API.DomainList, FileAccess.Read)] Stream domainListRead,
//            [Blob(Path.API.DomainList, FileAccess.Write)] Stream domainListWrite,
//            [Blob(Path.API.DomainLog, FileAccess.Read)] Stream domainLogRead,
//            [Blob(Path.API.DomainLog, FileAccess.Write)] Stream domainLogWrite,
//            [Blob(Path.API.AccountList, FileAccess.Read)] Stream accountListRead,
//            [Blob(Path.API.AccountList, FileAccess.Write)] Stream accountListWrite,
//            [Blob(Path.API.AppLog, FileAccess.Read)] Stream appLogRead,
//            [Blob(Path.API.AppLog, FileAccess.Write)] Stream appLogWrite,
//            TraceWriter log) => runApi(ApiName.DeleteDomain, request, configRead, configWrite, domainListRead, domainListWrite,
//                domainLogRead, domainLogWrite, accountListRead, accountListWrite, appLogRead, appLogWrite);

//        [FunctionName("CheckAccount")]
//        public static HttpResponseMessage checkAccount(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
//            [Blob(Path.API.Config, FileAccess.Read)] Stream configRead,
//            [Blob(Path.API.Config, FileAccess.Write)] Stream configWrite,
//            [Blob(Path.API.DomainList, FileAccess.Read)] Stream domainListRead,
//            [Blob(Path.API.DomainList, FileAccess.Write)] Stream domainListWrite,
//            [Blob(Path.API.DomainLog, FileAccess.Read)] Stream domainLogRead,
//            [Blob(Path.API.DomainLog, FileAccess.Write)] Stream domainLogWrite,
//            [Blob(Path.API.AccountList, FileAccess.Read)] Stream accountListRead,
//            [Blob(Path.API.AccountList, FileAccess.Write)] Stream accountListWrite,
//            [Blob(Path.API.AppLog, FileAccess.Read)] Stream appLogRead,
//            [Blob(Path.API.AppLog, FileAccess.Write)] Stream appLogWrite,
//            TraceWriter log) => runApi(ApiName.CheckAccount, request, configRead, configWrite, domainListRead, domainListWrite,
//                domainLogRead, domainLogWrite, accountListRead, accountListWrite, appLogRead, appLogWrite);

//        [FunctionName("CreateAccount")]
//        public static HttpResponseMessage createAccount(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
//            [Blob(Path.API.Config, FileAccess.Read)] Stream configRead,
//            [Blob(Path.API.Config, FileAccess.Write)] Stream configWrite,
//            [Blob(Path.API.DomainList, FileAccess.Read)] Stream domainListRead,
//            [Blob(Path.API.DomainList, FileAccess.Write)] Stream domainListWrite,
//            [Blob(Path.API.DomainLog, FileAccess.Read)] Stream domainLogRead,
//            [Blob(Path.API.DomainLog, FileAccess.Write)] Stream domainLogWrite,
//            [Blob(Path.API.AccountList, FileAccess.Read)] Stream accountListRead,
//            [Blob(Path.API.AccountList, FileAccess.Write)] Stream accountListWrite,
//            [Blob(Path.API.AppLog, FileAccess.Read)] Stream appLogRead,
//            [Blob(Path.API.AppLog, FileAccess.Write)] Stream appLogWrite,
//            TraceWriter log) => runApi(ApiName.CreateAccount, request, configRead, configWrite, domainListRead, domainListWrite,
//                domainLogRead, domainLogWrite, accountListRead, accountListWrite, appLogRead, appLogWrite);

//        [FunctionName("DeleteAccount")]
//        public static HttpResponseMessage deleteAccount(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
//            [Blob(Path.API.Config, FileAccess.Read)] Stream configRead,
//            [Blob(Path.API.Config, FileAccess.Write)] Stream configWrite,
//            [Blob(Path.API.DomainList, FileAccess.Read)] Stream domainListRead,
//            [Blob(Path.API.DomainList, FileAccess.Write)] Stream domainListWrite,
//            [Blob(Path.API.DomainLog, FileAccess.Read)] Stream domainLogRead,
//            [Blob(Path.API.DomainLog, FileAccess.Write)] Stream domainLogWrite,
//            [Blob(Path.API.AccountList, FileAccess.Read)] Stream accountListRead,
//            [Blob(Path.API.AccountList, FileAccess.Write)] Stream accountListWrite,
//            [Blob(Path.API.AppLog, FileAccess.Read)] Stream appLogRead,
//            [Blob(Path.API.AppLog, FileAccess.Write)] Stream appLogWrite,
//            TraceWriter log) => runApi(ApiName.DeleteAccount, request, configRead, configWrite, domainListRead, domainListWrite,
//                domainLogRead, domainLogWrite, accountListRead, accountListWrite, appLogRead, appLogWrite);




//        /** PRIVATE METHODS **/

//        //prepare the needed data managers & calls the right API logic function
//        //based on name and returns the their responses
//        private static HttpResponseMessage runApi(ApiName apiName, HttpRequestMessage request, Stream configRead, Stream configWrite, Stream domainListRead, Stream domainListWrite, Stream domainLogRead, Stream domainLogWrite, Stream accountListRead, Stream accountListWrite, Stream appLogRead, Stream appLogWrite)
//        {
//            //final raw error cather for unpredictable failures
//            //errors caught here are not logged but details of the error are passed to caller in "more info"
//            //Example: wrong XML formating
//            try
//            {
//                //request manager & log manager is diclared outside so that error catcher has access
//                //incases of unexpected failure
//                var appLog = new Data(appLogRead, appLogWrite);
//                var logManager = new LogManager(appLog);
//                var requestManager = new RequestManager(request);

//                try
//                {
//                    //prepare data
//                    var config = new Data(configRead, configWrite);
//                    var domainList = new Data(domainListRead, domainListWrite);
//                    var domainLog = new Data(domainLogRead, domainLogWrite);
//                    var accountList = new Data(accountListRead, accountListWrite);

//                    //place the data into managers
//                    var configManager = new ApiConfigManager(config);
//                    var accountManager = new AccountManager(accountList);
//                    var domainManager = new DomainManager(domainList, domainLog);

//                    //prepare logic
//                    var api = new API(requestManager, configManager, accountManager, domainManager, logManager);

//                    //run logic based on api name
//                    switch (apiName)
//                    {
//                        case ApiName.UpdateDomain:
//                            return api.updateDomain();
//                        case ApiName.ListDomain:
//                            return api.listDomain();
//                        case ApiName.AddDomain:
//                            return api.addDomain();
//                        case ApiName.DeleteDomain:
//                            return api.deleteDomain();
//                        case ApiName.CheckAccount:
//                            return api.checkAccount();
//                        case ApiName.CreateAccount:
//                            return api.createAccount();
//                        case ApiName.DeleteAccount:
//                            return api.deleteAccount();
//                        default:
//                            throw new Exception("Switch for API name not specified");
//                    }
//                }
//                //if unexpected errors, pass error to user
//                catch (Exception e)
//                {
//                    //log the error internally
//                    logManager.error(e);

//                    //let user know error has occured
//                    return requestManager.getReply(Reply.UnexpectedError);
//                }
//            }
//            catch (Exception e)
//            {
//                //create the message
//                var message = Message.create("Fail", "Unexpected error occured", e.ToString());

//                //send the error message to the user
//                return request.CreateResponse(HttpStatusCode.OK, message);
//            }


//        }

//    }
//}
