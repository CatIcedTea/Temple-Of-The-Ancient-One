using Godot;
using System;

public partial class combat_handler : Node2D
{
	int playerRoll;
	int enemyRoll;

	PackedScene [] enemyPool = {ResourceLoader.Load<PackedScene>("res://scene/enemy/cultist.tscn"), 
	ResourceLoader.Load<PackedScene>("res://scene/enemy/possessedWater.tscn"),
	ResourceLoader.Load<PackedScene>("res://scene/enemy/floatingEssence.tscn"),
	ResourceLoader.Load<PackedScene>("res://scene/enemy/awakenedCultist.tscn"),
	ResourceLoader.Load<PackedScene>("res://scene/enemy/fragment.tscn"),
	ResourceLoader.Load<PackedScene>("res://scene/enemy/ancientOne.tscn")};
	public int enemyPoolLimit = 1;
	Panel combatPanel;
	Label log;
	public player play;
	public enemy enem;
	AnimatedSprite2D enemySprite;
	AnimationPlayer enemyHit;
	AnimationPlayer cameraShake;
	AnimationPlayer anim;
	AnimatedSprite2D attackEffects;
	ColorRect combatEndScreen;
	AnimatedSprite2D transition;
	Node2D abilitiesPanel;
	Node2D hexesPanel;
	Label hexesPanelText;
	TextureButton attackButton;
	AudioStreamPlayer mainAmbienceMusic;
	AudioStreamPlayer battleMusic;
	Node2D sfxNode;

	public bool isPlayerTurn = true;
	bool playerIsDefending = false;
	bool isEnemyTurn = false;
	bool hit = false;
	bool enemyIsAlive = false;
	public bool freeAction = true;
	public bool isInCombat = false;

	public bool skillUse = false;
	int skillUseDamage = 0;

	public int playerToHit;
	public int playerAC;
	public int playerDR;
	public int playerDamage;

	public bool isFinal = false;

	public override void _Ready()
	{
		combatPanel = GetNode<Panel>("CombatPanel");
		log = GetParent().GetNode<Panel>("TextBox").GetNode<Label>("Text");
		play = GetParent().GetNode<player>("Player");
		cameraShake = GetParent().GetNode<Camera2D>("Camera2D").GetNode<AnimationPlayer>("Screenshake");
		attackEffects = GetNode<AnimatedSprite2D>("CombatFlash");
		combatEndScreen = GetNode<ColorRect>("CombatEnd");
		transition = GetParent().GetNode<AnimatedSprite2D>("Transition");
		abilitiesPanel = GetNode<Node2D>("PlayerAbilities");
		hexesPanel = GetNode<Node2D>("PlayerHexes");
		attackButton = GetNode<Panel>("CombatPanel").GetNode<HBoxContainer>("HBoxContainer").GetNode<VBoxContainer>("VBoxContainer").GetNode<TextureButton>("Attack");
		hexesPanelText = hexesPanel.GetNode<PanelContainer>("PlayerHexesList").GetNode<VBoxContainer>("VBoxContainer").GetNode<Label>("Label");
		mainAmbienceMusic = GetParent().GetNode<Node2D>("Sound").GetNode<AudioStreamPlayer>("MainAmbienceMusic");
		battleMusic = GetParent().GetNode<Node2D>("Sound").GetNode<AudioStreamPlayer>("BattleMusic");
		sfxNode = GetParent().GetNode<Node2D>("Sound").GetNode<Node2D>("SFX");
	}

	public override async void _Process(double delta)
	{
		if(isInCombat){
			if(!enem.isAlive){
				battleMusic.Stop();
				sfxNode.GetNode<AudioStreamPlayer>("win").Play();
				isInCombat = false;
				enemySprite.Stop();

				abilitiesPanel.Visible = false;
				hexesPanel.Visible = false;

				anim.Play("dead");
				combatEndScreen.Visible = true;

				if(enem.name == "The Ancient One"){
					log.VisibleCharacters = 0;
					log.Text = "It is done. The only thing left to do is to drink from the cup to cure your curse.";
				}
				else{
					log.VisibleCharacters = 0;
					log.Text = "Experience gained: " + enem.xp + "\nCurse reduced: -3%";
					if(!play.GetNode<inventoryLimit>("Inventory").isFull()){
						enem.dropItem();
					}
					else{
						log.Text += "\nInventory is full, discarding obtained item";
					}
					play.gainXp(enem.xp);
					play.curse -= 3;
				}
			}

			if(isEnemyTurn && enem.isAlive){
				playerRoll = roll_d20();
				enemyRoll = roll_d20();

				isEnemyTurn = false;
				await ToSignal(GetTree().CreateTimer(1.5), Timer.SignalName.Timeout);

				if(enem.isAlive){
					attackEffects.Play("combatflash");

					if(playerRoll + playerAC > enemyRoll + enem.toHit){	
						hit = false;
					}
					else{
						hit = true;
					}
				}

			}
		}

		//Combat end screen
		if(combatEndScreen.Visible){
			if(Input.IsActionJustPressed("Click")){
				if(enem.name == "The Ancient One"){
					endGame();
				}
				else{
					combatEndScreen.Visible = false;
					endCombat();
				}
			}
		}

		//Updates amount of hexes currently available
		if(hexesPanel.Visible){
			hexesPanelText.Text = "Hexes " + play.currMem + "/" + play.mem;
		}

		//Music
		if(isInCombat && mainAmbienceMusic.VolumeDb > -80)
			mainAmbienceMusic.VolumeDb -= 40 * (float)delta;
		if(combatPanel.Visible == false&& mainAmbienceMusic.VolumeDb < 0){
			mainAmbienceMusic.VolumeDb += 40 * (float)delta;
			if(mainAmbienceMusic.VolumeDb > 0)
				mainAmbienceMusic.VolumeDb = 0;
		}
	}

	public void spawnEnemy(){
		int roll;
		if(!isFinal){
			roll = new Random().Next(0, enemyPoolLimit);
		}
		else{
			roll = enemyPool.Length - 1;
		}
		var enemy = enemyPool[roll].Instantiate();
		enemy.Name = "Enemy";
		
		AddChild(enemy);
		MoveChild(enemy, 0);

		enem = GetNode<enemy>("Enemy");
		enemySprite = enem.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		enemyHit = enem.GetNode<AnimationPlayer>("HitAnimation");
		anim = enem.GetNode<AnimationPlayer>("IdleAnimation");

		setPlayerInitStat();

		isPlayerTurn = true;
		enemyIsAlive = true;
		isInCombat = true;
		freeAction = true;
	}

	void _on_attack_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(isPlayerTurn){
			playerRoll = roll_d20();
			enemyRoll = roll_d20();
			sfxNode.GetNode<AudioStreamPlayer>("attack").Play();
			attackEffects.Play("attack");

			if(playerRoll + playerToHit >= enemyRoll + enem.ac){
				hit = true;
			}
			else{
				hit = false;
			}
			isPlayerTurn = false;
		}
	}

	void _on_defend_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(isPlayerTurn){
			playerIsDefending = true;
			isEnemyTurn = true;
			isPlayerTurn = false;

			log.VisibleCharacters = 0;
			log.Text = "You brace for incoming strikes, taking reduced damage until the end of your next attack";
			freeAction = true;
		}
	}

	void _on_use_ability_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(abilitiesPanel.Visible)
			abilitiesPanel.Visible = false;
		else
			abilitiesPanel.Visible = true;

		if(hexesPanel.Visible)
			hexesPanel.Visible = false;
	}

	void _on_use_hex_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(hexesPanel.Visible)
			hexesPanel.Visible = false;
		else
			hexesPanel.Visible = true;

		if(abilitiesPanel.Visible)
			abilitiesPanel.Visible = false;
	}

	void _on_run_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(enem.name == "The Ancient One"){
			log.VisibleCharacters = 0;
			log.Text = "There is no escaping The True Manifestation of the Ancient One... Accept your fate.";
		}
		else if(isPlayerTurn){
			log.VisibleCharacters = 0;
			log.Text = "Attemping escape...";

			int roll = roll_d20() + play.dexterity;

			if(roll > 14){
				endCombat();
				log.VisibleCharacters = 0;
				log.Text = "You successfully escaped, curse increased by 2%";

				play.curse += 2;
			}
			else{
				log.Text += "\nYou failed to escape";
				isEnemyTurn = true;
				isPlayerTurn = false;
		}
		}
	}

	//Methods to force attack and damage for abilities
	public void playerDoAttack(){
		attackButton.EmitSignal("pressed");
	}
	public void playerDoAttack(int dam){
		isPlayerTurn = false;
		attackEffects.Play("attack");
		skillUseDamage = dam;
	}
	public void playerTakeDamage(int dam){
		isPlayerTurn = false;
		attackEffects.Play("combatflash");
		skillUseDamage = dam;
	}

	//Set player stats to initial player stats
	private void setPlayerInitStat(){
		playerToHit = play.toHit;
		playerAC = play.ac;
		playerDR = play.dr;
		playerDamage = play.damage;
	}

	void _on_combat_flash_animation_finished(){
		if(!skillUse){
			if(attackEffects.Animation == "attack"){
				log.VisibleCharacters = 0;

				if (hit){
					sfxNode.GetNode<AudioStreamPlayer>("impact").Play();
					enemyHit.Play("hit");

					log.Text = "ATTACK SUCCESSFUL: ";
					if(playerDamage - enem.dr > 0)
						enem.takeDamage(playerDamage - enem.dr);
					else
						enem.takeDamage(1);
				}
				else{
					log.Text = "ATTACK MISSED: ";
				}

				if(enem.ac < 0)
					log.Text += "You Rolled " + playerRoll + "(+" + playerToHit + ") vs ENEMY " + enemyRoll + "(" + enem.ac + ")";
				else
					log.Text += "You Rolled " + playerRoll + "(+" + playerToHit + ") vs ENEMY " + enemyRoll + "(+" + enem.ac + ")";
				playerIsDefending = false;
				isEnemyTurn = true;
			}

			if(attackEffects.Animation == "combatflash" && !isPlayerTurn && enemyIsAlive){
				if(!hit){
					log.Text += "\nATTACK DODGED: ";
				}
				else{
					sfxNode.GetNode<AudioStreamPlayer>("hit").Play();
					cameraShake.Play("shake");

					log.Text += "\nHIT TAKEN: ";

					if(!playerIsDefending){
						if(enem.damage - playerDR > 0)
							play.takeDamage(enem.damage - playerDR);
						else
							play.takeDamage(1);
					}
					else{
						if((enem.damage - playerDR)/2 > 0)
							play.takeDamage((enem.damage - playerDR)/2);
						else
							play.takeDamage(1);
					}
				}

				if(enem.toHit < 0)
					log.Text += "You Rolled " + playerRoll + "(+" + playerAC + ") vs ENEMY " + enemyRoll + "(" + enem.toHit + ")";
				else
					log.Text += "You Rolled " + playerRoll + "(+" + playerAC + ") vs ENEMY " + enemyRoll + "(+" + enem.toHit + ")";
				isPlayerTurn = true;
				freeAction = true;

				setPlayerInitStat();
			}
		}
		else{
			if(attackEffects.Animation == "attack"){
				sfxNode.GetNode<AudioStreamPlayer>("impact").Play();
				sfxNode.GetNode<AudioStreamPlayer>("hit_normal").Play();

				enemyHit.Play("hit");

				if(skillUseDamage - enem.dr > 0)
					enem.takeDamage(skillUseDamage - enem.dr);
				else
					enem.takeDamage(1);

				skillUse = false;

				playerIsDefending = false;
				isPlayerTurn = true;
				freeAction = true;
			}

			if(attackEffects.Animation == "combatflash"){
				sfxNode.GetNode<AudioStreamPlayer>("hit").Play();
				cameraShake.Play("shake");
				if(!playerIsDefending){
						if(skillUseDamage - playerDR > 0)
							play.takeDamage(skillUseDamage - playerDR);
						else
							play.takeDamage(1);
					}
					else{
						if((skillUseDamage - playerDR)/2 > 0)
							play.takeDamage((skillUseDamage - playerDR)/2);
						else
							play.takeDamage(1);
					}

				skillUse = false;
			}
		}
	}

	private async void endCombat(){
		isInCombat = false;
		transition.Play("fadetoblack");
		await ToSignal(GetTree().CreateTimer(1), Timer.SignalName.Timeout);
		transition.Play("reversefade");

		combatPanel.Visible = false;
		abilitiesPanel.Visible = false;
		hexesPanel.Visible = false;
		RemoveChild(enem);
	}

	private void endGame(){
		GetTree().CurrentScene.GetNode<Node2D>("WinScreen").Visible = true;
	}

	private int roll_d20(){
		return new Random().Next(1, 21);
	}

	private int roll_d8(){
		return new Random().Next(1, 9);
	}

	void _on_abilities_close_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		abilitiesPanel.Visible = false;
	}

	void _on_hexes_close_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		hexesPanel.Visible = false;
	}
}
