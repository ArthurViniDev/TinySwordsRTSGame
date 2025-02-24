using UnityEngine;

public enum states { Idle, Movement, Farm, Hunt }

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitManager unitManager;
    private UnitMovement _unitMovement;
    private UnitFarm _unitFarm;
    private UnitHunt _unitHunt;
    
    public states currentState;
    private void Start()
    {
        unitManager.units.Add(GetComponent<Unit>());
        _unitMovement = GetComponent<UnitMovement>();
        _unitFarm = GetComponent<UnitFarm>();
        _unitHunt = GetComponent<UnitHunt>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case states.Movement:
                _unitMovement.UpdateState();
                break;
            case states.Farm:
                _unitFarm.UpdateState();
                break;
            case states.Hunt:
                _unitHunt.UpdateState();
                break;
        }
    }

    public void UnitUpdate()
    {
        
    }
}
