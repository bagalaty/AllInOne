using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Repositories
{
    interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(string id);
        BaseModel Create(T model);
        BaseModel Update(T model);
        bool Delete(string id);
    }
}
