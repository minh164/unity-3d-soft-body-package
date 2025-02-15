using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sola164.SoftBody.Animations
{
    public class Walking : Animation
    {
        public float walkSpeed = 0.5f;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        void FixedUpdate()
        {
            Walk();
        }

        private void Walk()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (horizontal != 0) {
                foreach (Rigidbody rigidbody in rigidbodies) {
                    // rigidbody.AddForce(new Vector3(1,0,0) * horizontal * walkSpeed, ForceMode.Impulse);
                    rigidbody.MovePosition(rigidbody.position + Vector3.right * Time.fixedDeltaTime * horizontal * walkSpeed);
                }
            }

            if (vertical != 0) {
                foreach (Rigidbody rigidbody in rigidbodies) {
                    // rigidbody.AddForce(new Vector3(0,0,1) * vertical * walkSpeed, ForceMode.Impulse);
                    rigidbody.MovePosition(rigidbody.position + Vector3.forward * Time.fixedDeltaTime * vertical * walkSpeed);
                }
            }
        }
    }
}
