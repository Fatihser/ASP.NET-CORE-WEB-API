using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.Repositories
{
    public class DummyRepository : IDummyRepository
    {
        public string GetName()
        {
            return "Fatih";
        }
    }
}
