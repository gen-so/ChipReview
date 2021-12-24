using BlazorApp.Shared;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace API
{
    public static class EntryPoint
    {

        [FunctionName("GetReview")]
        public static HttpResponseMessage getReview(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
        [Blob(Consts.API.ReviewList, FileAccess.Read)] Stream reviewListRead,
        [Blob(Consts.API.ReviewList, FileAccess.Write)] Stream reviewListWrite,
        [Blob(Consts.API.AppLog, FileAccess.Read)] Stream appLogRead,
        [Blob(Consts.API.AppLog, FileAccess.Write)] Stream appLogWrite,
        TraceWriter log) =>
            runApi(apiName: ApiName.GetReviewAll,
                request: request,
                reviewListRead: reviewListRead,
                reviewListWrite: reviewListWrite,
                appLogRead: appLogRead,
                appLogWrite: appLogWrite);

        [FunctionName("GetReviewAll")]
        public static HttpResponseMessage getReviewAll(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
        [Blob(Consts.API.ReviewList, FileAccess.Read)] Stream reviewListRead,
        [Blob(Consts.API.ReviewList, FileAccess.Write)] Stream reviewListWrite,
        [Blob(Consts.API.AppLog, FileAccess.Read)] Stream appLogRead,
        [Blob(Consts.API.AppLog, FileAccess.Write)] Stream appLogWrite,
        TraceWriter log) =>
            runApi(apiName: ApiName.GetReviewAll,
                request: request,
                reviewListRead: reviewListRead,
                reviewListWrite: reviewListWrite,
                appLogRead: appLogRead,
                appLogWrite: appLogWrite);


        [FunctionName("GetChipAll")]
        public static HttpResponseMessage getChipAll(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
        [Blob(Consts.API.ChipList, FileAccess.Read)] Stream chipListRead,
        [Blob(Consts.API.ChipList, FileAccess.Write)] Stream chipListWrite,
        [Blob(Consts.API.AppLog, FileAccess.Read)] Stream appLogRead,
        [Blob(Consts.API.AppLog, FileAccess.Write)] Stream appLogWrite,
        TraceWriter log) =>
            runApi(ApiName.GetChipAll,
                request: request,
                chipListRead: chipListRead,
                chipListWrite: chipListWrite,
                appLogRead: appLogRead,
                appLogWrite: appLogWrite);


        [FunctionName("AddNewReview")]
        public static HttpResponseMessage addNewReview(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
        [Blob(Consts.API.ReviewList, FileAccess.Read)] Stream reviewListRead,
        [Blob(Consts.API.ReviewList, FileAccess.Write)] Stream reviewListWrite,
        [Blob(Consts.API.AppLog, FileAccess.Read)] Stream appLogRead,
        [Blob(Consts.API.AppLog, FileAccess.Write)] Stream appLogWrite,
        TraceWriter log) =>
            runApi(ApiName.AddNewReview,
                request: request,
                reviewListRead: reviewListRead,
                reviewListWrite: reviewListWrite,
                appLogRead: appLogRead,
                appLogWrite: appLogWrite);


        [FunctionName("DeleteReview")]
        public static HttpResponseMessage deleteReview(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage request,
        [Blob(Consts.API.ReviewList, FileAccess.Read)] Stream reviewListRead,
        [Blob(Consts.API.ReviewList, FileAccess.Write)] Stream reviewListWrite,
        [Blob(Consts.API.AppLog, FileAccess.Read)] Stream appLogRead,
        [Blob(Consts.API.AppLog, FileAccess.Write)] Stream appLogWrite,
        TraceWriter log) =>
            runApi(ApiName.AddNewReview,
                request: request,
                reviewListRead: reviewListRead,
                reviewListWrite: reviewListWrite,
                appLogRead: appLogRead,
                appLogWrite: appLogWrite);




        /** PRIVATE METHODS **/

        //prepare the needed data managers & calls the right API logic function
        //based on name and returns the their responses
        private static HttpResponseMessage runApi(ApiName apiName, HttpRequestMessage request = null, Stream configRead = null, Stream configWrite = null, Stream reviewListRead = null, Stream reviewListWrite = null, Stream chipListRead = null, Stream chipListWrite = null, Stream accountListRead = null, Stream accountListWrite = null, Stream appLogRead = null, Stream appLogWrite = null)
        {
            //final raw error cather for unpredictable failures
            //errors caught here are not logged but details of the error are passed to caller in "more info"
            //Example: wrong XML formatting
            try
            {
                //request manager & log manager is declared outside so that error catcher has access
                //in cases of unexpected failure
                var appLog = new Data(appLogRead, appLogWrite); //prep data
                LogManager.Initialize(appLog); //load into manager
                RequestManager.Initialize(request);


                try
                {
                    //initialize all needed managers
                    InitializeAllManager();

                    //run logic based on api name
                    switch (apiName)
                    {
                        case ApiName.GetReview:
                            return API.getReview();
                        case ApiName.AddNewReview:
                            return API.addNewReview();
                        case ApiName.GetReviewAll:
                            return API.getReviewAll();
                        case ApiName.GetChipAll:
                            return API.getChipAll();
                        case ApiName.DeleteReview:
                            return API.deleteReview();
                        default:
                            throw new Exception("Switch for API name not specified");
                    }
                }
                //if unexpected errors, pass error to user
                catch (Exception e)
                {
                    //log the error internally
                    //TODO errors in log needs to go to errorlog.xml
                    LogManager.Error(e);

                    //let user know error has occurred
                    return RequestManager.getReply(Reply.UnexpectedError, e.Message);
                }
            }
            catch (Exception e)
            {
                //create the message
                var message = Message.create("Fail", "Unexpected error occured", e.ToString());

                //send the error message to the user
                return request.CreateResponse(HttpStatusCode.OK, message);
            }



            //FUNCTIONS

            //only initialize the manager if needed data is provided
            void InitializeAllManager()
            {
                //only init if got data
                if (reviewListRead != null)
                {
                    //prepare data
                    var reviewList = new Data(reviewListRead, reviewListWrite);

                    //place the data into managers
                    ReviewManager.Initialize(reviewList);
                }

                //only init if got data
                if (chipListRead != null)
                {
                    //prepare data
                    var chipList = new Data(chipListRead, chipListWrite);

                    //place the data into managers
                    ChipManager.Initialize(chipList);
                }

            }

        }







    }





}
