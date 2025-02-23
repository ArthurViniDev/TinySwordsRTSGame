using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitManager unitManager;
    private void Start()
    {
        unitManager.units.Add(GetComponent<Unit>());
    }

    public void Movement()
    {
        
    }

    public void Farm()
    {
        
    }

    public void Attack()
    {
        
    }
}
