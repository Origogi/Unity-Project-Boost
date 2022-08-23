using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float period = 2f;
    
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (period <= Mathf.Epsilon)
        {
            return;
        }
        float cycles = Time.time / period;
        
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        float movementFactor = (rawSinWave + 1f) / 2f;
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
