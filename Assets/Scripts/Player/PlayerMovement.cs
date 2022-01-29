using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Key up = Key.UpArrow;
    [SerializeField] private Key left = Key.LeftArrow;
    [SerializeField] private Key down = Key.DownArrow;
    [SerializeField] private Key right = Key.RightArrow;

    public bool CanMove { get { return canMove; } set { canMove = value; } }

    private bool canMove = true;

    private void Start()
    {
        if(characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    private void Update()
    {
        if (!canMove) return;

        Vector3 move = new Vector3(GetHorizontalValue(), 0, GetVerticalValue());
        characterController.Move(move * Time.deltaTime * moveSpeed);
    }

    private float GetHorizontalValue()
    {
        if(Keyboard.current[right].isPressed)
        {
            return 1;
        }
        else if(Keyboard.current[left].isPressed)
        {
            return -1;
        }

        return 0;
    }

    private float GetVerticalValue()
    {
        if (Keyboard.current[up].isPressed)
        {
            return 1;
        }
        else if (Keyboard.current[down].isPressed)
        {
            return -1;
        }

        return 0;
    }
}
