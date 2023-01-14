using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TeacherStates { Counting, Inspecting, Waiting };

public class TeacherAction : MonoBehaviour
{
    private const float InitialCountTime = 4f;
    private const float InitialWaitTime = 2f;
    private const float InitialInspectionTime = 6f;
    private const float InspectionDeviation = 1f;

    private float _currentCountTime;
    private float _currentWaitingTime;
    private float _currentInspectionTime;

    private TeacherStates currentTeacherState = TeacherStates.Waiting;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        _currentCountTime = InitialCountTime;
        _currentWaitingTime = InitialWaitTime;
        _currentInspectionTime = InitialInspectionTime + Random.Range(-InspectionDeviation, InspectionDeviation);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }

    private void StateMachine()
    {
        switch (currentTeacherState)
        {
            case TeacherStates.Counting:
                Count();
                break;
            case TeacherStates.Inspecting:
                Inspect();
                break;
            case TeacherStates.Waiting:
                Wait();
                break;
            default:
                break;
        }
    }

    private void Count()
    {
        if (_currentCountTime > 0)
        {
            _currentCountTime -= Time.deltaTime;
        }
        else
        {
            _animator.SetBool("Turned", true);
            currentTeacherState = TeacherStates.Inspecting;
        }
    }

    private void Inspect()
    {
        if (_currentInspectionTime > 0)
        {
            _currentInspectionTime -= Time.deltaTime;
            CheckForSleepingStudents();
        }
        else
        {
            _animator.SetBool("Turned", false);
            _currentInspectionTime = InitialInspectionTime + Random.Range(-InspectionDeviation, InspectionDeviation);
            _currentCountTime = InitialCountTime;
            _currentWaitingTime = InitialWaitTime;
            currentTeacherState = TeacherStates.Waiting;
        }
    }

    private void Wait()
    {

        if (_currentWaitingTime > 0f)
        {
            _currentWaitingTime -= Time.deltaTime;
        }
        else
        {
            currentTeacherState = TeacherStates.Counting;
        }
    }

    private void CheckForSleepingStudents()
    {
        // TODO
    }
}
