using Godot;
using System;

public partial class cultist : enemy
{
	public override void _Ready()
	{
		name = "Cultist";
		hp = 15;
		currHP = 14;
		damage = 2;
		ac = 1;
		dr = 1;
		toHit = 1;
		xp = 15;
		dropChance = 25;
		itemPool = new string[]{"Knife", "Cultist Knife", "Cultist Cloak"};

		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
