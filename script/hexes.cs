using Godot;

public partial class hexes : TextureButton
{
	[Export] public string HexName;
	[Export] string Tooltip;
	combat_handler combatHandler;
	AnimatedSprite2D anim;
	Label log;
	Label tooltipLabel;
	Callable call;
	bool isCasting = false;

	Node2D sfxNode;

	public override void _Ready()
	{
		GetNode<Label>("Label").Text = HexName;
		combatHandler = GetParent().GetParent().GetParent().GetParent<combat_handler>();
		anim = combatHandler.GetNode<AnimatedSprite2D>("CombatFlash");
		log = combatHandler.GetParent().GetNode<Panel>("TextBox").GetNode<Label>("Text");
		tooltipLabel = combatHandler.GetNode<Label>("CombatTooltip");

		call = new Callable(this, "castHex");

		anim.Connect("animation_finished", call);

		sfxNode = GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX");
	}

    public override void _Process(double delta)
    {
        if(tooltipLabel.Visible){
			tooltipLabel.Position = GetGlobalMousePosition();
		}
    }

    void castHex(){
		if(anim.Animation == "hex" && isCasting){
			if(HexName == "Cause Harm"){
				log.Text += "<Cause Harm>";
				combatHandler.play.takeSanityDamage(3);
				combatHandler.enem.takeDamage(4);
			}
			else if(HexName == "Force Mend Wound"){
				log.Text += "<Force Mend Wound>";
				combatHandler.play.takeSanityDamage(2);
				combatHandler.play.takeDamage(-4);
				
			}
			else if(HexName == "Mind Wipe"){
				log.Text += "<Mind Wipe>";
				combatHandler.play.takeSanityDamage(3);
				combatHandler.play.currMem++;
				combatHandler.freeAction = true;
			}

			isCasting = false;
		}
	}

	private void useHex(){
		log.VisibleCharacters = 0;
		log.Text = "Casting Hex: ";
		isCasting = true;

		combatHandler.freeAction = false;
		anim.Play("hex");
		sfxNode.GetNode<AudioStreamPlayer>("hex").Play();
	}

	void _on_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		if(HexName == "Mind Wipe" && !anim.IsPlaying()){
			useHex();
		}
		else if(combatHandler.play.currMem > 0){
			if(combatHandler.isInCombat && combatHandler.isPlayerTurn && combatHandler.play.currMem > 0 && combatHandler.freeAction){
				combatHandler.play.currMem--;
				useHex();
			}
			else if(!combatHandler.isInCombat && (HexName == "Force Mend Wound" || HexName == "Mind Wipe")&& !anim.IsPlaying()){
				if(HexName != "Mind Wipe")
				combatHandler.play.currMem--;
				useHex();
			}
		}
		else if(!anim.IsPlaying()){
			log.VisibleCharacters = 0;
			log.Text = "Out of memorized hexes, rest to prepare more.";
		}
	}

	void _on_mouse_entered(){
		tooltipLabel.Text = Tooltip;
		tooltipLabel.Visible = true;
	}

	void _on_mouse_exited(){
		tooltipLabel.Visible = false;
	}
}
