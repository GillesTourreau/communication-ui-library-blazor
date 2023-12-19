//-----------------------------------------------------------------------
// <copyright file="LoadPreviousChatMessagesEvent.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Collections.ObjectModel;

    public class LoadPreviousChatMessagesEvent
    {
        public LoadPreviousChatMessagesEvent(int startIndex, int endIndex)
        {
            this.Messages = new Collection<ChatMessage>();
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
        }

        public Collection<ChatMessage> Messages { get; }

        public int StartIndex { get; }

        public int EndIndex { get; }

        public bool HasMoreMessages { get; set; }
    }
}
