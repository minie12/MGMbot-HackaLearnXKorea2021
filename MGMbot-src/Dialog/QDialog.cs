// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.AI.QnA.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Configuration;
using Microsoft.Bot.Builder;


namespace MGMbot.Dialog
{
    /// <summary>
    /// This is an QnA Dialog. Gets prompt from user and find answer from QnA Maker.
    /// </summary>
    public class QDialog : ComponentDialog
    {
        public QDialog(IBotServices services, IConfiguration configuration)
        {
            AddDialog(new QnAMakerBaseDialog(services, configuration));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
               IntroStepAsync,
               AnsStepAsync,
               ReturnStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // gets prompt from user
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("") }, cancellationToken);
        }

        private async Task<DialogTurnResult> AnsStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if ((string)stepContext.Result == "메뉴")
            {
                return await stepContext.ContinueDialogAsync();
            }

            else
                return await stepContext.BeginDialogAsync(nameof(QnAMakerDialog), null, cancellationToken);
        }

        private async Task<DialogTurnResult> ReturnStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if((string)stepContext.Result == "메뉴")
            {
                return await stepContext.ContinueDialogAsync();
            }
            else
            {
                return await stepContext.BeginDialogAsync(nameof(QDialog), null, cancellationToken);
            }
        }
    }
}
