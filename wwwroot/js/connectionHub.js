//create Connection

export default function ConfigureConnectionUserHub() {

    return new signalR.HubConnectionBuilder()
        //.configureLogging(signalR.LogLevel.Information)
        .withUrl("/hubs/userHub",
            signalR.HttpTransportType.WebSockets |
            signalR.HttpTransportType.ServerSentEvents)
        .build();
    
}