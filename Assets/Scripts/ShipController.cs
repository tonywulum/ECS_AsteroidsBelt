using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 25f;
    public float strafeSpeed = 8f;
    public float hoverSpeed = 5f;

    private float activeForwardSpeed;
    private float activeStrafeSpeed;
    private float activeHoverSpeed;

    private float forwardAcceleration = 2.5f;
    private float strafeAcceleration = 2f;
    private float hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput;
    private Vector2 screenCenter;
    private Vector2 mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f;
    public float rollAcceleration = 3f;

    float deltaTime;
    bool shipFlying = true;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput,
                                Input.GetAxisRaw("Roll"),
                                rollAcceleration * deltaTime);

        transform.Rotate(-mouseDistance.y * lookRateSpeed * deltaTime,
                            mouseDistance.x * lookRateSpeed * deltaTime,
                            rollInput * rollSpeed * deltaTime,
                            Space.Self);

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed,
                                        Input.GetAxisRaw("Vertical") * forwardSpeed,
                                        forwardAcceleration * deltaTime);

        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed,
                                        Input.GetAxisRaw("Horizontal") * strafeSpeed,
                                        strafeAcceleration * deltaTime);

        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed,
                                        Input.GetAxisRaw("Hover") * hoverSpeed,
                                        hoverAcceleration * deltaTime);

        transform.position += transform.forward * activeForwardSpeed * deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * deltaTime) +
                                (transform.up * activeHoverSpeed * deltaTime);


    }
}
