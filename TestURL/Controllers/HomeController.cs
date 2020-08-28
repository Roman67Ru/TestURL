using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestURL.Models;

using NHibernate.Cfg;
using NHibernate.Dialect;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.Driver.MySqlConnector;
using NHibernate;
using NHibernate.Mapping.Attributes;
using System.Data;
using System.Reflection;
using NHibernate.Tool.hbm2ddl;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Diagnostics;

namespace TestURL.Controllers
{
    public class HomeController : Controller
    {
        private static Configuration ConfigureNHibernate()
        {
            NHibernateProfiler.Initialize();

            var cfg = new Configuration();
            string con = "Server=localhost;Database=urldb;port=3306;User Id=root;password=123Qwerty";

            cfg.DataBaseIntegration(x => {
                x.ConnectionString = con;
                x.Driver<MySqlConnectorDriver>();
                x.Dialect<MySQLDialect>();
                x.IsolationLevel = IsolationLevel.RepeatableRead;
                x.Timeout = 10;
                x.BatchSize = 10;
            });

            HbmSerializer.Default.Validate = true;
            cfg.AddInputStream(HbmSerializer.Default.Serialize(Assembly.GetExecutingAssembly()));

            cfg.SessionFactory().GenerateStatistics();

            return cfg;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Grid([DataSourceRequest]DataSourceRequest request)
        {

            var cfg = ConfigureNHibernate();

            var sessionFactory = cfg.BuildSessionFactory();
            new SchemaUpdate(cfg).Execute(true, true);

            var session = sessionFactory.OpenSession();
            var tx = session.BeginTransaction();

            IQueryable<MainURLViewModel> mURL =  session.Query<MainURLViewModel>();
                tx.Commit();
            



                DataSourceResult result = mURL.ToDataSourceResult(request, e => new MainURLViewModel
                {
                    idMainURL = e.idMainURL,
                    LongUrl = e.LongUrl,
                    ShortUrl = e.ShortUrl,
                    DateAdd = e.DateAdd,
                    Count = e.Count
                });
                return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult Create([DataSourceRequest]DataSourceRequest request, MainURLViewModel e)
        {
            string Rand = Guid.NewGuid().ToString();
            string NewRand = "";
           for(int i = 0; i<5; i++)
            {
                NewRand = NewRand + Rand[i];
            }
            //Когда проект будет размещен на сервере меняем localhost:44372 на домен и все будет красиво
            var g = "https://localhost:44372/Home/go/" + NewRand;

            var mURL = new MainURLViewModel
            {
                LongUrl = e.LongUrl,
                ShortUrl = g,
                DateAdd = DateTime.Now,
                Count = 0
            };
    

            var cfg = ConfigureNHibernate();

            var sessionFactory = cfg.BuildSessionFactory();
            new SchemaUpdate(cfg).Execute(true, true);

            var session = sessionFactory.OpenSession();
            var tx = session.BeginTransaction();
            session.Save(mURL);
            tx.Commit();
            e.ShortUrl = g;
            e.DateAdd = DateTime.Now;
            return Json(new[] {e }.ToDataSourceResult(request));
        }

        public ActionResult Go()
        {
            string qwe = this.RouteData.Values.Values.ToList()[2].ToString();
            qwe = "https://localhost:44372/Home/go/" + qwe;

            string str = "";
            var cfg = ConfigureNHibernate();

            var sessionFactory = cfg.BuildSessionFactory();
            new SchemaUpdate(cfg).Execute(true, true);

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                MainURLViewModel mURL = session.Query<MainURLViewModel>().First(x=>x.ShortUrl == qwe);
                str = mURL.LongUrl;
                mURL.Count = mURL.Count + 1;
                session.Save(mURL);
                tx.Commit();
            }

            return Redirect(str);
        }
        public IActionResult Destroy([DataSourceRequest]DataSourceRequest request, MainURLViewModel model)
        {
            var cfg = ConfigureNHibernate();

            var sessionFactory = cfg.BuildSessionFactory();
            new SchemaUpdate(cfg).Execute(true, true);

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.Delete(model);
                tx.Commit();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}
