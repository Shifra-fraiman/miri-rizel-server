using CopyRight.Dal.Interfaces;
using CopyRight.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace CopyRight.Dal.Service
{
    public class CustomerService : ICustomer
    {
        private CopyRightContext db;
        public CustomerService(CopyRightContext db)
        {
            this.db = db;
        }
        public async Task<List<Customer>> ReadAllAsync() {
            return await db.Customers
               .Include(t => t.StatusNavigation).ToListAsync();
           }
        


        public async Task<Customer> CreateAsync(Customer item)
        {
            try
            {
                await db.Customers.AddAsync(item);
                await db.SaveChangesAsync();
                return item;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int customerId)
        {
            try
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                if (customer == null)
                    throw new Exception("customer does not exist in DB");
                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> existsEmailAsync(string email)
        {
           List<Customer> cstomers= await ReadAsync(c => c.Email == email); 
            if (cstomers.Count == 0)
                return false;
            return true;
        }
        public async  Task<Customer> GetByIdAsync(int customerId)
        {
            try
            {
                Customer customer = await db.Customers.Include(c => c.StatusNavigation) 
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
                if (customer == null)
                    throw new Exception("Customer does not exist in DB");

                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public async Task<List<Customer>> ReadAsync(Predicate<Customer> filter)
        {
            List<Customer> customers = await db.Customers.ToListAsync();
            return customers.FindAll(filter);
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            try
            {
                int index = db.Customers.ToList().FindIndex(x => x.CustomerId == customer.CustomerId);
                if (index == -1)
                    throw new Exception("customer does not exist in DB");
                var existingCustomer = db.Customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.BusinessName = customer.BusinessName;
                existingCustomer.Email = customer.Email;
                existingCustomer.Source = customer.Source;
                existingCustomer.Status = customer.Status;
                existingCustomer.CreatedDate = customer.CreatedDate;
                existingCustomer.Phone = customer.Phone;

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);    
            }
        }
        public async Task<List<StatusCodeUser>> GetCustomerStatusAsync()
        {
            try
            {
                return await db.StatusCodeUsers.ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException("An error occurred while saving data to the database. Please try again later.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("error-", ex);
            }
        }
    }
}