using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sola164.SoftBody.Animations
{
    public class Animation : MonoBehaviour
    {
        protected SoftBody softBody;
        protected Rigidbody[] rigidbodies;
        protected Rigidbody mainRigid; 

        // Start is called before the first frame update
        protected virtual void Start()
        {
            softBody = gameObject.GetComponent<SoftBody>();
            if (! softBody) {
                Debug.LogError("Can not found SoftBody class in game object");
                return;
            }

            rigidbodies = new Rigidbody[softBody.GetBones().Length];
            int rigidIndex = 0;
            foreach (var bone in softBody.GetBones())
            {
                rigidbodies[rigidIndex] = bone.GetComponent<Rigidbody>();
                rigidIndex++;
            }

            mainRigid = gameObject.GetComponent<Rigidbody>();
        }
    }
}
