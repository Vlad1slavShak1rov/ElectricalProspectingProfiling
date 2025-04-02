using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectricalProspectingProfiling.Database.DAL
{
    public class RepositoryContractL:IRepository<Contract>
    {
        private MyDBContext context;
        public RepositoryContractL(MyDBContext context)
        {
            this.context = context;  
        }
        public async Task<List<Contract>> GetAll() 
        {
            return context.Contracts.ToList(); 
        }
        public async Task<Contract> GetById(int id)
        {
            return context.Contracts.Find(id);
        }
        public async void Add(Contract entity)
        {
            try
            {
                context.Contracts.Add(entity);
               await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public async void Update(Contract entity)
        {
            try
            {
                context.Contracts.Update(entity);
                await context.SaveChangesAsync();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Remove(Contract entity)
        {
            try
            {
                context.Contracts.Remove(entity);
                await context.SaveChangesAsync();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }        
    }
}
