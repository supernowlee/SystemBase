using System;
using System.Linq;
using SystemBase.Repository.Interfaces;
using SystemBase.Repository.Models;
using SystemBase.Service.Interfaces;

namespace SystemBase.Service.Services
{
    public class StaffService : IStaffService
    {
        private readonly IRepository<Staff, int> Reposty;

        public StaffService(IRepository<Staff, int> repository)
        {
            Reposty = repository;
        }

        public Staff GetByName(string name)
        {
            var result = Reposty.Find(f => f.Name == name).FirstOrDefault();

            return result ?? new Staff();
        }

        public Staff Get(int id)
        {
            var result = Reposty.FindById(id);

            return result;
        }

        public int Create(Staff staff)
        {
            Reposty.Create(staff);

            return staff.Id;
        }

        public bool Update(Staff staff)
        {
            try
            {
                Reposty.Update(staff);

                return true;
            }
            catch (Exception ex)
            {
                // 例外處理
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Reposty.Delete(id);

                return true;
            }
            catch (Exception ex)
            {
                // 例外處理
                return false;
            }
        }
    }
}
