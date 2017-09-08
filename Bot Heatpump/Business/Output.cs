using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bot_Quickstart.Business
{
    public static class Output
    {
        public static string HomeLocation { get; set; }
        public static string WallConstruction { get; set; }
        public static bool DoubleGlazingInsulation { get; set; }

        public static bool CeilingInsulation { get; set; }

        public static bool WallInsulation { get; set; }

        public static bool UnderFloorInsulation { get; set; }

        public static Single Length { get; set; }

        public static Single Height { get; set; }

        public static Single Width { get; set; }


        public static Single CalcRoomArea()
        {
            return Length * Width * Height;
        }
        public enum Wall { other, Weatherboards, Brick, Tiltslab }
        public enum Location { other, Mild, Moderate, Cold }



    }
}