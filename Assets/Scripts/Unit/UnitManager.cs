using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitManager : MonoBehaviour
{
    public List<Unit> units;
    public List<Unit> unitsSelected;
    
    private int _unitsCurrentCount;

    public GameObject positionTargetPrefab;
    public Vector2 targetPosition;
    [SerializeField] private Vector2 positionOffset;
    
    [SerializeField] private LayerMask unitLayer;
    private Camera _camera;
    private Vector2 _origin;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _origin = _camera.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0)) SelectUnit();
        if(Input.GetMouseButtonDown(1) && unitsSelected.Count > 0) HandleUnitState();
        if (_unitsCurrentCount != unitsSelected.Count)
        {
            _unitsCurrentCount = unitsSelected.Count;
            foreach (var unit in units)
            {
                if (unitsSelected.Contains(unit))
                {
                    unit.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    unit.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    unit.gameObject.GetComponent<Unit>().currentState = states.None;
                }
            }
        }
    }

    private void HandleUnitState()
    {
        RaycastHit2D hit  = Physics2D.Raycast(_origin, Vector2.zero);
        
        if (hit.collider.gameObject.CompareTag("Resource"))
        {
            SetUnitState(states.Farm);
        }
        else if (hit.collider.gameObject.CompareTag("HunterTarget"))
        {
            SetUnitState(states.Hunt);
        }
        else if(hit.collider.gameObject.CompareTag("Ground"))
        {
            Instantiate(positionTargetPrefab, hit.point + positionOffset, Quaternion.identity);
            targetPosition = hit.point;
            SetUnitState(states.Movement);
        }
    }

    private void SetUnitState(states state)
    {
        foreach (var unit in unitsSelected)
        {
            unit.currentState = state;
        }
    }
    
    private void SelectUnit()
    {
        RaycastHit2D hit  = Physics2D.Raycast(_origin, Vector2.zero, unitLayer);
        if (hit.collider.gameObject.layer != 6) return;
        if (hit)
        {
            var hitUnitComponent = hit.collider.gameObject.GetComponent<Unit>();
            if(!unitsSelected.Contains(hitUnitComponent))
            {
                unitsSelected.Add(hitUnitComponent);
            }
            else
            {
                unitsSelected.Remove(hitUnitComponent);
            }
        }
    }
}
