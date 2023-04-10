using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace QuickStart {
    public class PlayerDecider : MonoBehaviour
    {
        static float lerpSpeed = 20f;
        bool playing = false;
        Vector2 direction;
        public bool day;
        public bool item = false;
        bool decided = false;
        public Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            //find if day
            day = GameObject.Find("Network Manager").GetComponent<ConnectionManager>().day;
            direction = Vector2.Lerp(direction, GetComponent<PlayerScript>().avgMove, Time.deltaTime * lerpSpeed);
            animator.SetBool("day", day);
            animator.SetBool("item", item);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);

            if (day && !playing) {
                playing = true;
                GameObject.Find("MusicHandler").GetComponent<SoundHandler>().playDayTheme();
            } else if (!day && !playing) {
                playing = true;
                GameObject.Find("MusicHandler").GetComponent<SoundHandler>().playNightTheme();
            }
            if (item && GameObject.Find("Tilemap_Gates")) {
                GameObject.Destroy(GameObject.Find("Tilemap_Gates"));
            }
        }
    }
}