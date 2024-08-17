using Godot;
using System;

public partial class fragment : enemy
{
	public override void _Ready()
	{
		name = "Fragment of The Ancient One";
		hp = 30;
		currHP = 30;
		damage = 5;
		ac = 3;
		dr = 2;
		toHit = 4;
		xp = 25;
		dropChance = 33;
		itemPool = new string[]{"Ritual Sword", "Bloody Idol", "Witch's Charm"};

		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
