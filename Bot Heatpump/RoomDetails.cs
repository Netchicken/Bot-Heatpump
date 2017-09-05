using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace Bot_Heatpump
{
    // The RoomDetails class represents the form that you want to complete using information that is collected from the user. 
    // It must be serializable so the bot can be stateless. 
    // The order of fields defines the default sequence in which the user is asked questions.

    // The enumerations define the valid options for each field in RoomDetails, and the order of the values represents the sequence in which they are presented to the user in a conversation.
    //https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-formflow

    //To connect the form to the framework, you must add it to the controller. 
    public enum WallConstruction { o, other, Weatherboards, Brick, Tiltslab }
    public enum Location { o, none, NorthIsland, SouthIsland, StewartIsland }
    public enum Insulation { o, none, Ceiling, Wall, Floor, WindowsDoubleGlazed }
    [Serializable]
    public class RoomDetails
    {

        public Single Length { get; set; }
        public Single Height { get; set; }
        public Single Width { get; set; }
        public Single RoomArea => CalcRoomArea();


        // [Prompt("Please enter your Wall Type Construction")]
        public List<WallConstruction> Walls;

        //  [Prompt("Please enter your Room Insulation")]
        public List<Insulation> RoomInsulation;

        //  [Prompt("Please enter Where you live")]
        public List<Location> HomeLocation;


        private Single CalcRoomArea()
        {
            return Length * Width * Height;
        }

        public string HeatPumpCalc()
        {//really cold
            if (Walls.Contains(WallConstruction.Weatherboards) && CalcRoomArea() > 10 && RoomInsulation.Contains(Insulation.none) && HomeLocation.Contains(Location.StewartIsland))
            {
                return "Get the biggest, you are going to freeze";
            }
            return "Get a small one and put on a jersey you wuss";

        }


        public static IForm<RoomDetails> BuildForm()
        {
            return new FormBuilder<RoomDetails>()
            .Message("Welcome to the simple Heatpump configuration bot!")
                .Message("Please enter room measurements in Meters")
              .Build();
        }

    }
}