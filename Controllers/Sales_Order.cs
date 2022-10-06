using C_OrderTaking_Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace C_OrderTaking_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sales_Order : ControllerBase
    {
        General db = new General();
        public string sono;

        //public void getSetNos(string getset)
        //{

        //    if (getset == "Get")
        //    {
        //        Dictionary<string, string> parameters = new Dictionary<string, string>();
        //        parameters.Add("@type", "Get");
        //        parameters.Add("@cmpcode", "CUBIX");
        //        parameters.Add("@Deptno", "ECOM");
        //        parameters.Add("@vrtype", "SO");
        //        DataTable vnos = db.getTableDictionary("[sp_SelectVouchernos]", true, parameters);
        //        if (vnos != null)
        //            sono = vnos.Rows[0]["NEXT NO"].ToString();
        //    }
        //    if (getset == "Set")
        //    {
        //        Dictionary<string, string> parameters = new Dictionary<string, string>();
        //        parameters.Add("@type", "Set");
        //        parameters.Add("@cmpcode", "CUBIX");
        //        parameters.Add("@Deptno", "ECOM");
        //        parameters.Add("@vrtype", "SO");
        //        DataTable vnos = db.getTableDictionary("[sp_SelectVouchernos]", true, parameters);
        //        if (vnos != null)
        //            sono = vnos.Rows[0]["NEXT NO"].ToString();
        //    }
        //}

        [Route("Get")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetDescSearch(string type, string desc)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@Mod", "Previous"); 
            parameters.Add("@Ser", desc);
            parameters.Add("@salesman", "");
            DataTable dt = db.getTableDictionary("[Sp_odrtrkn_Item]", true, parameters);

            // var result = new ObjectResult(dt);
            // var result = new ObjectResult(dt);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            var result = new ObjectResult(json);
            return result;

        }

        [Route("{Salesall}/{desc}")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Getall(string Salesall, string desc)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@Mod", "ALL"); //"Sitem"); 
            parameters.Add("@Ser", "");
            parameters.Add("@salesman", "");
            DataTable dt = db.getTableDictionary("[Sp_odrtrkn_Item]", true, parameters);
            // var result = new ObjectResult(dt);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            var result = new ObjectResult(json);
            return result;

        }

        [Route("{Salesall}/{details}/{No}")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Getdetails(string Salesall, string details, string No)
        {

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@Mod", "Details"); 
            parameters.Add("@Ser", No);
            parameters.Add("@salesman", "");
            DataTable dt = db.getTableDictionary("[Sp_odrtrkn_Item]", true, parameters);
            // var result = new ObjectResult(dt);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            var result = new ObjectResult(json);
            return result;

        }

        [HttpPost]
        public ActionResult Post(Object value)
        {

            DataTable soTable = JsonConvert.DeserializeObject<DataTable>(value.ToString());
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            //DataTable soTable = JsonConvert.DeserializeObject<DataTable>(value.ToString());

            //getSetNos("Set");
           // var c= soTable.Rows[0]["Cust_Acc"].ToString();
            
            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@operation", "Save");
            parameters.Add("@so_no", "");
            parameters.Add("@so_date", DateTime.Now.ToString("MM/dd/yyyy"));
            parameters.Add("@cust_acc", soTable.Rows[0]["Cust_Acc"].ToString());
            parameters.Add("@jv_num", "00");
            parameters.Add("@comments", "");
            parameters.Add("@sale_man", soTable.Rows[0]["sales_man"].ToString());     
            parameters.Add("@inv_no", "0");
            parameters.Add("@so_status", "");
            parameters.Add("@area_code", "");
            parameters.Add("@so_ref", "");
            parameters.Add("@so_doc", "");
            parameters.Add("@fc", "AED");
            parameters.Add("@so_amount", soTable.Rows[0]["Total_Amt"].ToString());
            parameters.Add("@so_fcamt", "0");
            parameters.Add("@so_fcrate", "1");
            parameters.Add("@so_disc", "0");
            parameters.Add("@due_date", DateTime.Now.ToString("MM/dd/yyyy")); //dateTimePicker1.Value.AddDays(textBox7.Text == "" ? 1 : Convert.ToDouble(textBox7.Text)).Date.ToString("MM/dd/yyyy"));
            parameters.Add("@so_fdisc", "0");
            parameters.Add("@accdesc", soTable.Rows[0]["Acc_Descr"].ToString());
            parameters.Add("@PAYMENT", "");
            parameters.Add("@MANUF", "");
            parameters.Add("@origin", "");
            parameters.Add("@shipment", "");
            parameters.Add("@delivery", "");
            parameters.Add("@validity", "");
            parameters.Add("@packing", "");
            parameters.Add("@netwt", "");
            parameters.Add("@grosswt", "");
            parameters.Add("@insurance", "");
            parameters.Add("@custom", "ONLINE");
            parameters.Add("@foot1", "");
            parameters.Add("@frdet", "");
            double rcdcamt = 0;
            double.TryParse("", out rcdcamt);
            parameters.Add("@fruprice", rcdcamt.ToString());
            parameters.Add("@framt", "0");
            parameters.Add("@fob", "Y");
            parameters.Add("@qtn_no", "0");
            parameters.Add("@tel", "");
            double rcdamt = 0;
            double.TryParse("", out rcdamt);
            parameters.Add("@advance", rcdamt.ToString());
            parameters.Add("@inv_date", DateTime.Now.ToString("MM/dd/yyyy"));
            parameters.Add("@inv_total", "0");
            parameters.Add("@footer1", "");
            parameters.Add("@deptno", "HO");
            parameters.Add("@joBcode", "");
            parameters.Add("@S_Ordefrom", "Website");
            parameters.Add("@S_orderStatus", "New");

            if (db.SqlbulkInsert("[sp_Salesorderordertakingdirect]", true, parameters, soTable, "@salesOrderItm"))
                return Ok(new { Result = "Saved" });
            else
                return NotFound(new { Result = "something went wrong" });

        }
       

    }
}
