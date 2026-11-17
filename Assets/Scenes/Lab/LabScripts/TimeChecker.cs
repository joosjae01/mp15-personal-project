using System;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    [SerializeField] private float _time;

    private void Update()
    {
        _time -= Time.deltaTime;
        CheckTime();
    }

    private void CheckTime()
    {
        int min = (int) _time / 60;
        int sec = (int) _time % 60;

        Debug.Log($"{min:00} : {sec:00}");
    }
}
