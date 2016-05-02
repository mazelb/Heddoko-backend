using DAL.Models;
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
    [RoutePrefix("admin/api/users")]
    [Auth(Roles = DAL.Constants.Roles.Admin)]
    public class UsersController : BaseAdminController<User, UserAPIModel>
    {
        const string Search = "Search";

        public override KendoResponse<IEnumerable<UserAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<User> items = null;
            int count = 0;
            if (request != null)
            {
                if (request.Filter != null)
                {
                    switch (request.Filter.Filters.Count())
                    {

                        case 1:
                            KendoFilterItem searchFilter = request.Filter.Get(Search);
                            if (searchFilter != null
                            && !string.IsNullOrEmpty(searchFilter.Value))
                            {
                                //items = UoW.UserRepository.Search(searchFilter.Value);
                            }
                            break;
                    }
                }
            }

            if (items == null)
            {
                items = UoW.UserRepository.Admins();
            }

            count = items.Count();

            if (request != null)
            {
                if (request.Take.HasValue)
                {
                    items = items.Skip(request.Skip.Value)
                                 .Take(request.Take.Value);

                }
            }

            var result = items.ToList()
                              .Select(c => Convert(c));

            return new KendoResponse<IEnumerable<UserAPIModel>>()
            {
                Response = result,
                Total = count
            };
        }

        public override KendoResponse<UserAPIModel> Get(int id)
        {
            User item = UoW.UserRepository.Get(id);

            return new KendoResponse<UserAPIModel>()
            {
                Response = Convert(item)
            };
        }

        protected override User Bind(User item, UserAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            return item;
        }

        protected override UserAPIModel Convert(User item)
        {
            if (item == null)
            {
                return null;
            }

            return new UserAPIModel()
            {
                ID = item.ID,
                Name = item.Name
            };
        }
    }
}
