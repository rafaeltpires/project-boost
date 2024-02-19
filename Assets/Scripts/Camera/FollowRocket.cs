using System;
    
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

    public class FollowRocket : MonoBehaviour
    {
        //public Transform rocket;
    
        public Transform rocket;
        public float smoothSpeed = 0.125f;
        public Vector3 locationOffset = new Vector3(0, 1, -15);

        private void Start()
        {
            rocket = GameObject.FindWithTag("Player").transform;
        }

        void FixedUpdate()
        {
            Vector3 desiredPosition = rocket.position + rocket.rotation * locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
