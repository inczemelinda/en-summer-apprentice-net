using Microsoft.EntityFrameworkCore;
using TMS.Models;
using TMS.Repositories;

namespace TMS.Repository
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly TicketManagementSystemContext _dbContext;

        public TicketCategoryRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }

        public int Add(TicketCategory ticketCategory)
        {
            throw new NotImplementedException();
        }

        public void Delete(TicketCategory ticketCategory)
        {
            _dbContext.Remove(ticketCategory);
            _dbContext.SaveChanges();
        }

        public IEnumerable<TicketCategory> GetAll()
        {
            var ticketCategories = _dbContext.TicketCategories;

            return ticketCategories;
        }

        public async Task<TicketCategory> GetById(int id)
        {
            var ticketCategory = await _dbContext.TicketCategories.Where(t => t.TicketCategoryId == id).FirstOrDefaultAsync();

            return ticketCategory;
        }

        public void Update(TicketCategory ticketCategory)
        {
            _dbContext.Entry(ticketCategory).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}