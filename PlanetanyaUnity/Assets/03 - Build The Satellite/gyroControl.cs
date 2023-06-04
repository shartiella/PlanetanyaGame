
using UnityEngine;
using TMPro;


public class gyroControl : MonoBehaviour
{
    public TextMeshProUGUI txt;
    private float X;
    private float Y;
    private float Z;
    public float speed = 1;
    private Quaternion initialRotation;
    private Vector3 previousRotation;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled= true;
        initialRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        txt.text = "attitude: " + Input.gyro.attitude.ToString() + "\n gravity: " + Input.gyro.gravity.ToString() + "\n rotationRate: " + Input.gyro.rotationRate.ToString() + "\n rotationRateUnbiased: " + Input.gyro.rotationRateUnbiased.ToString() + "\n userAcceleration: " + Input.gyro.userAcceleration.ToString() + "\n OBJECT ROTATION" + transform.rotation.ToString();
        txt.text += "\n device rotation: \n X:" + Input.gyro.attitude.eulerAngles.x.ToString() + " Y:" + Input.gyro.attitude.eulerAngles.y.ToString() + " Z:" + Input.gyro.attitude.eulerAngles.z.ToString();

        if (Input.gyro.attitude.x != 0 && Input.gyro.attitude.y != 0 && Input.gyro.attitude.z != 0 && Input.gyro.attitude.w != 1)
        {
            X = Input.gyro.attitude.eulerAngles.x;
            Y = Input.gyro.attitude.eulerAngles.y;
            Z = Input.gyro.attitude.eulerAngles.z;
            //transform.rotation = Input.gyro.attitude;

            previousRotation = new Vector3(X, Y, Z);

            X = Input.gyro.attitude.eulerAngles.x;
            Y = Input.gyro.attitude.eulerAngles.y;
            Z = Input.gyro.attitude.eulerAngles.z;
            Vector3 direction = previousRotation - new Vector3(X, Y, Z);

            transform.position = new Vector3();

            transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            transform.Translate(new Vector3(0, 0, -10)); //מיקום המצלמה

            X = Input.gyro.attitude.eulerAngles.x;
            Y = Input.gyro.attitude.eulerAngles.y;
            Z = Input.gyro.attitude.eulerAngles.z;
            previousRotation = new Vector3(X, Y, Z);
        }
    }
}
