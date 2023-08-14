﻿using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.IRepositories
{
    public interface IEmployeeRepository
    {
        void AddEmployee(GroupEmployeeEntity employee);
    }
}
