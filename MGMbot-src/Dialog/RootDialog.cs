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
using Microsoft.Extensions.Logging;

namespace MGMbot.Dialog
{
    /// <summary>
    /// This is an example root dialog. Replace this with your applications.
    /// </summary>
    public class RootDialog : ComponentDialog
    {
        /// <summary>
        /// QnA Maker initial dialog
        /// </summary>
        private const string InitialDialog = "initial-dialog";

        /// <summary>
        /// Initializes a new instance of the <see cref="RootDialog"/> class.
        /// </summary>
        /// <param name="services">Bot Services.</param>
        public RootDialog(IBotServices services, IConfiguration configuration, MapDialog mapDialog)
            : base("root")
        {
            AddDialog(new QnAMakerBaseDialog(services, configuration));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(mapDialog);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
               IntroStepAsync,
               ActStepAsync,
               FinalStepAsync,
               ReturnStepAsync
            }));

            // AddDialog(new WaterfallDialog(InitialDialog).AddStep(InitialStepAsync));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var card = new HeroCard
            {
                Images = new List<CardImage> { new CardImage("http://drive.google.com/uc?export=view&id=1HrqzgfF6SQTE13NkKn-hLLeRqA389q-_") },
                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, title: "������", value: "������"),
                    new CardAction(ActionTypes.ImBack, title: "�������� ���չο� ����Ʈ", value: "�������� ������Ʈ"),
                    new CardAction(ActionTypes.ImBack, title: "QnA ��� ���", value: "QnA ��� ���")
                },
            };

            var attachments = new List<Attachment>();
            var reply = MessageFactory.Attachment(attachments);
            reply.Attachments.Add(card.ToAttachment());
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            var messageText = "";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);

            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"�ȳ��ϼ���, MGM ê�� �����Դϴ�.\r\n" +
                        $"�������㿡 ���� �ñ��Ͻ� ������ �������ּ���."), cancellationToken);


            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
            //return await stepContext.BeginDialogAsync(nameof(QnAMakerDialog), null, cancellationToken);
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((string)stepContext.Result == $"������")
            {   
                return await stepContext.BeginDialogAsync(nameof(MapDialog), null, cancellationToken);
            }
            else
            {
                return await stepContext.BeginDialogAsync(nameof(QnAMakerDialog), null, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Choices = ChoiceFactory.ToChoices(new List<string> { "ó������" }),
                }, cancellationToken);
        }

        private async Task<DialogTurnResult> ReturnStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.ReplaceDialogAsync(InitialDialogId, null, cancellationToken);
        }
/*
        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            return await stepContext.BeginDialogAsync(nameof(QnAMakerDialog), null, cancellationToken);
        }*/

/*        // get QnA Answer
        private async Task<DialogTurnResult> QnaAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var response = await qnaMaker.GetAnswersAsync(stepContext);

            // use answer found in qnaResults[0].answer
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text(response[0].Answer) }, cancellationToken);
        }*/
    }
}
