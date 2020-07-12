using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PersonsJqueryAPI.Models;
using System.Data.Entity.Validation;



namespace PersonsJqueryAPI.Controllers
{
    public class PersonTableJqueriesController : ApiController
    {

        // Database m.m
        private persteenolsen_com_dbEntities db = new persteenolsen_com_dbEntities();

        
        // GET: api/PersonTableJqueries
        public IQueryable<PersonTableJquery> GetPersonTableJquery()
        {
            return db.PersonTableJquery;
        }

        // GET: api/PersonTableJqueries/5
        [ResponseType(typeof(PersonTableJquery))]
        public IHttpActionResult GetPersonTableJquery(int id)
        {
            PersonTableJquery personTableJquery = db.PersonTableJquery.Find(id);
            if (personTableJquery == null)
            {
                return NotFound();
            }

            return Ok(personTableJquery);
        }


        // BEMÆRK: Alt nedenstående kode herfra bruges ikke i min medsendte GET GUI, men til ADMIN GUI
        // som jeg ikke har sendt med!!!!

        // Denne metode er min validering af brugerens input - skal matche værdierne i modellen
        // Bruges vedr POST og PUT
        private string ValidatePerson(PersonTableJquery personTable)
        {

            var errorS = "";

            errorS += "You need to enter valid data at: \n\n";

            if (string.IsNullOrEmpty(personTable.Navn) || ((personTable.Navn).Length > 20))
                errorS += "Name\n";
            if (string.IsNullOrEmpty(personTable.Efternavn) || ((personTable.Efternavn).Length > 20))
                errorS += "LastName\n";
            if (string.IsNullOrEmpty(personTable.Alder))
                errorS += "Age\n";

            if (!string.IsNullOrEmpty(personTable.Alder))
            {
                try
                {
                    int alder = Int32.Parse(personTable.Alder);
                    if (alder < 18 || alder > 99)
                        errorS += "Age is not correct, need to be between 18 and 99\n";
                }
                catch (Exception)
                {
                    errorS += "Age is not a number\n";
                }
            }


            return errorS;

        }


        // PUT: api/PersonTablesAPI
        public HttpResponseMessage PutPersonTable(int id, PersonTableJquery personTable)
        {
            if (!ModelState.IsValid)
            {

               // Min validerings metode kales
                var errorS = "";
                errorS = ValidatePerson(personTable);


                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorS);


            }

            if (id != personTable.Id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The person does not exits!");
            }

            db.Entry(personTable).State = EntityState.Modified;


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonTableJqueryExists(id))
                {
                   
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "This is no ID in the table!");
                }
                else
                {
                    throw;
                }
            }

            catch (DbEntityValidationException ex)

            {

                // Retrieve the error messages as a list of strings.

                var errorMessages = ex.EntityValidationErrors

                        .SelectMany(x => x.ValidationErrors)

                        .Select(x => x.ErrorMessage);



                // Join the list to a single string.

                var fullErrorMessage = string.Join("; ", errorMessages);



                // Combine the original exception message with the new one.

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);



                // Throw a new DbEntityValidationException with the improved exception message.

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

            }


            catch (Exception)
            {
                // return No
            }

            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Content");
        }


        // POST: api/PersonTablesAPI
        [ResponseType(typeof(PersonTableJquery))]
        public HttpResponseMessage PostPersonTable(PersonTableJquery personTable)
        {
            if (!ModelState.IsValid)
            {
                var errorS = "";
                errorS = ValidatePerson(personTable);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorS);
            }

            db.PersonTableJquery.Add(personTable);
            db.SaveChanges();
            
            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Content");
        }



        // DELETE: api/PersonTableJqueries/5
        [ResponseType(typeof(PersonTableJquery))]
        public IHttpActionResult DeletePersonTableJquery(int id)
        {
            PersonTableJquery personTableJquery = db.PersonTableJquery.Find(id);
            if (personTableJquery == null)
            {
                return NotFound();
            }

            db.PersonTableJquery.Remove(personTableJquery);
            db.SaveChanges();

            return Ok(personTableJquery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonTableJqueryExists(int id)
        {
            return db.PersonTableJquery.Count(e => e.Id == id) > 0;
        }
    }
}