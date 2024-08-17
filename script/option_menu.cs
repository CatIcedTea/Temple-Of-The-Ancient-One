using Godot;
using System;

public partial class option_menu : Node2D
{
	Node2D sfxNode;
	public override void _Ready()
	{
		sfxNode = GetTree().CurrentScene.GetNode<Node2D>("Sound").GetNode<Node2D>("SFX");
	}

	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Escape")){
			if(!Visible)
				Visible = true;
			else
				Visible = false;
		}
	}

	void _on_resume_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		Visible = false;
	}
	void _on_resume_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}

	void _on_restart_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		GetTree().ReloadCurrentScene();
	}
	void _on_restart_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}

	void _on_quit_to_menu_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		GetTree().ChangeSceneToFile("res://scene/main_menu.tscn");
	}
	void _on_quit_to_menu_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}

	void _on_quit_to_desktop_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_soft").Play();
		GetTree().Quit();
	}
	void _on_quit_to_desktop_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}
}
