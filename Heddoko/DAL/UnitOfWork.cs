﻿/**
 * @file UnitOfWork.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using DAL.Repository;
using DAL.Repository.Interface;

namespace DAL
{
    public sealed class UnitOfWork : IDisposable
    {
        private readonly HDContext _db;
        private readonly HDMongoContext _mongodb;

        public UnitOfWork(HDContext context = null, HDMongoContext mongoContext = null)
        {
            _db = context ?? new HDContext();
            _mongodb = mongoContext ?? HDMongoContext.Instance;
        }

        public static UnitOfWork Create()
        {
            return new UnitOfWork();
        }

        public HDContext Context => _db;

        public void Save()
        {
            _db.SaveChanges();
        }

        #region PrivateRepository

        private IUserRepository _userRepository;

        private IAccessTokenRepository _accessTokenRepository;

        private IAssetRepository _assetRepository;

        private IOrganizationRepository _organizationRepository;

        private ILicenseRepository _licenseRepository;

        private IBrainpackRepository _brainpackRepository;

        private IComponentRepository _componentRepository;

        private IDataboardRepository _databoardRepository;

        private IKitRepository _kitRepository;

        private IPantsOctopiRepository _pantsOctopiRepository;

        private IPantsRepository _pantsRepository;

        private IPowerboardRepository _powerboardRepository;

        private ISensorRepository _sensorRepository;

        private ISensorSetRepository _sensorSetRepository;

        private IShirtOctopiRepository _shirtOctopiRepository;

        private IShirtRepository _shirtRepository;

        private IFirmwareRepository _firmwareRepository;

        private IAssemblyCacheRepository _assemblyCacheRepository;

        private ITeamRepository _teamRepository;

        private IStreamConnectionsCacheRepository _streamConnectionsCacheRepository;

        private IProcessedFrameRepository _processedFrameRepository;

        private IApplicationRepository _applicationRepository;

        private IAnalysisFrameRepository _analysisFrameRepository;

        private IRawFrameRepository _rawFrameRepository;

        private IUserActivityRepository _userActivityRepository;

        private IDeviceRepository _deviceRepository;

        private IRecordRepository _recordRepository;

        private IErgoScoreRecordRepository _ergoScoreRecordRepository;

        #endregion

        #region PublicRepository

        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_db));

        public IAccessTokenRepository AccessTokenRepository => _accessTokenRepository ?? (_accessTokenRepository = new AccessTokenRepository(_db));

        public IAssetRepository AssetRepository => _assetRepository ?? (_assetRepository = new AssetRepository(_db));

        public IOrganizationRepository OrganizationRepository => _organizationRepository ?? (_organizationRepository = new OrganizationRepository(_db));

        public ILicenseRepository LicenseRepository => _licenseRepository ?? (_licenseRepository = new LicenseRepository(_db));

        public IBrainpackRepository BrainpackRepository => _brainpackRepository ?? (_brainpackRepository = new BrainpackRepository(_db));

        public IComponentRepository ComponentRepository => _componentRepository ?? (_componentRepository = new ComponentRepository(_db));

        public IDataboardRepository DataboardRepository => _databoardRepository ?? (_databoardRepository = new DataboardRepository(_db));

        public IKitRepository KitRepository => _kitRepository ?? (_kitRepository = new KitRepository(_db));

        public IPantsOctopiRepository PantsOctopiRepository => _pantsOctopiRepository ?? (_pantsOctopiRepository = new PantsOctopiRepository(_db));

        public IPantsRepository PantsRepository => _pantsRepository ?? (_pantsRepository = new PantsRepository(_db));

        public IPowerboardRepository PowerboardRepository => _powerboardRepository ?? (_powerboardRepository = new PowerboardRepository(_db));

        public ISensorRepository SensorRepository => _sensorRepository ?? (_sensorRepository = new SensorRepository(_db));

        public ISensorSetRepository SensorSetRepository => _sensorSetRepository ?? (_sensorSetRepository = new SensorSetRepository(_db));

        public IShirtOctopiRepository ShirtOctopiRepository => _shirtOctopiRepository ?? (_shirtOctopiRepository = new ShirtOctopiRepository(_db));

        public IShirtRepository ShirtRepository => _shirtRepository ?? (_shirtRepository = new ShirtRepository(_db));

        public IFirmwareRepository FirmwareRepository => _firmwareRepository ?? (_firmwareRepository = new FirmwareRepository(_db));

        public IAssemblyCacheRepository AssemblyCacheRepository => _assemblyCacheRepository ?? (_assemblyCacheRepository = new AssemblyCacheRepository());

        public ITeamRepository TeamRepository => _teamRepository ?? (_teamRepository = new TeamRepository(_db));

        public IStreamConnectionsCacheRepository StreamConnectionsCacheRepository => _streamConnectionsCacheRepository ?? (_streamConnectionsCacheRepository = new StreamConnectionsCacheRepository());

        public IProcessedFrameRepository ProcessedFrameRepository => _processedFrameRepository ?? (_processedFrameRepository = new ProcessedFrameRepository(_mongodb));

        public IApplicationRepository ApplicationRepository => _applicationRepository ?? (_applicationRepository = new ApplicationRepository(_db));

        public IAnalysisFrameRepository AnalysisFrameRepository => _analysisFrameRepository ?? (_analysisFrameRepository = new AnalysisFrameRepository(_mongodb));

        public IRawFrameRepository RawFrameRepository => _rawFrameRepository ?? (_rawFrameRepository = new RawFrameRepository(_mongodb));

        public IUserActivityRepository UserActivityRepository => _userActivityRepository ?? (_userActivityRepository = new UserActivityRepository(_mongodb));

        public IDeviceRepository DeviceRepository => _deviceRepository ?? (_deviceRepository = new DeviceRepository(_db));

        public IRecordRepository RecordRepository => _recordRepository ?? (_recordRepository = new RecordRepository(_db));

        public IErgoScoreRecordRepository ErgoScoreRecordRepository => _ergoScoreRecordRepository ?? (_ergoScoreRecordRepository = new ErgoScoreRecordRepository(_mongodb));

        #endregion

        #region IDisposable

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}