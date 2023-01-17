using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class BadStudent : MonoBehavior
// {
//   // These values will appear in the editor, full properties will not.
//     private bool isSleeping = false;

//   // Called on startup of the GameObject it's assigned to.
//     void Start()
//     {

//     }
//   // Called every frame. The frame rate varies every second.
//     void Update()
//     {

//     }
// }

public class BadStudent : MonoBehaviour
{
    Transform t;
    Animator anim;
    private bool isSleeping = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Ball")
        {
            WakeStudent();
        }
    }

    private void SetSleepState(bool state)
    {
        isSleeping = state;
    }

    public void WakeStudent()
    {
        SetSleepState(false);
        anim.SetBool("isSleeping", false);
    }

    public void SnoozeStudent()
    {
        SetSleepState(true);
        anim.SetBool("isSleeping", true);
    }

    public bool getSleepState()
    {
        return isSleeping;
    }

    public void ChangeSleepState()
    {

        int num = Random.Range(1, 20);

        if (num > 15)
        {
            // change state
            SnoozeStudent();


        }
    }
}