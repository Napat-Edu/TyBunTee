using System;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] bool isCrashed;
    [SerializeField] Transform trainHeader;

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

    public void SetTrainPosition(Transform nextPosition)
    {
        nextTrainPosition = nextPosition;
    }

    public Transform GetTrainPosition()
    {
        return trainHeader.transform;
    }
}
