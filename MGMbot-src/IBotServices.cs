// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Bot.Builder.AI.QnA;

namespace MGMbot
{
    public interface IBotServices
    {
        QnAMaker QnAMakerService { get; }
    }
}
