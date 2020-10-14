using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	[SerializeField] LayerMask TowerLayers;
	[SerializeField] Inventory inventory;
	[SerializeField] Camera cam;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{

			Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero,0,TowerLayers);
			if (hit)
			{
			
				TroopPlaceholder temp = inventory.PopHighlightedTroop();
				if (temp != null)
				{
					hit.collider.gameObject.GetComponent<Tower>().AssignUnit(temp);
					
				}
			}

		}
	}
}
