using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Data.Sql;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CVHSReunion.Models;
using System.IO;
using System.Text;

namespace CVHSReunion.Controllers
{
    public class AlumniController : ApiController
    {
        private CVHSReunionContext db = new CVHSReunionContext();


        // GET: api/Alumni
        public IHttpActionResult GetUsers()
        {


            var query = (from users in db.Users
                         where users.status != "delete"
                         //select users);
                        select new
                        {
                            users.firstName,
                            users.lastName,
                            users.status
                        }).ToList();
            return Ok(query);
        }

        // GET: api/Alumni/5

        [ResponseType(typeof(Users))]
        public IHttpActionResult GetUsers(int id)
        {
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsers()
        {
            db.Users

              .ToList()
              .ForEach(a => a.status = "deleted");

            db.SaveChanges();


            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Alumni/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsers(int id, Users users)
        {
            db.Users

              .ToList()
              .ForEach(a => a.status = "deleted");

            db.SaveChanges();


            return StatusCode(HttpStatusCode.NoContent);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != users.userId)
            //{
            //    return BadRequest();
            //}

            //db.Entry(users).State = EntityState.Modified;

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UsersExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Alumni
        [ResponseType(typeof(Users))]
        public IHttpActionResult PostUsers(Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            users.status = "unpaid";
            users.helpPlanning = users.helpPlanning.ToLower();
            users.firstName = users.firstName.ToLower();
            users.lastName = users.lastName.ToLower();
            users.emailAddress = users.emailAddress.ToLower();
            History historys = new History
            {
                firstName = users.firstName,
                lastName = users.lastName,
                status = users.status,
                emailAddress = users.emailAddress,
                phoneNumber = users.phoneNumber,
                helpPlanning = users.helpPlanning

            };
            db.Historys.Add(historys);
            var query = from u in db.Users
                        where   u.emailAddress == users.emailAddress &&
                                u.lastName == users.lastName &&
                                u.firstName == users.firstName
                        select u;

            if (query.Any())
            {
                db.SaveChanges();
                return Ok();
            }

            
                ASCIIEncoding encoding=new ASCIIEncoding();
                
                 var postData   =  users.firstName + ", " + users.lastName + ", " + users.emailAddress + ", " + users.status + ", " + users.phoneNumber + ", " + users.helpPlanning;
                byte[] data = encoding.GetBytes(postData);
                HttpWebRequest myRequest =
      (HttpWebRequest)WebRequest.Create("https://prod-10.southcentralus.logic.azure.com:443/workflows/d389f0776d624f4cb67cbac47602d4fd/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=57vadI-kp4j__Ra0oiA7-2Hx71L85hO_KLHLXmVZ8bw");
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;
                Stream newStream = myRequest.GetRequestStream();
                // Send the data.
                newStream.Write(data, 0, data.Length);
                newStream.Close();
            
            db.Users.Add(users);
            
            db.SaveChanges();

            return Ok(db.Entry(users).Entity);
        }
       
        // DELETE: api/Alumni/5
        //[ResponseType(typeof(Users))]
        //public IHttpActionResult DeleteUsers(int id)
        //{
        //    Users users = db.Users.Find(id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Users.Remove(users);
        //    db.SaveChanges();

        //    return Ok(users);
        //}
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersExists(int id)
        {
            return db.Users.Count(e => e.userId == id) > 0;
        }
    }
}