using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    public static CarController _instance;

    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public Rigidbody rb;
    private float brakeTorque = 0;
    public float maxBrakeTorque = 600;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical")*2;
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        brakeTorque = 0;
        if (Lap._instance.canMove)
        {
            foreach (AxleInfo axlenInfo in axleInfos)
            {
                if (axlenInfo.leftWheel.rpm > 5 && motor < 0)
                    brakeTorque = maxBrakeTorque;
                else if (axlenInfo.leftWheel.rpm < -5 && motor > 0)
                    brakeTorque = maxBrakeTorque;
                continue;
            }

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
                if (true)
                {
                    axleInfo.leftWheel.brakeTorque = brakeTorque;
                    axleInfo.rightWheel.brakeTorque = brakeTorque;
                }
            }


        }

        if (Input.GetKeyDown(KeyCode.R) && Lap._instance.start == 1 && Lap._instance.canMove == true)
        {
            if(Lap._instance.checkPointID == 0)
            {
                transform.position = new Vector3(Lap._instance.checkPoint[Lap._instance.checkPoint.Length-1].transform.position.x, Lap._instance.checkPoint[Lap._instance.checkPoint.Length-1].transform.position.y + 0.5f, Lap._instance.checkPoint[Lap._instance.checkPoint.Length-1].transform.position.z);
                transform.rotation = Quaternion.Lerp(Lap._instance.checkPoint[Lap._instance.checkPoint.Length-1].transform.rotation, Quaternion.Euler(0, 0, 90), 0.05f);
            }
            else
            {
                transform.position = new Vector3 (Lap._instance.checkPoint[Lap._instance.checkPointID-1].transform.position.x, Lap._instance.checkPoint[Lap._instance.checkPointID - 1].transform.position.y + 0.5f, Lap._instance.checkPoint[Lap._instance.checkPointID - 1].transform.position.z);
                transform.rotation = Quaternion.Lerp(Lap._instance.checkPoint[Lap._instance.checkPointID - 1].transform.rotation , Quaternion.Euler(0, 0, 90), 0.05f);
            }
            rb.velocity = new Vector3(0,0,0);
        }

    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}