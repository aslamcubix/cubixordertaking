using C_OrderTaking_Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Threading.Tasks;

namespace C_OrderTaking_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUser : ControllerBase
    {
        General Con = new General();

        [HttpGet]

        public ActionResult<IEnumerable<string>> Get(string user,string pass)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@mod","Log");
            parameters.Add("@Username", user);
            parameters.Add("@password", pass);
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
