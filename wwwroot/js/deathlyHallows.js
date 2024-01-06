//connect to methods that hub invokes aka receive notifications from hub
export default function UpdateDealthyHallowCount(connetionUserHub) {

    connetionUserHub.on("UpdateDealthyHallowCount", (raceCounter) => {

        var cloakSpan = document.getElementById("cloakCounter");
        var stoneSpan = document.getElementById("stoneCounter");
        var wandSpan = document.getElementById("wandCounter");

        cloakSpan.innerText = raceCounter.cloak.toString();
        stoneSpan.innerText = raceCounter.stone.toString();
        wandSpan.innerText = raceCounter.wand.toString();
    });
}