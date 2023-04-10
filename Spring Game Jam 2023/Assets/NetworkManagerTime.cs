using UnityEngine;
using Mirror;
public class NetworkManagerTime : NetworkManager
{
    public Transform DaySpawn;
    public Transform NightSpawn;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform start = numPlayers == 0 ? DaySpawn : NightSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        Application.Quit();
    }

    public override void OnClientDisconnect()
    {
        Application.Quit();
        base.OnClientDisconnect();
    }
}