using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerDecider : NetworkBehaviour
{
    public bool day;
    bool decided = false;
    public Animator animator;
    public AnimationClip[] dayDirectionAnimations; //0 idle, 1 up, 2 down, 3 left, 4 right
    //public AnimationClip[] nightDirectionAnimations; //0 idle, 1 up, 2 down, 3 left, 4 right
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();

    }
    void Start()
    {
        
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

        if (Input.GetAxis("Horizontal") > 0.1) {
            //if (day) animator.
        }
        else if (Input.GetAxis("Horizontal") < -0.1) {
            //right
        }
        else if (Input.GetAxis("Verticle") > 0.1) {
            //up
        }
        else if (Input.GetAxis("Verticle") < -0.1) {
            //down
        }
    }
}
