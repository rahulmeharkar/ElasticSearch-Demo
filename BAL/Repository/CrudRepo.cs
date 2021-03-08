using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Elasticsearch.Net;
using BAL.Models;
using System.Data;

namespace BAL.Repository
{
    public class CrudRepo
    {
        public List<EmployeeDetail> listemp()
        {
            List<EmployeeDetail> emp = new List<EmployeeDetail>();


            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("employeedetail");
            var client = new ElasticClient(settings);

            //DataTable datatable = new DataTable();
            //datatable.Columns.Add("emp_id", typeof(Int32));
            //datatable.Columns.Add("emp_name",typeof(string));
            //datatable.Columns.Add("emp_dept",typeof(string));
            //datatable.Columns.Add("emp_mobnum", typeof(string));
            //try
            //{
                var res = client.Search<EmployeeDetail>(s => s.Index("employeedetail"));
            //.Size(10));//.Query(q => q.MatchAll()));
            foreach (var ht in res.Hits)
            {
                EmployeeDetail em = new EmployeeDetail();
                em.emp_id = Convert.ToInt32(ht.Source.emp_id);
                em.emp_name = Convert.ToString(ht.Source.emp_name);
                em.emp_dept = Convert.ToString(ht.Source.emp_dept);
                em.emp_mobnum = Convert.ToString(ht.Source.emp_mobnum);
                em.Id = Convert.ToString(ht.Id);
                emp.Add(em);
            }
            //}
            //catch(Exception ex)
            //{
            //    Console.Write(ex.Message);
            //}
            //Object ob = new Object();       
            //emp = res.Hits.Select(s => s.Source).ToArray();
            return emp;
        }
           

       public List<Object> listindex()
        {
            List<Object> listindex = new List<Object>();
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("employeedetail");
            var client = new ElasticClient(settings);
            var res = client.Cat.Indices();
            foreach(var record in res.Records)
            {
                Object ob = new Object();
                ob = record;
                listindex.Add(ob);
            }
            return listindex;
        }
        public int addrecord(EmployeeDetail emp)
        {
            int i = 0;
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("employeedetail");
            var client = new ElasticClient(settings);
            var id = Guid.NewGuid();
            var res = client.Index(emp, p => p.Index("employeedetail").Id(id.ToString()));
            if(res.IsValid == true)
            {
                i++;
            }
            return i;
        }
        public int deleteindex(EmployeeDetail emp)
        {
            int i = 0;
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            var client = new ElasticClient(settings);
            var res = client.Indices.Delete(emp.index);
            if (res.IsValid == true)
            {
                i++;
            }
            return i;
        }
        public int deleterecord(EmployeeDetail emp)
        {
            int i = 0;
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("employeedetail");
            var client = new ElasticClient(settings);
            var res = client.Delete<dynamic>(emp.Id);
            if (res.IsValid == true)
            {
                i++;
            }
            return i;
        }
        public int UpdateRecord(EmployeeDetail emp)
        {
            int i = 0;
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("employeedetail");
            var client = new ElasticClient(settings);
            var res = client.Index(emp, p => p.Index("employeedetail").Id(emp.Id));
            if (res.IsValid == true)
            {
                i++;
            }
            return i;
        }
        public List<EmployeeDetail> searchresult(string txt)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("employeedetail");
            var client = new ElasticClient(settings);
            var res = client.Search<EmployeeDetail>(s => s.Query(a =>a.Match(c => c.Field("emp_name").Query(txt))));
            List<EmployeeDetail> emp = new List<EmployeeDetail>();
            foreach(var hit in res.Hits)
            {
                EmployeeDetail em =new EmployeeDetail();
                em.emp_name = Convert.ToString(hit.Source.emp_name);
                em.emp_dept = Convert.ToString(hit.Source.emp_dept);
                em.emp_mobnum = Convert.ToString(hit.Source.emp_mobnum);
                em.emp_id = Convert.ToInt32(hit.Source.emp_id);
                emp.Add(em);
            }
            return emp;
        }
        public EmployeeDetail aggrigationresult()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("bank");
            var client = new ElasticClient(settings);
            var res = client.Search<EmployeeDetail>(a => a.Aggregations(p => p.Average("Average Balance",avg => avg.Field("balance"))));
            EmployeeDetail empp = new EmployeeDetail();
            empp.aggresult = res.Aggregations.Values;
            return empp;
        }
        public List<EmployeeDetail> QueryString(string txt)
        {
            
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("employeedetail");
            var client = new ElasticClient(settings);
            var res = client.Search<EmployeeDetail>(s => s.Query(ss => ss.QueryString(sss => sss.Query(txt))));
            List<EmployeeDetail> empp = new List<EmployeeDetail>();
            foreach (var ht in res.Hits)
            {
                EmployeeDetail emp = new EmployeeDetail();
                emp.emp_id = Convert.ToInt32(ht.Source.emp_id);
                emp.emp_name = Convert.ToString(ht.Source.emp_name);
                emp.emp_dept = Convert.ToString(ht.Source.emp_dept);
                emp.emp_mobnum = Convert.ToString(ht.Source.emp_mobnum);
                empp.Add(emp);
            }
            return empp;
        }

        public List<EmployeeDetail> Highlight(string txt)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("employeedetail");
            var client = new ElasticClient(settings);
            var res = client.Search<EmployeeDetail>(s => s.Query(ss => ss.QueryString(sss => sss.Query(txt))).Highlight(a => a.Fields(aa => aa.Field("*").PreTags("<b style='color:red'>").PostTags("</b>"))));
            List<EmployeeDetail> empp = new List<EmployeeDetail>();
            foreach (var ht in res.Hits)
            {
                EmployeeDetail emp = new EmployeeDetail();
                emp.emp_id = Convert.ToInt32(ht.Source.emp_id);
                var rees = ht.Highlight.Values;
                emp.emp_name = rees;
                emp.emp_dept = Convert.ToString(ht.Source.emp_dept);
                emp.emp_mobnum = Convert.ToString(ht.Source.emp_mobnum);
                empp.Add(emp);
            }
            return empp;
        }
    }
}
