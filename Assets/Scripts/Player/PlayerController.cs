﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Practical;
using GLOBALS;

namespace Game.Controls
{
    public class PlayerController : MonoBehaviour
    {
        // GAMEPAD
        Gamepad gamepad = Gamepad.current;
        public Vector2 LSinp;
        public Vector2 LSdir;
        public Vector2 RSinp;
        public Vector2 RSdir;
        private float LSDeadzone = 0.2f;
        private float RSDeadzone = 0.2f;

        // CAMERA TARGET
        public GameObject camTarget;
        public float camSpd = 0.075f;
        public float camDist = 0.5f;

        // PLAYER VARIABLES
        public float moveSpd = 2.5f;
        public float moveMag;
        public float moveLen;
        public Vector2 movement;
        public Vector2 moveTo;
        public Vector2 moveToDir;
        public Vector2 moveDir;
        public Vector2 faceDir;

        public Animator animator;
        public float animSpdFactor = 0.5f;

        public Rigidbody2D rb;

        // CROSSHAIR VARIABLES        
        public GameObject crossHair;
        public Vector2 crossTargetPos;
        public Vector2 crossCurrentPos;
        public float crossTargetDist;
        public float crossDistance = 9f;
        public float crossSpd = 0.25f;
        public float crossOpacity;
        private SpriteRenderer sprite; 
        
        // UPDATE
        void Update()
        {
            ProcessInput();
        }

        // FIXED UPDATE
        void FixedUpdate() 
        {
            Move();
            MoveCrosshair();
            MoveCamTarget();
        }

        // LATE UPDATE
        void LateUpdate()
        {

        }

        // PROCESS INPUT
        void ProcessInput()
        {
            if (gamepad == null)
            {
                return;
            }

            // LEFT STICK INPUT
            LSinp = gamepad.leftStick.ReadValue();
            if(LSinp.magnitude < LSDeadzone){
                LSinp = new Vector2(0f, 0f);
            } else {
                LSinp = Vector2.ClampMagnitude(LSinp, 1.0f);
            }

            LSdir = LSinp.normalized;

            // RIGHT STICK INPUT
            RSinp = gamepad.rightStick.ReadValue();
            if(RSinp.magnitude < RSDeadzone){
                RSinp = new Vector2(0f, 0f);
            } else {
                RSinp = Vector2.ClampMagnitude(RSinp, 1.0f);
            }

            RSdir = RSinp.normalized;
        }

        // MOVE PLAYER
        void Move()
        {
            /*moveToDir = Vector2.ClampMagnitude(LSinp, 1.0f);
            if (moveToDir.magnitude < LSDeadzone)
            {
                moveToDir = new Vector2(0f, 0f);
            }*/

            moveLen = LSinp.magnitude;
            moveTo = LSinp * moveSpd;
            movement = Vector2.Lerp(movement, moveTo, Globals.G_INERTIA);

            if (movement.magnitude > 0.01f) {
                rb.velocity = movement;
                moveDir = movement.normalized;
                moveMag = movement.magnitude;
            } else {
                movement = new Vector2(0f, 0f);
                rb.velocity = new Vector2(0f, 0f);
                moveMag = 0f;
            }

            // ANIMATOR
            animator.SetFloat("horizontal", moveDir.x);
            animator.SetFloat("vertical", moveDir.y);
            animator.SetFloat("magnitude", moveMag);
            animator.speed = moveMag * animSpdFactor;
        }

        // MOVE CROSSHAIR
        private void MoveCrosshair()
        {
            // CHECK FOR RIGHT STICK ACTION
            if(RSinp.magnitude > RSDeadzone)
            {
                // CREATE NEW TARGET
                //Vector2 crossDir = new Vector2(RSinp.x, RSinp.y);
                crossTargetPos = new Vector2(rb.position.x, rb.position.y) + (crossDistance * RSinp);

                // PLAYER FACING
                faceDir = RSinp.normalized;
            }
            else
            {
                // RETURN TO PLAYER
                crossTargetPos = new Vector2(rb.position.x, rb.position.y);
                faceDir = moveDir;
            }

            // GET DISTANCE TO TARGET
            crossTargetDist = Vector2.Distance(
                new Vector2(crossHair.transform.position.x, crossHair.transform.position.y), 
                crossTargetPos);

            // SMOOTH CROSSHAIR MOVEMENT
            if (Mathf.CeilToInt(crossTargetDist * 16) > 2)
            {
                //crossHair.transform.position = Vector2.MoveTowards(crossHair.transform.position, crossTargetPos, (0.75f));
                crossHair.transform.position = Vector2.Lerp(crossHair.transform.position, crossTargetPos, crossSpd);
            }

            // ADJUST CROSSHAIR OPACITY BASED ON DISTANCE TO PLAYER
            crossOpacity = (Vector3.Distance(crossHair.transform.position, rb.transform.position) - 2) / 5;
            sprite = crossHair.GetComponent<SpriteRenderer>();
            sprite.color = new Color(1,1,1,crossOpacity);            
        }

        private void MoveCamTarget()
        {
            // FIND MIDPOINT BETWEEN PLAYER AND CROSSHAIR
            Vector3 target = Vector3.Lerp(rb.position, crossHair.transform.position, camDist);

            // IF DISTANCE TOO SMALL, JUST SET POSITION TO TARGET
            if(Vector3.Distance(camTarget.transform.position, target) > (4 / 16))
            {
                camTarget.transform.position = Vector3.Lerp(camTarget.transform.position, target, camSpd);
            } else {
                camTarget.transform.position = target;
            }
        }
    }
}