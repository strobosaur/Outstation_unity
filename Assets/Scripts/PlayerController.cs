using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Practical;
using GLOBALS;

namespace Game.Controls
{
    public class PlayerController : MonoBehaviour
    {
        Gamepad gamepad = Gamepad.current;

        // CAMERA TARGET
        public GameObject camTarget;
        public float camSpd = 0.04f;
        public float camDist = 0.5f;

        // PLAYER VARIABLES

        public float moveSpd = 2.5f;
        public float moveMag;
        public float moveLen;
        public Vector2 movement;
        public Vector2 moveDir;
        public Animator animator;
        public Rigidbody2D rb;

        // CROSSHAIR VARIABLES        
        public GameObject crossHair;
        private Vector2 targetPos;
        private Vector2 currentPos;
        public float crossDistance = 9f;
        public float crossSpd;
        private SpriteRenderer sprite; 
        
        void Update()
        {
            ProcessInput();
        }

        void FixedUpdate() 
        {
            Move();
            MoveCrosshair();
            MoveCamTarget();
        }

        void ProcessInput()
        {
            if (gamepad == null)
            {
                return;
            }

            Vector3 LSTinp = gamepad.leftStick.ReadValue();

            moveDir = new Vector2(LSTinp.x, LSTinp.y);
            moveDir = Vector2.ClampMagnitude(moveDir, 1.0f);
            moveLen = moveDir.magnitude;

            /* if(LSTinp.x != 0)
            {
                Debug.Log("LSX: " + LSTinp.x);
                Debug.Log("#LSX: " + moveDir.x);
            }
            if(LSTinp.y != 0)
            {
                Debug.Log("LSY: " + LSTinp.y);
                Debug.Log("#LSY: " + moveDir.y);
            } */
        }

        void Move()
        {
            //moveDir *= moveLen;
            //movement.x = Functions.Approach(movement.x, moveDir.x, Globals.G_INERTIA);
            //movement.y = Functions.Approach(movement.y, moveDir.y, Globals.G_INERTIA);
            movement = Vector2.Lerp(movement, moveDir, Globals.G_INERTIA);
            rb.velocity = movement * moveSpd;            
            moveMag = movement.magnitude;
            
            animator.SetFloat("horizontal", movement.x);
            animator.SetFloat("vertical", movement.y);
            animator.SetFloat("magnitude", moveMag);
            animator.speed = moveMag;
        }

        private void MoveCrosshair()
        {
            // GET GAMEPAD INPUT
            Vector3 RSTinp = gamepad.rightStick.ReadValue();
            Vector2 RSTdir = new Vector2(RSTinp.x, RSTinp.y);
            RSTdir = Vector2.ClampMagnitude(RSTdir, 1.0f);
            float RSTlen = RSTdir.magnitude;

            // CHECK FOR RIGHT STICK ACTION
            if(RSTinp.magnitude > 0.1)
            {
                // CREATE NEW TARGET
                Vector2 crossDir = new Vector2(RSTdir.x, RSTdir.y);
                targetPos = new Vector2(rb.position.x, rb.position.y) + (crossDistance * crossDir);
            }
            else
            {
                // RETURN TO PLAYER
                targetPos = new Vector2(rb.position.x, rb.position.y);
            }

            // GET DISTANCE TO TARGET
            float targetDist = Vector3.Distance(new Vector2(crossHair.transform.position.x, crossHair.transform.position.y), targetPos);

            // SMOOTH CROSSHAIR MOVEMENT
            if (Mathf.CeilToInt(targetDist * 16) > 2)
            {
                crossHair.transform.position = Vector2.MoveTowards(crossHair.transform.position, targetPos, (0.75f));
            }

            // ADJUST CROSSHAIR OPACITY BASED ON DISTANCE TO PLAYER
            float opacity = (Vector3.Distance(crossHair.transform.position, rb.transform.position) - 2) / 5;
            sprite = crossHair.GetComponent<SpriteRenderer>();
            sprite.color = new Color(1,1,1,opacity);            
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