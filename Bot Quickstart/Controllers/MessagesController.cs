using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace Bot_Quickstart
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it. First, the Post method within Controllers\MessagesController.cs receives the message from the user and invokes the root dialog.
        /// 
        /// The root dialog processes the message and generates a response. The MessageReceivedAsync method within Dialogs\RootDialog.cs sends a reply that echos back the user's message, prefixed with the text 'You sent' and ending in the text 'which was ## characters', where ## represents the number of characters in the user's message.
        /// </summary>

        [ResponseType(typeof(void))]
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity != null)
            {
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:         //note the makerootdialog change
                        await Conversation.SendAsync(activity, MakeRootDialog);
                        break;

                    case ActivityTypes.ConversationUpdate:
                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    default:
                        Trace.TraceError($"Unknown activity type ignored: {activity.GetActivityType()}");
                        break;
                }
            }


            if (activity.Type == ActivityTypes.Message)
            {
                //   await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }


        internal static IDialog<RoomDetails> MakeRootDialog()
        {
            //  return Chain.From(() => FormDialog.FromForm(RoomDetails.BuildForm));

            return Chain.From(() => FormDialog.FromForm(RoomDetails.BuildForm))
                .Do(async (context, order) =>
                {
                    try
                    {
                        var completed = await order;
                        //add calcs here
                        var output = completed.HeatPumpCalc();
                        // Post the answer
                        await context.PostAsync("The type of heatpump you need is ... " + output);
                    }
                    catch (FormCanceledException<RoomDetails> e)
                    {
                        string reply;
                        if (e.InnerException == null)
                        {
                            reply = $"You quit on {e.Last} -- maybe you can finish next time!";
                        }
                        else
                        {
                            reply = "Sorry, I've had a short circuit. Please try again.";
                        }
                        await context.PostAsync(reply);
                    }
                });



        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }




    }
}