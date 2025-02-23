using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<Unit> units;
    public List<Unit> unitsSelected;

    private int _selectedUnitCount;
    private int _unitsCurrentCount;
    [SerializeField] private LayerMask unitLayer;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
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
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 origin = new Vector2
                (_camera.ScreenToWorldPoint(Input.mousePosition).x, _camera.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit  = Physics2D.Raycast(origin, Vector2.zero, unitLayer);
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
}
