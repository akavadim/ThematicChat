import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as SignalR from '@microsoft/signalr';

const hubConnection = new SignalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

hubConnection.on("OnChatPublished", (chat) => console.log("Chat published: ", chat))
hubConnection.on("OnChatUnpublished", (id) => console.log("Chat unpublished: "+ id))
hubConnection.start()

ReactDOM.render(
    <div>
        <button onClick={publishChat}>Publish chat</button>
        <button onClick={removeChat}>Remove chat</button>
    </div>,
    document.getElementById("root")
)

function publishChat() {
    var chatModel = {
        Title: "Kek",
        ChatType: 1,
        UserAge: 1,
        UserGender: 0,
        SearchableAges: [1, 2],
        SearchableGenders: [1, 2]
    }
    hubConnection.send("PublishChat", chatModel)
}

function removeChat() {
    hubConnection.send("UnpublishChat")
}
