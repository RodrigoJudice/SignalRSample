//create Connection

export default function ConfigureConnectionMainHub() {

    return new signalR.HubConnectionBuilder()
        //.configureLogging(signalR.LogLevel.Information)
        .withUrl("/hubs/main",
            signalR.HttpTransportType.WebSockets |
            signalR.HttpTransportType.ServerSentEvents)
        .build();
    
}