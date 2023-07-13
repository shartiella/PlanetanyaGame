
using UnityEngine;
using TMPro;


public class gyroControl : MonoBehaviour
{
    //public TextMeshProUGUI txt;
    private float X;
    private float Y;
    private float Z;
    private Quaternion initialRotation;
    private Vector3 previousRotation;
    //[SerializeField] private GameObject camPos;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled= true;
        initialRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        //txt.text = "DEVICE rotation: " + Input.gyro.attitude.eulerAngles + "\n rotationRate: "+ Input.gyro.rotationRate + "\n rotationRateUnbiased: " + Input.gyro.rotationRateUnbiased;
        //txt.text += "\n OBJECT position: " + transform.position +"\n OBJECT rotation" + transform.rotation.eulerAngles;
        //txt.text += "\n CAMERA position: " + camPos.transform.position + "\n camera rotation: " + camPos.transform.rotation.eulerAngles;

        if (Input.gyro.attitude.x != 0 && Input.gyro.attitude.y != 0 && Input.gyro.attitude.z != 0 && Input.gyro.attitude.w != 1)
        {
            //X = Input.gyro.attitude.eulerAngles.x;
            //Y = Input.gyro.attitude.eulerAngles.y;
            //Z = Input.gyro.attitude.eulerAngles.z;
            ////transform.rotation = Input.gyro.attitude;

            //previousRotation = new Vector3(X, Y, Z);

            //X = Input.gyro.attitude.eulerAngles.x;
            //Y = Input.gyro.attitude.eulerAngles.y;
            //Z = Input.gyro.attitude.eulerAngles.z;
            //Vector3 direction = previousRotation - new Vector3(X, Y, Z);

            transform.position = new Vector3();
            //transform.rotation = new Quaternion();

            //transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            //transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);

            //transform.Translate(new Vector3(0, 0, 0));

            //X = Input.gyro.attitude.eulerAngles.x;
            //Y = Input.gyro.attitude.eulerAngles.y;
            //Z = Input.gyro.attitude.eulerAngles.z;
            //previousRotation = new Vector3(X, Y, Z);

            transform.Rotate(new Vector3(1, 0, 0), -Input.gyro.rotationRate.x);
            transform.Rotate(new Vector3(0, 1, 0), -Input.gyro.rotationRate.y, Space.World);
            //transform.Rotate(new Vector3(0, 0, 1), Input.gyro.rotationRate.z);
        }
    }
}
