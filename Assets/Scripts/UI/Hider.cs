using UnityEngine;

public class Hider : MonoBehaviour
{
    //Hider collider
    [SerializeField]
    private BoxCollider2D _hiderCollider;

    [SerializeField]
    private PauseController _pauseController;

    [SerializeField]
    private LevelController _levelController;



    //Initialize the component
    private void Start()
    {
        _hiderCollider.size = new Vector2(Screen.width, Screen.height);
    }



    //Update method, called each frame
    private void Update()
    {
        //If we press mouse button
        //We will check what type of object we pressed
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 90);
            
            if (hit.collider != null && hit.collider.GetComponent<Hider>() && !_levelController.Ended)
                _pauseController.PauseGame(true);
        }
    }
}
