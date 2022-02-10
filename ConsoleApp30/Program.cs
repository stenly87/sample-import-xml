using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp30
{
    class Program
    {
        static void Main(string[] args)
        {
            DB db = new DB();
            
            string path = @"C:\Users\user\Desktop\КЗ_РЧ20_21_Основная группа\Session 1\blood_services.xml";
            XmlSerializer xml = new XmlSerializer(typeof(List<record>));
            List<record> result = null;
            using (var fs = File.OpenRead(path))
                result = (List<record>)xml.Deserialize(fs);
            Console.WriteLine(result[0].finishedDT.ToShortDateString());
            
            db.BloodServices.AddRange(result);
            using (var transaction = db.Database.BeginTransaction())
            {
                db.Database.ExecuteSqlRaw("set identity_insert [1135_blood].[dbo].[BloodServices] ON;");
                db.SaveChanges();
                db.Database.ExecuteSqlRaw("set identity_insert [1135_blood].[dbo].[BloodServices] OFF;");
                transaction.Commit();
            }
        }
    }

    public class record
    { 
        [Key]
        
        public int blood { get; set; }
        public int service { get; set; }
        public double result { get; set; }
        public long finished { get; set; }
        public bool accepted { get; set; }
        public string status { get; set; }
        public string analyzer { get; set; }
        public int user { get; set; }

        public DateTime finishedDT { get =>
                DateTimeOffset.FromUnixTimeMilliseconds(finished).DateTime;
        }
    }

    /*public class record
    {
        public int id { get; set;}
        public int patient { get; set; }
        public long date { get; set; }
        public int barcode { get; set; }
    }*/
}
