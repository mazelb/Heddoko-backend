using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Services
{
    public static class LicenseManager
    {
        public static void Check()
        {
            try
            {
                UnitOfWork uow = new UnitOfWork();
                uow.LicenseRepository.Check();

                uow.Save();
            }
            catch (Exception ex)
            {
                Trace.TraceError($"LicenseManager.Check.Exception ex: {ex.GetOriginalException()}");
                throw ex;
            }
        }
    }
}
