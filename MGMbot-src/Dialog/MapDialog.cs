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

            if ((string)stepContext.Values["center1"] == "서울")
            {
                cards.Add(MakeMapCards("강남면허시험장", 37.5083183, 127.0651209, "서울특별시 강남구 대치동 테헤란로114길 23").ToAttachment());
                cards.Add(MakeMapCards("강서면허시험장", 37.5501788, 126.8196349, "서울특별시 강서구 외발산동 남부순환로 171").ToAttachment());
                cards.Add(MakeMapCards("도봉면허시험장", 37.6582111, 127.0565395, "서울특별시 노원구 상계동 동일로 1449\r\n").ToAttachment());
                cards.Add(MakeMapCards("서부면허시험장", 37.5792537, 126.8778281, "서울특별시 마포구 상암동 월드컵로42길 13").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "부산")
            {
                cards.Add(MakeMapCards("부산남부면허시험장", 35.1269225, 129.105866, "부산광역시 남구 용호1동 용호로 16\r\n  ").ToAttachment());
                cards.Add(MakeMapCards("부산북부면허시험장", 35.1775595, 128.981102, "부산광역시 사상구 덕포동 사상로367번길 35").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "경기")
            {
                cards.Add(MakeMapCards("용인면허시험장", 37.2894685, 127.1057513, "경기도 용인시 기흥구 용구대로 2267").ToAttachment());
                cards.Add(MakeMapCards("안산면허시험장", 37.3454366, 126.8263053, "경기도 안산시 단원구 순환로 352 \r\n").ToAttachment());
                cards.Add(MakeMapCards("의정부면허시험장", 37.7593117, 127.0757833, "경기도 의정부시 금오로 109번길 55\r\n  ").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "강원")
            {
                cards.Add(MakeMapCards("춘천면허시험장", 37.9470965, 127.748418, "강원도 춘천시 신북읍 신북로 247").ToAttachment());
                cards.Add(MakeMapCards("강릉면허시험장", 37.7953524, 128.8166345, "강원도 강릉시 사천면 중앙서로 464").ToAttachment());
                cards.Add(MakeMapCards("원주면허시험장", 37.3383034, 127.8962993, "강원도 원주시 호저면 사제로 596").ToAttachment());
                cards.Add(MakeMapCards("태백면허시험장", 37.1809663, 128.9682463, "강원도 태백시 수아밭길 166\r\n").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "전라")
            {
                cards.Add(MakeMapCards("전북면허시험장", 35.8623954, 127.0684835, "전라북도 전주시 덕진구 팔복로 359").ToAttachment());
                cards.Add(MakeMapCards("전남면허시험장", 35.0084324, 126.703068, "전라남도 나주시 내영산2길 49\r\n ").ToAttachment());
                cards.Add(MakeMapCards("광양면허시험장", 34.9613622, 127.5696968, "전라남도 광양시 광양읍 대학로 11").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "충청")
            {
                cards.Add(MakeMapCards("청주면허시험장", 36.5768403, 127.565366, "충청북도 청주시 상당구 가덕면 교육원로 131-20").ToAttachment());
                cards.Add(MakeMapCards("충주면허시험장", 36.9389417, 127.8956336, "충청북도 충주시 달천동 대가주1길 16\r\n     ").ToAttachment());
                cards.Add(MakeMapCards("예산면허시험장", 36.6727236, 126.7858517, "충청남도 예산군 오가면 국사봉로 500\r\n     ").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "경상")
            {
                cards.Add(MakeMapCards("문경면허시험장", 36.6369607, 128.1713829, "경상북도 문경시 점촌4동 신기공단1길 12\r\n      ").ToAttachment());
                cards.Add(MakeMapCards("포항면허시험장", 36.6369607, 129.3907099, "경상북도 포항시 남구 오천읍 냉천로 656\r\n      ").ToAttachment());
                cards.Add(MakeMapCards("마산면허시험장", 35.1245401, 128.4853692, "경상남도 창원시 마산합포구 진동면 진북산업로 90-1").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "인천")
            {
                cards.Add(MakeMapCards("인천면허시험장", 37.3842847, 126.7062256, "인천광역시 남동구 논현고잔동 아암대로 1247").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "대구")
            {
                cards.Add(MakeMapCards("대구면허시험장", 35.9240308, 128.5486152, "대구광역시 북구 태전2동 태암남로 38").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "대전")
            {
                cards.Add(MakeMapCards("대전면허시험장", 36.2861071, 127.4598978, "대전광역시 동구 산서로1660번길 90").ToAttachment());
            }

            else if ((string)stepContext.Values["center1"] == "울산")
            {
                cards.Add(MakeMapCards("울산면허시험장", 35.5758571, 129.0972091, "울산광역시 울주군 상북면 봉화로 342").ToAttachment());
            }

            else //제주
            {
                cards.Add(MakeMapCards("제주면허시험장", 33.4059987, 126.3856305, "제주특별자치도 제주시 애월읍 평화로 2072").ToAttachment());
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
