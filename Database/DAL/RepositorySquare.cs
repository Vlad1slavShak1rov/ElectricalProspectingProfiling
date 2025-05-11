using ElectricalProspectingProfiling.Database.context;
using ElectricalProspectingProfiling.Model;
using ElectricalProspectingProfiling.Windows;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElectricalProspectingProfiling.Database.DAL
{
    public class RepositorySquare
    {
        private MyDBContext context;

        public RepositorySquare(MyDBContext context)
        {
            this.context = context;
        }
        public async Task<List<Square>> GetAll()
        {
            return await context.Squares.ToListAsync();
        }
        public async Task<Square> GetById(int id)
        {
            return await context.Squares.FirstOrDefaultAsync(sq=>sq.ID == id);
        }
        public async Task Add(Square entity)
        {
            try
            {
                await context.Squares.AddAsync(entity);
                await context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        public async void Update(Square entity)
        {
            try
            {
                context.Squares.Update(entity);
                await context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void Remove(Square entity)
        {
            try
            {
                context.Squares.Remove(entity);
                await context.SaveChangesAsync();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
