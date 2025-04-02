using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Model;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectricalProspectingProfiling.Database.DAL
{
    public class RepositoryCoordinatsSquare:IRepository<CoordinatsSquare>
    {
        private MyDBContext context;
        public RepositoryCoordinatsSquare(MyDBContext context)
        {
            this.context = context;
        }

        public async Task<List<CoordinatsSquare>> GetAll()
        {
            return context.CoordinatsSquare.ToList();
        }
        public async Task<CoordinatsSquare> GetById(int id)
        {
            return context.CoordinatsSquare.Find(id);
        }
        public async void Add(CoordinatsSquare entity)
        {
            try
            {
                context.CoordinatsSquare.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Update(CoordinatsSquare entity)
        {
            try
            {
                context.CoordinatsSquare.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Remove(CoordinatsSquare entity)
        {
            try
            {
                context.CoordinatsSquare.Remove(entity);
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
