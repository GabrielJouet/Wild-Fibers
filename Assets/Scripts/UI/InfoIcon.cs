using UnityEngine;

public class InfoIcon : MonoBehaviour
{
    public TowerInfo Tower { get; set; } = null;

    public EnemyInfo Enemy { get; set; } = null;

    public void Activate()
    {
        transform.parent.parent.parent.GetComponent<Library>().ShowSpecificInfo(this);
    }
}