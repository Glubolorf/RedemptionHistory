using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Redemption.WorldGeneration.SubWorlds
{
	public class SoullessSub : Subworld
	{
		public override int width
		{
			get
			{
				return 4200;
			}
		}

		public override int height
		{
			get
			{
				return 1200;
			}
		}

		public override ModWorld modWorld
		{
			get
			{
				return ModContent.GetInstance<RedeWorld>();
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
						Main.spawnTileY = 123;
						Main.spawnTileX = 432;
						Main.worldSurface = 0.0;
						Main.rockLayer = 0.0;
						this.SoullessBiome();
					})
				};
			}
		}

		public void SoullessBiome()
		{
			Mod mod = Redemption.inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(0, 255, 0)] = mod.TileType("ShadestoneTile");
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(0, 0, 255)] = mod.WallType("ShadestoneWallTile");
			colorToWall[Color.Black] = -1;
			BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/SubWorlds/SoullessCaverns"), colorToTile, mod.GetTexture("WorldGeneration/SubWorlds/SoullessCavernsWalls"), colorToWall, mod.GetTexture("WorldGeneration/SubWorlds/SoullessCavernsLiquids"), null).Generate(0, 0, true, true);
		}

		public override void Load()
		{
			Redemption.soullessBiomeActive = true;
			Main.dayTime = true;
			Main.time = 27000.0;
			Main.worldRate = 0;
		}

		public override void Unload()
		{
			Redemption.soullessBiomeActive = false;
		}

		public class SoullessBiomeNPC : GlobalNPC
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
				if (Redemption.soullessBiomeActive)
				{
					pool.Clear();
					pool.Add(base.mod.NPCType("SoullessWanderer"), 0.1f);
					pool.Add(base.mod.NPCType("TheSoulless2"), 0.005f);
					pool.Add(base.mod.NPCType("SmallShadesoulNPC"), 0.05f);
					pool.Add(base.mod.NPCType("ShadesoulNPC"), 0.02f);
				}
			}
		}
	}
}
