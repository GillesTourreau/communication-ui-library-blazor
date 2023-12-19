//-----------------------------------------------------------------------
// <copyright file="ChatMessage.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    public class ChatMessage : MessageCommon
    {
        public ChatMessage(string id, DateTime createdOn, MessageContentType contentType)
            : base("chat", id, createdOn)
        {
            this.ContentType = contentType;
        }

        [JsonPropertyName("content")]
        [JsonPropertyOrder(3)]
        public string? Content { get; set; }

        [JsonPropertyName("senderId")]
        [JsonPropertyOrder(6)]
        public string? SenderId { get; set; }

        [JsonPropertyName("senderDisplayName")]
        [JsonPropertyOrder(7)]
        public string? SenderDisplayName { get; set; }

        [JsonPropertyName("status")]
        [JsonPropertyOrder(8)]
        [JsonConverter(typeof(JsonEnumStringValueJsonConverter<MessageStatus>))]
        public MessageStatus? Status { get; set; }

        [JsonPropertyName("mine")]
        [JsonPropertyOrder(11)]
        public bool Mine { get; set; }

        [JsonPropertyName("contentType")]
        [JsonConverter(typeof(JsonEnumStringValueJsonConverter<MessageContentType>))]
        [JsonPropertyOrder(13)]
        public MessageContentType ContentType { get; }
    }
}
