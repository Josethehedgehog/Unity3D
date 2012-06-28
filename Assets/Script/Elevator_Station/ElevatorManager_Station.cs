using UnityEngine;
using System.Collections;

public class ElevatorManager_Station : MonoBehaviour 
{
    public float DoorWidth = 0.92f;
    public float FloorHeight = 6.6f;
    public float OpendoorSpeed = 0.01f;
    private float doorSpeed;
    public float elevatorWorkSpeed = 0.025f;
    private float WorkSpeed;
    private float addVaule = 0;
    public float DistanceAway = 7.5f;

    public bool isDiffer = true;
    public bool isEnter = false;
    public bool isWait = false;

    private GameObject Greta;
    private Transform Elevator_Main;
    private Transform OneButton;
    private Transform TwoButton;
    private Transform InButton_F;
    private Transform InButton_B;

    public enum DoorState
    {
        close,open
    }
    public bool doorRunning = false;
    public DoorState doorstate = DoorState.close;

    public enum ElevatorFloor
    { 
        one = 1, two 
    }
    public bool ElevatorRunning = false;
    public ElevatorFloor elevatorFloor;   

    #region �]�w�q������}��
    /// <summary>
    /// �]�w�q��������A
    /// </summary>
    /// <param name="state">�������A(open , close)</param>
    public void SetdoorState(DoorState state)
    {
        if (state == DoorState.open)
        {
            doorstate = DoorState.open;
            doorSpeed = Mathf.Abs(doorSpeed);
        }
        else
        {
            doorstate = DoorState.close;
            doorSpeed = -Mathf.Abs(doorSpeed);
        }
        doorRunning = true;
    }
    #endregion

    #region  �]�w�q��Ӽh
    /// <summary>
    /// �]�w���N�e�����Ӽh
    /// </summary>
    /// <param name="floor">�Ӽh</param>
    public void SetFloor(ElevatorFloor floor)
    {
        if (floor == ElevatorFloor.two)
        {
            elevatorFloor = ElevatorFloor.two;
            WorkSpeed = Mathf.Abs(WorkSpeed);
        }
        else
        {
            elevatorFloor = ElevatorFloor.one;
            WorkSpeed = -Mathf.Abs(WorkSpeed);
        }
        ElevatorRunning = true;
    }
    #endregion

    #region Initialization
    void Start()
    {
        doorSpeed = OpendoorSpeed;
        WorkSpeed = elevatorWorkSpeed;

        Greta = GameObject.Find("Greta");
        OneButton = transform.Find("oneButton");
        TwoButton = transform.Find("twoButton");
        Elevator_Main = transform.Find("Elevator_Main");
        InButton_F = transform.Find(Elevator_Main.name + "/InButton_front");
        InButton_B = transform.Find(Elevator_Main.name + "/InButton_back");
    }
    #endregion    
    
    #region Update
    void Update()
    {
        #region �W�X�d��۰�����
        if (doorstate == DoorState.open && !doorRunning)
        {
            if (getDistance() >= DistanceAway)
                SetdoorState(DoorState.close);
        }
        #endregion        

        #region ����q��W�U�����ʶq
        if (ElevatorRunning && !doorRunning)
        {
            if (Mathf.Abs(addVaule) < FloorHeight)
            {
                Elevator_Main.position += transform.TransformDirection(new Vector3(0, 0, WorkSpeed));
                if (isEnter)
                    Greta.transform.position += transform.TransformDirection(new Vector3(0, 0, WorkSpeed));
                addVaule += WorkSpeed;
            }
            else
            {
                addVaule = 0;
                ElevatorRunning = false;
                SetdoorState(DoorState.open);
            }
        }
        #endregion        

        #region ����q����}�������ʶq
        if (doorRunning && !ElevatorRunning)
        {
            if (Mathf.Abs(addVaule) < DoorWidth)
            {
                if (GetCurrentFloor() == 2)
                {
                    TwoButton.Find("doorR1").transform.position += transform.TransformDirection(new Vector3(0, doorSpeed / 2.0f, 0));
                    TwoButton.Find("doorR2").transform.position += transform.TransformDirection(new Vector3(0, doorSpeed, 0));
                    TwoButton.Find("doorL1").transform.position -= transform.TransformDirection(new Vector3(0, doorSpeed / 2.0f, 0));
                    TwoButton.Find("doorL2").transform.position -= transform.TransformDirection(new Vector3(0, doorSpeed, 0));
                }
                else
                {
                    OneButton.Find("doorR1").transform.position += transform.TransformDirection(new Vector3(0, doorSpeed / 2.0f, 0));
                    OneButton.Find("doorR2").transform.position += transform.TransformDirection(new Vector3(0, doorSpeed, 0));
                    OneButton.Find("doorL1").transform.position -= transform.TransformDirection(new Vector3(0, doorSpeed / 2.0f, 0));
                    OneButton.Find("doorL2").transform.position -= transform.TransformDirection(new Vector3(0, doorSpeed, 0));
                }

                if (GetCurrentFloor() == 1 && isDiffer)
                {
                    InButton_F.Find("doorR1").transform.position += transform.TransformDirection(new Vector3(0, doorSpeed / 2.0f, 0));
                    InButton_F.Find("doorR2").transform.position += transform.TransformDirection(new Vector3(0, doorSpeed, 0));
                    InButton_F.Find("doorL1").transform.position -= transform.TransformDirection(new Vector3(0, doorSpeed / 2.0f, 0));
                    InButton_F.Find("doorL2").transform.position -= transform.TransformDirection(new Vector3(0, doorSpeed, 0));
                }
                else
                {
                    InButton_B.Find("doorR1").transform.position -= transform.TransformDirection(new Vector3(0, doorSpeed / 2.0f, 0));
                    InButton_B.Find("doorR2").transform.position -= transform.TransformDirection(new Vector3(0, doorSpeed, 0));
                    InButton_B.Find("doorL1").transform.position += transform.TransformDirection(new Vector3(0, doorSpeed / 2.0f, 0));
                    InButton_B.Find("doorL2").transform.position += transform.TransformDirection(new Vector3(0, doorSpeed, 0));
                }
                addVaule += doorSpeed;
            }
            else
            {
                addVaule = 0;
                doorRunning = false;
            }
        }
        #endregion        
    }
    #endregion    

    #region ��o��e�q��Ҧb�Ӽh
    public int GetCurrentFloor()
    {
        return (int)elevatorFloor;
    }
    #endregion

    #region ��o�PGreta���Z��
    float getDistance()
    {
        return Vector3.Distance(transform.position, Greta.transform.position);
    }
    #endregion
    

    void OnTriggerEnter()
    {
        isEnter = true;
    }

    void OnTriggerExit()
    {
        isEnter = false;
    }
}
