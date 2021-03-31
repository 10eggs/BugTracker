using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Utils
{
    public class ModelDistributor : IModelDistributor
    {
        //Here proxy!
        private Dictionary<string, object> Storage
        {
            get
            {
                if (Storage == null)
                    return new Dictionary<string, object>();
                return Storage;
            }
        }

        public T GetData<T>(string key)
        {
            return (T)Storage[key];
        }

        public void SetData(string key, object value)
        {
            Storage.Add(key, value);
        }


    }
}
