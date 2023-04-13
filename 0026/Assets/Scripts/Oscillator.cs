using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField][Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        SinCycles();
        ChangingPosition();
    }

    private void ChangingPosition()
    {
        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }

    private void SinCycles()
    {
        if (period <= Mathf.Epsilon) { return; }
        const float tau = Mathf.PI * 2;
        float cycles = Time.time / period;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1) / 2;
    }
}
