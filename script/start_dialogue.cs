using Godot;
using System;

public partial class start_dialogue : ColorRect
{
	string [] text = {"During what was supposed to be your relaxing vacation by the beach, you stumbled upon a situation you weren't meant see.", 
	"While everyone was out playing in the water, you stepped away for a bit to get away from all the noise and commotion.",
	"During then, you noticed a cave a ways out.",
	"It admittedly peaked your curiosity and made you want to check it out.",
	"As you climb up the rocky hill, you start hearing strange voices echoing throughout, though you can't see anything as it was pitch dark inside.",
	"Then, one of your friends called out to you, diverting your attention away, asking what you were doing and telling you to come back",
	"You replied saying that you're going to be heading back now.",
	"As you step away, you can't seem to shake the thought of whatever could be inside it.",
	"It was now night, All your friends are asleep but the thought of whatever was inside that cave is stuck in your mind.",
	"With nothing to do, you foolishly decided to go back and check out whatever was inside.",
	"Arriving at the entrance again, you turn on your flashlight and start to head in.",
	"The cave runs on for a long while, until you notice something strange, something unnatural.",
	"There's a set of stairs going down, brick stairs.",
	"You held in your breath and slowly breathe out.",
	"You start slowly descending into the depth of the cave and you noticed something...",
	"The same strange whispers you heard this morning.",
	"It is faint but as you descend further, it gets louder and louder until...",
	"You walked into a scene of several robed figures, they all turn and look at you.",
	"You can't seem see any of their faces, if they even had any.",
	"That is when all of them rushed you and pinned you down.",
	"They dragged you back to where they were and you finally realized...",
	"They were performing a ritual, one that you abruptly interrupted.",
	"And now they were going to use you for another.",
	"You struggled but their process starts, whispers and the carving of symbols onto your flesh.",
	"With miraculous luck, you somehow managed to break free and ran all the way to your car and drove back home.",
	"Shaken up, you can't even tell your friends what happened.",
	"Arriving home and falling down on your knees, exhausted, that is when you noticed...",
	"You are cursed, the process of their ritual on you has already begun as you start seeing mysterious black liquid filling your veins.",
	"Not sure what to do, you began searching all sorts of information on curses and the occult.",
	"Until finally you found what you were looking for.",
	"The curse of a primordial god, whose name is uncomprehensible to humans, and is only referred to as \"The Ancient One\" by its followers.",
	"In order to break your curse, you would have to return back to the cave where its temple is located, delve through its 99 depths and use the Relic of the Ancient One at the end before your curse completely takes over you.",
	"With this knowledge, you began preparing, learning how to defend yourself and studying hexes used by the cultists of The Ancient One.",
	"Finally ready, you returned to the temple, bracing yourself as you start to descend back down..."};
	int index = 0;
	Label label;

	public override void _Ready()
	{
		label = GetNode<Label>("Label");
		label.VisibleCharacters = 0;
		label.Text = text[index];
	}

	public override void _Process(double delta)
	{
		if(Visible){
			if(Input.IsActionJustPressed("Click")){
				if(index < text.Length - 1){
					index++;
					label.VisibleCharacters = 0;
					label.Text = text[index];
				}
				else{
					GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<AudioStreamPlayer>("MainAmbienceMusic").VolumeDb = -80;
					GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<AudioStreamPlayer>("MainAmbienceMusic").Play();
					Visible = false;
				}
			}
		}
	}

	void _on_skip_button_down(){
		GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("click_hard");
		GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<AudioStreamPlayer>("MainAmbienceMusic").VolumeDb = -80;
		GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<AudioStreamPlayer>("MainAmbienceMusic").Play();
		label.Text = "";
		Visible = false;
	}

	void _on_skip_mouse_entered(){
		GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("hover_over");
	}
}
