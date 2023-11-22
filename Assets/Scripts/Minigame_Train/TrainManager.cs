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
    [SerializeField] GameObject pointer;

    [SerializeField] Transform trainInitialPosition;

    int currentPointerIndex;
    int trainAmount;
    int crashedAmount;
    int rangeBetweenTrain;
    int rangeBetweenPointerAndTrain;

    void Awake()
    {
        currentPointerIndex = 0;
        trainAmount = 0;
        crashedAmount = 0;
        rangeBetweenPointerAndTrain = 150;
        rangeBetweenTrain = 300;

        InitTrainSection(0);

        Train train = trainSection.GetComponentInChildren<Train>();
        pointer.transform.position = train.transform.position + new Vector3(0, -rangeBetweenPointerAndTrain, 0);
    }

    void InitTrainSection(int mode)
    {
        if (mode == 0)
        {
            // easy mode
            trainAmount = 10;
            crashedAmount = 1;
        }

        int currentCrashedAmount = 0;
        Vector3 genTrainPosition = trainInitialPosition.transform.position;
        for (int i = 0; i < trainAmount; i++)
        {
            GameObject newGameObject;

            if (currentCrashedAmount != crashedAmount && (Random.Range(0, 1.0f) < 0.5 || (i + 1 + crashedAmount >= trainAmount)))
            {
                newGameObject = Instantiate(crashedTrain, genTrainPosition, Quaternion.identity);
                currentCrashedAmount += 1;
            }
            else
            {
                newGameObject = Instantiate(normalTrain, genTrainPosition, Quaternion.identity);
            }

            newGameObject.transform.SetParent(trainSection.transform);
            genTrainPosition += new Vector3(rangeBetweenTrain, 0, 0);
        }
    }

    public void GoLeftBokey()
    {
        Train[] trains = trainSection.GetComponentsInChildren<Train>();
        if (currentPointerIndex > 0)
        {
            currentPointerIndex -= 1;
            pointer.transform.position = trains[currentPointerIndex].transform.position + new Vector3(0, -rangeBetweenPointerAndTrain, 0);
        }
    }

    public void GoRightBokey()
    {
        Train[] trains = trainSection.GetComponentsInChildren<Train>();
        if (currentPointerIndex < trainAmount - 1)
        {
            currentPointerIndex += 1;
            pointer.transform.position = trains[currentPointerIndex].transform.position + new Vector3(0, -rangeBetweenPointerAndTrain, 0);
        }
    }
}
