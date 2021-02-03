using System;
using System.Collections.Generic;
using System.Text;
using SystemBase.Repository.Models;

namespace SystemBase.Service.Interfaces
{
    public interface IStaffService 
    {
        Staff GetByName(string name);

        Staff Get(int id);

        int Create(Staff staff);

        bool Update(Staff staff);

        bool Delete(int id);
    }
}
