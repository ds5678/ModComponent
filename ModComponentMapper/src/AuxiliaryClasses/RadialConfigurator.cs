using System.Collections.Generic;
using ModComponentAPI;

//did a first pass through; HAS ISSUES, which I think I fixed
//does not need to be declared

namespace ModComponentMapper
{
    public class RadialConfigurator
    {
        private static Dictionary<Radial, List<string>> gears = new Dictionary<Radial, List<string>>();

        public static void RegisterGear(Radial radial, string gearName)
        {
            if (!gears.ContainsKey(radial))
            {
                gears.Add(radial, new List<string>());
            }

            gears[radial].Add(gearName);
        }

        internal static void InsertAllGears()
        {
            foreach (KeyValuePair<Radial, List<string>> eachEntry in gears)
            {
                Radial radial = eachEntry.Key;
                List<string> gearNames = eachEntry.Value;

                foreach (string eachGearName in gearNames)
                {
                    if (!InsertGear(radial, eachGearName))
                    {
                        break;
                    }
                }
            }
        }

        /*Just a test
        private static bool InsertGear(UnhollowerBaseLib.Il2CppStringArray radialOrder, string gearName)
        {
            for (int i = 0; i < radialOrder.Length; i++)
            {
                if (string.IsNullOrEmpty(radialOrder[i]))
                {
                    radialOrder[i] = gearName;
                    return true;
                }
            }

            if (radialOrder.Length < InterfaceManager.m_Panel_ActionsRadial.m_RadialArms.Length)
            {
                UnhollowerBaseLib.Il2CppStringArray newRadialOrder = new UnhollowerBaseLib.Il2CppStringArray(radialOrder.Length + 1);
                radialOrder.CopyTo(newRadialOrder, 0);
                radialOrder = newRadialOrder;
                radialOrder[newRadialOrder.Length - 1] = gearName;
                return true;
            }

            return false;
        }
        //end test*/

        //original method
        //internal static bool InsertGear(Radial radial, string gearName)
        //{
        //    switch (radial)
        //    {
        //        case Radial.LightSources:
        //            return InsertGear(ref InterfaceManager.m_Panel_ActionsRadial.m_LightSourceRadialOrder, gearName); //<======================================

        //        case Radial.Weapons:
        //            return InsertGear(ref InterfaceManager.m_Panel_ActionsRadial.m_WeaponRadialOrder, gearName);

        //        case Radial.Navigation:
        //            return InsertGear(ref InterfaceManager.m_Panel_ActionsRadial.m_NavigationRadialOrder, gearName);

        //        case Radial.Campcraft:
        //            return InsertGear(ref InterfaceManager.m_Panel_ActionsRadial.m_PlaceItemRadialOrder, gearName);

        //        default:
        //            return false;
        //    }
        //}
        internal static bool InsertGear(Radial radial, string gearName)
        {
            switch (radial)
            {
                case Radial.LightSources:
                    for (int i = 0; i < InterfaceManager.m_Panel_ActionsRadial.m_LightSourceRadialOrder.Length; i++)
                    {
                        if (string.IsNullOrEmpty(InterfaceManager.m_Panel_ActionsRadial.m_LightSourceRadialOrder[i]))
                        {
                            InterfaceManager.m_Panel_ActionsRadial.m_LightSourceRadialOrder[i] = gearName;
                            return true;
                        }
                    }

                    if (InterfaceManager.m_Panel_ActionsRadial.m_LightSourceRadialOrder.Length < InterfaceManager.m_Panel_ActionsRadial.m_RadialArms.Length)
                    {
                        UnhollowerBaseLib.Il2CppStringArray newRadialOrder = new UnhollowerBaseLib.Il2CppStringArray(InterfaceManager.m_Panel_ActionsRadial.m_LightSourceRadialOrder.Length + 1);
                        InterfaceManager.m_Panel_ActionsRadial.m_LightSourceRadialOrder.CopyTo(newRadialOrder, 0);
                        InterfaceManager.m_Panel_ActionsRadial.m_LightSourceRadialOrder = newRadialOrder;
                        InterfaceManager.m_Panel_ActionsRadial.m_LightSourceRadialOrder[newRadialOrder.Length - 1] = gearName;
                        return true;
                    }

                    return false;

                case Radial.Weapons:
                    for (int i = 0; i < InterfaceManager.m_Panel_ActionsRadial.m_WeaponRadialOrder.Length; i++)
                    {
                        if (string.IsNullOrEmpty(InterfaceManager.m_Panel_ActionsRadial.m_WeaponRadialOrder[i]))
                        {
                            InterfaceManager.m_Panel_ActionsRadial.m_WeaponRadialOrder[i] = gearName;
                            return true;
                        }
                    }

                    if (InterfaceManager.m_Panel_ActionsRadial.m_WeaponRadialOrder.Length < InterfaceManager.m_Panel_ActionsRadial.m_RadialArms.Length)
                    {
                        UnhollowerBaseLib.Il2CppStringArray newRadialOrder = new UnhollowerBaseLib.Il2CppStringArray(InterfaceManager.m_Panel_ActionsRadial.m_WeaponRadialOrder.Length + 1);
                        InterfaceManager.m_Panel_ActionsRadial.m_WeaponRadialOrder.CopyTo(newRadialOrder, 0);
                        InterfaceManager.m_Panel_ActionsRadial.m_WeaponRadialOrder = newRadialOrder;
                        InterfaceManager.m_Panel_ActionsRadial.m_WeaponRadialOrder[newRadialOrder.Length - 1] = gearName;
                        return true;
                    }

                    return false;

                case Radial.Navigation:
                    for (int i = 0; i < InterfaceManager.m_Panel_ActionsRadial.m_NavigationRadialOrder.Length; i++)
                    {
                        if (string.IsNullOrEmpty(InterfaceManager.m_Panel_ActionsRadial.m_NavigationRadialOrder[i]))
                        {
                            InterfaceManager.m_Panel_ActionsRadial.m_NavigationRadialOrder[i] = gearName;
                            return true;
                        }
                    }

                    if (InterfaceManager.m_Panel_ActionsRadial.m_NavigationRadialOrder.Length < InterfaceManager.m_Panel_ActionsRadial.m_RadialArms.Length)
                    {
                        UnhollowerBaseLib.Il2CppStringArray newRadialOrder = new UnhollowerBaseLib.Il2CppStringArray(InterfaceManager.m_Panel_ActionsRadial.m_NavigationRadialOrder.Length + 1);
                        InterfaceManager.m_Panel_ActionsRadial.m_NavigationRadialOrder.CopyTo(newRadialOrder, 0);
                        InterfaceManager.m_Panel_ActionsRadial.m_NavigationRadialOrder = newRadialOrder;
                        InterfaceManager.m_Panel_ActionsRadial.m_NavigationRadialOrder[newRadialOrder.Length - 1] = gearName;
                        return true;
                    }

                    return false;

                case Radial.Campcraft:
                    for (int i = 0; i < InterfaceManager.m_Panel_ActionsRadial.m_PlaceItemRadialOrder.Length; i++)
                    {
                        if (string.IsNullOrEmpty(InterfaceManager.m_Panel_ActionsRadial.m_PlaceItemRadialOrder[i]))
                        {
                            InterfaceManager.m_Panel_ActionsRadial.m_PlaceItemRadialOrder[i] = gearName;
                            return true;
                        }
                    }

                    if (InterfaceManager.m_Panel_ActionsRadial.m_PlaceItemRadialOrder.Length < InterfaceManager.m_Panel_ActionsRadial.m_RadialArms.Length)
                    {
                        UnhollowerBaseLib.Il2CppStringArray newRadialOrder = new UnhollowerBaseLib.Il2CppStringArray(InterfaceManager.m_Panel_ActionsRadial.m_PlaceItemRadialOrder.Length + 1);
                        InterfaceManager.m_Panel_ActionsRadial.m_PlaceItemRadialOrder.CopyTo(newRadialOrder, 0);
                        InterfaceManager.m_Panel_ActionsRadial.m_PlaceItemRadialOrder = newRadialOrder;
                        InterfaceManager.m_Panel_ActionsRadial.m_PlaceItemRadialOrder[newRadialOrder.Length - 1] = gearName;
                        return true;
                    }

                    return false;

                default:
                    return false;
            }
        }

        private static bool InsertGear(ref string[] radialOrder, string gearName)
        {
            for (int i = 0; i < radialOrder.Length; i++)
            {
                if (string.IsNullOrEmpty(radialOrder[i]))
                {
                    radialOrder[i] = gearName;
                    return true;
                }
            }

            if (radialOrder.Length < InterfaceManager.m_Panel_ActionsRadial.m_RadialArms.Length)
            {
                string[] newRadialOrder = new string[radialOrder.Length + 1];
                radialOrder.CopyTo(newRadialOrder, 0);
                radialOrder = newRadialOrder;
                radialOrder[newRadialOrder.Length - 1] = gearName;
                return true;
            }

            return false;
        }
    }
}