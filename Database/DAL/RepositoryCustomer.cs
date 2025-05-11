using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectricalProspectingProfiling.Database.DAL
{
    public class RepositoryCustomer
    {
        private MyDBContext context;
        public RepositoryCustomer(MyDBContext context)
        {
            this.context = context;
        }
        public async Task<List<Customer>> GetAll()
        {
            return context.Customer.ToList();
        }
        public async Task<Customer> GetById(int id)
        {
            return context.Customer.Find(id);
        }
        public async Task Add(Customer entity)
        {
            try
            {
                context.Customer.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task Update(Customer entity)
        {
            try
            {
                context.Customer.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task Remove(Customer entity)
        {
            try
            {
                context.Customer.Remove(entity);
                await context.SaveChangesAsync();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
