using Godot;
using System;

public partial class awakenedCultist : enemy
{
	public override void _Ready()
	{
		name = "Awakened Cultist";
		hp = 25;
		currHP = 25;
		damage = 5;
		ac = 2;
		dr = 2;
		toHit = 4;
		xp = 20;
		dropChance = 25;
		itemPool = new string[]{"Hand Axe", "Hand Scythe", "Cultist Knife", "Cultist Cloak"};

		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
