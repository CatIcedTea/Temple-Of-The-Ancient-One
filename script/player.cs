using Godot;
using System;
using System.Xml.XPath;

public partial class player : Node2D
{
	public int strength = 1;
	int initStrength = 1;
	public int constitution = 1;
	int initConstitution = 1;
	public int dexterity = 1;
	int initDexterity = 1;
	public int intelligence = 1;
	int initIntelligence = 1;
	public int mind = 1;
	int initMind = 1;

	int hp = 10;
	public int currHP = 10;
	int initHP = 10;
	public int sanity = 10;
	public int additionalSanity = 0;
	public int currSanity = 10;
	int initSanity = 10;
	public int damage = 1;
	public int curse = 0;
	public int ac = 3;
	public int additionalAC = 0;
	public int dr = 0;
	public int additionalDR = 0;
	public int ba = 0;
	public int additionalDamage = 0;
	public int toHit = 0;
	public int additionalToHit = 0;
	public int mem = 1;
	public int additionalMEM = 0;
	public int currMem = 1;
	public int xp = 0;
	int hexSave = 0;
	public int skillpoint = 3, initSkillpoint = 3;
	bool isResting = false;
	bool firstTime = true;

	public bool isAlive = true;

	public bool weaponEquipped;
	public bool trinketEquipped;

	Label log;
	Panel characterSheet;
	Node2D inventory;
	skillcheck skillcheckEvent;

	Label hpDisplay;
	Label sanityDisplay;
	Label curseDisplay;
	Label xpDisplay;

	TextureProgressBar hpBar;
	TextureProgressBar sanityBar;

	Node2D sfxNode;

	public override void _Ready()
	{
		log = GetParent().GetNode<Panel>("TextBox").GetNode<Label>("Text");

		characterSheet =  GetNode<Panel>("CharacterSheet");
		skillcheckEvent = GetNode<skillcheck>("Skillcheck");

		hpBar = GetNode<TextureProgressBar>("HpBar");
		sanityBar = GetNode<TextureProgressBar>("SanityBar");

		hpDisplay = hpBar.GetNode<Label>("HP");
		sanityDisplay = sanityBar.GetNode<Label>("Sanity");
		curseDisplay = GetNode<Label>("Curse");
		xpDisplay = GetNode<Label>("Xp");
		inventory = GetNode<Node2D>("Inventory");

		sfxNode = GetParent().GetNode<Node2D>("Sound").GetNode<Node2D>("SFX");
	}

	public override void _Process(double delta)
	{
		if(characterSheet.Visible == true)
			update_stats();
		
		//Secondary stats
		if(characterSheet.Visible){
			initHP = 7 + constitution*3;
			initSanity = 8 + intelligence*2 + (mind-1)/2 + additionalSanity;
		}
		else{
			hp = 7 + constitution*3;
			sanity = 8 + intelligence*2 + (mind-1)/2 + additionalSanity;
		}

		damage = 1 + ba + additionalDamage;
		toHit = dexterity-1 + additionalToHit;
		ac = 3 + (int)(dexterity * 1.5f) + additionalAC;
		dr = (constitution-1)/2 + additionalDR;
		ba = strength/2;
		mem = intelligence + mind/2 + additionalMEM;
		hexSave = mind - 1;

		if(currHP > hp){
			currHP = hp;
		}
		if(currSanity > sanity){
			currSanity = sanity;
		}
		if(currMem > mem){
			currMem = mem;
		}

		if(currHP <= 0 || currSanity <= 0 || curse >= 100){
			isAlive = false;
		}

		//HP and Sanity text
		hpDisplay.Text = currHP + "/" + hp;
		sanityDisplay.Text = currSanity + "/" + (sanity);
		curseDisplay.Text = "Curse: " + curse + "%";

		//HP and Sanity bar
		hpBar.MaxValue = hp;
		hpBar.Value = currHP;
		sanityBar.MaxValue = sanity;
		sanityBar.Value = currSanity;

		//Display XP
		if(xp >= 100){
			xpDisplay.Text = "Level Up Available On Rest";
			xpDisplay.GetNode<AnimationPlayer>("AnimationPlayer").Play("blink");
		}
		else{
			xpDisplay.GetNode<AnimationPlayer>("AnimationPlayer").Stop();
			xpDisplay.Text = "Exp: " + xp;
		}
	}

	//Take damage by specified amount
	public void takeDamage(int dam){
		currHP -= dam;
	}
	public void takeSanityDamage(int dam){
		currSanity -= dam;
	}
	public void gainXp(int amount){
		xp += amount;
	}

	//Initiate a skillcheck event
	public void startSkillcheck(){
		skillcheckEvent.setEvent();
	}

	//Rests
	public void rest(){
		currHP = initHP;
		currSanity = sanity;
		currMem = mem;
		curse += 4;
	}

	public void levelUp(){
		while(xp >= 100){
			hp += 2;
			skillpoint += 2;
			xp -= 100;
		}
		characterSheet.Visible = true;
		isResting = true;
	}

	//Closes character sheet and saves values
	async void _on_confirm_button_up(){	
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		if(isResting){
			initStrength = strength;
			initConstitution = constitution;
			initDexterity = dexterity;
			initIntelligence = intelligence;
			initMind = mind;
			initSkillpoint = skillpoint;

			characterSheet.Visible = false;
			rest();
			GetParent().GetNode<AnimatedSprite2D>("Transition").Play("reverserest");
			isResting = false;
		}
		else if(firstTime){
			initStrength = strength;
			initConstitution = constitution;
			initDexterity = dexterity;
			initIntelligence = intelligence;
			initMind = mind;
			initSkillpoint = skillpoint;

			hp = 7 + constitution*3;
			sanity = 8 + intelligence*2 + (mind-1)/2 + additionalSanity;

			currHP = hp;
			currSanity = sanity;
			currMem = mem;

			characterSheet.Visible = false;
			GetParent().GetNode<AnimatedSprite2D>("Transition").Play("reverserest");
			await ToSignal(GetTree().CreateTimer(1), Timer.SignalName.Timeout);
			

			firstTime = false;
		}
		else{
			log.VisibleCharacters = 0;
			log.Text = "Can only level up during rests.";
		}
	}

	//Closes character sheet without saving values
	async void _on_close_button_up(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();

		strength = initStrength;
		constitution = initConstitution;
		dexterity = initDexterity;
		intelligence = initIntelligence;
		mind = initMind;
		skillpoint = initSkillpoint;

		characterSheet.Visible = false;
		if(isResting){
			rest();
			GetParent().GetNode<AnimatedSprite2D>("Transition").Play("reverserest");
			isResting = false;
		}

		if(firstTime){
			firstTime = false;
			GetParent().GetNode<AnimatedSprite2D>("Transition").Play("reverserest");
			await ToSignal(GetTree().CreateTimer(1), Timer.SignalName.Timeout);
		}
	}

	//CHARACTER SHEET BUTTONS
	void _on_str_minus_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(strength > 1 && strength > initStrength){
			strength--;
			skillpoint++;
		}
	}
	void _on_con_minus_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(constitution > 1 && constitution > initConstitution){
			constitution--;
			skillpoint++;
		}
	}
	void _on_dex_minus_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(dexterity > 1 && dexterity > initDexterity){
			dexterity--;
			skillpoint++;
		}
	}
	void _on_int_minus_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(intelligence > 1 && intelligence > initIntelligence){
			intelligence--;
			skillpoint++;
		}
	}
	void _on_mnd_minus_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(mind > 1 && mind > initMind){
			mind--;
			skillpoint++;
		}
	}
	void _on_str_plus_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(strength < 9 && skillpoint > 0 && (isResting || firstTime)){
			strength++;
			skillpoint--;
		}
	}
	void _on_con_plus_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(constitution < 9 && skillpoint > 0 && (isResting || firstTime)){
			constitution++;
			skillpoint--;
		}
	}
	void _on_dex_plus_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(dexterity < 9 && skillpoint > 0 && (isResting || firstTime)){
			dexterity++;
			skillpoint--;
		}
	}
	void _on_int_plus_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(intelligence < 9 && skillpoint > 0 && (isResting || firstTime)){
			intelligence++;
			skillpoint--;
		}
	}
	void _on_mnd_plus_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(mind < 9 && skillpoint > 0 && (isResting || firstTime)){
			mind++;
			skillpoint--;
		}
	}

	void _on_inventory_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		if(!inventory.Visible)
			inventory.Visible = true;
		else
			inventory.Visible = false;

		if(GetParent().GetNode<combat_handler>("CombatHandler").GetNode<Node2D>("PlayerHexes").Visible)
			GetParent().GetNode<combat_handler>("CombatHandler").GetNode<Node2D>("PlayerHexes").Visible = false;
		if(GetParent().GetNode<combat_handler>("CombatHandler").GetNode<Node2D>("PlayerAbilities").Visible)
			GetParent().GetNode<combat_handler>("CombatHandler").GetNode<Node2D>("PlayerAbilities").Visible = false;
	}

	void _on_inventory_close_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		inventory.Visible = false;
	}

	//Update display stats on character sheet
	private void update_stats(){
		VBoxContainer stat = characterSheet.GetNode<BoxContainer>("BoxContainer").GetNode<VBoxContainer>("PrimaryStat");
		VBoxContainer secondaryStat = characterSheet.GetNode<BoxContainer>("BoxContainer").GetNode<VBoxContainer>("SecondaryStat");

		stat.GetNode<Label>("SP").Text = skillpoint.ToString();

		stat.GetNode<Label>("STR").Text = strength.ToString();
		stat.GetNode<Label>("CON").Text = constitution.ToString();
		stat.GetNode<Label>("DEX").Text = dexterity.ToString();
		stat.GetNode<Label>("INT").Text = intelligence.ToString();
		stat.GetNode<Label>("MND").Text = mind.ToString();

		secondaryStat.GetNode<Label>("HP").Text = initHP.ToString();
		secondaryStat.GetNode<Label>("SANITY").Text = initSanity.ToString();
		secondaryStat.GetNode<Label>("TH").Text = "+" + toHit.ToString();
		secondaryStat.GetNode<Label>("AC").Text = ac.ToString();
		secondaryStat.GetNode<Label>("DR").Text = "+" + dr.ToString();
		secondaryStat.GetNode<Label>("BA").Text = "+" + ba.ToString();
		secondaryStat.GetNode<Label>("MEM").Text = mem.ToString();
		secondaryStat.GetNode<Label>("HEX").Text = "+" + hexSave.ToString();
	}
}
