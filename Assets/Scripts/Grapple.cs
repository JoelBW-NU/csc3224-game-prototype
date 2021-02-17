using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    Rigidbody2D player;

    LineRenderer grappleLine;

    DistanceJoint2D joint;

    GameObject activePoint;

    bool grappled = false;
    bool grappleFrame = false;

    bool leftForce = false;
    bool rightForce = false;

    [SerializeField]
    float force = 5;

    [SerializeField]
    float maxVelocity = 8;

    Vector2 grapplePos;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        grappleLine = GetComponent<LineRenderer>();
        joint = GetComponent<DistanceJoint2D>();
        grappleLine.positionCount = 2;        
    }

    // Update is called once per frame
    void Update()
    {
        if (grappled)
        {
            grappleLine.SetPositions(new Vector3[] { transform.position, grapplePos });   

            if (Input.GetMouseButtonUp(1) && !grappleFrame)
            {
                Ungrapple();
            }
        }   
        
        if (grappleFrame)
        {
            grappled = true;
            grappleFrame = false;
        }

        if (Input.GetKey(KeyCode.E))
        {
            Time.timeScale = 0.25f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        
    }

    void FixedUpdate()
    {      
        if (grappled)
        {
            float magnitude = player.velocity.magnitude;

            Vector2 pullVector = grapplePos - player.position;

            Vector2 newDirection = Vector3.Cross(pullVector, Vector3.forward).normalized;

            /*if (Input.GetKeyDown(KeyCode.D))
            {
                player.AddForce(-transform.right * force);
                //player.velocity = newDirection * Mathf.Clamp(magnitude, 0, maxVelocity);
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                player.AddForce(transform.right * force);
                //player.velocity = -newDirection * Mathf.Clamp(magnitude, 0, maxVelocity);
            }*/

            //player.velocity = newDirection * Mathf.Clamp(magnitude, 0, maxVelocity);

            if (Input.GetKey(KeyCode.D))
            {
                player.AddForce(-transform.right * force);
            }

            if (Input.GetKey(KeyCode.A))
            {
                player.AddForce(transform.right * force);
            }

            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, Vector2.SignedAngle(Vector2.up, player.position - grapplePos)));
        }

        player.velocity = Vector2.ClampMagnitude(player.velocity, maxVelocity);
    }

    public void GrappleToPoint(GameObject grapplePoint)
    {
        grappleFrame = true;       
        joint.enabled = true;
        joint.connectedBody = grapplePoint.GetComponent<Rigidbody2D>();
        activePoint = grapplePoint;
        grapplePos = grapplePoint.transform.position;
    }

    public void Ungrapple()
    {
        activePoint.GetComponent<GrapplePoint>().grappled = false;
        grappled = false;
        joint.enabled = false;
        grappleLine.SetPositions(new Vector3[] { transform.position, transform.position });            
    }
}
