using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.WorldGeneration;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace Redemption
{
	public class RedeLab : ModWorld
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int ShiniesIndex3 = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Final Cleanup"));
			tasks.Insert(ShiniesIndex3, new PassLegacy("Clearing Liquids for Ship", delegate(GenerationProgress progress)
			{
				this.PreSlayersShip();
			}));
			tasks.Insert(ShiniesIndex3 + 1, new PassLegacy("Slayer's Crashed Spaceship", delegate(GenerationProgress progress)
			{
				this.SlayersShip();
			}));
			tasks.Insert(ShiniesIndex3 + 2, new PassLegacy("Clearing Liquids for Lab", delegate(GenerationProgress progress)
			{
				this.PreLab();
			}));
			tasks.Insert(ShiniesIndex3 + 3, new PassLegacy("Abandoned Lab", delegate(GenerationProgress progress)
			{
				this.Lab();
			}));
		}

		public static int GetWorldSize()
		{
			if (Main.maxTilesX <= 4200)
			{
				return 1;
			}
			if (Main.maxTilesX <= 6400)
			{
				return 2;
			}
			if (Main.maxTilesX <= 8400)
			{
				return 3;
			}
			return 1;
		}

		public void Lab()
		{
			Point origin = new Point((int)((float)Main.maxTilesX * 0.55f), (int)((float)Main.maxTilesY * 0.65f));
			GenStructure genStructure = new AbandonedLab();
			new LabClear().Place(origin, WorldGen.structures);
			genStructure.Place(origin, WorldGen.structures);
		}

		public void PreLab()
		{
			WorldUtils.Gen(new Point((int)((float)Main.maxTilesX * 0.55f), (int)((float)Main.maxTilesY * 0.65f)), new Shapes.Rectangle(289, 217), Actions.Chain(new GenAction[]
			{
				new Actions.SetLiquid(0, 0)
			}));
		}

		public void SlayersShip()
		{
			Point origin = new Point((int)((float)Main.maxTilesX * 0.65f), (int)((float)Main.maxTilesY * 0.3f));
			if (Main.dungeonX < Main.maxTilesX / 2)
			{
				origin = new Point((int)((float)Main.maxTilesX * 0.35f), (int)((float)Main.maxTilesY * 0.3f));
			}
			GenStructure genStructure = new SlayerShip();
			new SlayerShipClear().Place(origin, WorldGen.structures);
			genStructure.Place(origin, WorldGen.structures);
		}

		public void PreSlayersShip()
		{
			Point origin = new Point((int)((float)Main.maxTilesX * 0.65f), (int)((float)Main.maxTilesY * 0.3f));
			if (Main.dungeonX < Main.maxTilesX / 2)
			{
				origin = new Point((int)((float)Main.maxTilesX * 0.35f), (int)((float)Main.maxTilesY * 0.3f));
			}
			origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
			WorldUtils.Gen(origin, new Shapes.Rectangle(80, 50), Actions.Chain(new GenAction[]
			{
				new Actions.SetLiquid(0, 0)
			}));
		}
	}
}
