using UnityEngine;
using UnityEngine.UI;

public class TrainManager : MonoBehaviour
{
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    [SerializeField] Button saveLeftTrainButton;
    [SerializeField] Button saveRightTrainButton;

    [SerializeField] GameObject trainSection;

    int currentTrainIndex;

    void Awake()
    {
        currentTrainIndex = 0;
        GameObject gameObject = new();
        gameObject.transform.parent = trainSection.transform;
    }
}
