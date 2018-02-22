﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Object_Classes;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller to receive data storage requests from Capture system.
    /// </summary>
    [Route("api/[controller]")]
    public class DataReceivalController : ControllerBase
    {

        #region HTTP Tester methods 
        /* TESTER method to see if controller is working via Postman. */
        // POST a string to: api/datareceival
        [HttpPost]
        public string Post([FromBody]string value)
        {
            return ("Data Receival controller accessed !!! You have submitted the following string via POST request: " + value);
        }

        /* TESTER method to see if controller is accessible via browser. */
        // GET request to: api/datareceival
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "nvidia", "is better", "than", "rasberry pi" };
        }
        #endregion

        /// <summary>
        /// API service that allows a client to store statistics into the database by providing a DataMessage Object.
        /// </summary>
        /// <param name="receivedMessage"></param>
        /// <returns>HTTP Status Code: OK or BAD REQUEST</returns>
        [HttpPost]
        public IActionResult Post([FromBody] DataMessage receivedMessage)
        {
            // Obtain database context.
            StatisticsDatabaseContext context = HttpContext.RequestServices.GetService(typeof(WebAPI.Models.StatisticsDatabaseContext)) as StatisticsDatabaseContext;

            // Store the PerSecondStats objects of the DataMessage to the database and receive a boolean indicating if operation was successful (true) or not (false).
            bool wasPersistSuccesful = context.storeStatsFromMessage(receivedMessage);

            if (wasPersistSuccesful)
            {
                return ( Ok( new JsonResult("Data Message's contents stored succesfully to database.") ) );
            }

            // Else, bad client request (problem with DataMessage object or something else).
            return (BadRequest(new JsonResult("There was a problem storing the data you provided into the database. Please verify your DataMessage object and its contents.")));
        }
    }
}
