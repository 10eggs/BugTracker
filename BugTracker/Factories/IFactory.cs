using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Persistance
{
    interface IFactory<T>
    {
        public T Create();
    }
}
