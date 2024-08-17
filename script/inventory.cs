using Godot;
using System;

public partial class inventory : Node2D
{
	void _on_inventory_close_pressed(){
		GetParent<Node2D>().Visible = false;
	}
}
