using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
public class Food : MonoBehaviour
{
    [SerializeField]
    private int score;
    public int Score { get => score; }

    [SerializeField]
    private float startSize = 0;

    [SerializeField]
    private float endSize = 1.5f;

    private SpriteRenderer spriteRenderer;
    private float currentRotation;
    private Vector3 currentSize;

    private bool init;
    private float timeStart;
    // Start is called before the first frame update
    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        transform.localScale = Vector3.one * startSize;
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.localScale.x < Vector3.one.x && !init)
        {
            currentSize = Vector3.Slerp (currentSize, Vector3.one * endSize, Time.deltaTime * 5);

            timeStart = Time.time;
            transform.localScale = currentSize;
        }
        else
        {
            if (!init)
                init = true;
            transform.localScale = Vector3.one * Mathf.Clamp (Mathf.PingPong (timeStart + Time.time, 1.5f), 0.8f, 1.5f);
        }
    }
}