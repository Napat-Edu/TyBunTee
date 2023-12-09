using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    [SerializeField] bool isCrashed;
    [SerializeField] Transform trainHeader;
    [SerializeField] Transform trainTail;
    [SerializeField] Image liner;

    Transform nextTrainPosition;

    bool isLeftConnected;
    bool isRightConnected;

    void Awake()
    {
        isLeftConnected = false;
        isRightConnected = false;
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
        liner.rectTransform.sizeDelta = new Vector2(nextTrainPosition.position.x - trainTail.position.x + 10, 60);
        liner.rectTransform.anchoredPosition = new Vector2(liner.rectTransform.anchoredPosition.x + (nextTrainPosition.position.x - trainTail.position.x + 8) / 2, liner.rectTransform.anchoredPosition.y);
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
                EnableTrainConnection();
            }
        }
    }
}
