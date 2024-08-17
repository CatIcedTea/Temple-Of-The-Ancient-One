using Godot;
using System;

public partial class floatingEssence : enemy
{
	public override void _Ready()
	{
		name = "Floating Essence";
		hp = 17;
		currHP = 17;
		damage = 4;
		ac = 4;
		dr = -1;
		toHit = 3;
		xp = 15;
		dropChance = 25;
		itemPool = new string[]{"Rabbit's Foot", "Hand Scythe", "Cultist Knife", "Witch's Charm"};

		base._Ready();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
