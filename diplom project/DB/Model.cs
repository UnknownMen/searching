using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace diplom_project.DB
{
    public class Model
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Hash { get; set; }

        public string GetHash()
        {
           
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(Filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

       
    }

   

}
