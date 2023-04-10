using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace QuickStart {
    public class PlayerDecider : MonoBehaviour
    {
        static float lerpSpeed = 20f;
        Vector2 direction;
        public bool day;
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
            int decisionDay = GameObject.Find("DecisionHolder").GetComponent<DecisionHolder>().day;
            if (decisionDay != 0) {
                if (decisionDay == 1)
                    day = true;
                else
                    day = false;
            }
            direction = Vector2.Lerp(direction, GetComponent<PlayerScript>().avgMove, Time.deltaTime * lerpSpeed);
            animator.SetBool("day", day);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
    }
}