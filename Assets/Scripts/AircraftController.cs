using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AircraftController : MonoBehaviour
{
    [SerializeField] private Transform runway;
    private Rigidbody rigidBody;

    private const float mass = 3000;
    private const float gravity = 9.8f;
    private const float dragCoeficient = 0.027f;
    private const float wingArea = 25.9f;
    private const float liftCoeficient = 1.2f;
    private const float engineForce = mass * gravity * 0.5f;
    private const float airDensity = 1.1455f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -gravity, 0);
        rigidBody.mass = mass;
        rigidBody.drag = 0;
        rigidBody.angularDrag = 0;
    }

    public void Equation()
    {
        ///http://www.jaymaron.com/flight.html
        ///https://www.slideshare.net/sureshkrv/mel341-3
        //At low altitudes above sea level, the pressure decreases by about 1.2 kPa (12 hPa) for every 100 metres
        ///Na equação:
        ///L = Lift
        ///CL = Angulo de ataque e formato da asa (constante no modelo)
        ///p = densidade do ar
        ///v = velocidade horizontal
        ///A = Area da asa (constante no modelo)
        //L = cl * 1/2*p V^2 * A 

        ///D = dc * 1/2 * p * V^2 * A

        ///Força horizontal= Força do motor - Força de arrasto
    }

    private float GetDrag()
    {
        var velocity = rigidBody.velocity.z;
        var drag = dragCoeficient * 0.5f * airDensity * Mathf.Pow(velocity, 2) * wingArea;
        Debug.Log("Velocity: " + velocity);
        Debug.Log("Drag: " + drag);
        return drag;
    }

    private float GetLift()
    {
        var velocity = rigidBody.velocity.z;
        var lift = liftCoeficient * 0.5f * airDensity * Mathf.Pow(velocity, 2) * wingArea;
        Debug.Log("lift: " + lift);
        return lift;
    }

    private void FixedUpdate()
    {
        var force = engineForce - GetDrag();
        rigidBody.AddForce(new Vector3(0, GetLift(), force));
        if (runway.position.z <= transform.position.z - 4000f)
        {
            runway.position = new Vector3(runway.position.x, runway.position.y, transform.position.z);
        }
    }
}
