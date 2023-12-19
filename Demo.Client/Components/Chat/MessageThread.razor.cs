//-----------------------------------------------------------------------
// <copyright file="MessageThread.razor.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    public partial class MessageThread
    {
        private ElementReference messageThreadContainer;

        private JsInterop jsInterop;

        public MessageThread()
        {
            this.jsInterop = new JsInterop(this);

            this.NumberOfChatMessagesToReload = 5;
        }

        [Parameter]
        public string? UserId { get; set; }

        [Parameter]
        public EventCallback<LoadPreviousChatMessagesEvent> OnLoadPreviousChatMessages { get; set; }

        [Parameter]
        public int NumberOfChatMessagesToReload { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await this.jsInterop.InitializeAsync();
            }
        }

        private sealed class JsInterop
        {
            private readonly MessageThread owner;

            private IJSObjectReference? module;

            private DotNetObjectReference<JsInterop>? callbackReference;

            private int startIndex;

            public JsInterop(MessageThread owner)
            {
                this.owner = owner;
                this.callbackReference = DotNetObjectReference.Create(this);
            }

            [JSInvokable]
            public async Task<LoadPreviousChatMessagesResult> OnLoadPreviousChatMessagesAsync(int messagesToLoad)
            {
                return await this.LoadPreviousChatMessagesAsync(messagesToLoad);
            }

            public async Task InitializeAsync()
            {
                if (this.owner.UserId is null)
                {
                    throw new InvalidOperationException($"No '{nameof(this.owner.UserId)}' has been specified on the '{nameof(MessageThread)}' component.");
                }

                this.module = await this.owner.JSRuntime.InvokeAsync<IJSObjectReference>(
                    "import",
                    "./Components/Chat/MessageThread.razor.js");

                var props = new
                {
                    userId = this.owner.UserId,
                    numberOfChatMessagesToReload = this.owner.NumberOfChatMessagesToReload,
                };

                await this.module.InvokeVoidAsync("initialize", this.owner.messageThreadContainer, props, this.callbackReference);
            }

            private async Task<LoadPreviousChatMessagesResult> LoadPreviousChatMessagesAsync(int messagesToLoad)
            {
                var @event = new LoadPreviousChatMessagesEvent(this.startIndex, this.startIndex + messagesToLoad);

                await this.owner.OnLoadPreviousChatMessages.InvokeAsync(@event);

                this.startIndex += messagesToLoad;

                return new LoadPreviousChatMessagesResult(@event.Messages, @event.HasMoreMessages);
            }

            internal sealed class LoadPreviousChatMessagesResult
            {
                public LoadPreviousChatMessagesResult(IReadOnlyList<ChatMessage> messages, bool noMoreMessages)
                {
                    this.Messages = messages;
                    this.NoMoreMessages = noMoreMessages;
                }

                [JsonPropertyName("messages")]
                public IReadOnlyList<ChatMessage> Messages { get; }

                [JsonPropertyName("noMoreMessages")]
                public bool NoMoreMessages { get; }
            }
        }
    }
}
