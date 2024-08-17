using Godot;
using System;

public partial class items : TextureButton
{
	public enum itemType{
		WEAPON,
		TRINKET
	}

	[Export] public string name;
	[Export] public itemType type;

	bool isEquipped = false;
	Label text;

	Node2D equipPanel;

	player play;
	Label log;

	public override void _Ready()
	{
		text = GetNode<Label>("Label");
		text.Text = name;

		play = GetParent().GetParent().GetParent().GetParent<player>();
		equipPanel = GetNode<Node2D>("EquipMenu");
		log = GetTree().CurrentScene.GetNode<Panel>("TextBox").GetNode<Label>("Text");

		if(name == "Knife"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Knife [Weapon]\nJust a plain, sharp knife";
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nDamage: 1 AC: +1 To Hit: +1";
		}
		else if(name == "Crowbar"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Crowbar [Weapon]\nWielding this makes you feel like you're a theoretical physicist";	
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nDamage: 2";	
		}
		else if(name == "Hand Axe"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Hand Axe [Weapon]\nA small axe that can be held in one hand";
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nDamage: 3";	
		}
		else if(name == "Cultist Knife"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Cultist Knife [Weapon]\nAn ornamental knife with strange symbols and blood of its previous victim or possibly owner";
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nDamage: 1 AC: +1 To Hit: +1 Memory: +1";
		}
		else if(name == "Ritual Sword"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Ritual Sword [Weapon]\nA decorated sword that was previous used in a ritual";
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nDamage: 3 AC: +1 To Hit: +3";
		}
		else if(name == "Shovel"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Shovel [Weapon]\nGood for digging the grave of your enemies, or your own";
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nDamage: 3 AC: -1";
		}
		else if(name == "Hand Scythe"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Hand Scythe [Weapon]\nA fast tool to cull its victims";
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nDamage: 2 AC: +3 To Hit: +3";

		}
		//Trinkets
		else if(name == "Cultist Cloak"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Cultist Cloak [Trinket]\nA simple cloak with ample protection";
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nDR: 2";
		}
		else if(name == "Bloody Idol"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Bloody Idol [Trinket]\nA small statuette of painted with blood";
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nDamage: +1";
		}
		else if(name == "Rabbit's Foot"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Rabbit's Foot [Trinket]\nAn old, traditional good luck charm";
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nAC: +3";
		}
		else if(name == "Witch's Charm"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Witch Charm [Trinket]\nAn occultic charm inscribed with various hexes";
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nSanity: +2 Memory: +2";
		}
		else if(name == "Wet Tome"){
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text = "Wet Tome [Trinket]\nA damp tome containing various rituals";
			equipPanel.GetNode<PanelContainer>("EquipPanel").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label").Text += "\nSanity: +4";
		}
	}

	public override void _Process(double delta)
	{
		if(isEquipped){
			text.Text = name + "(E)";
		}
		else{
			text.Text = name;
		}
	}

	private void equip(){
		//Weapons
		if(name == "Knife"){
			type = itemType.WEAPON;

			if(play.weaponEquipped){
				log.Text = "Another weapon is already currently equipped.";
			}
			else {
				log.Text = "Equipping " + name;
				play.additionalDamage += 1;
				play.additionalAC += 1;
				play.additionalToHit += 2;

				isEquipped = true;
				play.weaponEquipped = true;
			}
		}
		else if(name == "Crowbar"){
			type = itemType.WEAPON;

			if(play.weaponEquipped){
				log.Text = "Another weapon is already currently equipped.";
			}
			else {
			log.Text = "Equipping " + name;
			play.additionalDamage += 2;

			isEquipped = true;
			play.weaponEquipped = true;
			}
		}
		else if(name == "Hand Axe"){
			type = itemType.WEAPON;

			if(play.weaponEquipped){
				log.Text = "Another weapon is already currently equipped.";
			}
			else {
			log.Text = "Equipping " + name;
			play.additionalDamage += 3;

			isEquipped = true;
			play.weaponEquipped = true;
			}
		}
		else if(name == "Cultist Knife"){
			type = itemType.WEAPON;

			if(play.weaponEquipped){
				log.Text = "Another weapon is already currently equipped.";
			}
			else {
			log.Text = "Equipping " + name;
			play.additionalDamage += 1;
			play.additionalAC += 1;
			play.additionalToHit += 1;
			play.additionalMEM += 1;

			isEquipped = true;
			play.weaponEquipped = true;
			}
		}
		else if(name == "Ritual Sword"){
			type = itemType.WEAPON;

			if(play.weaponEquipped){
				log.Text = "Another weapon is already currently equipped.";
			}
			else {
			log.Text = "Equipping " + name;
			play.additionalDamage += 3;
			play.additionalToHit += 3;
			play.additionalAC += 1;

			isEquipped = true;
			play.weaponEquipped = true;
			}
		}
		else if(name == "Shovel"){
			type = itemType.WEAPON;

			if(play.weaponEquipped){
				log.Text = "Another weapon is already currently equipped.";
			}
			else {
			log.Text = "Equipping " + name;
			play.additionalDamage += 3;
			play.additionalAC += -1;

			isEquipped = true;
			play.weaponEquipped = true;
			}
		}
		else if(name == "Hand Scythe"){
			type = itemType.WEAPON;

			if(play.weaponEquipped){
				log.Text = "Another weapon is already currently equipped.";
			}
			else {
			log.Text = "Equipping " + name;
			play.additionalDamage += 2;
			play.additionalToHit += 3;
			play.additionalAC += 3;

			isEquipped = true;
			play.weaponEquipped = true;
			}
		}
		//Trinkets
		else if(name == "Cultist Cloak"){
			type = itemType.TRINKET;

			if(play.trinketEquipped){
				log.Text = "Another trinket is already currently equipped.";
			}
			else {
			log.Text = "Equipping " + name;
			play.additionalDR += 2;

			isEquipped = true;
			play.trinketEquipped = true;
			}
		}
		else if(name == "Bloody Idol"){
			type = itemType.TRINKET;

			if(play.trinketEquipped){
				log.Text = "Another trinket is already currently equipped.";
			}
			else {
			log.Text = "Equipping " + name;
			play.additionalDamage += 1;

			isEquipped = true;
			play.trinketEquipped = true;
			}
		}
		else if(name == "Rabbit's Foot"){
			type = itemType.TRINKET;

			if(play.trinketEquipped){
				log.Text = "Another trinket is already currently equipped.";
			}
			else {
			log.Text = "Equipping " + name;
			play.additionalAC += 3;

			isEquipped = true;
			play.trinketEquipped = true;
			}
		}
		else if(name == "Witch's Charm"){
			type = itemType.TRINKET;

			if(play.trinketEquipped){
				log.Text = "Another trinket is already currently equipped.";
			}
			else {
			log.Text = "Equipping " + name;
			play.additionalMEM += 2;
			play.additionalSanity += 2;

			isEquipped = true;
			play.trinketEquipped = true;
			}
		}
		else if(name == "Wet Tome"){
			type = itemType.TRINKET;

			if(play.trinketEquipped){
				log.Text = "Another trinket is already currently equipped.";
			}
			else {
			log.Text = "Equipping " + name;
			play.additionalSanity += 4;

			isEquipped = true;
			play.trinketEquipped = true;
			}
		}
	}

	private void unEquip(){
		//Weapons
		if(!isEquipped)
				log.Text = name + " is not currently equipped";
		else{
			log.Text = "Unequipping " + name;

			if(name == "Knife"){
					play.additionalDamage -= 1;
					play.additionalAC -= 1;
					play.additionalToHit -= 2;

					isEquipped = false;
					play.weaponEquipped = false;
			}
			else if(name == "Crowbar"){
				play.additionalDamage -= 2;

				isEquipped = false;
				play.weaponEquipped = false;
			}
			else if(name == "Hand axe"){
				play.additionalDamage -= 3;

				isEquipped = false;
				play.weaponEquipped = false;
			}
			else if(name == "Cultist Knife"){
				play.additionalDamage -= 2;
				play.additionalAC -= 1;
				play.additionalToHit -= 1;
				play.additionalMEM -= 1;

				isEquipped = false;
				play.weaponEquipped = false;
			}
			else if(name == "Ritual Sword"){
				play.additionalDamage -= 3;
				play.additionalToHit -= 2;
				play.additionalAC -= 1;

				isEquipped = false;
				play.weaponEquipped = false;
			}
			else if(name == "Shovel"){
				play.additionalDamage -= 4;
				play.additionalAC -= -1;

				isEquipped = false;
				play.weaponEquipped = false;
			}
			else if(name == "Hand Scythe"){
				play.additionalDamage -= 2;
				play.additionalToHit -= 2;
				play.additionalAC -= 3;

				isEquipped = false;
				play.weaponEquipped = false;
			}
			//Trinkets
			else if(name == "Cultist Cloak"){
				play.additionalAC -= 1;
				play.additionalDR -= 2;

				isEquipped = false;
				play.trinketEquipped = false;
			}
			else if(name == "Bloody Idol"){
				play.additionalDamage -= 1;

				isEquipped = false;
				play.trinketEquipped = false;
			}
			else if(name == "Rabbit's Foot"){
				play.additionalAC -= 2;

				isEquipped = false;
				play.trinketEquipped = false;
			}
			else if(name == "Witch's Charm"){
				play.additionalMEM -= 2;
				play.additionalSanity -= 2;

				isEquipped = false;
				play.trinketEquipped = false;
			}
			else if(name == "Wet Tome"){
				play.additionalSanity -= 4;

				isEquipped = false;
				play.trinketEquipped = false;
			}
		}
	}

	void _on_pressed(){
		GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("click_soft").Play();
		equipPanel.Visible = true;
	}

	void _on_close_equip_pressed(){
		GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("click_soft").Play();
		equipPanel.Visible = false;
	}

	void _on_equip_pressed(){
		GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("click_soft").Play();
		if(!isEquipped)
			GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("item").Play();
		log.VisibleCharacters = 0;
		if(GetTree().CurrentScene.GetNode<combat_handler>("CombatHandler").isInCombat){
			log.Text = "Can't change equipment while in combat";
		}
		else{
			equip();
			equipPanel.Visible = false;
		}
	}

	void _on_unequip_pressed(){
		GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("click_soft").Play();
		if(isEquipped)
			GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("item").Play();
		log.VisibleCharacters = 0;
		if(GetTree().CurrentScene.GetNode<combat_handler>("CombatHandler").isInCombat){
			log.Text = "Can't change equipment while in combat";
		}
		else{
			unEquip();
			equipPanel.Visible = false;
		}
	}

	void _on_discard_pressed(){
		GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("click_soft").Play();
		if(isEquipped){
			log.VisibleCharacters = 0;
			log.Text = "You need to unequip this item to discard it";
		}
		else
			QueueFree();
	}
}
