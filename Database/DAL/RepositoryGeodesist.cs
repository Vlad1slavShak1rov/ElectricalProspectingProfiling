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
    public class RepositoryGeodesist 
    {
        private MyDBContext context;
        public RepositoryGeodesist(MyDBContext context)
        {
            this.context = context;
        }
        public async Task<List<Geodesist>> GetAll()
        {
            return await context.Geodesist.ToListAsync();
        }
        public async Task<Geodesist> GetById(int id)
        {
            return context.Geodesist.Find(id);
        }
        public async Task Add(Geodesist entity)
        {
            try
            {
                context.Geodesist.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Update(Geodesist entity)
        {
            try
            {
                context.Geodesist.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Remove(Geodesist entity)
        {
            try
            {
                context.Geodesist.Remove(entity);
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
