using UnityEngine;

namespace _Source
{
    public class RobotDogMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float forwardSpeed = 2f; // скорость движения вперед

        [Header("Leg Joints")]
        public HingeJoint frontLeftLeg;
        public HingeJoint frontRightLeg;
        public HingeJoint backLeftLeg;
        public HingeJoint backRightLeg;

        [Header("Leg Settings")]
        public float legMotorSpeed = 100f; // скорость мотора ноги
        public float legMotorForce = 100f; // сила мотора

        private float stepTimer = 0f;
        private float stepDuration = 0.5f; // время одного шага

        void FixedUpdate()
        {
            // 1. Двигаем тело вперед
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.linearVelocity = new Vector3(-forwardSpeed, rb.linearVelocity.y, 0);

            // 2. Двигаем ноги
            stepTimer += Time.fixedDeltaTime;
            float motorDirection = (stepTimer % stepDuration < stepDuration / 2) ? 1f : -1f;

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
