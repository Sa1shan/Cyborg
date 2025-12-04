using UnityEngine;

namespace _Source
{
    public class RobotDogMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float forwardSpeed = 2f; 

        [Header("Leg Joints")]
        public HingeJoint frontLeftLeg;
        public HingeJoint frontRightLeg;
        public HingeJoint backLeftLeg;
        public HingeJoint backRightLeg;

        [Header("Leg Settings")]
        public float legMotorSpeed = 100f; 
        public float legMotorForce = 100f; 

        private float _stepTimer = 0f;
        private float _stepDuration = 0.5f; 

        void FixedUpdate()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.linearVelocity = new Vector3(-forwardSpeed, rb.linearVelocity.y, 0);
            
            _stepTimer += Time.fixedDeltaTime;
            float motorDirection = (_stepTimer % _stepDuration < _stepDuration / 2) ? 1f : -1f;

            MoveLeg(frontLeftLeg, motorDirection);
            MoveLeg(backRightLeg, motorDirection);
            MoveLeg(frontRightLeg, -motorDirection);
            MoveLeg(backLeftLeg, -motorDirection);
        }

        void MoveLeg(HingeJoint leg, float direction)
        {
            if (leg == null) return;

            JointMotor motor = leg.motor;
            motor.force = legMotorForce;
            motor.targetVelocity = legMotorSpeed * direction;
            motor.freeSpin = false;
            leg.motor = motor;
            leg.useMotor = true;
        }
    }
}
