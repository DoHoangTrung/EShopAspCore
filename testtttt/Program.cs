using EshopAspCore.Application.Utilities.Confirm;
using System;

namespace testtttt
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = "qwerqwerqwerqwer";
            var token = AesOperation.EncryptString(key, "Hello im FatBuu");

            var result = AesOperation.DecryptString(key, token);
            Console.WriteLine(result);
        }
    }
}
