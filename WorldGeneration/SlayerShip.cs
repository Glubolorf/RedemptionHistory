using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.World.Generation;

namespace Redemption.WorldGeneration
{
	public class SlayerShip : MicroBiome
	{
		public override bool Place(Point origin, StructureMap structures)
		{
			Mod mod = Redemption.inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(0, 255, 255)] = mod.TileType("SlayerShipPanelTile");
			colorToTile[new Color(255, 255, 0)] = mod.TileType("JunkMetalTile");
			colorToTile[new Color(100, 100, 100)] = mod.TileType("LabSupportBeamTile");
			colorToTile[new Color(220, 255, 255)] = mod.TileType("HalogenLampTile");
			colorToTile[new Color(255, 0, 255)] = mod.TileType("ShipGlassTile");
			colorToTile[new Color(255, 0, 0)] = mod.TileType("ElectricHazardTile");
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(0, 255, 0)] = mod.WallType("SlayerShipPanelWallTile");
			colorToWall[new Color(255, 0, 0)] = mod.WallType("JunkMetalWall");
			colorToWall[Color.Black] = -1;
			BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/SlayerShip"), colorToTile, mod.GetTexture("WorldGeneration/SlayerShipWalls"), colorToWall, null, mod.GetTexture("WorldGeneration/SlayerShipSlopes")).Generate(origin.X, origin.Y, true, true);
			WorldGen.PlaceObject(origin.X + 90, origin.Y + 23, (int)((ushort)mod.TileType("SlayerChairTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 90, origin.Y + 23, (int)((ushort)mod.TileType("SlayerChairTile")), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 84, origin.Y + 36, (int)((ushort)mod.TileType("SlayerFabricatorTile")), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 84, origin.Y + 36, (int)((ushort)mod.TileType("SlayerFabricatorTile")), 0, 0, -1, -1);
			this.ShipChest(origin.X + 45, origin.Y + 44);
			this.ShipChest(origin.X + 52, origin.Y + 48);
			this.ShipChest(origin.X + 81, origin.Y + 39);
			this.ShipChest(origin.X + 101, origin.Y + 39);
			this.ShipChest(origin.X + 53, origin.Y + 41);
			this.ShipChest(origin.X + 55, origin.Y + 41);
			this.ShipChest(origin.X + 58, origin.Y + 41);
			this.ShipChest(origin.X + 60, origin.Y + 41);
			this.ShipChest(origin.X + 108, origin.Y + 47);
			this.ShipChest(origin.X + 104, origin.Y + 47);
			this.ShipChest(origin.X + 102, origin.Y + 47);
			this.ShipChest(origin.X + 100, origin.Y + 47);
			return true;
		}

		public void ShipChest(int x, int y)
		{
			Mod mod = Redemption.inst;
			int PlacementSuccess = WorldGen.PlaceChest(x, y, (ushort)mod.TileType("HolochestTile"), false, 0);
			int[] HoloChestLoot = new int[]
			{
				mod.ItemType("Datalog1"),
				mod.ItemType("Datalog2"),
				mod.ItemType("Datalog3"),
				mod.ItemType("Datalog6"),
				mod.ItemType("Datalog335"),
				mod.ItemType("Datalog772"),
				mod.ItemType("Datalog919"),
				mod.ItemType("Datalog180499"),
				mod.ItemType("Datalog182500"),
				mod.ItemType("Datalog182501"),
				mod.ItemType("Datalog182573"),
				mod.ItemType("Datalog184753"),
				mod.ItemType("Datalog184989"),
				mod.ItemType("Datalog466105"),
				mod.ItemType("Datalog466476"),
				mod.ItemType("Datalog500198"),
				mod.ItemType("Datalog545675"),
				mod.ItemType("Datalog999735"),
				mod.ItemType("Datalog1000000"),
				mod.ItemType("Datalog1012875"),
				mod.ItemType("Datalog3650000"),
				mod.ItemType("Datalog5385430"),
				mod.ItemType("Datalog36500001"),
				mod.ItemType("Datalog164550614"),
				mod.ItemType("Datalog364635000"),
				mod.ItemType("Datalog365000000"),
				mod.ItemType("Datalog389035250")
			};
			int[] HoloChestLoot2 = new int[]
			{
				mod.ItemType("ScrapMetal"),
				mod.ItemType("AIChip"),
				mod.ItemType("Mk1Capacitator"),
				mod.ItemType("Mk2Capacitator"),
				mod.ItemType("Mk3Capacitator"),
				mod.ItemType("Mk1Plating"),
				mod.ItemType("Mk2Plating"),
				mod.ItemType("Mk3Plating")
			};
			int[] HoloChestLoot3 = new int[]
			{
				mod.ItemType("CarbonMyofibre"),
				mod.ItemType("XenomiteShard")
			};
			if (PlacementSuccess >= 0)
			{
				Chest chest = Main.chest[PlacementSuccess];
				Item item0 = chest.item[0];
				UnifiedRandom genRand0 = WorldGen.genRand;
				int[] array0 = new int[]
				{
					mod.ItemType("SlayerBigRevolver"),
					mod.ItemType("SlayerGravGun"),
					mod.ItemType("AndroidMinion"),
					mod.ItemType("SlayersChakram"),
					mod.ItemType("MissileDroneCaller")
				};
				item0.SetDefaults(Utils.Next<int>(genRand0, array0), false);
				chest.item[1].SetDefaults(Utils.Next<int>(WorldGen.genRand, HoloChestLoot2), false);
				chest.item[1].stack = WorldGen.genRand.Next(1, 3);
				chest.item[2].SetDefaults(Utils.Next<int>(WorldGen.genRand, HoloChestLoot2), false);
				chest.item[2].stack = WorldGen.genRand.Next(1, 3);
				chest.item[3].SetDefaults(Utils.Next<int>(WorldGen.genRand, HoloChestLoot3), false);
				chest.item[3].stack = WorldGen.genRand.Next(8, 12);
				chest.item[4].SetDefaults(Utils.Next<int>(WorldGen.genRand, HoloChestLoot), false);
			}
		}
	}
}
