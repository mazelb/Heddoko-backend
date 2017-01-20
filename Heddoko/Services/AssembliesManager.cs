/**
 * @file AssembliesManager.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Models;

namespace Services
{
    public static class AssembliesManager
    {
        private const int BrainpackLightPipes = 2;
        private const int BrainpackScrews = 6;
        private const int BrainpackButtons = 3;
        private const int ShirtSensors = 5;
        private const int PantsSensors = 4;
        private const int KitSensors = ShirtSensors + PantsSensors;

        public static List<Assembly> GetAssemblies(bool force = false)
        {
            UnitOfWork UoW = new UnitOfWork();
            List<Assembly> assemblies = UoW.AssemblyCacheRepository.GetCached();

            if (!force)
            {
                if (assemblies != null)
                {
                    return assemblies;
                }
            }

            int batteries = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.Batteries);
            int bpTop = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.BPTop);
            int bpBottom = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.BPBottom);
            int microSD = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.MicroSD);
            int sdCover = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.SDCover);
            int lightPipes = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.LightPipes);
            int bpScrews = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.BPScrews);
            int bpButtons = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.BPButtons);
            int capsuleScrew = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.CapsuleScrews);
            int capsuleTop = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.CapsuleTop);
            int capsuleBottom = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.CapsuleBottom);
            int capsuleButtons = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.CapsuleButtons);
            int auxCables = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.AuxCables);
            int overmoldBases = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.OverMoldBases);
            int usbChargers = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.USBChargers);
            int usbCables = UoW.ComponentRepository.GetQuantityReadyOfComponent(ComponentsType.USBCables);


            int powerBoards = UoW.PowerboardRepository.GetNumReady();
            int dataBoards = UoW.DataboardRepository.GetNumReady();
            int sensors = UoW.SensorRepository.GetNumReady();
            int shirtOctopi = UoW.ShirtOctopiRepository.GetNumReady();
            int pantsOctopi = UoW.PantsOctopiRepository.GetNumReady();
            int brainpacks = UoW.BrainpackRepository.GetNumReady();
            int shirts = UoW.ShirtRepository.GetNumReady();
            int pants = UoW.PantsRepository.GetNumReady();
            int kits = UoW.KitRepository.GetNumReady();

            int[] brainpackArr =
            {
                batteries,
                bpTop,
                bpBottom,
                microSD,
                sdCover,
                lightPipes / BrainpackLightPipes,
                bpScrews / BrainpackScrews,
                bpButtons / BrainpackButtons,
                powerBoards,
                dataBoards
            };

            int[] sensorArr =
            {
                capsuleScrew,
                capsuleTop,
                capsuleBottom,
                capsuleButtons,
                sensors
            };

            int[] shirtArr =
            {
                shirtOctopi,
                auxCables,
                overmoldBases / ShirtSensors
            };

            int[] pantsArr =
            {
                pantsOctopi,
                auxCables,
                overmoldBases / PantsSensors
            };

            int[] kitsArr =
            {
                brainpacks,
                shirts,
                pants,
                usbChargers,
                usbCables,
                sensors / KitSensors
            };

            assemblies = new List<Assembly>
            {
                new Assembly(AssembliesType.Brainpacks, brainpacks, brainpackArr.Min()),
                new Assembly(AssembliesType.Sensors, sensors, sensorArr.Min()),
                new Assembly(AssembliesType.Shirts, shirts, shirtArr.Min()),
                new Assembly(AssembliesType.Pants, pants, pantsArr.Min()),
                new Assembly(AssembliesType.Kits, kits, kitsArr.Min())
            };

            UoW.AssemblyCacheRepository.SetCache(assemblies);

            return assemblies;
        }
    }
}