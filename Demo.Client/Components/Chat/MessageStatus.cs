//-----------------------------------------------------------------------
// <copyright file="MessageStatus.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    public enum MessageStatus
    {
        [JsonEnumStringValue("delivered")]
        Delivered,

        [JsonEnumStringValue("sending")]
        Sending,

        [JsonEnumStringValue("seen")]
        Seen,

        [JsonEnumStringValue("failed")]
        Failed,
    }
}
