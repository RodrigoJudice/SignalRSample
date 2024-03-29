
import ConfigureConnectionMainHub from "./connectionHub.js"

const connetionUserHub = ConfigureConnectionMainHub();
let sendButton = document.getElementById("sendButton");
let notificationInput = document.getElementById("notificationInput");
let messageList = document.getElementById("messageList")
let notificationCounter = document.getElementById("notificationCounter")

sendButton.addEventListener("click", function (event) {
    connetionUserHub.invoke("SendNotification", notificationInput.value);
    notificationInput.value = "";
    event.preventDefault();
});

connetionUserHub.on("ReceiveNotification", function (message, messagesCounter) {

    notificationCounter.innerText = messagesCounter;
    let li = document.createElement("li");
    li.textContent = message;
    messageList.appendChild(li);
   

    
});

connetionUserHub.on("LoadNotifications", function (messages, messagesCounter) {

    notificationCounter.innerText = messagesCounter
    messages.map((message) => {
        let li = document.createElement("li");
        li.textContent = message;
        messageList.appendChild(li);
    }
    );
});


async function NewWindowLoadedMessages() {
    const result = await connetionUserHub.invoke("LoadNotifications");
    try {
        console.log(result);
    } catch (err) {
        console.error(err.toString());
    }
}


//start connection
connetionUserHub.start()
    .then(() => {
        console.log("connected to user count hub");
        NewWindowLoadedMessages();
    })
    .catch((err) => {
        return console.error(err.toString());
    });


