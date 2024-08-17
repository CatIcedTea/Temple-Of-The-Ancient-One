using Godot;
using System;

public partial class game : Node2D
{
	int depth = 0;

	//Encounter types
	private enum Encounter{
		NONE,
		SKILLCHECK,
		ENEMY
	};

	//Predetermined encounter direction
	Encounter left;
	Encounter forward;
	Encounter right;

	AnimatedSprite2D background;
	AnimatedSprite2D transition;
	player play;
	text log;
	Node2D moveButtons;
	combat_handler combatHandler;
	ColorRect PreCombatScreen;
	Node2D menuButtons;
	PanelContainer restMenu;
	PanelContainer levelMenu;
	Label depthText;
	Label moveButtonTooltip;

	AudioStreamPlayer mainAmbienceMusic;
	AudioStreamPlayer battleMusic;
	Node2D sfxNode;

	bool leftVision;
	bool rightVision;
	bool forwardVision;

	bool gameEnd = false;

	public override void _Ready()
	{
		background = GetNode<AnimatedSprite2D>("Background");
		transition = GetNode<AnimatedSprite2D>("Transition");
		play = GetNode<player>("Player");
		log = GetNode<Panel>("TextBox").GetNode<text>("Text");
		moveButtons = GetNode<Node2D>("MoveButtons");
		combatHandler = GetNode<combat_handler>("CombatHandler");
		PreCombatScreen = combatHandler.GetNode<ColorRect>("PreCombatScreen");
		menuButtons = GetNode<Node2D>("MenuButtons");
		restMenu = GetNode<Node2D>("RestMenu").GetNode<PanelContainer>("RestPanel");
		levelMenu = GetNode<Node2D>("RestMenu").GetNode<PanelContainer>("LevelUpPanel");
		depthText = GetNode<Label>("DepthText");
		moveButtonTooltip = GetNode<Label>("Tooltip");
		
		mainAmbienceMusic = GetNode<Node2D>("Sound").GetNode<AudioStreamPlayer>("MainAmbienceMusic");
		battleMusic = GetNode<Node2D>("Sound").GetNode<AudioStreamPlayer>("BattleMusic");
		sfxNode = GetNode<Node2D>("Sound").GetNode<Node2D>("SFX");

		mainAmbienceMusic.VolumeDb = -80;
	}

	public override void _Process(double delta)
	{
		if(PreCombatScreen.Visible){
			if(Input.IsActionJustPressed("Click")){
				PreCombatScreen.Visible = false;
				battleMusic.Play();
				combatHandler.GetNode<AnimatedSprite2D>("CombatFlash").Play("combatflash");
			}
		}

		if(!combatHandler.isInCombat)
			battleMusic.Stop();

		depthText.Text = "Depth: " + depth;

		if(moveButtonTooltip.Visible){
			moveButtonTooltip.Position = GetGlobalMousePosition();
		}

		if(!play.isAlive && !gameEnd){
			gameOver();
			gameEnd = true;
		}
	}

	//Move buttons
	void _on_forward_pressed(){
		if(!transition.IsPlaying()){
			depth++;
			if(depth == 99){
				doEncounter(Encounter.ENEMY);
			}
			else{
				doEncounter(forward);
				determineEncounter();
			}
		}
	}
	void _on_left_pressed(){
		if(!transition.IsPlaying()){
			depth++;
			if(depth == 99){
				doEncounter(Encounter.ENEMY);
			}
			else{
				doEncounter(left);
				determineEncounter();
			}
		}
	}
	void _on_right_pressed(){
		if(!transition.IsPlaying()){
			depth++;
			if(depth == 99){
				doEncounter(Encounter.ENEMY);
			}
			else{
				doEncounter(right);
				determineEncounter();
			}
		}
	}

	async void doEncounter(Encounter type){
		sfxNode.GetNode<AudioStreamPlayer>("footsteps").Play();
		if(depth == 15){
			combatHandler.enemyPoolLimit++;
		}
		else if(depth == 30){
			combatHandler.enemyPoolLimit++;
		}
		else if(depth == 45){
			combatHandler.enemyPoolLimit++;
		}
		else if(depth == 60){
			combatHandler.enemyPoolLimit++;
		}
		else if(depth == 99){
			combatHandler.isFinal = true;
		}
		moveButtons.GetNode<TextureButton>("Left").Disabled = true;
		moveButtons.GetNode<TextureButton>("Forward").Disabled = true;
		moveButtons.GetNode<TextureButton>("Right").Disabled = true;
		play.curse++;
		log.emptyText();
		transition.Play("fadetoblack");
		await ToSignal(GetTree().CreateTimer(1), Timer.SignalName.Timeout);

		if(depth == 99)
			background.Frame = background.SpriteFrames.GetFrameCount("background");
		else
			background.Frame = new Random().Next(0, background.SpriteFrames.GetFrameCount("background") - 1);
		
		if(type == Encounter.NONE){
			transition.Play("reversefade");
			log.displayFlavorText();
			play.gainXp(2);
			return;
		}
		else if(type == Encounter.SKILLCHECK){
			play.startSkillcheck();
			transition.Play("reversefade");
			return;
		}
		else{
			startCombat();
			return;
		}
	}

	//Helper method to roll a dice
	private int roll_d20(){
		return new Random().Next(1, 21);
	}
	private int roll_d8(){
		return new Random().Next(1, 9);
	}

	//Handles combat
	private void startCombat(){
		PreCombatScreen.Visible = true;
		transition.Play("default");
		PreCombatScreen.GetNode<Label>("PreCombatText").VisibleCharacters = 0;
		if(depth == 99){
			PreCombatScreen.GetNode<Label>("PreCombatText").Text = "When you entered the room, you feel an aura so evil and strong, it forces you to your knees. As you regain your composure, you look up and see...";
		}
		else{
			PreCombatScreen.GetNode<Label>("PreCombatText").Text = "Something evil is getting close...";
		}
		combatHandler.GetNode<Panel>("CombatPanel").Visible = true;
		combatHandler.spawnEnemy();
	}

	//35% none, 35% skillcheck, 30% enemy
	private void determineEncounter(){
		Random rand = new Random();
		int encounterRand = rand.Next(101);

		//Left
		if(encounterRand <= 30){
			left = Encounter.ENEMY;
		}
		else if(encounterRand <= 65){
			left = Encounter.SKILLCHECK;
		}
		else
			left = Encounter.NONE;
		if(roll_d8() + play.mind >= 8)
			leftVision = true;
		else
			leftVision = false;

		encounterRand = rand.Next(101);
		
		//Right
		if(encounterRand <= 20){
			right = Encounter.ENEMY;
		}
		else if(encounterRand <= 60){
			right = Encounter.SKILLCHECK;
		}
		else
			right = Encounter.NONE;
		if(roll_d8() + play.mind >= 8)
			rightVision = true;
		else
			rightVision = false;

		encounterRand = rand.Next(101);

		//Forward
		if(encounterRand <= 20){
			forward = Encounter.ENEMY;
		}
		else if(encounterRand <= 60){
			forward = Encounter.SKILLCHECK;
		}
		else
			forward = Encounter.NONE;
		if(roll_d8() + play.mind >= 8)
			forwardVision = true;
		else
			forwardVision = false;
	}

	//End game
	private void gameOver(){
		GetNode<ColorRect>("EndScreen").Visible = true;
		GetNode<ColorRect>("EndScreen").GetNode<Label>("Label").VisibleCharacters = 0;
		battleMusic.Stop();
		GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("win").Play();

		if(play.curse >= 100){
			GetNode<ColorRect>("EndScreen").GetNode<Label>("Label").Text = "You did not reach The Ancient One's Relic in time. Your curse now completely overtakes you as you slowly become the one of many abominations roaming these halls";
		}
		else if(play.currSanity <= 0){
			GetNode<ColorRect>("EndScreen").GetNode<Label>("Label").Text = "You have went insane, doomed to forever wander the decrepit halls of the Ancient One as your curse slowly take over you until your eventual demise.";
		}
		else if(play.currHP <= 0){
			GetNode<ColorRect>("EndScreen").GetNode<Label>("Label").Text = "You tragically perished within these halls. However, your curse still took over your body, reanimating you as an abomination to fell others just as you had.";
		}
		GetNode<ColorRect>("EndScreen").GetNode<Label>("Label").Text += "\nDepth Cleared: " + depth;
	}

	//Tooltip for move buttons
	void _on_forward_mouse_entered(){
		moveButtonTooltip.Visible = true;

		if(depth == 98){
			moveButtonTooltip.Text = "This is it... It would be advisable to rest before proceeding.";
		}
		else if(!forwardVision){
			moveButtonTooltip.Text = "You can't see anything through the darkness in front of you";
		}
		else if(forward == Encounter.NONE || forward == Encounter.SKILLCHECK){
			moveButtonTooltip.Text = "The path in front of you seems to be somewhat safe";
		}
		else if(forward == Encounter.ENEMY){
			moveButtonTooltip.Text = "You feel an evil presence in front of you";
		}
	}
	void _on_forward_mouse_exited(){
		moveButtonTooltip.Visible = false;
		moveButtonTooltip.Text = "";
	}
	void _on_left_mouse_entered(){
		moveButtonTooltip.Visible = true;

		if(depth == 98){
			moveButtonTooltip.Text = "This is it... It would be advisable to rest before proceeding.";
		}
		else if(!leftVision){
			moveButtonTooltip.Text = "The dark clouds your vision to the left";
		}
		else if(left == Encounter.NONE || left == Encounter.SKILLCHECK){
			moveButtonTooltip.Text = "You see no obvious danger on your left";
		}
		else if(left == Encounter.ENEMY){
			moveButtonTooltip.Text = "An ominous figure looms to your left";
		}
	}
	void _on_left_mouse_exited(){
		moveButtonTooltip.Visible = false;
		moveButtonTooltip.Text = "";
	}
	void _on_right_mouse_entered(){
		moveButtonTooltip.Visible = true;

		if(depth == 98){
			moveButtonTooltip.Text = "This is it... It would be advisable to rest before proceeding.";
		}
		else if(!rightVision){
			moveButtonTooltip.Text = "You try your hardest but you can't make out anything to your right";
		}
		else if(right == Encounter.NONE || right == Encounter.SKILLCHECK){
			moveButtonTooltip.Text = "You scan ahead to your right and see nothing";
		}
		else if(right == Encounter.ENEMY){
			moveButtonTooltip.Text = "A sinister aura burns to your right";
		}
	}
	void _on_right_mouse_exited(){
		moveButtonTooltip.Visible = false;
		moveButtonTooltip.Text = "";
	}

	//Menu buttons
	void _on_character_sheet_button_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		GetNode<Node2D>("Player").GetNode<Panel>("CharacterSheet").Visible = true;
	}
	void _on_abilities_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		if(!combatHandler.GetNode<Node2D>("PlayerAbilities").Visible)
			combatHandler.GetNode<Node2D>("PlayerAbilities").Visible = true;
		else 
			combatHandler.GetNode<Node2D>("PlayerAbilities").Visible = false;

		if(combatHandler.GetNode<Node2D>("PlayerHexes").Visible)
			combatHandler.GetNode<Node2D>("PlayerHexes").Visible = false;
		if(play.GetNode<Node2D>("Inventory").Visible)
			play.GetNode<Node2D>("Inventory").Visible = false;
	}
	void _on_hexes_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		if(!combatHandler.GetNode<Node2D>("PlayerHexes").Visible)
			combatHandler.GetNode<Node2D>("PlayerHexes").Visible = true;
			else 
			combatHandler.GetNode<Node2D>("PlayerHexes").Visible = false;

		if(combatHandler.GetNode<Node2D>("PlayerAbilities").Visible)
			combatHandler.GetNode<Node2D>("PlayerAbilities").Visible = false;
		if(play.GetNode<Node2D>("Inventory").Visible)
			play.GetNode<Node2D>("Inventory").Visible = false;
	}
	
	//Handles resting
	void _on_rest_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		if(!combatHandler.isInCombat){
			restMenu.Visible = true;
		}
	}

	async void _on_rest_yes_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		restMenu.Visible = false;
		transition.Play("rest");
		await ToSignal(GetTree().CreateTimer(1.5), Timer.SignalName.Timeout);
		if(play.xp >= 100 || play.skillpoint > 0){
			levelMenu.Visible = true;
		}
		else{
			play.rest();
			transition.Play("reverserest");
		}
	}
	void _on_rest_no_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		restMenu.Visible = false;
	}

	void _on_level_up_yes_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		levelMenu.Visible = false;
		play.levelUp();
	}

	void _on_level_up_no_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		levelMenu.Visible = false;
		play.rest();
		transition.Play("reverserest");
	}

	void _on_transition_animation_finished(){
		if(transition.Animation == "reversefade"){
			moveButtons.GetNode<TextureButton>("Left").Disabled = false;
			moveButtons.GetNode<TextureButton>("Forward").Disabled = false;
			moveButtons.GetNode<TextureButton>("Right").Disabled = false;
		}
	}

	//End buttons
	void _on_restart_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		GetTree().ReloadCurrentScene();
	}

	void _on_quit_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		GetTree().Quit();
	}

	void _on_option_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		GetNode<Node2D>("OptionMenu").Visible = true;
	}

	//Hover sounds
	void _on_character_sheet_button_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}
	void _on_inventory_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}
	void _on_abilities_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}
	void _on_hexes_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}
	void _on_rest_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}
	void _on_option_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}
}
