using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Security.Cryptography;

namespace QuickStart
{
    public class PlayerScript : NetworkBehaviour
    {
        // Start is called before the first frame update
        public static float speed = 4f;
        public GameObject otherPlayer;

        [SyncVar(hook = nameof(PlayerMove))]
        public Vector2 movement = Vector2.zero;

        [Command(requiresAuthority = false)]
        public void PlayerMove(Vector2 _Old, Vector2 _New) {
            movement = _New;
        }
        [Command(requiresAuthority = false)]
        public void CmdSetMove(Vector2 _move) {
            movement = _move;
        }

        [ClientRpc]
        public void moveRPC(Vector2 move) {
            movement = move;//transform.Translate(move.x * Time.deltaTime * speed, move.y * Time.deltaTime * speed, 0);
        }
        
        void Awake()
        {
            otherPlayer = GameObject.Find("Main Camera");
        }
        void Start() {
            if (!isLocalPlayer) return;
            GameObject.Find("Main Camera").transform.parent = transform;
                    GameObject.Find("Main Camera").transform.localPosition = new Vector3(0,0,-10);
        }
        // Update is called once per frame
        void Update()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 1) {
                for (int i = 0; i < players.Length; i++) {
                    if (GetComponent<NetworkIdentity>().netId != players[i].GetComponent<NetworkIdentity>().netId) {
                        otherPlayer = players[i];
                    }
                }
            }

            if (otherPlayer.name != "Main Camera") {
                Vector2 otherMove = otherPlayer.GetComponent<PlayerScript>().movement;
                Vector2 avgMove = new Vector2();
                avgMove = (movement+otherMove).normalized;
                transform.Translate(avgMove * Time.deltaTime * speed);
                if (this.gameObject.name == "Player [connId=0]") {
                    transform.Translate(avgMove * -0.5f * Time.deltaTime * speed);
                    otherPlayer.GetComponent<PlayerScript>().moveRPC(movement);
                }
            }

            if (isLocalPlayer){
                movement.x = Input.GetAxis("Horizontal");
                movement.y = Input.GetAxis("Vertical");
                CmdSetMove(movement);
                if (transform.childCount == 0) {
                    GameObject.Find("Main Camera").transform.parent = transform;
                    GameObject.Find("Main Camera").transform.localPosition = new Vector3(0,0,-10);
                }
            }
        }
    }
}