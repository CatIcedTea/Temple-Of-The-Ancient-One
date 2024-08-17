using Godot;
using System;

public partial class abilities : TextureButton
{
	[Export] string AbilityName;
	[Export] string Tooltip;
	combat_handler combatHandler;
	Label log;
	Label tooltipLabel;

	int initPlayerToHit;
	int initPlayerAC;
	int initPlayerDR;
	int initPlayerBA;

	Node2D sfxNode;

	public override void _Ready()
	{
		GetNode<Label>("Label").Text = AbilityName;
		combatHandler = GetParent().GetParent().GetParent().GetParent<combat_handler>();
		log = combatHandler.GetParent().GetNode<Panel>("TextBox").GetNode<Label>("Text");
		tooltipLabel = combatHandler.GetNode<Label>("CombatTooltip");

		sfxNode = GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX");
	}

    public override void _Process(double delta)
    {
        if(tooltipLabel.Visible){
			tooltipLabel.Position = GetGlobalMousePosition();
		}
    }

    public async void useAbility(){
		log.VisibleCharacters = 0;
		log.Text = "Using Ability: ";

		if(AbilityName == "All In"){
			log.Text += "<All In>";
			combatHandler.skillUse = true;
			combatHandler.playerTakeDamage(combatHandler.enem.damage);
			await ToSignal(GetTree().CreateTimer(1), Timer.SignalName.Timeout);
			combatHandler.skillUse = true;
			combatHandler.playerDoAttack(((combatHandler.playerDamage - combatHandler.play.ba) * 2) + combatHandler.play.ba);
		}
		else if(AbilityName == "Guarded Strike"){
			log.Text += "<Guarded Strike>";
			combatHandler.playerDR += 2;
			combatHandler.playerAC += 2;
			combatHandler.playerDamage -= 2;
			combatHandler.playerDoAttack();
		}
	}

	void _on_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(combatHandler.isInCombat && combatHandler.isPlayerTurn)
			useAbility();
	}

	void _on_mouse_entered(){
		tooltipLabel.Text = Tooltip;
		tooltipLabel.Visible = true;
	}

	void _on_mouse_exited(){
		tooltipLabel.Visible = false;
	}
}
