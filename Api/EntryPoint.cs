using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace BlazorApp.Api
{

    /// <summary>
    /// Place where API calls start their journey 
    /// </summary>
    public class EntryPoint
    {

        /** PUBLIC METHODS **/

        [FunctionName("GetReview")]
        public static HttpResponseMessage getReview(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
            [Blob(Consts.API.ReviewList, FileAccess.Read)]Stream reviewListRead,
            [Blob(Consts.API.ReviewList, FileAccess.Write)] Stream reviewListWrite,
            [Blob(Consts.API.AccountList, FileAccess.Read)] Stream accountListRead,
            [Blob(Consts.API.AccountList, FileAccess.Write)] Stream accountListWrite,
            [Blob(Consts.API.AppLog, FileAccess.Read)] Stream appLogRead,
            [Blob(Consts.API.AppLog, FileAccess.Write)] Stream appLogWrite,
            TraceWriter log) => runApi(ApiName.GetReview, request, reviewListRead, reviewListWrite, accountListRead, accountListWrite, appLogRead, appLogWrite);
        //{

        //get the chip and vendor from caller
        // string chip = request.Query["chip"];
        // string vendor = request.Query["vendor"];





        //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //dynamic data = JsonConvert.DeserializeObject(requestBody);
        //name = name ?? data?.name;

        //string responseMessage = string.IsNullOrEmpty(name)
        //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
        //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

        //return new OkObjectResult(responseMessage);
        //}




        /** PRIVATE METHODS **/

        //prepare the needed data managers & calls the right API logic function
        //based on name and returns the their responses
        private static HttpResponseMessage runApi(ApiName apiName, HttpRequestMessage request, Stream reviewListRead, Stream reviewListWrite, Stream accountListRead, Stream accountListWrite, Stream appLogRead, Stream appLogWrite)
        {
            //final raw error cather for unpredictable failures
            //errors caught here are not logged but details of the error are passed to caller in "more info"
            //Example: wrong XML formating
            try
            {
                //request manager & log manager is diclared outside so that error catcher has access
                //incases of unexpected failure
                var appLog = new Data(appLogRead, appLogWrite);
                //var logManager = new LogManager(appLog);
                var requestManager = new RequestManager(request);

                try
                {
                    //prepare data
                    var config = new Data(reviewListRead, reviewListWrite);
                    var accountList = new Data(accountListRead, accountListWrite);
                    var reviewList = new Data(reviewListRead, reviewListWrite);

                    //place the data into managers
                    var configManager = new ApiConfigManager(config);
                    var accountManager = new AccountManager(accountList);
                    var reviewManager = new ReviewManager(reviewList);

                    //prepare logic
                    var api = new API(requestManager, configManager, accountManager, reviewManager);

                    //run logic based on api name
                    switch (apiName)
                    {
                        case ApiName.GetReview:
                            return api.getReview();
                        case ApiName.ListDomain:
                            return api.listDomain();
                        case ApiName.AddDomain:
                            return api.addDomain();
                        case ApiName.DeleteDomain:
                            return api.deleteDomain();
                        case ApiName.CheckAccount:
                            return api.checkAccount();
                        case ApiName.CreateAccount:
                            return api.createAccount();
                        case ApiName.DeleteAccount:
                            return api.deleteAccount();
                        default:
                            throw new Exception("Switch for API name not specified");
                    }
                }
                //if unexpected errors, pass error to user
                catch (Exception e)
                {
                    //log the error internally
                    LogManager.Error(e);

                    //let user know error has occured
                    return requestManager.getReply(Reply.UnexpectedError);
                }
            }
            catch (Exception e)
            {
                //create the message
                var message = Message.create("Fail", "Unexpected error occured", e.ToString());

                //send the error message to the user
                return request.CreateResponse(HttpStatusCode.OK, message);
            }


        }


    }
}

