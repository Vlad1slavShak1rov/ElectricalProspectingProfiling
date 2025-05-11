using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectricalProspectingProfiling.Database.DAL
{
    public class RepositoryCoordinatsProfile
    {
        private MyDBContext context;
        public RepositoryCoordinatsProfile(MyDBContext context)
        {
            this.context = context;
        }
        public async Task<List<CoordinatsProfile>> GetAll()
        {
            return context.CoordinatsProfile.ToList();
        }
        public async Task<CoordinatsProfile> GetById(int id)
        {
            return context.CoordinatsProfile.Find(id);
        }
        public async Task Add(CoordinatsProfile entity)
        {
            try
            {
                context.CoordinatsProfile.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public async void Update(CoordinatsProfile entity)
        {
            try
            {
                context.CoordinatsProfile.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Remove(CoordinatsProfile entity)
        {
            try
            {
                context.CoordinatsProfile.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
