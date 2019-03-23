using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        if (InputManager.Instance.AxisMoving)
        {
            MoveTransform(InputManager.Instance.AxisHorizontal, InputManager.Instance.AxisVertical);
        }
    }

    public void MoveTransform(float x, float z)
    {
        var newPosition = transform.position + (new Vector3(x, 0, z) * speed * Time.deltaTime);
        
        transform.position = newPosition;
    }
}
