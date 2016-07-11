using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IDisposable
    {
        private HDContext db { get; set; }

        public UnitOfWork(HDContext context = null)
        {
            db = context ?? new HDContext();
        }

        #region PrivateRepository
        private IUserRepository userRepository { get; set; }

        private IGroupRepository groupRepository { get; set; }

        private IAccessTokenRepository accessTokenRepository { get; set; }

        private IAssetRepository assetRepository { get; set; }

        private IFolderRepository folderRepository { get; set; }

        private IMovementEventRepository movementEventRepository { get; set; }

        private IMovementFrameRepository movementFrameRepository { get; set; }

        private IMovementMarkerRepository movementMarkerRepository { get; set; }

        private IMovementRepository movementRepository { get; set; }

        private IProfileRepository profileRepository { get; set; }

        private IScreeningRepository screeningRepository { get; set; }

        private ITagRepository tagRepository { get; set; }

        private IOrganizationRepository organizationRepository { get; set; }

        private ILicenseRepository licenseRepository { get; set; }

        private IBrainpackRepository brainpackRepository { get; set; }

        private IComponentRepository componentRepository { get; set; }

        private IDataboardRepository databoardRepository { get; set; }

        private IKitRepository kitRepository { get; set; }

        private IPantsOctopiRepository pantsOctopiRepository { get; set; }

        private IPantsRepository pantsRepository { get; set; }

        private IPowerboardRepository powerboardRepository { get; set; }

        private ISensorRepository sensorRepository { get; set; }

        private ISensorSetRepository sensorSetRepository { get; set; }

        private IShirtOctopiRepository shirtOctopiRepository { get; set; }

        private IShirtRepository shirtRepository { get; set; }
        #endregion

        #region PublicRepository
        public IUserRepository UserRepository
        {
            get
            {
                return userRepository ?? (userRepository = new UserRepository(db));
            }
        }

        public IGroupRepository GroupRepository
        {
            get
            {
                return groupRepository ?? (groupRepository = new GroupRepository(db));
            }
        }

        public IAccessTokenRepository AccessTokenRepository
        {
            get
            {
                return accessTokenRepository ?? (accessTokenRepository = new AccessTokenRepository(db));
            }
        }

        public IAssetRepository AssetRepository
        {
            get
            {
                return assetRepository ?? (assetRepository = new AssetRepository(db));
            }
        }

        public IFolderRepository FolderRepository
        {
            get
            {
                return folderRepository ?? (folderRepository = new FolderRepository(db));
            }
        }

        public IMovementEventRepository MovementEventRepository
        {
            get
            {
                return movementEventRepository ?? (movementEventRepository = new MovementEventRepository(db));
            }
        }

        public IMovementFrameRepository MovementFrameRepository
        {
            get
            {
                return movementFrameRepository ?? (movementFrameRepository = new MovementFrameRepository(db));
            }
        }

        public IMovementMarkerRepository MovementMarkerRepository
        {
            get
            {
                return movementMarkerRepository ?? (movementMarkerRepository = new MovementMarkerRepository(db));
            }
        }

        public IMovementRepository MovementRepository
        {
            get
            {
                return movementRepository ?? (movementRepository = new MovementRepository(db));
            }
        }

        public IProfileRepository ProfileRepository
        {
            get
            {
                return profileRepository ?? (profileRepository = new ProfileRepository(db));
            }
        }

        public IScreeningRepository ScreeningRepository
        {
            get
            {
                return screeningRepository ?? (screeningRepository = new ScreeningRepository(db));
            }
        }

        public ITagRepository TagRepository
        {
            get
            {
                return tagRepository ?? (tagRepository = new TagRepository(db));
            }
        }

        public IOrganizationRepository OrganizationRepository
        {
            get
            {
                return organizationRepository ?? (organizationRepository = new OrganizationRepository(db));
            }
        }

        public ILicenseRepository LicenseRepository
        {
            get
            {
                return licenseRepository ?? (licenseRepository = new LicenseRepository(db));
            }
        }

        public IBrainpackRepository BrainpackRepository
        {
            get
            {
                return brainpackRepository ?? (brainpackRepository = new BrainpackRepository(db));
            }
        }

        public IComponentRepository ComponentRepository
        {
            get
            {
                return componentRepository ?? (componentRepository = new ComponentRepository(db));
            }
        }

        public IDataboardRepository DataboardRepository
        {
            get
            {
                return databoardRepository ?? (databoardRepository = new DataboardRepository(db));
            }
        }

        public IKitRepository KitRepository
        {
            get
            {
                return kitRepository ?? (kitRepository = new KitRepository(db));
            }
        }

        public IPantsOctopiRepository PantsOctopiRepository
        {
            get
            {
                return pantsOctopiRepository ?? (pantsOctopiRepository = new PantsOctopiRepository(db));
            }
        }

        public IPantsRepository PantsRepository
        {
            get
            {
                return pantsRepository ?? (pantsRepository = new PantsRepository(db));
            }
        }

        public IPowerboardRepository PowerboardRepository
        {
            get
            {
                return powerboardRepository ?? (powerboardRepository = new PowerboardRepository(db));
            }
        }

        public ISensorRepository SensorRepository
        {
            get
            {
                return sensorRepository ?? (sensorRepository = new SensorRepository(db));
            }
        }

        public ISensorSetRepository SensorSetRepository
        {
            get
            {
                return sensorSetRepository ?? (sensorSetRepository = new SensorSetRepository(db));
            }
        }

        public IShirtOctopiRepository ShirtOctopiRepository
        {
            get
            {
                return shirtOctopiRepository ?? (shirtOctopiRepository = new ShirtOctopiRepository(db));
            }
        }

        public IShirtRepository ShirtRepository
        {
            get
            {
                return shirtRepository ?? (shirtRepository = new ShirtRepository(db));
            }
        }
        #endregion

        public void Save()
        {
            db.SaveChanges();
        }

        #region IDisposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
