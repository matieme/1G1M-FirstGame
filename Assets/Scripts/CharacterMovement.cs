using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;
    public GameObject shadow;

    private Vector3 characterScale;
    private float characterScaleX;

    void Start()
    {
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;
    }

    private void FixedUpdate()
    {
        if (InputManager.Instance.AxisMoving)
        {
            MoveTransform(InputManager.Instance.AxisHorizontal, InputManager.Instance.AxisVertical);
            FlipPosition(InputManager.Instance.AxisHorizontal);
        }
    }

    private void FlipPosition(float xHorizontal)
    {
        if (xHorizontal < 0){
            characterScale.x = -characterScaleX;
        }
        if (xHorizontal > 0)        {
            characterScale.x = characterScaleX;
        }

        transform.localScale = characterScale;
    }

    public void MoveTransform(float x, float z)
    {  
        var newPosition = transform.position + (new Vector3(x, 0, z) * speed * Time.deltaTime);        
        transform.position = newPosition;       
    }
}
