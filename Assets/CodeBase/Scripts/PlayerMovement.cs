using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    private IInputService _inputService;
    private CharacterController controller;
    private float playerSpeed = 5.0f;

   [Inject]
   private void Construct(IInputService inputService)
   {
       _inputService = inputService;
   }
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        controller.slopeLimit = 180f;
    }

    void Update()
    {
        Vector3 move = GetMoveVector();
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    private Vector3 GetMoveVector()
    {
        return new Vector3(_inputService.Axis.x, 0, _inputService.Axis.y);
    }
}
