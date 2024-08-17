using Godot;
using System;

public partial class character_sheet : Panel
{
	Label tooltip;
	VBoxContainer primaryStat;
	VBoxContainer secondaryStat;

	public override void _Ready(){
		tooltip = GetNode<Label>("Tooltip");
		primaryStat = GetNode<BoxContainer>("BoxContainer").GetNode<VBoxContainer>("PrimaryStatName");
		secondaryStat = GetNode<BoxContainer>("BoxContainer").GetNode<VBoxContainer>("SecondaryStatName");
	}

    public override void _Process(double delta)
    {
		if(tooltip.VisibleCharacters < tooltip.Text.Length){
			tooltip.VisibleCharacters++;
		}
        tooltip.Position = GetLocalMousePosition();
    }

	//Tooltip popup
	void _on_strength_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "How physically strong you are. Affects Base Attack Damage (BA).";
		tooltip.Visible = true;
	}
	void _on_strength_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_consitution_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "Your general health and body. Affects Health (HP) and Damage Resist (DR).";
		tooltip.Visible = true;
	}
	void _on_consitution_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_dexterity_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "How nimble and agile your movement is. Affects Armor Class (AC) and To Hit Bonus (TO HIT)";
		tooltip.Visible = true;
	}
	void _on_dexterity_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_intelligence_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "Your intellect and capacity to learn. Affects Sanity (SANITY) and Memory (MEM)";
		tooltip.Visible = true;
	}
	void _on_intelligence_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_mind_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "General insight of yourself and your environment. Affects your ability to see ahead of you and Memory (MEM)";
		tooltip.Visible = true;
	}
	void _on_mind_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_hp_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "Your maximum health point, death if dropped below zero";
		tooltip.Visible = true;
	}
	void _on_hp_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_sanity_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "Your maximum sanity, used to perform hexes and also acts as a second health bar";
		tooltip.Visible = true;
	}
	void _on_sanity_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_th_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "Your To Hit Bonus, adds a bonus to your attack roll to determine whether you hit or miss when you roll an attacking 1d20";
		tooltip.Visible = true;
	}
	void _on_th_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_ac_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "Your Armor Class, added to your defending 1d20 roll to determine whether you dodge an attack";
		tooltip.Visible = true;
	}
	void _on_ac_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_dr_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "Your Damage Resistance, Flat incoming damage reduction but incoming damage can't go below 1";
		tooltip.Visible = true;
	}
	void _on_dr_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_ba_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "Your Base Attack, Flat damage increase to your attack";
		tooltip.Visible = true;
	}
	void _on_ba_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_mem_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "Your Memory, the amount of hexes you can memorize per rest";
		tooltip.Visible = true;
	}
	void _on_mem_mouse_exited(){
		tooltip.Visible = false;
	}
	void _on_hex_mouse_entered(){
		tooltip.VisibleCharacters = 0;
		tooltip.Text = "Your Hex Saving Throw, adds a bonus to your 1d8 saving throw to resist against incoming hexes";
		tooltip.Visible = true;
	}
	void _on_hex_mouse_exited(){
		tooltip.Visible = false;
	}

}
