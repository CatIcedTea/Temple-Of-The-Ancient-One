using Godot;
using System;
using System.Diagnostics;

public partial class skillcheck : Panel
{
	Label textBox;
	Label box1;
	Label box2;
	Label box3;
	Label box4;

	Node eventList;
	Event ev;

	player play;

	Label log;

	AnimatedSprite2D transition;
	AudioStreamPlayer beep;

	int maxEventList;

	bool isFinished = false;

	public override void _Ready()
	{
		play = GetParent<player>();

		log = GetParent().GetParent().GetNode<Panel>("TextBox").GetNode<Label>("Text");

		transition = GetParent().GetParent().GetNode<AnimatedSprite2D>("Transition");

		textBox = GetNode<Label>("Text");
		box1 = GetNode<BoxContainer>("Choice").GetNode<VBoxContainer>("Left").GetNode<TextureButton>("TopLeft").GetNode<Label>("Label");
		box2 = GetNode<BoxContainer>("Choice").GetNode<VBoxContainer>("Right").GetNode<TextureButton>("TopRight").GetNode<Label>("Label");
		box3 = GetNode<BoxContainer>("Choice").GetNode<VBoxContainer>("Left").GetNode<TextureButton>("BottomLeft").GetNode<Label>("Label");
		box4 = GetNode<BoxContainer>("Choice").GetNode<VBoxContainer>("Right").GetNode<TextureButton>("BottomRight").GetNode<Label>("Label");
		eventList = GetNode<Node>("Event");
		maxEventList = eventList.GetChildCount();

		beep = GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("dialogue_low");
		
		textBox.VisibleCharacters = 0;
	}

	public override void _Process(double delta)
	{
		if(textBox.VisibleCharacters < textBox.Text.Length){
			if(!beep.Playing){
				beep.Play();
			}
			textBox.VisibleCharacters++;
		}
		
		if(Input.IsActionPressed("Click") && Visible && isFinished){
			textBox.Text = "";
			Visible = false;
			transition.Play("reversefade");
		}
	}

	public void setEvent(){
		isFinished = false;
		textBox.VisibleCharacters = 0;
		Visible = true;
		int num = new Random().Next(1, maxEventList + 1);
		string name = "Event" + num.ToString();

		ev = eventList.GetNode<Event>(name);

		textBox.Text = ev.text;
		box1.Text = ev.box1;
		box2.Text = ev.box2;
		box3.Text = ev.box3;
		box4.Text = ev.box4;
	}

	//Helper method to roll a d20
	private int roll_d10(){
		return new Random().Next(1, 10);
	}

	//Do skillcheck for each button
	void _on_top_left_pressed(){
		checkChoice(ev.box1Stat, ev.box1Target, ev.box1Success, ev.box1Fail, ev.box1FailDamageType, ev.box1FailDamage);
	}
	void _on_top_right_pressed(){
		checkChoice(ev.box2Stat, ev.box2Target, ev.box2Success, ev.box2Fail, ev.box2FailDamageType, ev.box2FailDamage);
	}
	void _on_bottom_left_pressed(){
		checkChoice(ev.box3Stat, ev.box3Target, ev.box3Success, ev.box3Fail, ev.box3FailDamageType, ev.box3FailDamage);
	}
	void _on_bottom_right_pressed(){
		checkChoice(ev.box4Stat, ev.box4Target, ev.box4Success, ev.box4Fail, ev.box4FailDamageType, ev.box4FailDamage);
	}


	//Compute the skillcheck
	private void checkChoice(Event.Stat stat, int target, string success, string fail, Event.Stat damageType, int damage){
		int roll = roll_d10();
		int totalRoll;

		if (stat == Event.Stat.STR){
			log.VisibleCharacters = 0;
			totalRoll = roll + play.strength;
			log.Text = "(STR check) You rolled " + roll + "(+" + play.strength + ") vs TARGET: " + target;
		}
		else if (stat == Event.Stat.CON){
			log.VisibleCharacters = 0;
			totalRoll = roll + play.constitution;
			log.Text = "(CON check) You rolled " + roll + "(+" + play.constitution + ") vs TARGET: " + target;
		}
		else if (stat == Event.Stat.DEX){
			log.VisibleCharacters = 0;
			totalRoll = roll + play.dexterity;
			log.Text = "(DEX check) You rolled " + roll + "(+" + play.dexterity + ") vs TARGET: " + target;
		}
		else if (stat == Event.Stat.INT){
			log.VisibleCharacters = 0;
			totalRoll = roll + play.intelligence;
			log.Text = "(INT check) You rolled " + roll + "(+" + play.intelligence + ") vs TARGET: " + target;
		}
		else if (stat == Event.Stat.MND){
			log.VisibleCharacters = 0;
			totalRoll = roll + play.mind;
			log.Text = "(MND check) You rolled " + roll + "(+" + play.mind + ") vs TARGET: " + target;
		}
		else{
			return;
		}

		textBox.VisibleCharacters = 0;
		if (totalRoll >= target){
			textBox.Text = success;
			log.Text += " (SUCCESS)";
			play.gainXp(5);
		}
		else{
			calculateDamage(damageType, damage);
			textBox.Text = fail;
			log.Text += " (FAILED)";
			play.gainXp(2);

			if(damageType == Event.Stat.HP)
				textBox.Text += "\nHP -" + damage;
			else if(damageType == Event.Stat.SANITY)
				textBox.Text += "\nSANITY -" + damage;
			else
				textBox.Text += "\nCURSE +" + damage + "%";
		}
		textBox.Text += "\nClick anywhere to proceed";

		isFinished = true;
	}

	private void calculateDamage(Event.Stat damageType, int damage){
		if(damageType == Event.Stat.HP)
			play.currHP -= damage;
		else if(damageType == Event.Stat.SANITY)
			play.currSanity -= damage;
		else if(damageType == Event.Stat.CURSE)
			play.curse += damage;
	}
}
