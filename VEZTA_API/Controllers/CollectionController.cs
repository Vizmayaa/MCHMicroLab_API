using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VEZTA.Controllers
{
    using DAL;
    using Models;
    using System.Data;
    using System.Web.Http.Cors;

    [RoutePrefix("api/collection")]

    public class CollectionController : ApiController
    {
        [HttpPost]
        [Route("masterlist")]
        public MasterResponse MasterList()
        {
            MasterResponse res = new MasterResponse();

            try
            {

                Collection_DAL dbhandle = new Collection_DAL();

                // Retrieve the full response from DAL
                res = dbhandle.MasterData();

                if (res != null)
                {
                    res.flag = 1;
                    res.message = "Success";
                }
                else
                {
                    res.flag = 0;
                    res.message = "No data found.";
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }
        
        [HttpPost]
        [Route("pendingCollection")]
        public PendingCollectionResponse pendingCollection()
        {
            PendingCollectionResponse res = new PendingCollectionResponse();

            try
            {

                Collection_DAL dbhandle = new Collection_DAL();

                // Retrieve the full response from DAL
                res = dbhandle.GetPendingCollection();

                if (res != null)
                {
                    res.flag = 1;
                    res.Message = "Success";
                }
                else
                {
                    res.flag = 0;
                    res.Message = "No data found.";
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }


        [HttpPost]
        [Route("insert")]
        public CollectionResponse Insert(Collection collection)
        {
            CollectionResponse res = new CollectionResponse();

          

            try
            {
                Collection_DAL dbhandle = new Collection_DAL();
                res =  dbhandle.Insert(collection);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
                
            }

            return res;
        }
       
        [HttpPost]
        [Route("download")]
        public CollectionDownloadOutput DownloadCollection(CollectionDownloadInput vInput)
        {

            CollectionDownloadOutput vOutput = new CollectionDownloadOutput();

            try
            {
                Collection_DAL dbhandle = new Collection_DAL();
                vOutput = dbhandle.DownloadCollection(vInput);

            }
            catch (Exception ex)
            {
                vOutput.flag = 0;
                vOutput.message = ex.Message;

            }

            return vOutput;
        }

        [HttpPost]
        [Route("update")]
        public CollectionResponse Update(Collection collection)
        {
            CollectionResponse res = new CollectionResponse();



            try
            {
                Collection_DAL dbhandle = new Collection_DAL();
                res = dbhandle.Update(collection);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }

            return res;
        }

        [HttpPost]
        [Route("CollectionNo")]
        public CollectionResponse GetNextCollectionNo()
        {
            CollectionResponse res = new CollectionResponse();



            try
            {
                Collection_DAL dbhandle = new Collection_DAL();
                res = dbhandle.NextCollectionNo();

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }

            return res;
        }

        [HttpPost]
        [Route("collectionlist")]
        public CollectionResponse CollectionList(CollectionInput collectionInput)
        {
            CollectionResponse res = new CollectionResponse();
            List<Collection> collections = new List<Collection>();
            try
            {

                Collection_DAL dbhandle = new Collection_DAL();
                collections = dbhandle.GetCollection(collectionInput);

                res.flag = 1;
                res.Message = "Success";
                res.CollectionData = collections;
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("refdoctors")]
        public RefDoctorResponse GetReferenceDoctors()
        {
            RefDoctorResponse res = new RefDoctorResponse();

            try
            {
                Collection_DAL dbhandle = new Collection_DAL();
                res = dbhandle.GetReferenceDoctors();  

                if (res != null && res.Data != null && res.Data.Count > 0)
                {
                    res.flag = 1;
                    res.Message = "Success";
                }
                else
                {
                    res.flag = 0;
                    res.Message = "No doctors found.";
                }
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
       
        [HttpPost]
        [Route("delete/{id:int}")]
        public CollectionResponse DeleteCollection(int id)
        {
            CollectionResponse res = new CollectionResponse();
            try
            {
                Collection_DAL dbhandle = new Collection_DAL();
                res = dbhandle.DeleteCollection(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }


    }
}