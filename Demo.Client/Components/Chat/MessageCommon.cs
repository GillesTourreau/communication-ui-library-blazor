//-----------------------------------------------------------------------
// <copyright file="MessageCommon.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    public abstract class MessageCommon
    {
        protected MessageCommon(string messageType, string id, DateTime createdOn)
        {
            this.MessageType = messageType;
            this.Id = id;
            this.CreatedOn = createdOn;
        }

        [JsonPropertyName("messageType")]
        [JsonPropertyOrder(0)]
        public string MessageType { get; set; }

        [JsonPropertyName("messageId")]
        [JsonPropertyOrder(1)]
        public string Id { get; }

        [JsonPropertyName("createdOn")]
        [JsonPropertyOrder(2)]
        public DateTime CreatedOn { get; }
    }
}
