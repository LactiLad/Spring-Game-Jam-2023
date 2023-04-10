using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace QuickStart {
    public class Server : NetworkBehaviour
    {
        [ClientRpc]
        public void moveRPC(Vector2 move, GameObject player) {
            if (isServer)
                player.GetComponent<PlayerScript>().movement = move;
        }   
    }
}