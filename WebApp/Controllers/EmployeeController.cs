using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAL.Models;
using BAL.Repository;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    public class EmployeeController : ApiController
    {
        CrudRepo repo = null;
       public EmployeeController()
        {
            repo = new CrudRepo();
        }
        [HttpGet]
        [Route("api/listemp")]
        public IHttpActionResult ListEmp()
        {
            List<EmployeeDetail> emp = new List<EmployeeDetail>();
            emp = repo.listemp();
            return Ok(emp);
        }
        [HttpGet]
        [Route("api/listindex")]
        public IHttpActionResult ListIndex()
        {
            List<Object> indlist = new List<Object>();
            indlist = repo.listindex();
            return Ok(indlist);
        }

        [HttpPost]
        [Route("api/newemployee")]
        public IHttpActionResult NewEmployee(JObject obj)
        {
            var s = JsonConvert.DeserializeObject<List<EmployeeDetail>>(obj["models"].ToString());
            int j = 0;
            for (int i = 0; i < s.Count; i++)
            {
                j = repo.addrecord(s[0]);
            }
            if (j >= 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("api/deleteindex")]
        public IHttpActionResult DeleteIndex(JObject obj)
        {
            var s = JsonConvert.DeserializeObject<List<EmployeeDetail>>(obj["models"].ToString());           
            var result = repo.deleteindex(s[0]);
            if(result >= 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [Route("api/deleterecord")]
        public IHttpActionResult DeleteRecord(JObject obj)
        {
            var s = JsonConvert.DeserializeObject<List<EmployeeDetail>>(obj["models"].ToString());
            var result = repo.deleterecord(s[0]);
            if (result >= 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("api/updaterecord")]
        public IHttpActionResult UpdateRecord(JObject obj)
        {
            var s = JsonConvert.DeserializeObject<List<EmployeeDetail>>(obj["models"].ToString());
            int j = 0;
            for (int i = 0; i < s.Count; i++)
            {
                j = repo.UpdateRecord(s[0]);
            }
            if (j >= 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("api/search")]
        public IHttpActionResult Search(string text)
        {
            List<EmployeeDetail> emp = new List<EmployeeDetail>();
            emp = repo.searchresult(text);
            return Ok(emp);
        }
        [HttpGet]
        [Route("api/bank")]
        public IHttpActionResult GetAvgBalance()
        {
            EmployeeDetail emp = new EmployeeDetail();
            emp = repo.aggrigationresult();
            return Ok(emp.aggresult);
        }
        [HttpGet]
        [Route("api/querystring")]
        public IHttpActionResult QueryString(string txt)
        {
            List<EmployeeDetail> emp = new List<EmployeeDetail>();
            emp = repo.QueryString(txt);
            return Ok(emp);
        }

        [HttpGet]
        [Route("api/helight")]
        public IHttpActionResult QueryHighliht(string txt)
        {
            List<EmployeeDetail> emp = new List<EmployeeDetail>();
            emp = repo.Highlight(txt);
            return Ok(emp);
        }
    }
}
