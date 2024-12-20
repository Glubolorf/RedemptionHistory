using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Redemption.WorldGeneration.SubWorlds
{
	public class EmptyHallsSub : Subworld
	{
		public override int width
		{
			get
			{
				return 600;
			}
		}

		public override int height
		{
			get
			{
				return 600;
			}
		}

		public override ModWorld modWorld
		{
			get
			{
				return null;
			}
		}

		public override SubworldGenPass[] tasks
		{
			get
			{
				return new SubworldGenPass[]
				{
					new SubworldGenPass("Loading", 1f, delegate(GenerationProgress progress)
					{
						progress.Message = "Loading";
						Main.spawnTileY = 81;
						Main.worldSurface = 0.0;
						Main.rockLayer = 0.0;
						NPC.NewNPC(4352, 1280, ModLoader.GetMod("Redemption").NPCType("FallenAncient"), 0, 0f, 0f, 0f, 0f, 255);
						NPC.NewNPC(5200, 1280, ModLoader.GetMod("Redemption").NPCType("FallenAncient"), 0, 0f, 0f, 0f, 0f, 255);
						this.EmptyHall();
						this.EmptyHallStuff();
					})
				};
			}
		}

		public void EmptyHall()
		{
			Mod mod = Redemption.inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(0, 255, 0)] = mod.TileType("AncientHallBrickTile");
			colorToTile[new Color(255, 255, 0)] = mod.TileType("AncientStoneTile");
			colorToTile[new Color(0, 0, 255)] = mod.TileType("AncientWoodTile");
			colorToTile[new Color(255, 0, 255)] = 51;
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(255, 0, 255)] = mod.WallType("AncientHallPillarWall");
			colorToWall[new Color(0, 0, 255)] = mod.WallType("AncientStoneBrickWallTile");
			colorToWall[Color.Black] = -1;
			BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/SubWorlds/EmptyHalls"), colorToTile, mod.GetTexture("WorldGeneration/SubWorlds/EmptyHallsWalls"), colorToWall, null, null).Generate(0, 0, true, true);
		}

		public void EmptyHallStuff()
		{
			Mod mod = Redemption.inst;
			WorldGen.PlaceObject(300, 365, (int)((ushort)mod.TileType("AncientAltarTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, 300, 365, (int)((ushort)mod.TileType("AncientAltarTile")), 0, 0, -1, -1);
		}

		public override void Load()
		{
			Redemption.emptyHallActive = true;
			Main.dayTime = true;
			Main.time = 27000.0;
			Main.worldRate = 0;
		}

		public override void Unload()
		{
			Redemption.emptyHallActive = false;
		}

		public class EmptyHallNPC : GlobalNPC
		{
			public override bool InstancePerEntity
			{
				get
				{
					return true;
				}
			}

			public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
			{
				if (Redemption.emptyHallActive)
				{
					pool.Clear();
				}
			}
		}
	}
}
