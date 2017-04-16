using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DostiCardDataAccess;

namespace DostiCardApi.Controllers
{
    public class StoreController : ApiController
    {
        private DostiCardDBEntities storeEntities = new DostiCardDBEntities();

        [HttpGet]
        public HttpResponseMessage listOfStore() {
            return Request.CreateResponse(HttpStatusCode.OK, storeEntities.StoreTables.ToList());
        }

        [Route("api/Store/{store_id}")]
        public StoreTable GetStore(int store_id) {
            StoreTable store = storeEntities.StoreTables.FirstOrDefault(e => e.ID == store_id);
            if (store != null)
            {
                return store;
            }
            else
                return null;
        }

        [Route("api/Store/getStoreByAddress/{address}")]
        public StoreTable GetStoreWithAddress(String address)
        {
            StoreTable store = storeEntities.StoreTables.Where(e => e.Address == address).FirstOrDefault();
            if (store != null)
            {
                return store;
            }
            else return null;
        }

        [HttpGet]
        [Route("api/Store/isStoreAlreadyExist/{name}/{address}")]
        public Boolean isExist(String name, String address)
        {
            StoreTable store = storeEntities.StoreTables.Where(e => e.StoreName == name || e.Address == address).FirstOrDefault();
            if (store != null)
                return true;
            else
                return false;
        }

        [HttpPost]
        public StoreTable addStore([FromBody] StoreTable store)
        {
            if (store != null)
            {
                storeEntities.StoreTables.Add(store);
                storeEntities.SaveChanges();
                return store;
            }
            else return null;
        
        }
        
        public void DeleteStore(int id) {
            StoreTable store = storeEntities.StoreTables.FirstOrDefault(e => e.ID == id);
            if (store != null) {
                storeEntities.StoreTables.Remove(store);
            }
            storeEntities.SaveChanges();
        }

        [HttpPut]
        [Route("api/Store/updateStore/{store_name}/{store_address}")]
        public StoreTable updateStore(String store_name, String store_address, StoreTable store) {
            StoreTable entity = storeEntities.StoreTables.FirstOrDefault(e => e.StoreName == store_name && e.Address == store_address);
            if (entity != null) {
                entity.StoreName = store.StoreName;
                entity.City = store.City;
                entity.Address = store.Address;
                entity.PointsLimit = store.PointsLimit;
                entity.PaymentToGetOnePoint = store.PaymentToGetOnePoint;
                entity.PercentageDiscount = store.PercentageDiscount;
            }
            storeEntities.SaveChanges();
            return entity;
        }
        
    }
}
