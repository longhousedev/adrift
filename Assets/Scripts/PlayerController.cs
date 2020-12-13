using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    private float _stamina;
    private CharacterController _controller;
    public float movementModifier;
    private float _sprintModifier = 1f;
    public Transform groundedSphere;
    public float radius;
    public LayerMask mask;
    private const float Gravity = -9.81f;
    private float _fov;
    private bool _isSprinting;
    private bool _isGrounded;
    private float _lerpT;
    private float _bobT;
    private Vector3 _oldPos;
    private float _bobDelta;
    private float _sprintBobDelta;

    //Footsteps
    public AudioClip[] footsteps;
    public AudioClip[] footstepsSprint;
    public AudioSource footstepsSource;

    private Vector3 _velocity;
    //private bool _isGrounded;
    public float jumpHeight;
    private void Start()
    {
        _oldPos = transform.position;
        _bobT = 0.3f;
        _lerpT = 0.1f;
        _fov = cam.fieldOfView;
        _stamina = 100;
        _controller = GetComponent<CharacterController>();
        _velocity = Vector3.zero;
        StartCoroutine(nameof(StaminaRegen));
    }

    private void Update()
    {
        if (transform.position != _oldPos && !footstepsSource.isPlaying) Invoke(nameof(playFootstep), 0.1f);
        _oldPos = transform.position;
    }

    private void FixedUpdate()
    {
        //Checks if player on ground and checks to see if player is sprinting
        _isGrounded = Physics.CheckSphere(groundedSphere.position, radius, mask);
        if (_isGrounded && _velocity.y < 0) _velocity.y = 0;
        SprintHandler();

        //Gets the players input from the keyboard and moves the controller
        var x = Input.GetAxisRaw("Horizontal");
        var z = Input.GetAxisRaw("Vertical");
        var transform1 = transform;
        var movement = (transform1.right * x + transform1.forward * z);
        _controller.Move(movement * (movementModifier * Time.deltaTime * _sprintModifier));
        
        //Applies gravity on the player to prevent floating
        if (Input.GetButton("Jump") && _isGrounded) _velocity.y = Mathf.Sqrt(jumpHeight * -2f * Gravity);
        _velocity.y += Gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        //Head bob
        float oldX = 0;
        float oldY = 1.08f;
        _bobDelta = Mathf.Sin(Time.time * 5) / 4;
        _sprintBobDelta = Mathf.Sin(Time.time * 15) / 4;
        var transform2 = cam.transform;
        if (_isSprinting && movement.magnitude != 0)
        {
            transform2.localPosition = new Vector3(0, 1.08f + _sprintBobDelta,
                transform2.localPosition.z);
        }
        else if (!_isSprinting && movement.magnitude != 0)
        {
            transform2.localPosition = new Vector3(0, 1.08f + (_bobDelta / 2),
                cam.transform.localPosition.z);
        }
        else
        {
            var localPosition = transform2.localPosition;
            localPosition = new Vector3(
                Mathf.Lerp(localPosition.x, 0, _bobT),
                Mathf.Lerp(localPosition.y, 1.08f, _bobT), 0);
            transform2.localPosition = localPosition;
        }
    }

    private IEnumerator StaminaRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                if (_stamina + 5f > 100) _stamina = 100f;
                else _stamina += 5f;
            }
        }
    }

    private void SprintHandler()
    {
        var origX = 0;
        var origY = 1.08f;
        if (Input.GetKey(KeyCode.LeftShift) && _stamina > 0)
        {
            _isSprinting = true;
            _sprintModifier = 1.8f;
            _stamina -= 10f * Time.deltaTime;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _fov + 10, _lerpT);

        }
        else
        {
            _isSprinting = false;
            _sprintModifier = 1;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _fov, _lerpT);
        }
    }

    void playFootstep()
    {
        if (!_isSprinting)
        {
            footstepsSource.clip = footsteps[Random.Range(0, 8)];
            footstepsSource.Play();   
        }
        else
        {
            footstepsSource.clip = footstepsSprint[Random.Range(0, 8)];
            footstepsSource.Play();   
        }
    }
}
