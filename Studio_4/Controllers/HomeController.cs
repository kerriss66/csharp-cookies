using System;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Studio_4.Controllers
{

    public class HomeController : Controller
    {
        // Dicitonary to be used for counting the names
        public static Dictionary<string, int> names = new Dictionary<string, int>();

        #region Methods
        /// <summary>
        /// Create a greeting message to render to the page
        /// </summary>
        /// <param name="name"></param>
        /// <param name="language"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string CreateMessage(string name, string language, int count)
        {
            string greeting = (language.ToLower().Contains("english")) ? "Hello" :
                (language.ToLower().Contains("french")) ? "Bonjour" :
                (language.ToLower().Contains("dutch")) ? "Holla" :
                (language.ToLower().Contains("japanese")) ? "Kon'nichiwa" : "Hello";

            // Display the amount of times this user has visited this site
            string counterMsg = (count > 0) ? $"Welcome Back! You have visited {count} times" : "Welcome! This is your first visit!";

            return $"<p>{counterMsg}</p><h3>{greeting}, {name}</h3>";
        }
        #endregion

        #region LanguageForm
        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            string form = "<form method='post'>" +
                "<input type='text' name='name' placeholder='Enter name' />" +
                    "<select name='language'>" +
                        "<option value='English'>English</option>" +
                        "<option value='French'>French</option>" +
                        "<option value='Dutch'>Dutch</option>" +
                        "<option value='Japanese'>Japanese</option>" +
                    "</select>" +
                "<input type='submit' value='Greet Me!'>" +
                "</form>";

            return Content(form, "text/html");
        }
        #endregion

        #region DisplayGreeting
        [Route("/")]
        [HttpPost]
        public IActionResult Greeting(string name, string language)
        {
            // Set cookie count to 0
            int value = 0;

            // Get the cookie dictionary
            var myCookie = Request.Cookies;

            //TODO for loop method to clear all cookies
            //foreach (KeyValuePair<string, string> cookie in myCookie)
            //{
            //    Response.Cookies.Delete(cookie.Key);
            //}

            if (!myCookie.ContainsKey(name))
            {
                // Set the name and count into the cookie upon first visit
                Response.Cookies.Append(name, "1");
            }
            else
            {
                // Increase the count of the cookie if the cookie exists
                value = int.Parse(Request.Cookies[name]);
                Response.Cookies.Append(name, $"{++value}");
                //Response.Cookies.Delete(name);
            }

            string greeting = CreateMessage(name, language, value);
            return Content(greeting, "text/html");
        }
        #endregion

        //TODO create a button to clear all cookies
    }
}
