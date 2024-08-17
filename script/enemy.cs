using System;
using Godot;

public partial class enemy : Node2D
{
	public string name;
	public int hp = 10;
	public int currHP = 10;
	public int damage = 2;
	public int ac = 3;
	public int dr = 0;
	public int toHit = 0;
	public bool isAlive = true;
	public int xp = 10;
	public string [] itemPool;
	public int dropChance = 25;
	public bool isCharging = false;
	player play;
	items item;

	PanelContainer panel;
	TextureProgressBar hpBar;
	Label hpText;

	public override void _Ready()
	{
		play = GetParent().GetParent().GetNode<player>("Player");

		panel = GetNode<PanelContainer>("EnemyPanel");
		hpBar = panel.GetNode<VBoxContainer>("VBoxContainer").GetNode<TextureProgressBar>("HP");
		hpText = hpBar.GetNode<Label>("HPText");

		panel.GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Name").Text = name;
		hpBar.MaxValue = hp;
		hpBar.Value = currHP;
		hpText.Text = currHP + "/" + hp;
		panel.GetNode<VBoxContainer>("VBoxContainer").GetNode<HBoxContainer>("InfoBox").GetNode<VBoxContainer>("Left").GetNode<Label>("Damage").Text = "DAMAGE: " + damage;
		panel.GetNode<VBoxContainer>("VBoxContainer").GetNode<HBoxContainer>("InfoBox").GetNode<VBoxContainer>("Left").GetNode<Label>("AC").Text = "AC: " + ac;
		panel.GetNode<VBoxContainer>("VBoxContainer").GetNode<HBoxContainer>("InfoBox").GetNode<VBoxContainer>("Right").GetNode<Label>("DR").Text = "DR: " + dr;
		if(toHit >= 0)
			panel.GetNode<VBoxContainer>("VBoxContainer").GetNode<HBoxContainer>("InfoBox").GetNode<VBoxContainer>("Right").GetNode<Label>("ToHit").Text = "To Hit: +" + toHit;
		else
			panel.GetNode<VBoxContainer>("VBoxContainer").GetNode<HBoxContainer>("InfoBox").GetNode<VBoxContainer>("Right").GetNode<Label>("ToHit").Text = "To Hit: -" + toHit;

		currHP = hp;
	}

	public override void _Process(double delta)
	{
		hpBar.Value = currHP;
		hpText.Text = currHP + "/" + hp;

		if(currHP <= 0)
			isAlive = false;
	}

	public void attack(){
		if(new Random().Next(0, 11) == 10){
			GetTree().CurrentScene.GetNode<Panel>("TextBox").GetNode<Label>("Text").Text += name + " is charging up a strong attack";
		}
		play.currHP -= damage;
	}

	public void takeDamage(int dam){
		currHP -= dam;
	}

	public void dropItem(){
		if(new Random().Next(0, 101) <= dropChance){
			var itemDrop = ResourceLoader.Load<PackedScene>("res://scene/item.tscn");
			item = (items)itemDrop.Instantiate();

			item.name = itemPool[new Random().Next(0, itemPool.Length)];

			GetTree().CurrentScene.GetNode<player>("Player").GetNode<Node2D>("Inventory").GetNode<PanelContainer>("InventoryList").GetNode<VBoxContainer>("VBoxContainer").AddChild(item);
			GetTree().CurrentScene.GetNode<Panel>("TextBox").GetNode<Label>("Text").Text += "\nEnemy has dropped an item: " + item.name;
		}
	}
}
