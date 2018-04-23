﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ReviewMe
{
    public interface IAsyncLock
    {
        Task<IDisposable> LockAsync();
    }
}