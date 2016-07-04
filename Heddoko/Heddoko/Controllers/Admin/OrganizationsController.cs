using DAL;
using DAL.Models;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/organizations")]
    [AuthAPI(Roles = DAL.Constants.Roles.Admin)]
    public class OrganizationsController : BaseAdminController<Organization, OrganizationAPIModel>
    {
        const string Search = "Search";

        public override KendoResponse<IEnumerable<OrganizationAPIModel>> Get([FromUri]KendoRequest request)
        {
            IEnumerable<Organization> items = null;
            int count = 0;
            if (request != null)
            {
                if (request.Filter != null)
                {
                    switch (request.Filter.Filters.Count())
                    {
                        case 0:
                            if (items == null)
                            {
                                items = UoW.OrganizationRepository.All();
                            }
                            break;
                        case 1:
                            foreach (KendoFilterItem filter in request.Filter.Filters)
                            {
                                switch (filter.Field)
                                {
                                    case Search:
                                        if (!string.IsNullOrEmpty(filter.Value))
                                        {
                                            items = UoW.OrganizationRepository.Search(filter.Value);
                                        }

                                        if (items == null)
                                        {
                                            items = UoW.OrganizationRepository.All();
                                        }

                                        break;
                                }
                            }
                            break;
                    }
                }
                else
                {
                    if (items == null)
                    {
                        items = UoW.OrganizationRepository.All();
                    }
                }
            }

            if (items == null)
            {
                items = new List<Organization>();
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

            return new KendoResponse<IEnumerable<OrganizationAPIModel>>()
            {
                Response = result,
                Total = count
            };
        }

        public override KendoResponse<OrganizationAPIModel> Get(int id)
        {
            Organization item = UoW.OrganizationRepository.Get(id);

            return new KendoResponse<OrganizationAPIModel>()
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<OrganizationAPIModel> Post(OrganizationAPIModel model)
        {
            OrganizationAPIModel response = new OrganizationAPIModel();

            if (ModelState.IsValid)
            {
                Organization item = new Organization();
                Bind(item, model);
                UoW.OrganizationRepository.Create(item);

                Task.Run(() => Mailer.SendInviteAdminEmail(item));

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException()
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<OrganizationAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<OrganizationAPIModel> Put(OrganizationAPIModel model)
        {
            OrganizationAPIModel response = new OrganizationAPIModel();

            if (model.ID.HasValue)
            {
                Organization item = UoW.OrganizationRepository.GetFull(model.ID.Value);
                if (item != null)
                {
                    if (ModelState.IsValid)
                    {

                        Bind(item, model);
                        UoW.Save();

                        response = Convert(item);
                    }
                    else
                    {
                        throw new ModelStateException()
                        {
                            ModelState = ModelState
                        };
                    }
                }
            }

            return new KendoResponse<OrganizationAPIModel>()
            {
                Response = response
            };
        }

        public override KendoResponse<OrganizationAPIModel> Delete(int id)
        {
            Organization item = UoW.OrganizationRepository.GetFull(id);
            item.Status = OrganizationStatusType.Deleted;

            List<User> users = UoW.UserRepository.GetByOrganization(item.ID).ToList();

            foreach (User user in users)
            {
                user.Status = UserStatusType.Deleted;
                user.License = null;
                UoW.UserRepository.ClearCache(user);
            }

            UoW.Save();

            return new KendoResponse<OrganizationAPIModel>()
            {
                Response = Convert(item)
            };
        }

        protected override Organization Bind(Organization item, OrganizationAPIModel model)
        {
            if (model == null)
            {
                return null;
            }

            User user = null;

            if (model.User != null
             && item.User == null)
            {
                if (model.User.ID.HasValue)
                {
                    user = UoW.UserRepository.Get(model.User.ID.Value);
                }
                else
                {
                    user = UoW.UserRepository.GetByEmail(model.User.Email?.Trim());

                    if (user == null)
                    {
                        user = UoW.UserRepository.GetByUsername(model.User.Username?.ToLower().Trim());
                    }

                    if (user != null
                    && user.OrganizationID != null)
                    {
                        throw new Exception(i18n.Resources.UserAlreadyInOrganizations);
                    }

                    if (user == null)
                    {
                        user = new User();
                        user.FirstName = model.User.Firstname?.Trim();
                        user.LastName = model.User.Lastname?.Trim();
                        user.Email = model.User.Email?.Trim();
                        user.Username = model.User.Username?.ToLower().Trim();
                        user.Status = UserStatusType.Invited;
                        user.Role = UserRoleType.LicenseAdmin;
                        user.Phone = model.Phone;
                        user.InviteToken = PasswordHasher.Md5(DateTime.Now.Ticks.ToString());
                        UoW.UserRepository.Create(user);
                    }
                    else
                    {
                        user.Role = UserRoleType.LicenseAdmin;
                        user.Status = UserStatusType.Active;
                        UoW.UserRepository.SetCache(user);
                    }
                }

                item.User = user;
                user.Organization = item;
            }

            item.Name = model.Name;
            item.Phone = model.Phone;
            item.Address = model.Address;
            item.Notes = model.Notes;

            return item;
        }

        protected override OrganizationAPIModel Convert(Organization item)
        {
            if (item == null)
            {
                return null;
            }

            return new OrganizationAPIModel()
            {
                ID = item.ID,
                Name = item.Name,
                Phone = item.Phone,
                Address = item.Address,
                UserID = item.UserID,
                Notes = item.Notes,
                User = new UserAPIModel()
                {
                    Email = item.User.Email,
                    Name = item.User.Name,
                    Username = item.User.Username
                },
                DataAnalysisAmount = item.Licenses?.Where(c => c.Type == LicenseType.DataAnalysis
                                                            && c.ExpirationAt >= DateTime.Now
                                                            && c.Status == LicenseStatusType.Active).Sum(c => c.Amount),
                DataCollectorAmount = item.Licenses?.Where(c => c.Type == LicenseType.DataCollection
                                                             && c.ExpirationAt >= DateTime.Now
                                                             && c.Status == LicenseStatusType.Active).Sum(c => c.Amount)
            };
        }
    }
}
