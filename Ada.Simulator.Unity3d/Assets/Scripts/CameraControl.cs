using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public float HorizontalSpeed;
    public float VerticalSpeed;
    public float RotationSpeed;

    public Component LookAtTarget;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var transform = GetComponent<Camera>().transform;
        transform.Translate(new Vector3(HorizontalSpeed * Input.GetAxis("Horizontal"), VerticalSpeed * Input.GetAxis("Vertical")));

        transform.LookAt(LookAtTarget.transform);


    }
}
