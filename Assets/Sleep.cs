using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{

    public BadStudent[] students = new BadStudent[2];

    /*
    // Start is called before the first frame update
    void Start()
    {   
        for (int i = 0; i < 2; i++) 
        {
            // Find some gameobject that has the text tag "Sleepy" assigned to it.
            // Store a ref to it.
            var student = GameObject.FindGameObjectWithTag("Sleepy");
            if (!student) 
            {
                Debug.LogError("Could not find a sleepy student. Ensure it has the sleepy tag set.");
            }
            else {
                students[i] = student;
            }
        }
        
    }
    */



    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            BadStudent student = (BadStudent)students[i];
            student.ChangeSleepState();
        }
    }
}
