﻿using Microsoft.EntityFrameworkCore;
using TMS.Models;
using TMS.Repositories;

namespace TMS.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketManagementSystemContext _dbContext;

        public EventRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }

        public int Add(Event @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events
                                   .Include(e => e.EventType)
                                   .Include(e => e.Venue);

            return events;
        }

        public async Task<Event> GetById(int id)
        {
            var @event = await _dbContext.Events.Include(e => e.EventType)
                                                .Include(e => e.Venue)
                                                .Where(e => e.EventId == id)
                                                .FirstOrDefaultAsync();
            return @event;
        }

        public void Update(Event @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}