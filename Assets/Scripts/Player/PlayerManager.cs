using System.Globalization;
using AI;
using UnityEngine;
using UserInterface;

public class PlayerManager : BaseVehicleManager
{
    private UIManager uiManager;
    
    protected override void Awake()
    {
        base.Awake();
        uiManager = FindObjectOfType<UIManager>();
        if (!uiManager)
        {
            Debug.LogWarning("UIManager NOT Found");
        }
    }
    
    protected override void Update()
    {
        base.Update();
        UpdateUIValues();
    }

    private void UpdateUIValues()
    {
        if (!uiManager) return;
        if (uiManager.speedUnit != null) uiManager.speedUnit.text = currentSpeed.ToString(CultureInfo.InvariantCulture);
    }
    
    public void CheckLapCount()
    {
        if (currentLapIndex > _gameManager.laps && !_gameManager.ghostMode)
        {
            currentLapIndex = _gameManager.laps;
            _gameManager.raceFinished = true;
            _gameManager.gameOverCanvas.gameObject.SetActive(true);
            
            //for (int i = 0; i < finishLineManager.checkpointParent.childCount; i++)
            //{
            //    Checkpoint checkpoint = finishLineManager.checkpointParent.GetChild(i).GetComponent<Checkpoint>();
            //    if (checkpoint.transform.GetChild(0))
            //    {
            //        checkpoint.transform.GetChild(0).gameObject.SetActive(false);
            //    }
            //}
        }
        if (currentLapIndex < 0)
        {
            currentLapIndex = 0;
        }
    }
}
