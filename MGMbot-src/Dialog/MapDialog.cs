// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.9.2

using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Bot.AdaptiveCard.Prompt;
using System;
using System.Linq;
using Microsoft.Bot.Builder.Dialogs.Choices;
using AdaptiveCards;
using System.Data.SqlClient;
using System.Text;

namespace MGMbot.Dialog
{
    public class MapDialog : ComponentDialog
    {
        static string AdaptivePromptId = "adaptive";

        public MapDialog(UserState userState) : base(nameof(MapDialog))
        {
            AddDialog(new AdaptiveCardPrompt(AdaptivePromptId));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                Center1StepAsync,
                Center2StepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }


        private async Task<DialogTurnResult> Center1StepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Create the Adaptive Card
            var cardJson = File.ReadAllText("./Cards/center1Card.json");
            var cardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(cardJson),
            };

            // Create the text prompt
            var opts = new PromptOptions
            {
                Prompt = new Activity
                {
                    Attachments = new List<Attachment>() { cardAttachment },
                    Type = ActivityTypes.Message,
                }
            };

            // Display a Text Prompt and wait for input
            return await stepContext.PromptAsync(AdaptivePromptId, opts, cancellationToken);
        }

        private async Task<DialogTurnResult> Center2StepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string json = @$"{stepContext.Result}";
            JObject jobj = JObject.Parse(json);

            stepContext.Values["center1"] = jobj["center1"].ToString();

            List<Attachment> cards = new List<Attachment>();

            if ((string)stepContext.Values["center1"] == "����")
            {
                cards.Add(MakeMapCards("�������������", 37.5083183, 127.0651209, "����Ư���� ������ ��ġ�� �������114�� 23").ToAttachment());
                cards.Add(MakeMapCards("�������������", 37.5501788, 126.8196349, "����Ư���� ������ �ܹ߻굿 ���μ�ȯ�� 171").ToAttachment());
                cards.Add(MakeMapCards("�������������", 37.6582111, 127.0565395, "����Ư���� ����� ��赿 ���Ϸ� 1449\r\n").ToAttachment());
                cards.Add(MakeMapCards("���θ��������", 37.5792537, 126.8778281, "����Ư���� ������ ��ϵ� �����ŷ�42�� 13").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "�λ�")
            {
                cards.Add(MakeMapCards("�λ곲�θ��������", 35.1269225, 129.105866, "�λ걤���� ���� ��ȣ1�� ��ȣ�� 16\r\n  ").ToAttachment());
                cards.Add(MakeMapCards("�λ�Ϻθ��������", 35.1775595, 128.981102, "�λ걤���� ��� ������ ����367���� 35").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "���")
            {
                cards.Add(MakeMapCards("���θ��������", 37.2894685, 127.1057513, "��⵵ ���ν� ���ﱸ �뱸��� 2267").ToAttachment());
                cards.Add(MakeMapCards("�Ȼ���������", 37.3454366, 126.8263053, "��⵵ �Ȼ�� �ܿ��� ��ȯ�� 352 \r\n").ToAttachment());
                cards.Add(MakeMapCards("�����θ��������", 37.7593117, 127.0757833, "��⵵ �����ν� �ݿ��� 109���� 55\r\n  ").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "����")
            {
                cards.Add(MakeMapCards("��õ���������", 37.9470965, 127.748418, "������ ��õ�� �ź��� �źϷ� 247").ToAttachment());
                cards.Add(MakeMapCards("�������������", 37.7953524, 128.8166345, "������ ������ ��õ�� �߾Ӽ��� 464").ToAttachment());
                cards.Add(MakeMapCards("���ָ��������", 37.3383034, 127.8962993, "������ ���ֽ� ȣ���� ������ 596").ToAttachment());
                cards.Add(MakeMapCards("�¹���������", 37.1809663, 128.9682463, "������ �¹�� ���ƹ�� 166\r\n").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "����")
            {
                cards.Add(MakeMapCards("���ϸ��������", 35.8623954, 127.0684835, "����ϵ� ���ֽ� ������ �Ⱥ��� 359").ToAttachment());
                cards.Add(MakeMapCards("�������������", 35.0084324, 126.703068, "���󳲵� ���ֽ� ������2�� 49\r\n ").ToAttachment());
                cards.Add(MakeMapCards("������������", 34.9613622, 127.5696968, "���󳲵� ����� ������ ���з� 11").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "��û")
            {
                cards.Add(MakeMapCards("û�ָ��������", 36.5768403, 127.565366, "��û�ϵ� û�ֽ� ��籸 ������ �������� 131-20").ToAttachment());
                cards.Add(MakeMapCards("���ָ��������", 36.9389417, 127.8956336, "��û�ϵ� ���ֽ� ��õ�� �밡��1�� 16\r\n     ").ToAttachment());
                cards.Add(MakeMapCards("������������", 36.6727236, 126.7858517, "��û���� ���걺 ������ ������� 500\r\n     ").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "���")
            {
                cards.Add(MakeMapCards("������������", 36.6369607, 128.1713829, "���ϵ� ����� ����4�� �ű����1�� 12\r\n      ").ToAttachment());
                cards.Add(MakeMapCards("���׸��������", 36.6369607, 129.3907099, "���ϵ� ���׽� ���� ��õ�� ��õ�� 656\r\n      ").ToAttachment());
                cards.Add(MakeMapCards("������������", 35.1245401, 128.4853692, "��󳲵� â���� ���������� ������ ���ϻ���� 90-1").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "��õ")
            {
                cards.Add(MakeMapCards("��õ���������", 37.3842847, 126.7062256, "��õ������ ������ �������ܵ� �ƾϴ�� 1247").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "�뱸")
            {
                cards.Add(MakeMapCards("�뱸���������", 35.9240308, 128.5486152, "�뱸������ �ϱ� ����2�� �¾ϳ��� 38").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "����")
            {
                cards.Add(MakeMapCards("�������������", 36.2861071, 127.4598978, "���������� ���� �꼭��1660���� 90").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "���")
            {
                cards.Add(MakeMapCards("�����������", 35.5758571, 129.0972091, "��걤���� ���ֱ� ��ϸ� ��ȭ�� 342").ToAttachment());
            }

            else //����
            {
                cards.Add(MakeMapCards("���ָ��������", 33.4059987, 126.3856305, "����Ư����ġ�� ���ֽ� �ֿ��� ��ȭ�� 2072").ToAttachment());
            }

            var reply = MessageFactory.Attachment(cards);
            reply.Attachments = cards;
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            return await stepContext.ContinueDialogAsync();
        }

        private static HeroCard MakeMapCards(string name, double latitude, double longitude, string address)
        {
            HeroCard card = new HeroCard(
                            title: name,
                            images: new CardImage[] { new CardImage() { Url = MakeMap(latitude, longitude) } },
                            tap: new CardAction() { Value = $"http://maps.google.com/maps?q={ latitude},{longitude}", Type = "openUrl", },
                            text: address
                            );

            return card;
        }

        private static String MakeMap(double latitude, double longitude)
        {
            string latitudeStr = latitude.ToString();
            string longitudeStr = longitude.ToString();
            return $"http://maps.google.com/maps/api/staticmap?center={ latitudeStr },{ longitudeStr}&zoom=16&size=512x512&maptype=roadmap&markers=color:red%7C{ latitudeStr },{ longitudeStr }&sensor=false&key=AIzaSyCUGkyf6nzMobitlUprUzDNIqb2GTDj2lk";
        }


    }

}
