import ConfigureConnectionMainHub from "./connectionHub.js"

const connetionMainHub = ConfigureConnectionMainHub();



    let sendMessage = document.getElementById("sendMessage");
    let messagesList = document.getElementById("messagesList");
    sendMessage.disable = true;

    connetionMainHub.on("MessageReceived", (user, message) => {
        let li = document.createElement("li");
        messagesList.appendChild(li);
        li.textContent = `${user} - ${message}`;
    }
    );

    sendMessage.addEventListener("click",  (event) => {

        const sender = document.getElementById("senderEmail").value;
        const receiver = document.getElementById("receiverEmail").value;
        const message = document.getElementById("chatMessage").value;


        if (receiver.length > 0)  {
            connetionMainHub.send("SendMessageToReceiver", sender, receiver, message)
                .catch((err) => console.error("SendMessageToReceiver", err.toString()));
        }
        else {
            connetionMainHub.send("SendMessageToAll", sender, message)
                .catch((err) => console.error("SendMessageToAll", err.toString()));
        }
        document.getElementById("chatMessage").value = "";
        event.preventDefault();

       

    });



//start connection
connetionMainHub.start()
    .then(() => {
        console.log("connected to hub");
        sendMessage.disable = false;
    })
    .catch((err) => {
        return console.error(err.toString());
    });
