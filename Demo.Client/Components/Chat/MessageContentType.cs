//-----------------------------------------------------------------------
// <copyright file="MessageContentType.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    public enum MessageContentType
    {
        [JsonEnumStringValue("unknown")]
        Unknown,

        [JsonEnumStringValue("text")]
        Text,

        [JsonEnumStringValue("html")]
        Html,

        [JsonEnumStringValue("richtext/html")]
        RichTextHtml,
    }
}
