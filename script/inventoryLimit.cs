using Godot;
using System;

public partial class inventoryLimit : Node2D
{
	int currItems;
	int limit = 5;
	VBoxContainer inv;
	Label text;
	public override void _Ready()
	{
		inv = GetNode<PanelContainer>("InventoryList").GetNode<VBoxContainer>("VBoxContainer");
		text = inv.GetNode<Label>("Label");
	}

	public override void _Process(double delta)
	{
		currItems = inv.GetChildCount(false) - 1;
		text.Text = "Inventory " + currItems + "/" + limit;
	}

	public bool isFull(){
		return currItems >= limit;
	}
}
