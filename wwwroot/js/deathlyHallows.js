import ConfigureConnectionMainHub from "./connectionHub.js"

const connetionUserHub = ConfigureConnectionMainHub();

//connect to methods that hub invokes aka receive notifications from hub
connetionUserHub.on("UpdateDealthyHallowCount", (raceCounter) => {

    var cloakSpan = document.getElementById("cloakCounter");
    var stoneSpan = document.getElementById("stoneCounter");
    var wandSpan = document.getElementById("wandCounter");

    cloakSpan.innerText = raceCounter.cloak.toString();
    stoneSpan.innerText = raceCounter.stone.toString();
    wandSpan.innerText = raceCounter.wand.toString();
});


///start connection
connetionUserHub.start()
    .then(() => {
        console.log("connected to user count hub");
       
    })
    .catch((err) => {
        return console.error(err.toString());
    });