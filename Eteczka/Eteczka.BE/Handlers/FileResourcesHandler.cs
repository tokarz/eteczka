using System.Web;

namespace Eteczka.BE.Handlers
{
    public class FileResourcesHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string fileId = context.Request.Path.Substring("FILE_FETCH?src=".Length);
            context.Response.ContentType = "application/pdf";
            context.Response.WriteFile(context.Server.MapPath("~/Content/test/" + fileId));
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
