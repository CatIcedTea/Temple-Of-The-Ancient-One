using Godot;
using System;

public partial class main_menu : Node2D
{
	Node2D sfxNode;
	public override void _Ready()
	{
		sfxNode = GetNode<Node2D>("Sound");
	}

	void _on_play_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		GetTree().ChangeSceneToFile("res://scene/main.tscn");
	}
	void _on_play_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}

	void _on_setting_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		GetNode<PanelContainer>("SoundPanel").Visible = true;
	}
	void _on_setting_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}

	void _on_quit_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		GetTree().Quit();
	}
	void _on_quit_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}

	void _on_back_pressed(){
		sfxNode.GetNode<AudioStreamPlayer>("click_hard").Play();
		GetNode<PanelContainer>("SoundPanel").Visible = false;
	}
	void _on_back_mouse_entered(){
		sfxNode.GetNode<AudioStreamPlayer>("hover_over").Play();
	}
}
