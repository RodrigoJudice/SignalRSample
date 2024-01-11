import ConfigureConnectionMainHub from "./connectionHub.js"

const connetionMainHub = ConfigureConnectionMainHub();



//start connection
connetionMainHub.start()
    .then(() => {
        console.log("connected to hub");
    })
    .catch((err) => {
        return console.error(err.toString());
    });
