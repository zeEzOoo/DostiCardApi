using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DostiCardDataAccess;

namespace DostiCardApi.Controllers
{
    public class MerchantController : ApiController
    {
        private DostiCardDBEntities merchantEntities = new DostiCardDBEntities();

        [HttpGet]
        public HttpResponseMessage listOfMerchant() {
            return Request.CreateResponse(HttpStatusCode.OK, merchantEntities.MerchantTables.ToList());
        }

        public MerchantTable GetMerchent(int id) {
            MerchantTable merchant = merchantEntities.MerchantTables.FirstOrDefault(e => e.ID == id);
            if (merchant != null) {
                return merchant;
            } else
                return null;
        }

        [HttpGet]
        [Route("api/Merchant/{contactNo}/{password}")]
        public MerchantTable login(String contactNo, String password) {
            MerchantTable merchant = merchantEntities.MerchantTables.Where(e => e.ContactNo == contactNo && e.Password == password).FirstOrDefault();
            if (merchant != null)
            {
                return merchant;
            }
            else return null;
        }

        [HttpGet]
        [Route("api/Merchant/isMerchantExist/{contact_numb}")]
        public Boolean isExist(String contact_numb)
        {
            MerchantTable merchant = merchantEntities.MerchantTables.Where(e => e.ContactNo == contact_numb).FirstOrDefault();
            if (merchant != null)
                return true;
            else
                return false;
        }

        [HttpPost]
        public MerchantTable addNewMerchant(MerchantTable merchant) {
            if (merchant != null)
            {
                merchantEntities.MerchantTables.Add(merchant);
                merchantEntities.SaveChanges();
                return merchant;
            }
            else
                return null;
        }

        public void DeleteMerchant(int id) {
            MerchantTable merchant = merchantEntities.MerchantTables.Where(e => e.ID == id).FirstOrDefault();
            if (merchant != null) {
                merchantEntities.MerchantTables.Remove(merchant);
            }
            merchantEntities.SaveChanges();
        }

        [HttpPut]
        [Route("api/Merchant/updateMerchant/{id}")]
        public MerchantTable updateMerchant(int id, MerchantTable merchant) {
            MerchantTable entity = merchantEntities.MerchantTables.FirstOrDefault(e => e.ID == id);
            if (entity != null) {
                entity.Fname = merchant.Fname;
                entity.Lname = merchant.Lname;
                entity.Password = merchant.Password;
                entity.ContactNo = merchant.ContactNo;
                entity.Address = merchant.Address;
            }
            merchantEntities.SaveChanges();
            return entity;
        }

        [HttpPut]
        [Route("api/Merchant/updateStoreIdAndDesignation/{merchant_id}/{store_id}/{designation}")]
        public MerchantTable updateMerchantStoreIdAndDesignation(int merchant_id, int store_id, String designation) {
            MerchantTable merchant = merchantEntities.MerchantTables.Where(e => e.ID == merchant_id).FirstOrDefault();
            if (merchant != null) {
                merchant.StoreID = store_id;
                merchant.Designation = designation;
            }
            merchantEntities.SaveChanges();
            return merchant;
        }
    }
}
