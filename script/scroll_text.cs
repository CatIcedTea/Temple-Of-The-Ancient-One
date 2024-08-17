using Godot;
using System;

public partial class scroll_text : Label
{
	AudioStreamPlayer beep;
	public override void _Ready()
	{
		VisibleCharacters = 0;
		beep = GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX").GetNode<AudioStreamPlayer>("dialogue_low");
	}

	public override void _Process(double delta)
	{
		if(VisibleCharacters < Text.Length){
			if(!beep.Playing){
				beep.Play();
			}
			if(VisibleCharacters == Text.Length - 1){
			beep.Stop();
			}
			VisibleCharacters++;
		}
	}
}
