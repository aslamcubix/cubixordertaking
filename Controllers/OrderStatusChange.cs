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
    public class OrderStatusChange : ControllerBase
    {
        General Con = new General();

        [HttpPost]

        public ActionResult post(Object value)
        {
            DataTable  items = JsonConvert.DeserializeObject<DataTable>(value.ToString());
            if (items != null)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("@mod", "Status");
                parameters.Add("@Username", "");
                parameters.Add("@password", "");
                parameters.Add("@So_no", items.Rows[0]["So_no"].ToString());
                parameters.Add("@ChngUsr", items.Rows[0]["changeuser"].ToString());
                parameters.Add("@statusto", items.Rows[0]["statusto"].ToString());

                Con.InsertUpdateDeleteDictionary("[Sp_odrtrkn_User]", true, parameters);
            }
            return Ok(new { Result = "Status Updated" });
        }

        [Route("{OrderStatus}")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Getstatus()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@mod", "StatusDrop");
            parameters.Add("@Username", "");
            parameters.Add("@password", "");
            parameters.Add("@So_no", "");
            parameters.Add("@ChngUsr", "");
            parameters.Add("@statusto", "");
            DataTable dt = Con.getTableDictionary("[Sp_odrtrkn_User]", true, parameters);
            // var result = new ObjectResult(dt); 

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            var result = new ObjectResult(json);
            return result;

        }


    }
}
