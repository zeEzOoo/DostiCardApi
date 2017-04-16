using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DostiCardDataAccess;

namespace DostiCardApi.Controllers
{
    public class CustomerController : ApiController
    {
        private DostiCardDBEntities customerEntities = new DostiCardDBEntities();

        [HttpGet]
        public HttpResponseMessage listOfCustomer() {
            return Request.CreateResponse(HttpStatusCode.OK, customerEntities.CustomerTables.ToList());
        }

        public CustomerTable GetCustomer(int id) {
            CustomerTable customer = customerEntities.CustomerTables.FirstOrDefault(e => e.ID == id);
            if (customer != null)
            {
                return customer;
            }
            else
                return null;
        }

        [Route("api/Customer/{store_id}/{contact_number}")]
        public CustomerTable GetSpecificCustomer(int store_id, String contact_number) {
            CustomerTable customer = customerEntities.CustomerTables.FirstOrDefault(e => e.StoreID == store_id && e.ContactNo == contact_number);
            if (customer != null)
            {
                return customer;
            }
            else
                return null;
        }

        [Route("api/Customer/customer_store/{store_id}")]
        public HttpResponseMessage GetCustomerOfStore(int store_id) {
            return Request.CreateResponse(HttpStatusCode.OK, customerEntities.CustomerTables.Where(e => e.StoreID == store_id));
        }

        [HttpGet]
        [Route("api/Customer/isCustomerExist/{contact_numb}")]
        public Boolean isExist(String contact_numb)
        {
            CustomerTable customer = customerEntities.CustomerTables.Where(e => e.ContactNo == contact_numb).FirstOrDefault();
            if (customer != null)
                return true;
            else
                return false;
        }

        [HttpPost]
        public CustomerTable addCustomer(CustomerTable customer) {
            if (customer != null)
            {
                customerEntities.CustomerTables.Add(customer);
                customerEntities.SaveChanges();
                return customer;
            }
            else return null;
        }

        [Route("api/Customer/deletingCustomer/{storeId}/{contact}")]
        public void DeleteCustomer(int storeId, String contact) {
            CustomerTable customer = customerEntities.CustomerTables.FirstOrDefault(e => e.StoreID == storeId && e.ContactNo == contact);
            if (customer != null) {
                customerEntities.CustomerTables.Remove(customer);
            }
            customerEntities.SaveChanges();
        }

        [HttpPut]
        public CustomerTable updateCustomer(int id, [FromBody] CustomerTable customer) {
            CustomerTable entity = customerEntities.CustomerTables.FirstOrDefault(e => e.ID == id);
            if (entity != null) {
                entity.Fname = customer.Fname;
                entity.Lname = customer.Lname;
                entity.ContactNo = customer.ContactNo;
                entity.Address = customer.Address;
            }
            customerEntities.SaveChanges();
            return entity;
        }

        [HttpPut]
        [Route("api/Customer/{id}/{points}/{reserved_amount}/{rewards}")]
        public CustomerTable updatePointCard(int id, int points, int reserved_amount, int rewards) {
            CustomerTable customer = customerEntities.CustomerTables.Where(e => e.ID == id).FirstOrDefault();
            if (customer != null) {
                customer.Points = points;
                customer.ReservedAmount = reserved_amount;
                customer.Rewards = rewards;
            }
            customerEntities.SaveChanges();
            return customer;
        }

        [HttpPut]
        [Route("api/Customer/addLoad/{contact_no}/{balance}")]
        public CustomerTable updateGiftCard(String contact_no, int balance)
        {
            CustomerTable customer = customerEntities.CustomerTables.Where(e => e.ContactNo == contact_no).FirstOrDefault();
            if (customer != null)
            {
                customer.Load = balance;
            }
            customerEntities.SaveChanges();
            return customer;
        }

    }
}
