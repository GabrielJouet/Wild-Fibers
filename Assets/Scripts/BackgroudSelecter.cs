using UnityEngine;

public class BackgroudSelecter : MonoBehaviour
{
    [SerializeField]
    private InformationUIController _informationUIController;



    private void OnMouseDown()
    {
        _informationUIController.DisableEnemyInformation();
        _informationUIController.DisableTowerInformation();
    }
}