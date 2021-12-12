using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HunterManager : MonoBehaviour
{
    public BehaviorAgent hunter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = GetKeyboardInput();
    }

    private Vector3 GetKeyboardInput()
    {
        Vector3 movement = Vector3.zero;

        var keyboard = Keyboard.current;
        if (keyboard == null)
            return movement;

        if (keyboard.wasUpdatedThisFrame)
        {
            if (keyboard.wKey.isPressed)
            {

            }
        }

        return movement;
    }
}
