using System.Web;
using System.Web.SessionState;
using Eteczka.BE.Utils;

namespace Eteczka.BE.Handlers
{
    public class FileResourcesHandler : IHttpHandler, IReadOnlySessionState
    {
        private PlikiUtils _PlikiUtils;

        public FileResourcesHandler(PlikiUtils plikiUtils)
        {
            this._PlikiUtils = plikiUtils;
        }

        public void ProcessRequest(HttpContext context)
        {
            string fileId = context.Request.Path;
            string pathToFollow = context.Server.MapPath("~/Content/Restricted/" + this._PlikiUtils.WezNazwePlikuZeSciezki(fileId));

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
