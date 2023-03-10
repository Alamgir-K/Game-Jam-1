using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHandler : MonoBehaviour
{
    public GameObject ball;
    public GameObject arrow;
    public int maxPressTime = 100;
    int pressedTime = 0;

    Transform t;
    Vector3 throwDir;
    Vector3 debug;
    public bool thrown = false;
    public float bufferTime = 0.1f;

    private void Start()
    {
        t = GetComponent<Transform>();
    }

    void setDirectionVector()
    {
        // get mouse position
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // get intersection point of mouse ray at xz-plane
        Plane hPlane = new Plane(Vector3.up, Vector3.zero);
        float d;
        hPlane.Raycast(mouseRay, out d);
        d = Mathf.Abs(d);

        Vector3 hitPos = new Vector3(mouseRay.GetPoint(d).x, 0, mouseRay.GetPoint(d).z);

        // set direction vector
        throwDir = hitPos - new Vector3(t.position.x, 0, t.position.z);
        throwDir.Normalize();
    }

    IEnumerator buffer()
    {
        thrown = true;
        yield return new WaitForSeconds(bufferTime);
        thrown = false;
    }

    void FixedUpdate()
    {
        Debug.Log(pressedTime);
        // set direction vector pointing at mouse position at xz-plane
        setDirectionVector();

        // set arrow
        float yScale = pressedTime * 0.1f;
        arrow.transform.localPosition = throwDir * yScale * 0.5f + new Vector3(0, 1f, 0);
        arrow.transform.localScale = new Vector3(0.57f, 0.67f + yScale, 2.07f);
        arrow.transform.rotation = Quaternion.LookRotation(throwDir, Vector3.up) * Quaternion.AngleAxis(-90, Vector3.left);

        bool leftClick = Input.GetKey(KeyCode.Mouse0);
        if (!thrown)
        {
            if (!leftClick)
            {
                if (pressedTime != 0)
                {
                    // activate slingshot mechanic
                    GameObject ballClone = Instantiate(ball, t.position + throwDir * 1.5f + Vector3.up, Quaternion.LookRotation(Vector3.forward, Vector3.up));
                    Rigidbody ballRB = ballClone.GetComponent<Rigidbody>();
                    ballRB.velocity = throwDir * pressedTime + new Vector3(0, 3, 0);
                    debug = ballRB.velocity;
                    Destroy(ballRB.gameObject, 5f);
                    StartCoroutine("buffer");
                }
                pressedTime = 0;

            }
            else if (pressedTime < maxPressTime) pressedTime++;
        }

    }
}
