using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Xml.Linq;

namespace WebApplication1
{
    public class FormMiddleware
    {
        RequestDelegate next;
        public FormMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            StringBuilder stringBuilder = new StringBuilder();
            var response = context.Response;
            response.ContentType = "text/html; charset=utf-8";
            if (request.Cookies["Datetime"] != null && request.Cookies["Info"] != null)
            {
                string date = request.Cookies["Datetime"];
                string info = request.Cookies["Info"];
                stringBuilder.Append(date);
                stringBuilder.Append(" " + info);
                if (date == "2022-02-24T12:12")
                {
                    DateTime dateTime1 = new DateTime();
                    response.Cookies.Append("Datetime", DateTime.Now.ToString());
                    stringBuilder.Append(" " + DateTime.Now.ToString());
                    throw new Exception("Error Date!");
                }
            }
            else
            {
                stringBuilder.Append("No Kyki?");
            }
            if (request.Method == "POST")
            {
                response.Cookies.Append("Datetime", request.Form["date"].ToString());
                response.Cookies.Append("Info", request.Form["info"].ToString());
                response.Redirect(" ");
            }
            stringBuilder.Append("<form method='POST'>");
            stringBuilder.Append("<p>" + "<label>Дата</label><br/>" + "<input type=\"datetime-local\"  id=\"date\" name=\"date\" />" + "</p>");
            stringBuilder.Append("<p>" + "<label>Info</label><br/>" + "<input type=\"text\" name=\"info\" />" + "</p>");
            stringBuilder.Append("<p>" + "<input type='submit' value='Send'>" + "</p>");
            stringBuilder.Append("</form>");
            await response.WriteAsync(stringBuilder.ToString());
        }
    }
}
