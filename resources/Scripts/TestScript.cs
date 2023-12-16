using Godot;
using System;

public partial class TestScript : Node3D
{

	[Export]
	public Camera3D _camera;

	[Export]
	public VoxelTerrain _terrain;

	[Export]
	public Node _crosshair;

	private VoxelTool _voxelTool;


	private const float RayLength = 1000.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print(_camera.GetPath());
		GD.Print(_terrain.GetPath());

		_voxelTool = _terrain.GetVoxelTool();
		_voxelTool.Channel = VoxelBuffer.ChannelId.ChannelType;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// public override void _PhysicsProcess(double delta)
	// {
	// 	if (Input.IsActionPressed("mouse1"))
	// 	{
	// 		VoxelRaycastResult hit = _get_pointed_voxel();
	// 		if (hit != null)
	// 		{

	// 			var block_id = _voxelTool.GetVoxel(hit.Position);
	// 			var has_cube = block_id != 0;

	// 			if (has_cube)
	// 			{
	// 				_voxelTool.SetVoxel(hit.Position, 0);
	// 			}
	// 		}
	// 		else
	// 		{
	// 			GD.Print("DBG> Null hit.");
	// 		}
	// 	}
	// }

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed && eventMouseButton.ButtonIndex == MouseButton.Left)
		{
			GD.Print("Click");
			var from = _camera.ProjectRayOrigin(eventMouseButton.Position);
			var to = from + _camera.ProjectRayNormal(eventMouseButton.Position) * RayLength;

			VoxelRaycastResult result = _voxelTool.Raycast(from, to);

			var voxel_id = _voxelTool.GetVoxel(result.Position);

			_voxelTool.SetVoxel(result.Position, 0);

			GD.Print("DBG> {" + voxel_id + "}");
		}
	}
}
