using System.Collections.Generic;

namespace api.Domain.Services
{
    public interface ISemaphoreService    
    {
        bool Lock(string resource);
        bool Release(string resource); 
        IEnumerable<string> GetResourcesLocked();      
    }

    public class SemaphoreService : ISemaphoreService
    {
        private readonly object _control = new object();
        private HashSet<string> _collection = new HashSet<string>();
        
        public bool Lock(string resource)
        {
            lock(_control)
            {
                return _collection.Add(resource);
            }
        }

        public bool Release(string resource)
        {
            lock(_control)
            {
                return _collection.Remove(resource);
            }
        }

        public IEnumerable<string> GetResourcesLocked()
        {
            return _collection;
        }
    }
}