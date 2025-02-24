using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitManager unitManager;
    
    private IUnitState _unitState;
    private void Start()
    {
        unitManager.units.Add(GetComponent<Unit>());
    }

    private void Update()
    {
        _unitState.UpdateState();
    }
}
