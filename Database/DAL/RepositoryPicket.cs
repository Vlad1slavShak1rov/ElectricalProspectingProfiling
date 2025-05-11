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
    public class RepositoryPicket
    {
        private MyDBContext context;
        public RepositoryPicket(MyDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Picket>> GetAll()
        {
            return await context.Picket.ToListAsync();
        }
        public async Task<Picket> GetById(int id)
        {
            return context.Picket.Find(id);
        }

        public async Task<List<Picket>> GetByPicketID(int id)
        {
            return await context.Picket.Where(pc => pc.ПрофильID == id).ToListAsync();
        }
        public async Task Add(Picket entity)
        {
            try
            {
                context.Picket.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public async void Update(Picket entity)
        {
            try
            {
                context.Picket.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Remove(Picket entity)
        {
            try
            {
                context.Picket.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
