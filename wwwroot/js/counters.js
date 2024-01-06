//connect to methods that hub invokes aka receive notifications from hub
export default function UserViews(connetionUserHub) {
    
    connetionUserHub.on("UpdateTotalViews", (totalViews) => {
        var usersCountSpan = document.getElementById("totalViewCounter");
        usersCountSpan.innerText = totalViews.toString();
    });

    connetionUserHub.on("UpdateTotalUsers", (TotalUsers) => {
        var usersCountSpan = document.getElementById("totalUserCounter");
        usersCountSpan.innerText = TotalUsers.toString();
    });

}