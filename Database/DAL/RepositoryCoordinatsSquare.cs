using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectricalProspectingProfiling.Database.DAL
{
    public class RepositoryCoordinatsSquare
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
        public async Task Add(CoordinatsSquare entity)
        {
            try
            {
                context.CoordinatsSquare.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task Update(CoordinatsSquare entity)
        {
            try
            {
                context.CoordinatsSquare.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async Task Remove(CoordinatsSquare entity)
        {
            try
            {
                context.CoordinatsSquare.Remove(entity);
                await context.SaveChangesAsync();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
