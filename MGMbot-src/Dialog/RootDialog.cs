// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.AI.QnA.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;

namespace MGMbot.Dialog
{
    /// <summary>
    /// This is the main dialog of MGMbot. (mainly takes care of menu)
    /// </summary>
    public class RootDialog : ComponentDialog
    {
        private const string InitialDialog = "initial-dialog";
        private string MenuChoice = "";

        public RootDialog(IBotServices services, IConfiguration configuration, MapDialog mapDialog, QDialog qDialog)
            : base("root")
        {
            AddDialog(new QnAMakerBaseDialog(services, configuration));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(mapDialog);
            AddDialog(qDialog);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
               IntroStepAsync,
               ActStepAsync,
               FinalStepAsync,
               ReturnStepAsync
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // print following text on chatting box
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"안녕하세요, MGM 챗봇 서비스입니다.\r\n" +
                        $"운전면허에 대해 궁금하신 내용을 질문해주세요."), cancellationToken);

            // HeroCard for menu bar
            var card = new HeroCard
            {
                Images = new List<CardImage> { new CardImage("http://drive.google.com/uc?export=view&id=1HrqzgfF6SQTE13NkKn-hLLeRqA389q-_") },
                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, title: "안전운전 통합민원 사이트", value: "안전운전 웹사이트"),
                    new CardAction(ActionTypes.ImBack, title: "운전면허 시험장 위치", value: "시험장"),
                    new CardAction(ActionTypes.ImBack, title: "QnA", value: "QnA")
                },
            };

            var attachments = new List<Attachment>();
            var reply = MessageFactory.Attachment(attachments);
            reply.Attachments.Add(card.ToAttachment());
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            var messageText = "";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);

            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            MenuChoice = (string)stepContext.Result;

            if ((string)stepContext.Result == $"시험장")
            {   
                return await stepContext.BeginDialogAsync(nameof(MapDialog), null, cancellationToken);
            }
            else if ((string)stepContext.Result == $"안전운전 웹사이트")
            {
                return await stepContext.BeginDialogAsync(nameof(QnAMakerDialog), null, cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"저희 서비스가 주로 제공하는 내용은 다음과 같습니다.\r\n" +
                        $"- 시험 순서 \r\n- 수수료 \r\n- 시험 통과 기준 \r\n- 기타 지식"), cancellationToken);

                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"이런 식으로 질문해보세요!\r\n" +
                        $"- 기능시험 실격 기준이 뭐야? \r\n" +
                        $"- 스쿠터 면허는 어떤 종류야? \r\n" +
                        $"- 학과시험 수수료 얼마야?"), cancellationToken);

                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"메뉴를 다시 보고 싶으시면 \"메뉴\" 라고 입력해주세요."), cancellationToken);

                return await stepContext.BeginDialogAsync(nameof(QDialog), null, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (MenuChoice == "시험장" || MenuChoice == "안전운전 웹사이트")
            {
                return await stepContext.PromptAsync(nameof(ChoicePrompt),
                    new PromptOptions
                    {
                        Choices = ChoiceFactory.ToChoices(new List<string> { "처음으로" }),
                    }, cancellationToken);
            }
            else
                return await stepContext.ContinueDialogAsync();
        }

        private async Task<DialogTurnResult> ReturnStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.ReplaceDialogAsync(InitialDialogId, null, cancellationToken);
        }
    }
}
