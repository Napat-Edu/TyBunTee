using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    [SerializeField] bool isCrashed;
    [SerializeField] Transform trainHeader;
    [SerializeField] Transform trainTail;
    [SerializeField] Image liner;
    [SerializeField] Button trainButton;

    Transform nextTrainPosition;

    bool isLeftConnected;
    bool isRightConnected;
    bool isReConnectLineState;

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);

    void Awake()
    {
        isLeftConnected = false;
        isRightConnected = false;
        isReConnectLineState = false;
        trainButton.enabled = false;
    }

    void Update()
    {
        if (isReConnectLineState)
        {
            Vector2 mousePos = Input.mousePosition;
            float angleDegree = Mathf.Rad2Deg * Mathf.Atan((mousePos.y - trainTail.position.y) / (mousePos.x - trainTail.position.x));
            float hypoLength = (mousePos.x - trainTail.position.x) / Mathf.Cos(Mathf.Deg2Rad * angleDegree);

            liner.rectTransform.sizeDelta = new Vector2(hypoLength, 60);
            liner.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angleDegree));

            // SetCursorPos();
        }
    }

    public void SetLeftConnected(bool isConnect)
    {
        isLeftConnected = isConnect;
    }

    public void SetRightConnected(bool isConnect)
    {
        isRightConnected = isConnect;
    }

    public void SetNextTrainPosition(Transform nextPosition)
    {
        nextTrainPosition = nextPosition;
        ConnectLine();
    }

    public Transform GetTrainPosition()
    {
        return trainHeader.transform;
    }

    void ConnectLine()
    {
        liner.rectTransform.sizeDelta = new Vector2(nextTrainPosition.position.x - trainTail.position.x + 8, 60);
        isRightConnected = true;
    }

    public void DisableTrainConnection()
    {
        liner.enabled = false;
        isRightConnected = false;
    }

    public void EnableTrainConnection()
    {
        liner.enabled = true;
        isRightConnected = true;
    }

    public void ConnectTrain()
    {
        if (!isRightConnected)
        {
            TrainManager trainManager = FindObjectOfType<TrainManager>();
            Train nextTrain = trainManager.GetNextTrainInfo();
            if (nextTrain != null)
            {
                // SetNextTrainPosition(nextTrain.transform);
                isReConnectLineState = true;
                liner.enabled = true;
                nextTrain.EnableTrainButton();
            }
        }
    }

    public void EnableTrainButton()
    {
        trainButton.enabled = true;
    }

    public void DisableTrainButton()
    {
        trainButton.enabled = false;
    }

    public void ClickTrainButtonConnect()
    {
        TrainManager trainManager = FindObjectOfType<TrainManager>();
        Train beforeTrain = trainManager.GetBeforeTrainInfo();

        DisableTrainButton();

        beforeTrain.SetIsReConnectLineState();
        beforeTrain.EnableTrainConnection();
        beforeTrain.ConnectLine();
    }

    public void SetIsReConnectLineState()
    {
        isReConnectLineState = false;
    }
}
