using System.Collections.Generic;

using ModComponentAPI;

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

        internal static bool InsertGear(Radial radial, string gearName)
        {
            switch (radial)
            {
                case Radial.LightSources:
                    return InsertGear(ref InterfaceManager.m_Panel_ActionsRadial.m_LightSourceRadialOrder, gearName);

                case Radial.Weapons:
                    return InsertGear(ref InterfaceManager.m_Panel_ActionsRadial.m_WeaponRadialOrder, gearName);

                case Radial.Navigation:
                    return InsertGear(ref InterfaceManager.m_Panel_ActionsRadial.m_NavigationRadialOrder, gearName);

                case Radial.Campcraft:
                    return InsertGear(ref InterfaceManager.m_Panel_ActionsRadial.m_PlaceItemRadialOrder, gearName);

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