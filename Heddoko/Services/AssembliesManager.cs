using DAL;
using DAL.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Services
{
    public static class AssembliesManager
    {
        public static List<Assembly> GetAssemblies()
        {
            //TODO - BENB - add cache with expiration and scheduled job which should update that cache information
            UnitOfWork UoW = new UnitOfWork();
           
            List<Assembly> assemblies = new List<Assembly>();

            // Get number of components from DB
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

            int[] brainpackArr = { batteries, bpTop, bpBottom, microSD, sdCover, (lightPipes / 2),
                                    (bpScrews / 6), (bpButtons / 3), powerBoards, dataBoards };

            int[] sensorArr = { capsuleScrew, capsuleTop, capsuleBottom, capsuleButtons, sensors };

            int[] shirtArr = { shirtOctopi, (sensors / 9), auxCables, (overmoldBases / 9) };

            int[] pantsArr = { pantsOctopi, (sensors / 9), auxCables, (overmoldBases / 9) };

            int[] kitsArr = { brainpacks, shirts, pants, usbChargers, usbCables, (sensors / 9) };

            assemblies.Add(new Assembly(AssembliesType.Brainpacks, brainpacks, brainpackArr.Min()));

            assemblies.Add(new Assembly(AssembliesType.Sensors, sensors, sensorArr.Min()));

            assemblies.Add(new Assembly(AssembliesType.Shirts, shirts, shirtArr.Min()));

            assemblies.Add(new Assembly(AssembliesType.Pants, pants, pantsArr.Min()));

            assemblies.Add(new Assembly(AssembliesType.Kits, kits, kitsArr.Min()));

            return assemblies;
        }
    }
}
