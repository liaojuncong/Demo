using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sino.GrpcClient.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    var list = MsgServiceClient.GetList(0, "1", 0, 0);
                    Console.WriteLine(list.ToString());
                }
                catch (Exception ex)
                {

                }
                Console.ReadKey();
            }
        }
    }
}
