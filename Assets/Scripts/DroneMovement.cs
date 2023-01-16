using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DroneMovement : MonoBehaviour
{

    private Vector3 _mousePosition;
    [SerializeField] private float _droneSpeed = 10f;
    [SerializeField] private float _droneHeight = 10f;

    [SerializeField] private Character _player;
    [SerializeField] private Transform _droneRestingPosition;
    [SerializeField] private Rigidbody _rigidbody;
    public float maxDistance = 10f;


    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.ReadValue(), _droneHeight, -Camera.main.transform.position.z));
    }

    private void FixedUpdate()
    {
        HandleDroneMovement(_player.isMouseAiming);
    }

    private void HandleDroneMovement(bool mouseAiming)
    {
        if (mouseAiming)
        {
            _rigidbody.MovePosition(transform.position + _mousePosition * Time.fixedDeltaTime * _droneSpeed);
            //keep drone within max distance
            float actualDistance = Vector2.Distance(_player.transform.position, transform.position);

            if (actualDistance > maxDistance)
            {
                Vector2 centerToPosition = transform.position - _player.transform.position;
                centerToPosition.Normalize();
                transform.position = (Vector2)_player.transform.position + centerToPosition * maxDistance;
            }
        }
        //if (mouseAiming)
        //{
        //    _mousePosition = Mouse.current.position.ReadValue();
        //    //move drone towards mouse position
        //    transform.position = Vector2.MoveTowards(transform.position, _mousePosition, _droneSpeed * Time.deltaTime);

        //    //keep drone within max distance
        //    float actualDistance = Vector2.Distance(_player.transform.position, transform.position);

        //    if (actualDistance > maxDistance)
        //    {
        //        Vector2 centerToPosition = transform.position - _player.transform.position;
        //        centerToPosition.Normalize();
        //        transform.position = (Vector2)_player.transform.position + centerToPosition * maxDistance;
        //    }
        //}
        //else
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, _droneRestingPosition.position, _droneSpeed * Time.deltaTime);
        //}

    }
}
