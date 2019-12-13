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

        #region Display Form Route
        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            // Get the temp data from the ClearCookie route
            ViewBag.cookie = TempData["cookieMessage"];

            return View();
        }
        #endregion

        #region Display Greeting Route
        [Route("/")]
        [HttpPost]
        public IActionResult Greeting(string name = "World", string language = "Hello")
        {
            // Set cookie count to 0
            int value = 0;

            // Get the cookie dictionary
            var myCookie = Request.Cookies;

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
            }

            string greeting = CreateMessage(name, language, value);
            return Content(greeting, "text/html");
        }
        #endregion

        #region Clear Cookies Route
        [Route("/ClearCookies")]
        public IActionResult ClearCookies()
        {
            // Get all cookies
            var myCookie = Request.Cookies;

            // For loop method to clear all cookies
            foreach (KeyValuePair<string, string> cookie in myCookie)
            {
                Response.Cookies.Delete(cookie.Key);
            }
            // Send temp data to the action route
            TempData["cookieMessage"] = "cookies have been erased";

            // redirect to the action funciton
            return RedirectToAction("Index");
        }
        #endregion

    }
}
