using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using Mirror.Discovery;
public class ConnectionManager : NetworkManagerHUD
{
    public bool foundServer = false;
    public bool connected = false;
    public bool start = false;
    public bool day = true;
    public float time = 0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (start) {
            if (!connected) {
                try {
                    GetComponent<NetworkManager>().StartHost();
                    connected = true;
                    day = true;
                }
                catch (Exception e) {
                    GetComponent<NetworkManager>().StartClient();
                    connected = true;
                    day = false;
                }
            }
        }
    }

    public void serverFound() {
        foundServer = true;
    }
    public void started() {
        start = true;
    }
}
