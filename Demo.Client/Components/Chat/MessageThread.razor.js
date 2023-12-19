import { createElement, createRoot, useState, MessageThread } from '/communication-react.js';

// React component which contains a MessageThread component.
const messageThreadComponent = (props) => {
    const [messages, setMessages] = useState([]);

    props.messages = messages;
    props.onLoadPreviousChatMessages = (messagesToLoad) => onLoadPreviousChatMessages(messagesToLoad, setMessages, props.eventCallback);

    return createElement(MessageThread, props, null);
};

// JS function called to retrieve previous chat messages.
async function onLoadPreviousChatMessages(messagesToLoad, setMessages, eventCallback) {

    // Call the .NET method "OnLoadPreviousChatMessagesAsync()" method.
    var result = await eventCallback.invokeMethodAsync('OnLoadPreviousChatMessagesAsync', messagesToLoad);

    result.messages.map((message) => {
        // Fix the 'createdOn' datetime format from ISO 8601 to JS Date.
        message.createdOn = new Date(message.createdOn);
    });

    setMessages((previousMessages) => {
        return [...result.messages, ...previousMessages];
    });

    return result.noMoreMessages;
};

// Called by Blazor component to render the component using React.
export function initialize(divElement, args, eventCallback) {
    createRoot(divElement).render(createElement(messageThreadComponent, { ...args, eventCallback }, null));
}
