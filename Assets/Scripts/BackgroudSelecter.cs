using UnityEngine;

public class BackgroudSelecter : MonoBehaviour
{
    [SerializeField]
    private InformationUIController _informationUIController;



    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out RaycastHit hit, Mathf.Infinity))
            {
                if(hit.collider.gameObject == gameObject)
                {
                    _informationUIController.DisableEnemyInformation();
                    _informationUIController.DisableTowerInformation();
                    _informationUIController.DisableTowerChooseButton();
                    _informationUIController.DisableTowerSellButton();
                }
            }
        }
    }
}