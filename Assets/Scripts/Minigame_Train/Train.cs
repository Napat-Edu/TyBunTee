using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    [SerializeField] bool isCrashed;
    [SerializeField] Transform trainHeader;
    [SerializeField] Image connectLiner;

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
        // connect line to next train position
    }

    public Transform GetTrainPosition()
    {
        return trainHeader.transform;
    }
}
