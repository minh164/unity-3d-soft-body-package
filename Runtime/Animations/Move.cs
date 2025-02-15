using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sola164.SoftBody.Animations
{
    public class Move : Animation
    {
        public float jumpForce = 0.5f;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        void Update()
        {
        }

        // Update is called once per fixed delta time (0.02).
        void FixedUpdate()
        {
            if (Input.GetButton("Jump")) {
                foreach (Rigidbody rigidbody in rigidbodies) {
                    rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }
        }
    }
}
