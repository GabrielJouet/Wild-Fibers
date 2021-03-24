using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EnemyInfo : MonoBehaviour
{
    private Enemy _enemy;
    public Enemy Enemy
    {
        get
        {
            return _enemy;
        }

        set
        {
            _enemy = value;
            GetComponent<Button>().enabled = true;
            transform.GetChild(0).GetComponent<Image>().sprite = Enemy.ScreenShot;
        }
    }


    public void Activate()
    {
        transform.parent.parent.parent.GetComponent<Library>().ShowEnemyInfo(this);
    }
}