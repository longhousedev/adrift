using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    public float movementModifier;
    public Transform groundedSphere;
    public float radius;
    public LayerMask mask;
    private float _gravity = -9.81f;
    private Vector3 _velocity;
    //private bool _isGrounded;
    public float jumpHeight;
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _velocity = Vector3.zero;
    }
    
    private void FixedUpdate()
    {
        //CURRENTLY DEBUGGING THIS SECTION
        //Checks if player on ground
        //_isGrounded = Physics.CheckSphere(groundedSphere.position, radius, mask);
        //Debug.Log(_isGrounded);
        //if (_isgrounded && _velocity.y < 0) _velocity.y = 0;

        //if (_controller.isGrounded) _velocity.y = -_gravity * Time.deltaTime;
        //Debug.Log(_controller.isGrounded);

        
        //if (Input.GetButton("Jump") && _isGrounded) _velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
        //END DEBUGGING
        
        //Applies gravity on the player to prevent floating
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
        
        //Gets the players input from the keyboard and moves the controller
        var x = Input.GetAxisRaw("Horizontal");
        var z = Input.GetAxisRaw("Vertical");
        var transform1 = transform;
        var movement = (transform1.right * x + transform1.forward * z);
        _controller.Move(movement * (movementModifier * Time.deltaTime));
    }
}
