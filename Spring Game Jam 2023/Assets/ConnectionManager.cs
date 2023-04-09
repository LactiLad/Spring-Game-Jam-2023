using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Discovery;
public class ConnectionManager : NetworkManagerHUD
{
    public bool foundServer = false;
    public bool connected = false;
    public float time = 0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 3f && !foundServer && !connected) {
            GetComponent<NetworkManager>().StartHost();
            connected = true;
        }
    }

    public void serverFound() {
        foundServer = true;
    }
}
