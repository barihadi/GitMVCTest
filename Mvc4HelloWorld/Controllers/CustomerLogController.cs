using Mvc4HelloWorld.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc4HelloWorld.Controllers
{
    public class CustomerLogController : Controller
    {
        private PDBEntities2 db = new PDBEntities2();
        //
        // GET: /CustomerLog/

        public ActionResult Index()
        {
            return View();
        }

     
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CustomerLog/Create

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                // TODO: Add insert logic here
                db.Customers.Add(customer);
                db.SaveChanges();
                TempData["Message"] = "Log Successfully Added";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Message"] = "Could not add customer to DB";
                return View();
            }
        }


        public ActionResult WebGrid()
        {

            List<GridModel> gridDataList = new List<GridModel>();  // creating list of model.

            DataSet ds = new DataSet();
            ds = mydata(); // fill dataset

            foreach (DataRow dr in ds.Tables[0].Rows) // loop for adding add from dataset to list<modeldata>
            {
                gridDataList.Add(new GridModel
                {
                    CustomerId = Convert.ToInt32(dr["CustomerId"]), // adding data from dataset row in to list<modeldata>
                    CustomerName = dr["CustomerName"].ToString(),
                    Amount = Convert.ToDouble(dr["Amount"])
                   
                });
            }
            return View(gridDataList);
        }




        public DataSet mydata()
        {
            DataSet myrec = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PDBCon"].ToString());
                SqlCommand cmd = new SqlCommand("select top 10 * from Customer", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                //DataSet myrec = new DataSet();
                da.Fill(myrec);
            }
            catch (Exception e)
            {
                var er = e.Message;
            }
            return myrec;
        }


        //
        // GET: /CustomerLog/Edit/5

        private double GetCelciusTemp(double temp)
        {
          return Math.Round((temp - 32) * 5/ 9) ;
        }

        private double GetFarenhietTemp(double temp)
        {
          return Math.Round((temp * 9 / 5) + 32) ;
           
        }



    }
}
