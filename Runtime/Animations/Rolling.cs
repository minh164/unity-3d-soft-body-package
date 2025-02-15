using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sola164.SoftBody.Animations
{
    public class Rolling : Animation
    {
        public float rollSpeed = 100f;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
            // Roll();
        }

        void FixedUpdate()
        {
            // Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, 100) * Time.fixedDeltaTime);
            // Vector3 centerRight = CalculateRollSide("right");
            // mainRigid.MoveRotation(mainRigid.rotation * deltaRotation);
        }

        private void RollByForce()
        {
            
        }

        private void Roll()
        {
            // Debug.DrawLine(centerRight, centerRight + Vector3.forward, Color.red);
            // Debug.DrawLine(centerLeft, centerLeft + Vector3.forward, Color.blue);
            // Debug.DrawLine(centerBackward, centerBackward + Vector3.right * 2, Color.green);
            // Debug.DrawLine(centerForward, centerForward + Vector3.right * 2, Color.yellow);

            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                Vector3 centerRight = CalculateRollSide("right");
                StartCoroutine(RollAround(centerRight, Vector3.forward, true));
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                Vector3 centerLeft = CalculateRollSide("left");
                StartCoroutine(RollAround(centerLeft, Vector3.forward));
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                Vector3 centerBackward = CalculateRollSide("backward");
                StartCoroutine(RollAround(centerBackward, Vector3.right, true));
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                Vector3 centerForward = CalculateRollSide("forward");
                StartCoroutine(RollAround(centerForward, Vector3.right));
            }
        }

        private IEnumerator RollAround(Vector3 rollSide, Vector3 rollAxis, bool negativeAngle = false)
        {
            float rollAngle = 90;
            while (rollAngle > 0) {
                float eachRoll = Mathf.Min(Time.deltaTime * rollSpeed, rollAngle);
                transform.RotateAround(rollSide, rollAxis, eachRoll * (negativeAngle ? -1 : 1));
                rollAngle -= eachRoll;

                // Return each frame.
                yield return null;
            }
        }

        private Vector3 CalculateRollSide(string rollDirection)
        {
            // Convert local scale to world scale.x
            Vector3 scale = transform.TransformDirection(transform.localScale);

            // Ensure scale values are positive.
            scale.x = scale.x < 0 ? (scale.x * -1) : scale.x;
            scale.y = scale.y < 0 ? (scale.y * -1) : scale.y;
            scale.z = scale.z < 0 ? (scale.z * -1) : scale.z;

            // Because parent object does not have RigidBody, the position (transform.position) get from it will be incorrect,
            // so have to use position from centroid cell.
            Vector3 position = softBody.GetCentroidCell().transform.position;
            
            // Use for normal object (not a SoftBody object).
            // Vector3 position = transform.position;

            Vector3 center = Vector3.zero;
            switch (rollDirection) {
                case "right":
                    center = new Vector3(
                        position.x + scale.x / 2,
                        position.y - scale.y / 2, 
                        position.z
                    );
                    break;
                case "left":
                    center = new Vector3(
                        position.x - scale.x / 2,
                        position.y - scale.y / 2, 
                        position.z
                    );
                    break;
                case "backward":
                    center = new Vector3(
                        position.x,
                        position.y - scale.y / 2, 
                        position.z - scale.z / 2
                    );
                    break;
                case "forward":
                    center = new Vector3(
                        position.x,
                        position.y - scale.y / 2, 
                        position.z + scale.z / 2
                    );
                    break;
            }

            return center;
        }
    }
}
