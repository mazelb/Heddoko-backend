using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;
using i18n;
using Microsoft.AspNet.Identity;
using Constants = DAL.Constants;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/teams")]
    [AuthAPI(Roles = Constants.Roles.LicenseAdminAndAdmin)]
    public class TeamsController : BaseAdminController<Team, TeamAPIModel>
    {
        private const string Search = "Search";
        private const string IsDeleted = "IsDeleted";
        private const string Used = "Used";

        public TeamsController() { }

        public TeamsController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }

        public override KendoResponse<IEnumerable<TeamAPIModel>> Get([FromUri] KendoRequest request)
        {
            IEnumerable<Team> items = null;
            int count = 0;

            bool forceOrganization = false;
            bool isDeleted = false;
            bool isUsed = false;

            if (UserManager.IsInRole(CurrentUser.Id, Constants.Roles.LicenseAdmin))
            {
                forceOrganization = true;

                if (!CurrentUser.OrganizationID.HasValue)
                {
                    throw new Exception(Resources.WrongObjectAccess);
                }
            }


            if (request?.Filter != null)
            {
                KendoFilterItem isUsedFilter = request.Filter.Get(Used);

                if (isUsedFilter != null)
                {
                    items = UoW.TeamRepository.GetByOrganization(CurrentUser.OrganizationID.Value);
                    isUsed = true;
                }

                if (items == null)
                {
                    KendoFilterItem isDeletedFilter = request.Filter.Get(IsDeleted);
                    if (isDeletedFilter != null)
                    {
                        isDeleted = true;
                    }


                    KendoFilterItem searchFilter = request.Filter.Get(Search);
                    if (!string.IsNullOrEmpty(searchFilter?.Value))
                    {
                        items = UoW.TeamRepository.Search(searchFilter.Value, forceOrganization ? CurrentUser.OrganizationID : null, isDeleted);
                    }

                    if (items == null
                        &&
                        CurrentUser.OrganizationID.HasValue)
                    {
                        items = UoW.TeamRepository.GetByOrganization(CurrentUser.OrganizationID.Value, isDeleted);
                    }
                }
            }

            if (items == null)
            {
                items = UserManager.IsInRole(CurrentUser.Id, Constants.Roles.Admin) ? UoW.TeamRepository.All(null, isDeleted) : UoW.TeamRepository.GetByOrganization(CurrentUser.OrganizationID.Value, isDeleted);
            }

            count = items.Count();

            if (request?.Take != null)
            {
                items = items.Skip(request.Skip.Value)
                             .Take(request.Take.Value);
            }

            List<TeamAPIModel> itemsDefault = new List<TeamAPIModel>();

            if (isUsed)
            {
                itemsDefault.Add(new TeamAPIModel(true)
                {
                    ID = 0
                });
            }

            itemsDefault.AddRange(items.ToList().Select(Convert));


            return new KendoResponse<IEnumerable<TeamAPIModel>>
            {
                Response = itemsDefault,
                Total = count
            };
        }

        public override KendoResponse<TeamAPIModel> Get(int id)
        {
            Team item = UoW.TeamRepository.GetFull(id);

            return new KendoResponse<TeamAPIModel>
            {
                Response = Convert(item)
            };
        }

        public override KendoResponse<TeamAPIModel> Post(TeamAPIModel model)
        {
            TeamAPIModel response;

            if (ModelState.IsValid)
            {
                Team item = new Team();
                item = Bind(item, model);
                if (item.Id == 0)
                {
                    UoW.TeamRepository.Create(item);
                }
                else
                {
                    UoW.Save();
                }

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<TeamAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<TeamAPIModel> Put(TeamAPIModel model)
        {
            TeamAPIModel response = new TeamAPIModel();

            if (!model.ID.HasValue)
            {
                return new KendoResponse<TeamAPIModel>
                {
                    Response = response
                };
            }

            Team item = UoW.TeamRepository.GetFull(model.ID.Value);
            if (item == null)
            {
                return new KendoResponse<TeamAPIModel>
                {
                    Response = response
                };
            }

            if (ModelState.IsValid)
            {
                Bind(item, model);
                UoW.Save();

                response = Convert(item);
            }
            else
            {
                throw new ModelStateException
                {
                    ModelState = ModelState
                };
            }

            return new KendoResponse<TeamAPIModel>
            {
                Response = response
            };
        }

        public override KendoResponse<TeamAPIModel> Delete(int id)
        {
            Team item = UoW.TeamRepository.GetFull(id);

            if (item.Id == CurrentUser.Id)
            {
                return new KendoResponse<TeamAPIModel>
                {
                    Response = Convert(item)
                };
            }

            item.Status = TeamStatusType.Deleted;

            UoW.Save();

            return new KendoResponse<TeamAPIModel>
            {
                Response = Convert(item)
            };
        }


        protected override Team Bind(Team item, TeamAPIModel model)
        {
            if (model == null)
            {
                return null;
            }
            item.Name = model.Name;
            item.Address = model.Address;
            item.Notes = model.Notes;
            item.Status = model.Status;

            if (item.Organization == null
             && CurrentUser.OrganizationID.HasValue)
            {
                item.Organization = UoW.OrganizationRepository.Get(CurrentUser.OrganizationID.Value);
            }

            return item;
        }

        protected override TeamAPIModel Convert(Team item)
        {
            if (item == null)
            {
                return null;
            }

            return new TeamAPIModel
            {
                ID = item.Id,
                Name = item.Name,
                Status = item.Status,
                Organization = item.Organization,
                Address = item.Address,
                IDView = item.IDView,
                Notes = item.Notes
            };
        }
    }
}