using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DbMain db = new DbMain())
            {
                var i = 1;

                var products = db.Products.AsNoTracking();
                var prodLocs = db.ProductLocs.AsNoTracking();
                string lang = "ru";
                var res = (from p in products
                          from pl in prodLocs.Where(x => x.Id == p.Id).DefaultIfEmpty()
                          where pl.Lang == lang
                          select new
                          {
                              pId = p.Id,
                              pName = pl.Name,
                              pGuid = p.OId
                          }).Take(200);


                foreach (var item in res)
                {
                    Console.WriteLine($"[{i}] \t {item.pName} \t - \t {item.pGuid}  \t - \t {item.pId}");
                    i++;
                }

                var cps = db.Companies.Take(10);
                i = 1;
                foreach (var com in cps)
                {
                    Console.WriteLine($"[{i}] \t {com.Id} \t - \t {com.GlnCode}  \t - \t {com.Type}");
                    i++;
                }

                /*
                var pc = db.tProdAlls.Select(p=>p.PropertyName).Distinct();
                i = 1;
                foreach (var p in pc)
                {
                    Console.WriteLine($"[{i}] \t - \t {p}");
                    i++;
                }            

                
                var prds = db.Products.Take(100);  
                foreach (Product p in prds)
                {
                    Console.WriteLine($"[{i}] \t {p.Id} \t - \t {p.Name}");
                    i++;
                }
                */
            }
            Console.Read();
        }

    }
}
