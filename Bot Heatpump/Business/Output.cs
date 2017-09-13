using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace Bot_Quickstart.Business
{
    //https://www.designnavigator.co.nz/CRC.php
    public static class Output
    {
        public static string HomeLocation { get; set; }
        public static string WallConstruction { get; set; }
        public static bool DoubleGlazingInsulation { get; set; }

        public static bool CeilingInsulation { get; set; }

        public static bool ExteriorWallInsulation { get; set; }

        public static bool UnderFloorInsulation { get; set; }

        public static Single Length { get; set; }

        public static Single Height { get; set; }

        public static Single Width { get; set; }

        private static int CalcHomeLocation(string HomeLocation)
        {
            switch (HomeLocation)
            {
                case "Mild":
                    return 0;
                case "Moderate":
                    return 5;
                case "Cold":
                    return 10;
                default:
                    return 0;
            }
        }

        private static int CalcWall(string WallConstruction)
        {
            //
            switch (WallConstruction)
            {
                case "Weatherboards":
                    //Timber-framed wall with brick veneer cladding - R 2.0
                    return 10;
                case "Brick":
                    return 2;
                case "Tiltslab":
                    return 0;
                default:
                    return 0;
            }
        }

        private static Single CalcCeilingInsulation(bool CeilingInsulation)
        {
            return CeilingInsulation ? 0 : 10;
        }
        private static Single CalcDoubleGlazing(bool DoubleGlazingInsulation)
        {
            return DoubleGlazingInsulation ? 0 : 10;
        }
        private static Single CalcExtWallInsulation(bool ExteriorWallInsulation)
        {
            //About 22 percent of heat from an average uninsulated home is lost through the walls. https://www.building.govt.nz/building-code-compliance/h-energy-efficiency/h1-energy-efficiency/building-code-requirements-for-house-insulation/r-values-for-common-construction-types/
            return ExteriorWallInsulation ? 0 : 10;
        }

        private static Single CalcUnderFloorInsulation(bool UnderFloorInsulation)
        {//Around 14 percent of heat is typically lost through the floor.
            return UnderFloorInsulation ? 0 : 10;
        }



        public static Single CalcRoomArea()
        {
            return Length * Width * Height;
        }
        public enum Wall { other, Weatherboards, Brick, Tiltslab }
        public enum Location { other, Mild, Moderate, Cold }

        public static string Calculation()
        {
            Single TotalOfCalc = CalcDoubleGlazing(DoubleGlazingInsulation) + CalcHomeLocation(HomeLocation) +
                                 CalcUnderFloorInsulation(UnderFloorInsulation) +
                                 CalcExtWallInsulation(ExteriorWallInsulation) +
                                 CalcCeilingInsulation(CeilingInsulation);

            if (Output.CalcRoomArea() > 10)
            {
                return "Get the biggest, you are going to freeze";
            }
            return "Get a small one and put on a jersey you wuss";
        }

    }
}