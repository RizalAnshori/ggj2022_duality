using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
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
        Vector3 move = new Vector3(GetHorizontalValue(), 0, GetVerticalValue());
        if (!canMove || move == Vector3.zero)
        {
            animator.Play("idle");
            return;
        }
        characterController.Move(move * Time.deltaTime * moveSpeed);
    }

    private float GetHorizontalValue()
    {
        if(Keyboard.current[right].isPressed)
        {
            animator.Play("right");
            return 1;
        }
        else if(Keyboard.current[left].isPressed)
        {
            animator.Play("left");
            return -1;
        }

        return 0;
    }

    private float GetVerticalValue()
    {
        if (Keyboard.current[up].isPressed)
        {
            animator.Play("up");
            return 1;
        }
        else if (Keyboard.current[down].isPressed)
        {
            animator.Play("down");
            return -1;
        }

        return 0;
    }
}
