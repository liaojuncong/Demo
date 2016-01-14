using Orleans;
using Sample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GrainClient.Initialize();

            while (true)
            {
                Console.WriteLine("请输入用户的手机号码来判断是否存在：");
                //var mobileNumber = Console.ReadLine();

                for (var i = 0; i < 10000; i++)
                {
                    //Task.Run(() =>
                    {
                        var mobileNumber = i.ToString();
                        var userService = GrainClient.GrainFactory.GetGrain<IUserGrain>(mobileNumber);
                        Console.WriteLine($"用户{mobileNumber}，{(userService.Exist().Result ? "已经存在" : "还不存在")}");
                    }
                    //);
                }
                if (Console.ReadLine() == "exit")
                    GrainClient.Uninitialize();
            }
        }
    }
}
