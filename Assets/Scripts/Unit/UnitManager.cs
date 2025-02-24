using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<Unit> units;
    public List<Unit> unitsSelected;

    private int _selectedUnitCount;
    private int _unitsCurrentCount;
    
    [SerializeField] private LayerMask unitLayer;
    //[SerializeField] private Unit unit;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) SelectUnit();
        if(Input.GetMouseButtonDown(1) && _selectedUnitCount > 0) HandleUnitState();
        if (_unitsCurrentCount != _selectedUnitCount)
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
                }
            }
        }
    }

    private void HandleUnitState()
    {
        Vector2 origin = new Vector2
            (_camera.ScreenToWorldPoint(Input.mousePosition).x, _camera.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit  = Physics2D.Raycast(origin, Vector2.zero);
        
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
        Vector2 origin = new Vector2
            (_camera.ScreenToWorldPoint(Input.mousePosition).x, _camera.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit  = Physics2D.Raycast(origin, Vector2.zero, unitLayer);
        if (hit.collider.gameObject.layer != 6) return;
        if (hit)
        {
            var hitUnitComponent = hit.collider.gameObject.GetComponent<Unit>();
            if(!unitsSelected.Contains(hitUnitComponent))
            {
                unitsSelected.Add(hitUnitComponent);
                _selectedUnitCount++;
            }
            else
            {
                unitsSelected.Remove(hitUnitComponent);
                _selectedUnitCount--;
            }
        }
    }
}
