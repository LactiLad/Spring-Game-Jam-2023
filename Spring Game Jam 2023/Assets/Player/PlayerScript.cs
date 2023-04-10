using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using Mirror.RemoteCalls;
using System.Security.Cryptography;

namespace QuickStart
{
    public class PlayerScript : NetworkBehaviour
    {
        // Start is called before the first frame update
        public static float speed = 4f;
        public Vector2 avgMove;
        bool playing = false;
        public GameObject otherPlayer;
        GameObject NoteScreen;
        float noteLooking = 0;
        public bool grabbable = false;

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
        [ClientRpc]
        public void quitRPC() {
            Application.Quit();
        }
        
        void Awake()
        {
            otherPlayer = GameObject.Find("Main Camera");
        }
        void Start() {
            if (!isLocalPlayer) return;
            GameObject.Find("Main Camera").transform.parent = transform;
            GameObject.Find("Main Camera").transform.localPosition = new Vector3(0,0,-10);
            GameObject.Find("WinScreen").transform.localScale = Vector3.zero;
        }
        // Update is called once per frame
        void Update()
        {
            if (NoteScreen == null) 
                NoteScreen = GameObject.Find("NoteScreen");

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
                avgMove = new Vector2();
                avgMove = (movement+otherMove).normalized;
                if (movement.magnitude > 1.5) avgMove *= 2;
                transform.Translate(avgMove * Time.deltaTime * speed);
                if (this.gameObject.GetComponent<NetworkIdentity>().sceneId == 0) {
                    //transform.Translate(avgMove * -0.5f * Time.deltaTime * speed);
                    otherPlayer.GetComponent<PlayerScript>().moveRPC(movement*10);
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
            if (!isLocalPlayer) return;
            if (noteLooking < 0.25f) {
                NoteScreen.transform.localScale = Vector3.zero;
            } else {
                NoteScreen.transform.localScale = new Vector3(1,1,1);
                noteLooking /= 1+(1f * Time.deltaTime);
                if (noteLooking > 1f) noteLooking = 1f; 
            }
        }
        void OnTriggerStay2D(Collider2D col)
        {
            if (!isLocalPlayer) return;
            if (col.transform.tag == "item") {
                if (Input.GetKeyDown("space") || Input.GetKeyDown("left shift") || Input.GetKeyDown("enter") || Input.GetKeyDown("mouse 0")) {
                    GetComponent<PlayerDecider>().item = true;
                    GameObject.Destroy(col.transform.gameObject);
                }
            }
            if (col.transform.tag == "note") {
                noteLooking += Time.deltaTime;
                NoteScreen.GetComponentInChildren<TextMeshProUGUI>().text = col.transform.GetComponent<Note>().text;
            }
            if (col.transform.tag == "win") {
                otherPlayer.GetComponent<PlayerScript>().quitRPC();
                GameObject.Find("WinScreen").transform.localScale = new Vector3(1,1,1);
                if (!playing) {
                    GameObject.Find("MusicHandler").GetComponent<SoundHandler>().playDuskTheme();
                    playing = true;
                }
            }
        }
    }
}