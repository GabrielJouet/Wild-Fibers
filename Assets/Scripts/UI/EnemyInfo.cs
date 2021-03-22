using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            GetComponent<Image>().sprite = Enemy.ScreenShot;
        }
    }


    public void Activate()
    {
        transform.parent.parent.parent.GetComponent<Library>().ShowEnemyInfo(this);
    }
}