using Godot;
using System;

public partial class sound : Node2D
{
	AudioStreamPlayer mainAmbienceMusic;
	AudioStreamPlayer battleMusic;

	public override void _Ready()
	{
		mainAmbienceMusic = GetNode<AudioStreamPlayer>("MainAmbienceMusic");
		battleMusic = GetNode<AudioStreamPlayer>("BattleMusic");
	}

	void _on_main_ambience_music_finished(){
		mainAmbienceMusic.Play();
	}

	void _on_audio_stream_player_finished(){
		battleMusic.Play();
	}
}
