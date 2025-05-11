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
    public class RepositoryProfile
    {
        private MyDBContext context;

        public RepositoryProfile(MyDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Profile>> GetAll()
        {
            return context.Profile.ToList();
        }
        public async Task<Profile> GetById(int id)
        {
            return context.Profile.Find(id);
        }

        public async Task<List<Profile>> GetBySquareID(int id)
        {
            return await context.Profile.Where(pr=>pr.ПлощадьID == id).ToListAsync();
        }
        public async Task Add(Profile entity)
        {
            try
            {
                context.Profile.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Update(Profile entity)
        {
            try
            {
                context.Profile.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Remove(Profile entity)
        {
            try
            {
                context.Profile.Remove(entity);
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
