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
    public class RepositoryMeasurement
    {
        private MyDBContext context;
        public RepositoryMeasurement(MyDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Measurement>> GetAll()
        {
            return context.Measurement.ToList();
        }
        public async Task<Measurement> GetById(int id)
        {
            return context.Measurement.Find(id);
        }
        public async Task Add(Measurement entity)
        {
            try
            {
                context.Measurement.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Update(Measurement entity)
        {
            try
            {
                context.Measurement.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Remove(Measurement entity)
        {
            try
            {
                context.Measurement.Remove(entity);
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
