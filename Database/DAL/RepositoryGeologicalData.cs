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
    public class RepositoryGeologicalData
    {
        private MyDBContext context;
        public RepositoryGeologicalData(MyDBContext context)
        {
            this.context = context;
        }

        public async Task<List<GeologicalData>> GetAll()
        {
            return context.GeologicalData.ToList();
        }
        public async Task<GeologicalData> GetById(int id)
        {
            return context.GeologicalData.Find(id);
        }
        public async Task Add(GeologicalData entity)
        {
            try
            {
                context.GeologicalData.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Update(GeologicalData entity)
        {
            try
            {
                context.GeologicalData.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Remove(GeologicalData entity)
        {
            try
            {
                context.GeologicalData.Remove(entity);
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
