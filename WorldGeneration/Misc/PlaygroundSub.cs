using System;
using System.Collections.Generic;
using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Redemption.WorldGeneration.Misc
{
	public class PlaygroundSub : Subworld
	{
		public override int width
		{
			get
			{
				return 2000;
			}
		}

		public override int height
		{
			get
			{
				return 2000;
			}
		}

		public override ModWorld modWorld
		{
			get
			{
				return null;
			}
		}

		public override bool noWorldUpdate
		{
			get
			{
				return true;
			}
		}

		public override bool saveSubworld
		{
			get
			{
				return false;
			}
		}

		public override bool saveModData
		{
			get
			{
				return true;
			}
		}

		public override List<GenPass> tasks
		{
			get
			{
				return new List<GenPass>
				{
					new SubworldGenPass(1f, delegate(GenerationProgress progress)
					{
						progress.Message = "Loading";
						Main.spawnTileY = 1000;
						Main.spawnTileX = 1000;
						Main.worldSurface = 1040.0;
						Main.rockLayer = 1200.0;
						this.DoThing();
					}),
					new SubworldGenPass(1f, delegate(GenerationProgress progress)
					{
						progress.Message = "Blah";
						this.DoThing2();
					})
				};
			}
		}

		public void DoThing()
		{
		}

		public void DoThing2()
		{
		}

		public override void Load()
		{
			Main.cloudAlpha = 0f;
			Main.numClouds = 0;
			SLWorld.drawUnderworldBackground = false;
			SLWorld.noReturn = false;
			Main.dayTime = false;
			Main.time = 0.0;
			Main.rainTime = 0;
			Main.raining = false;
			Main.maxRaining = 0f;
		}
	}
}
