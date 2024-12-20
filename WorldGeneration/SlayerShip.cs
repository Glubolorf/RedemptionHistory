using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Items.Lore;
using Redemption.Items.Materials.HM;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.HM.Magic;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.Items.Weapons.HM.Summon;
using Redemption.Tiles.Containers;
using Redemption.Tiles.Furniture.Ship;
using Redemption.Tiles.Tiles;
using Redemption.Walls;
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
			Mod mod = Redemption.Inst;
			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(0, 255, 255)] = ModContent.TileType<SlayerShipPanelTile>();
			colorToTile[new Color(255, 255, 0)] = ModContent.TileType<JunkMetalTile>();
			colorToTile[new Color(100, 100, 100)] = ModContent.TileType<LabSupportBeamTile>();
			colorToTile[new Color(220, 255, 255)] = ModContent.TileType<HalogenLampTile>();
			colorToTile[new Color(255, 0, 255)] = ModContent.TileType<ShipGlassTile>();
			colorToTile[new Color(255, 0, 0)] = ModContent.TileType<ElectricHazardTile>();
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;
			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(0, 255, 0)] = ModContent.WallType<SlayerShipPanelWallTile>();
			colorToWall[new Color(255, 0, 0)] = ModContent.WallType<JunkMetalWall>();
			colorToWall[Color.Black] = -1;
			BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/SlayerShip"), colorToTile, mod.GetTexture("WorldGeneration/SlayerShipWalls"), colorToWall, null, mod.GetTexture("WorldGeneration/SlayerShipSlopes"), null, null).Generate(origin.X, origin.Y, true, true);
			WorldGen.PlaceObject(origin.X + 90, origin.Y + 23, (int)((ushort)ModContent.TileType<SlayerChairTile>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 90, origin.Y + 23, (int)((ushort)ModContent.TileType<SlayerChairTile>()), 0, 0, -1, -1);
			WorldGen.PlaceObject(origin.X + 84, origin.Y + 36, (int)((ushort)ModContent.TileType<SlayerFabricatorTile>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, origin.X + 84, origin.Y + 36, (int)((ushort)ModContent.TileType<SlayerFabricatorTile>()), 0, 0, -1, -1);
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
			Redemption inst = Redemption.Inst;
			int PlacementSuccess = WorldGen.PlaceChest(x, y, (ushort)ModContent.TileType<HolochestTile>(), false, 0);
			int[] HoloChestLoot = new int[]
			{
				ModContent.ItemType<Datalog1>(),
				ModContent.ItemType<Datalog2>(),
				ModContent.ItemType<Datalog3>(),
				ModContent.ItemType<Datalog6>(),
				ModContent.ItemType<Datalog335>(),
				ModContent.ItemType<Datalog772>(),
				ModContent.ItemType<Datalog919>(),
				ModContent.ItemType<Datalog180499>(),
				ModContent.ItemType<Datalog182500>(),
				ModContent.ItemType<Datalog182501>(),
				ModContent.ItemType<Datalog182573>(),
				ModContent.ItemType<Datalog184753>(),
				ModContent.ItemType<Datalog184989>(),
				ModContent.ItemType<Datalog466105>(),
				ModContent.ItemType<Datalog466476>(),
				ModContent.ItemType<Datalog500198>(),
				ModContent.ItemType<Datalog545675>(),
				ModContent.ItemType<Datalog999735>(),
				ModContent.ItemType<Datalog1000000>(),
				ModContent.ItemType<Datalog1012875>(),
				ModContent.ItemType<Datalog3650000>(),
				ModContent.ItemType<Datalog5385430>(),
				ModContent.ItemType<Datalog36500001>(),
				ModContent.ItemType<Datalog164550614>(),
				ModContent.ItemType<Datalog364635000>(),
				ModContent.ItemType<Datalog365000000>(),
				ModContent.ItemType<Datalog389035250>()
			};
			int[] HoloChestLoot2 = new int[]
			{
				ModContent.ItemType<ScrapMetal>(),
				ModContent.ItemType<AIChip>(),
				ModContent.ItemType<Mk1Capacitator>(),
				ModContent.ItemType<Mk2Capacitator>(),
				ModContent.ItemType<Mk3Capacitator>(),
				ModContent.ItemType<Mk1Plating>(),
				ModContent.ItemType<Mk2Plating>(),
				ModContent.ItemType<Mk3Plating>()
			};
			int[] HoloChestLoot3 = new int[]
			{
				ModContent.ItemType<CarbonMyofibre>(),
				ModContent.ItemType<XenomiteShard>()
			};
			if (PlacementSuccess >= 0)
			{
				Chest chest = Main.chest[PlacementSuccess];
				Item item0 = chest.item[0];
				UnifiedRandom genRand0 = WorldGen.genRand;
				int[] array0 = new int[]
				{
					ModContent.ItemType<SlayerBigRevolver>(),
					ModContent.ItemType<SlayerGravGun>(),
					ModContent.ItemType<AndroidMinion>(),
					ModContent.ItemType<SlayersChakram>(),
					ModContent.ItemType<MissileDroneCaller>()
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
