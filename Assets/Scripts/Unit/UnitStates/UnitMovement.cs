using UnityEngine;

public class UnitMovement : MonoBehaviour, IUnitState
{
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private float movementSpeed;
    
    public Vector2 moveToThisPosition;
    public void UpdateState()
    {
        if (!unitManager.unitsSelected.Contains(this.GetComponent<Unit>())) return;
        moveToThisPosition = unitManager.targetPosition;
        transform.position = Vector2.MoveTowards(transform.position, unitManager.targetPosition, movementSpeed * Time.deltaTime);
    }
}
