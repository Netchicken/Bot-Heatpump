using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot_Quickstart.Business;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;

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

        [Prompt("What is the climate where you live? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public List<Output.Location> HomeLocation;

        [Prompt("What is your Room Length in Meters? eg: 4.5 {||}")]
        public Single Length { get; set; }
        [Prompt("What is your Room Height in Meters? eg: 2.5 {||}")]
        public Single Height { get; set; }
        [Prompt("What is your Room Width in Meters? eg 3.5 {||}")]
        public Single Width { get; set; }

        [Prompt("Was your home build after 1990 Y/N {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public bool After1990 { get; set; }

        [Prompt("What are your external walls made from? {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public List<Output.Wall> WallConstruction;

        [Prompt("Do you have Double Glazing? Y/N {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public bool DoubleGlazingInsulation { get; set; }

        [Prompt("Do you have Ceiling Insulation? Y/N {||}")]
        public bool CeilingInsulation { get; set; }

        [Template(TemplateUsage.Bool, "Do you have Exterior Wall Insulation?  Y/N {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public bool ExteriorWallInsulation { get; set; }

        [Template(TemplateUsage.Bool, "Do you have UnderFloor Insulation?  Y/N {||}", ChoiceStyle = ChoiceStyleOptions.Buttons)]
        public bool UnderFloorInsulation { get; set; }




        //[Prompt("Your Room Area is {&}{||}", ChoiceStyle = ChoiceStyleOptions.InlineNoParen, Feedback = FeedbackOptions.Always)]
        //public Single RoomArea2 { get { return CalcRoomArea(); } }

        //public Single RoomArea => CalcRoomArea();
        public static IForm<RoomDetails> BuildForm()
        {
            //http://tomipaananen.azurewebsites.net/?p=1641

            return new FormBuilder<RoomDetails>()
            .Message("Welcome to the simple Heatpump configuration bot!")
                .Field(nameof(HomeLocation))
                .Field(nameof(Length))
                .Field(nameof(Height))
                .Field(nameof(Width))
                .Field(nameof(After1990))
                .Field(new FieldReflector<RoomDetails>(nameof(After1990))
                .SetActive(state => SetFieldActive(After1990, true))
                    .SetNext((value, state) =>
                    {
                        if (state.After1990.Equals(value))
                        {

                            .Build();
                        }
                        else
                        {
                            return new NextStep();
                        }
                    }))


                .Field(nameof(WallConstruction))
                .Field(nameof(DoubleGlazingInsulation))
                .Field(nameof(CeilingInsulation))
                .Field(nameof(ExteriorWallInsulation))
                .Field(nameof(UnderFloorInsulation))
              .Build();
        }

        private static bool SetFieldActive(RoomDetails After1990State, bool isOver1990)
        {
            bool setActive = false;

            if (After1990State.After1990.Equals(isOver1990))
            {
                setActive = true;
            }

            return setActive;
        }


        public string HeatPumpCalc()
        {
            Output.HomeLocation = HomeLocation[0].ToString();
            Output.CeilingInsulation = CeilingInsulation;
            Output.DoubleGlazingInsulation = DoubleGlazingInsulation;
            Output.UnderFloorInsulation = UnderFloorInsulation;
            Output.WallConstruction = WallConstruction[0].ToString();
            Output.ExteriorWallInsulation = ExteriorWallInsulation;

            return Output.Calculation();

        }






    }
}