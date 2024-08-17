using Godot;
using System;

public partial class possessedWater : enemy
{
	public override void _Ready()
	{
		name = "Possessed Water";
		hp = 20;
		currHP = 20;
		damage = 1;
		ac = -3;
		dr = 3;
		toHit = 0;
		xp = 15;
		dropChance = 25;
		itemPool = new string[]{"Crowbar", "Wet Tome", "Shovel"};

		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
