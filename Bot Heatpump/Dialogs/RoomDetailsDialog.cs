using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot_Quickstart.Business;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace Bot_Heatpump.Dialogs
{
    // The RoomDetails class represents the form that you want to complete using information that is collected from the user. 
    // It must be serializable so the bot can be stateless. 
    // The order of fields defines the default sequence in which the user is asked questions.

    // The enumerations define the valid options for each field in RoomDetails, and the order of the values represents the sequence in which they are presented to the user in a conversation.
    //https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-formflow

    //To connect the form to the framework, you must add it to the controller. 


    [Serializable]
    public class RoomDetails
    {//https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-formflow-advanced#customize-prompts-using-the-prompt-attribute

        // the {||} element binds the list of choices for that field.

        [Prompt("What is the climate where you live?{&} {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public List<Output.Location> HomeLocation;

        [Prompt("What are your external walls made from? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public List<Output.Wall> WallConstruction;

        [Prompt("Do you have Double Glazing? Y/N {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public bool DoubleGlazingInsulation { get; set; }

        [Prompt("Do you have Ceiling Insulation? Y/N {||}")]
        public bool CeilingInsulation { get; set; }

        [Template(TemplateUsage.Bool, "Do you have Wall Insulation?  Y/N {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public bool WallInsulation { get; set; }

        [Template(TemplateUsage.Bool, "Do you have UnderFloor Insulation?  Y/N {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public bool UnderFloorInsulation { get; set; }

        [Prompt("What is your Room Length in Meters? eg: 4.5 {||}")]
        public Single Length { get; set; }
        [Prompt("What is your Room Height in Meters? eg: 2.5 {||}")]
        public Single Height { get; set; }
        [Prompt("What is your Room Width in Meters? eg 3.5 {||}")]
        public Single Width { get; set; }


        //[Prompt("Your Room Area is {&}{||}", ChoiceStyle = ChoiceStyleOptions.InlineNoParen, Feedback = FeedbackOptions.Always)]
        //public Single RoomArea2 { get { return CalcRoomArea(); } }

        //public Single RoomArea => CalcRoomArea();


        public string HeatPumpCalc()
        {
            Output.HomeLocation = HomeLocation.ToString();
            Output.CeilingInsulation = CeilingInsulation;
            Output.DoubleGlazingInsulation = DoubleGlazingInsulation;
            Output.UnderFloorInsulation = UnderFloorInsulation;
            Output.WallConstruction = WallConstruction.ToString();

            return Output.Calculation();

        }



        public static IForm<RoomDetails> BuildForm()
        {
            return new FormBuilder<RoomDetails>()
            .Message("Welcome to the simple Heatpump configuration bot!")
              .Build();
        }


    }
}