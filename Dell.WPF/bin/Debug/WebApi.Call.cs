using Dell.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Dell.WebAPICall
{ 
    public class WebAPI
    {
        /// <summary>
        /// Create a http client
        /// </summary>
        /// <returns>a <see cref="HttpClient"/></returns>
        public static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:49911/api/")
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json") { CharSet = "utf-8" });

            return client;
        }

        /// <summary>
        /// Fetch a list with <see cref="Customer"/> objects from database.
        /// </summary>
        /// <returns>a <see cref="List{Customer}"/>.</returns>
        public async Task<IList<Customer>> Read()
        {
            IList<Customer> customers = null;
            try
            {
                using (var client = GetHttpClient())
                {
                    var response = await client.GetAsync("customers");
                    response.EnsureSuccessStatusCode();
                    customers = await response.Content.ReadAsAsync<IList<Customer>>();
                }
                return customers;
            }
            catch (Exception ex)
            {
                throw new Exception("Read failed!", ex);
            }
        }

        /// <summary>
        /// Fetch a <see cref="Customer"/> object of the specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a <see cref="Customer"/></returns>
        public async Task<Customer> ReadByID(int id)
        {
            Customer customer = null;
            try
            {
                using (var client = GetHttpClient())
                {
                    var response = await client.GetAsync("customers/" + id);
                    response.EnsureSuccessStatusCode();
                    customer = await response.Content.ReadAsAsync<Customer>();
                }
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception("Read failed!", ex);
            }
        }

        /// <summary>
        /// Fetch a list with <see cref="Customer"/> objects from database, with a specific e-mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns>a <see cref="List{Customer}"/></returns>
        public async Task<List<Customer>> ReadByEmail(string email)
        {
            List<Customer> customer = null;
         
            using (var client = GetHttpClient())
            {
                var response = await client.GetAsync("customers/byEmail/" + email);
                customer = await response.Content.ReadAsAsync<List<Customer>>();
            }
            return customer;
        }

        /// <summary>
        /// Add a new customer in database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns> a <see cref="bool"/></returns>
        public async Task<bool> Add(Customer customer)
        {
            bool result = false;
            try
            {
                using (var httpClient = GetHttpClient())
                {
                    var response = await httpClient.PostAsJsonAsync("customers/", customer);
                    response.EnsureSuccessStatusCode();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception("Inserted failed!", ex);
            }
        }

        /// <summary>
        /// Update a customer in databse
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>a <see cref="bool"/></returns>
        public async Task<bool> Update(Customer customer)
        {
            bool result = true;
            using (var client = GetHttpClient())
            {
                var response = await client.PutAsJsonAsync($"customers/{customer.ID}", customer);
                response.EnsureSuccessStatusCode();
            }
            return result;
        }
    }
}
