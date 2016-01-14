using Orleans;
using Sample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Implements
{
    public class UserGrain : Grain, IUserGrain
    {
        public Task<bool> Exist()
        {
            Console.WriteLine($"{this.GetPrimaryKeyString()} : 我处理了一个请求。");
            return Task.FromResult(this.GetPrimaryKeyString() == "1388888888");
        }
    }
}
