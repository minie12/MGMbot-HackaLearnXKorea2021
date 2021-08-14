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

namespace MGMbot
{
    public class QnABot<T> : ActivityHandler where T : Microsoft.Bot.Builder.Dialogs.Dialog
    {

        // 봇 시작할때 카드 출력하기
        HeroCard card = new HeroCard
        {
            Images = new List<CardImage> { new CardImage("http://drive.google.com/uc?export=view&id=1HrqzgfF6SQTE13NkKn-hLLeRqA389q-_") },
            Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, title: "시험장", value: "시험장"),
                    new CardAction(ActionTypes.ImBack, title: "안전운전 통합민원 사이트", value: "안전운전 웹사이트"),
                    new CardAction(ActionTypes.ImBack, title: "QnA", value: "QnA")
                },
        };

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

        // d예아`~~!!@!@
        protected async Task printMenu(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var attachments = new List<Attachment>();
            var reply = MessageFactory.Attachment(attachments);
            reply.Attachments.Add(card.ToAttachment());
            await turnContext.SendActivityAsync(reply, cancellationToken);

            await turnContext.SendActivityAsync(MessageFactory.Text($"메뉴를 다시 보고 싶으시면\r\n" +
                $"메뉴 라고 말씀해주세요."), cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = $"{turnContext.Activity.Text}";
            if (replyText == $"메뉴")
            {
                await printMenu(turnContext, cancellationToken);
            }
            else
            {
                // Run the Dialog with the new message Activity.
                await Dialog.RunAsync(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
            }
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            // 봇 시작할때 카드 출력하기
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"안녕하세요, MGM 챗봇 서비스입니다.\r\n" +
                        $"운전면허에 대해 궁금하신 내용을 질문해주세요."), cancellationToken);
                    //await printMenu(turnContext, cancellationToken);
                }
            }
        }
    }
}
