using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Utils
{
    public interface IModelDistributor
    {
        void SetData(string key, object value);
        T GetData<T>(string key);
    }
}
