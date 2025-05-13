import * as signalR from '@microsoft/signalr';

const baseUrl = import.meta.env.VITE_API_BASE_URL;

export const connection = new signalR.HubConnectionBuilder()
  .withUrl(`${baseUrl}hubs/ws`, {
    withCredentials: true
  })
  .withAutomaticReconnect()
  .configureLogging(signalR.LogLevel.Information)
  .build();

export const startConnection = async () => {
  try {
    if (connection.state === signalR.HubConnectionState.Disconnected) {
      await connection.start();
      console.log('SignalR connected.');
    }
  } catch (err) {
    console.error('SignalR connection error:', err);
    setTimeout(startConnection, 3000);
  }
};
