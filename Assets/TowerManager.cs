using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	[Header("General")]
	[SerializeField] LayerMask TowerLayers;
	[SerializeField] Inventory inventory;
	[SerializeField] Camera cam;
	[Space]
	[Header("Tower Placement")]
	[SerializeField] GameObject emptyTower;
	[SerializeField] LayerMask towerFreeZone;
	GameObject floatingTower;

	private void Start()
	{
		GameStats.Instance.Initialize();	
	}

	private void Update()
	{
		if (GameStats.Instance.GetCursorState() == CursorState.Free)
		{
			if (Input.GetMouseButtonDown(0))
			{
				CheckTowerAssignment();
			}
		}
		else if (GameStats.Instance.GetCursorState() == CursorState.BuyingTower)
		{
			UpdateBuyingTower();
		}

	}

	private void CheckTowerAssignment()
	{
		Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero, 0, TowerLayers);
		if (hit)
		{
			Tower tempTower = hit.collider.gameObject.GetComponent<Tower>();
			TroopPlaceholder temp;
			if (tempTower.isUnitAssigned)
			{
				temp = tempTower.RemoveUnit();
				if (!inventory.AddTroop(temp))
				{
					tempTower.AssignUnit(temp);
				}
			}
			else
			{
				temp = inventory.PopHighlightedTroop();
				if (temp != null)
				{
					tempTower.AssignUnit(temp);
				}
			}
		}
	}

	private void UpdateBuyingTower()
	{
		Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		floatingTower.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
		RaycastHit2D hit = Physics2D.BoxCast(new Vector2(mousePos.x, mousePos.y), floatingTower.GetComponent<BoxCollider2D>().size, 0, Vector2.zero,0, towerFreeZone);
		if (hit)
		{
			SetFloatingTowerColor(Color.red);
		}
		else
		{
			SetFloatingTowerColor(Color.white);
		}

		if (Input.GetMouseButtonDown(0) && !hit)
		{
			floatingTower.layer = 9;
			SetFloatingTowerColor(Color.white);
			GameStats.Instance.SetCursorState(CursorState.Free);
			floatingTower = null;
		}

	}

	private void SetFloatingTowerColor(Color c)
	{
		floatingTower.GetComponent<SpriteRenderer>().color = c;
	}

	public void BuyTowerButton()
	{
		if (GameStats.Instance.GetCursorState() == CursorState.Free)
		{
			GameStats.Instance.SetCursorState(CursorState.BuyingTower);
			floatingTower = Instantiate(emptyTower);
			floatingTower.layer = 10;
		}
		else if(GameStats.Instance.GetCursorState() == CursorState.BuyingTower)
		{
			Destroy(floatingTower);
			GameStats.Instance.SetCursorState(CursorState.Free);
		}
	}
}
