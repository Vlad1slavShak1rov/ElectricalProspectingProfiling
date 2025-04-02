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
    public class RepositorySquare :IRepository<Square>
    {
        private MyDBContext context;

        public RepositorySquare(MyDBContext context)
        {
            this.context = context;
        }
        public async Task<List<Square>> GetAll()
        {
            return context.Squares.ToList();
        }
        public async Task<Square> GetById(int id)
        {
            return context.Squares.Find(id);
        }
        public async void Add(Square entity)
        {
            try
            {
                context.Squares.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
