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
        public static int next;
        public int Id { get; set; } = next++;
        public string Filename { get; set; }
        public string Hash { get; set; }

        public string ShortName { get {
                return Path.GetFileNameWithoutExtension(Filename);
            } }

        public string GetHash32()
        {
            return new Crc32().Get(Filename);
            //using (var md5 = MD5.Create())
            //{
            //    using (var stream = File.OpenRead(Filename))
            //    {
            //        var hash = md5.ComputeHash(stream);
            //        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            //    }
            //}
        }

        public string GetHashMD5()
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
