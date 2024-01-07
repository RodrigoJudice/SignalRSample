import ConfigureConnectionMainHub from "./connectionHub.js"

const connetionUserHub = ConfigureConnectionMainHub();

    function ShowToast(msg, timeout = 1000) {
        liveToastMessage.innerText = msg;

        $("#liveToast").show();
        window.setTimeout(function () {
            $('#liveToast').hide();

        }, timeout);

    }

    let liveToastMessage = document.getElementById("liveToastMessage");
    let lbl_houseJoined = document.getElementById("lbl_houseJoined");

    let btn_un_gryffindor = document.getElementById("btn_un_gryffindor");
    let btn_un_slytherin = document.getElementById("btn_un_slytherin");
    let btn_un_hufflepuff = document.getElementById("btn_un_hufflepuff");
    let btn_un_ravenclaw = document.getElementById("btn_un_ravenclaw");
    let btn_gryffindor = document.getElementById("btn_gryffindor");
    let btn_slytherin = document.getElementById("btn_slytherin");
    let btn_hufflepuff = document.getElementById("btn_hufflepuff");
    let btn_ravenclaw = document.getElementById("btn_ravenclaw");

    let trigger_gryffindor = document.getElementById("trigger_gryffindor");
    let trigger_slytherin = document.getElementById("trigger_slytherin");
    let trigger_hufflepuff = document.getElementById("trigger_hufflepuff");
    let trigger_ravenclaw = document.getElementById("trigger_ravenclaw");


    //Subscribe
    btn_gryffindor.addEventListener("click", function (event) {
        event.preventDefault();
        connetionUserHub.invoke("JoinHouse", "Gryffindor");
    });

    btn_hufflepuff.addEventListener("click", function (event) {
        event.preventDefault();
        connetionUserHub.invoke("JoinHouse", "Hufflepuff");
    });

    btn_ravenclaw.addEventListener("click", function (event) {
        event.preventDefault();
        connetionUserHub.invoke("JoinHouse", "Ravenclaw");
    });

    btn_slytherin.addEventListener("click", function (event) {
        event.preventDefault();
        connetionUserHub.invoke("JoinHouse", "Slytherin");
    });

    //UnSubscribe
    btn_un_gryffindor.addEventListener("click", function (event) {
        connetionUserHub.send("LeaveHouse", "Gryffindor");
        event.preventDefault();
    });

    btn_un_hufflepuff.addEventListener("click", function (event) {
        event.preventDefault();
        connetionUserHub.invoke("LeaveHouse", "Hufflepuff");
    });

    btn_un_ravenclaw.addEventListener("click", function (event) {
        event.preventDefault();
        connetionUserHub.invoke("LeaveHouse", "Ravenclaw");
    });

    btn_un_slytherin.addEventListener("click", function (event) {
        event.preventDefault();
        connetionUserHub.invoke("LeaveHouse", "Slytherin");
    });

    //trigger notification

    trigger_gryffindor.addEventListener("click", function (event) {
        connetionUserHub.send("TriggerHouseNotification", "Gryffindor");
        event.preventDefault();
    });

    trigger_hufflepuff.addEventListener("click", function (event) {
        event.preventDefault();
        connetionUserHub.invoke("TriggerHouseNotification", "Hufflepuff");
    });

    trigger_ravenclaw.addEventListener("click", function (event) {
        event.preventDefault();
        connetionUserHub.invoke("TriggerHouseNotification", "Ravenclaw");
    });

    trigger_slytherin.addEventListener("click", function (event) {
        event.preventDefault();
        connetionUserHub.invoke("TriggerHouseNotification", "Slytherin");
    });

    
    connetionUserHub.on("SubscriptionStatus", (GroupsJoined, houseName, HasSubscribed) => {
        lbl_houseJoined.innerText = GroupsJoined;


        switch (houseName) {
            case "Gryffindor":
                btn_gryffindor.style.display = HasSubscribed ? "none" : "";
                btn_un_gryffindor.style.display = HasSubscribed ? "" : "none";
                break;
            case "Hufflepuff":
                btn_hufflepuff.style.display = HasSubscribed ? "none" : "";
                btn_un_hufflepuff.style.display = HasSubscribed ? "" : "none";
                break;
            case "Ravenclaw":
                btn_ravenclaw.style.display = HasSubscribed ? "none" : "";
                btn_un_ravenclaw.style.display = HasSubscribed ? "" : "none";
                break;
            case "Slytherin":
                btn_slytherin.style.display = HasSubscribed ? "none" : "";
                btn_un_slytherin.style.display = HasSubscribed ? "" : "none";
                break;
            default:
                break;

        }

        ShowToast(`You have ${HasSubscribed ? "Joined" : "Left"} successfully. ${houseName}`);

    });

    connetionUserHub.on("InfomartionMembersToHouse", (houseName, HasSubscribed) => {
        ShowToast(`Member has ${HasSubscribed ? "Joined" : "Left"} from ${houseName}`);
    });

    connetionUserHub.on("TriggerHouseNotification", (houseName) => {
        ShowToast(`A new notification for ${houseName} has been launched`);
    });
   
///start connection
connetionUserHub.start()
    .then(() => {
        console.log("connected to user count hub");

    })
    .catch((err) => {
        return console.error(err.toString());
    });