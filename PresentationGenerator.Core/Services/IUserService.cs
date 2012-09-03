﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PresentationGenerator.Core.Services
{
    public interface IUserService
    {
        bool DoesUserExistWithUsername(string username);
        bool DoesUserExistWithUsernameAndPassword(string username, string password);
    }
}
