// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.5.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class QnABot<T> : ActivityHandler where T : Microsoft.Bot.Builder.Dialogs.Dialog
    {
        protected readonly BotState ConversationState;
        protected readonly Microsoft.Bot.Builder.Dialogs.Dialog Dialog;
        protected readonly BotState UserState;

        public QnABot(ConversationState conversationState, UserState userState, T dialog)
        {
            ConversationState = conversationState;
            UserState = userState;
            Dialog = dialog;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occurred during the turn.
            await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken) =>
            // Run the Dialog with the new message Activity.
            await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            // 봇 시작할때 카드 출력하기
            var card = new HeroCard
            {
                Images = new List<CardImage> { new CardImage("http://drive.google.com/uc?export=view&id=1wU1TiDkOX54c_aeYEnOjNAzb0MB6JdoI") },
                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, title: "시험장 장소", value: "시험장 장소"),
                    new CardAction(ActionTypes.ImBack, title: "웹사이트", value: "웹사이트"),
                    new CardAction(ActionTypes.ImBack, title: "QnA", value: "QnA"),
                    new CardAction(ActionTypes.ImBack, title: "QUIZ", value: "QUIZ")
                },
            };

            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"무엇이든지 물어보세요!"), cancellationToken);

                    var attachments = new List<Attachment>();
                    var reply = MessageFactory.Attachment(attachments);
                    reply.Attachments.Add(card.ToAttachment());
                    await turnContext.SendActivityAsync(reply, cancellationToken);
                }
            }
        }
    }
}
