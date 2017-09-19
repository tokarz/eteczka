using System.Web;
using System.Web.SessionState;
using Eteczka.BE.Utils;

namespace Eteczka.BE.Handlers
{
    public class FileResourcesHandler : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            ////if(context.Session["User"]==null)
            //// context.Response.Redirect("~/login.aspx");

            //var filExtension = GettingExtension(context.Request.RawUrl);
            //context.Response.ClearContent();
            //context.Response.ClearHeaders();
            //context.Response.ContentType = "application/pdf";
            //context.Response.AddHeader("Content-Disposition", "attachment");
            //context.Response.WriteFile(context.Request.RawUrl);
            //context.Response.Flush();


            string fileId = context.Request.Path;
            string pathToFollow = context.Server.MapPath("~/Content/Restricted/" + new PlikiUtils().WezNazwePlikuZeSciezki(fileId));

            context.Response.ContentType = "application/octet-stream";
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment;filename=" + "someFile.pdf");
            context.Response.WriteFile(pathToFollow);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string GettingExtension(string rawUrl)
        {
            return rawUrl.Substring(rawUrl.LastIndexOf(".", System.StringComparison.Ordinal));
        }
    }
}
