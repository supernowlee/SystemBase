using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SystemBase.Repository.Interfaces;
using SystemBase.Repository.Models;

namespace SystemBase.Repository.Repositories
{
    public class StaffRepository : IRepository<Staff, int>
    {
        private readonly StaffContext _context;

        public StaffRepository(StaffContext context)
        {
            _context = context;
        }

        public int Create(Staff entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void Update(Staff entity)
        {
            var oriUser = _context.Users.Single(x => x.Id == entity.Id);
            _context.Entry(oriUser).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Single(x => x.Id == id));
            _context.SaveChanges();
        }

        public IEnumerable<Staff> Find(Expression<Func<Staff, bool>> expression)
        {
            return _context.Users.Where(expression);
        }

        public Staff FindById(int id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }
    }
}