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
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�ȳ��ϼ���, MGM ê�� �����Դϴ�.\r\n" +
                        $"�������㿡 ���� �ñ��Ͻ� ������ �������ּ���."), cancellationToken);

            // HeroCard for menu bar
            var card = new HeroCard
            {
                Images = new List<CardImage> { new CardImage("http://drive.google.com/uc?export=view&id=1HrqzgfF6SQTE13NkKn-hLLeRqA389q-_") },
                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, title: "�������� ���չο� ����Ʈ", value: "�������� ������Ʈ"),
                    new CardAction(ActionTypes.ImBack, title: "�������� ������ ��ġ", value: "������"),
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

            if ((string)stepContext.Result == $"������")
            {   
                return await stepContext.BeginDialogAsync(nameof(MapDialog), null, cancellationToken);
            }
            else if ((string)stepContext.Result == $"�������� ������Ʈ")
            {
                return await stepContext.BeginDialogAsync(nameof(QnAMakerDialog), null, cancellationToken);
            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"���� ���񽺰� �ַ� �����ϴ� ������ ������ �����ϴ�.\r\n" +
                        $"- ���� ���� \r\n- ������ \r\n- ���� ��� ���� \r\n- ��Ÿ ����"), cancellationToken);

                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�̷� ������ �����غ�����!\r\n" +
                        $"- ��ɽ��� �ǰ� ������ ����? \r\n" +
                        $"- ������ ����� � ������? \r\n" +
                        $"- �а����� ������ �󸶾�?"), cancellationToken);

                await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�޴��� �ٽ� ���� �����ø� \"�޴�\" ��� �Է����ּ���."), cancellationToken);

                return await stepContext.BeginDialogAsync(nameof(QDialog), null, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (MenuChoice == "������" || MenuChoice == "�������� ������Ʈ")
            {
                return await stepContext.PromptAsync(nameof(ChoicePrompt),
                    new PromptOptions
                    {
                        Choices = ChoiceFactory.ToChoices(new List<string> { "ó������" }),
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
