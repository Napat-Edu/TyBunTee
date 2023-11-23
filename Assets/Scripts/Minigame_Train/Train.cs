using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] bool isCrashed;
    [SerializeField] Transform trainHeader;
    [SerializeField] Transform trainTail;
    [SerializeField] LineRenderer connectLiner;

    Transform nextTrainPosition;

    bool isLeftConnected;
    bool isRightConnected;

    void Awake()
    {
        isLeftConnected = false;
        isRightConnected = false;
        connectLiner.SetPosition(0, new Vector3(trainTail.transform.position.x, trainTail.transform.position.y, 0));
        connectLiner.SetPosition(1, new Vector3(trainTail.transform.position.x, trainTail.transform.position.y, 0));
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
        connectLiner.SetPosition(1, new Vector3(nextTrainPosition.transform.position.x, nextTrainPosition.transform.position.y, 0));
    }

    public Transform GetTrainPosition()
    {
        return trainHeader.transform;
    }
}
