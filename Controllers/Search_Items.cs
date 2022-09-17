using C_OrderTaking_Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace C_OrderTaking_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Search_Items : ControllerBase
    {
        General db = new General();

        [Route("{type}/{desc}")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetDescSearch(string type, string desc)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@Mod", type); //"Sitem"); 
            parameters.Add("@Ser", desc);
            parameters.Add("@salesman", "");
            DataTable dt = db.getTableDictionary("[Sp_odrtrkn_Item]", true, parameters);
           // var result = new ObjectResult(dt);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            var result = new ObjectResult(json);
            return result;
        
        }
    }
}
