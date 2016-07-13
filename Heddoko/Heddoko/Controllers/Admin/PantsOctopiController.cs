using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/pantsoctopi")]
    [AuthAPI(Roles =DAL.Constants.Roles.Admin)]

    public abstract class PantsOctopiController : BaseAdminController<PantsOctopi, PantsOctopiAPIModel>
    {
        const string Search = "Search";
        
        public override KendoResponse<IEnumerable<PantsOctopiAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<PantsOctopi> items = null;
            int count = 0;

            items = new List<PantsOctopi>();

            count = items.Count();

            if (request != null && request.Take.HasValue)
            {
                items = items.Skip(request.Skip.Value).Take(request.Take.Value);
            }

            List<PantsOctopiAPIModel> itemsDefault = new List<PantsOctopiAPIModel>();

            itemsDefault.AddRange(items.ToList().Select(c => Convert(c)));

            return new KendoResponse<IEnumerable<PantsOctopiAPIModel>>()
            {
                Response = itemsDefault,
                Total = count
            };
        }
    }
}