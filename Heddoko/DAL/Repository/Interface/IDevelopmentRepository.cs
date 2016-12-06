﻿using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IDevelopmentRepository : IBaseRepository<Development>
    {
        IEnumerable<Development> All(bool isDeleted);

        Development GetByClient(string client);

        IEnumerable<Development> GetByUserId(int UserId);
    }
}
