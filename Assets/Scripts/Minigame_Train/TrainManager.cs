using UnityEngine;
using UnityEngine.UI;

public class TrainManager : MonoBehaviour
{
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    [SerializeField] Button saveLeftTrainButton;
    [SerializeField] Button saveRightTrainButton;

    [SerializeField] GameObject trainSection;
    [SerializeField] GameObject normalTrain;
    [SerializeField] GameObject crashedTrain;

    [SerializeField] Transform trainInitialPosition;

    void Awake()
    {
        InitTrainSection(0);
    }

    void InitTrainSection(int mode)
    {
        int trainAmount = 0;
        int crashedAmount = 0;

        if (mode == 0)
        {
            // easy mode
            trainAmount = 4;
            crashedAmount = 1;
        }

        int currentCrashedAmount = 0;
        Vector3 genTrainPosition = trainInitialPosition.transform.position;
        for (int i = 0; i < trainAmount; i++)
        {
            GameObject newGameObject;

            if (currentCrashedAmount != crashedAmount && (Random.Range(0, 1.0f) < 0.5 || ((i + 1) + crashedAmount >= trainAmount)))
            {
                newGameObject = Instantiate(crashedTrain, genTrainPosition, Quaternion.identity);
                currentCrashedAmount += 1;
            }
            else
            {
                newGameObject = Instantiate(normalTrain, genTrainPosition, Quaternion.identity);
            }

            newGameObject.transform.SetParent(trainSection.transform);
            genTrainPosition += new Vector3(200, 0, 0);
        }
    }
}
