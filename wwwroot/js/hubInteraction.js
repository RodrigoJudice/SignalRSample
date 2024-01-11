import UserViews from "./counters.js";
import ConfigureConnectionMainHub from "./connectionHub.js"

const connetionUserHub = ConfigureConnectionMainHub();

UserViews(connetionUserHub);


//invoke hub methods aka sent notification to hub with return and pass parameter
async function NewWindowLoadedOnClient() {
    const result = await connetionUserHub.invoke("NewWindowLoaded","Parametro1", "Parametro2");
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
        NewWindowLoadedOnClient();
    })
    .catch((err) => {
        return console.error(err.toString());
    });
