using UnityEngine;

public class InfoIcon : MonoBehaviour
{
    public TowerData Tower { get; set; } = null;

    public Enemy Enemy { get; set; } = null;

    public void Activate()
    {
        transform.parent.parent.parent.GetComponent<Library>().ShowSpecificInfo(this);
    }
}