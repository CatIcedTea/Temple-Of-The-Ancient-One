using Godot;
using System;

public partial class ancientone : enemy
{
	public override void _Ready()
	{
		name = "The Ancient One";
		hp = 45;
		currHP = 45;
		damage = 7;
		ac = 5;
		dr = 3;
		toHit = 5;
		xp = 100;
		dropChance = 0;
		itemPool = new string[]{};

		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
