using Godot;
using System;
using System.Threading.Tasks;

public partial class SceneTransitionRect : ColorRect
{
	// Path to the next scene to transition to
	[Export(PropertyHint.File, "*.tscn")]
	public string NextScenePath { get; set; }

	// Reference to the AnimationPlayer node
	private AnimationPlayer _animPlayer;

	public override void _Ready()
	{
		_animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animPlayer.PlayBackwards("fade");
	}

	public async Task TransitionTo(string nextScene = null)
	{
		string sceneToLoad = nextScene ?? NextScenePath;
		_animPlayer.Play("fade");
		await ToSignal(_animPlayer, "animation_finished");
		GetTree().ChangeSceneToFile(sceneToLoad);
	}
}
