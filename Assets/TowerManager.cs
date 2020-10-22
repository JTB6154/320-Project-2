using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
	public List<GameObject> projectiles;
	[SerializeField] TextMeshProUGUI TowerPurchaseButtonText;
	[SerializeField] GameObject EnemyManager;

	public float barDisplay; //current progress
	public Vector2 pos = new Vector2(200, 500);
	public Vector2 size = new Vector2(600, 20);
	public Texture2D emptyTex;
	public Texture2D fullTex;

	private void Start()
	{
		GameStats.Instance.Initialize();	
	}

	void OnGUI()
	{
		//draw the background:
		GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
		GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

		//draw the filled-in part:
		GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
		GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
		GUI.EndGroup();
		GUI.EndGroup();
	}

	private void Update()
	{
		barDisplay = (float)(GameStats.Instance.GetPlayerHealth())/100;

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

		if (Input.GetMouseButtonDown(0) && !hit && GameStats.Instance.PurchaseTower())
		{
			floatingTower.layer = 9;
			SetFloatingTowerColor(Color.white);
			GameStats.Instance.SetCursorState(CursorState.Free);
			floatingTower = null;
			SetTowerPurchaseButtonText();
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
			floatingTower.GetComponent<Tower>().enemyQueueHolder = EnemyManager;
			floatingTower.layer = 10;
			SetTowerPurchaseButtonText();
		}
		else if(GameStats.Instance.GetCursorState() == CursorState.BuyingTower)
		{
			Destroy(floatingTower);
			GameStats.Instance.SetCursorState(CursorState.Free);
			SetTowerPurchaseButtonText();
		}
	}

	private void SetTowerPurchaseButtonText()
	{
		if (GameStats.Instance.GetCursorState() == CursorState.BuyingTower)
		{
			TowerPurchaseButtonText.text = "Cancel";
		}
		else if (GameStats.Instance.GetCursorState() == CursorState.Free)
		{
			TowerPurchaseButtonText.text = "Buy Tower: " + GameStats.Instance.GetCurrentTowerCost();
		}
	}
}
