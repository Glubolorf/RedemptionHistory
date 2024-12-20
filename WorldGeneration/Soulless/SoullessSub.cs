using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Placeable.Furniture.Misc;
using Redemption.Items.Placeable.Furniture.Shade;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.NPCs.Soulless;
using Redemption.Tiles.Containers;
using Redemption.Tiles.Furniture.Shade;
using Redemption.Tiles.Natural;
using Redemption.Tiles.Ores;
using Redemption.Tiles.Plants;
using Redemption.Tiles.Tiles;
using Redemption.Walls;
using SubworldLibrary;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.Utilities;
using Terraria.World.Generation;

namespace Redemption.WorldGeneration.Soulless
{
	public class SoullessSub : Subworld
	{
		public override int width
		{
			get
			{
				return 2200;
			}
		}

		public override int height
		{
			get
			{
				return 1800;
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
				return true;
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
				List<GenPass> list = new List<GenPass>();
				list.Add(new SubworldGenPass(0.01f, delegate(GenerationProgress progress)
				{
					progress.Message = "Loading";
					this.SoullessDecoWiring();
				}));
				list.Add(new SubworldGenPass(1f, delegate(GenerationProgress progress)
				{
					WorldGen.noTileActions = true;
					Main.spawnTileY = 827;
					Main.spawnTileX = 432;
					Main.worldSurface = 635.0;
					Main.rockLayer = 635.0;
					this.SoullessBiome();
				}));
				list.Add(new SubworldGenPass(0.05f, delegate(GenerationProgress progress)
				{
					this.SoullessDeco2();
				}));
				list.Add(new SubworldGenPass(0.3f, delegate(GenerationProgress progress)
				{
					progress.Message = "Furnishing Caverns";
					WorldGen.noTileActions = false;
					this.SoullessDeco();
				}));
				list.Add(new SubworldGenPass(0.1f, delegate(GenerationProgress progress)
				{
					progress.Message = "Sprinkling Spooky Pots";
					for (int num = 0; num < 1800; num++)
					{
						int xAxis = WorldGen.genRand.Next(1755);
						int yAxis = WorldGen.genRand.Next(1755);
						for (int DecoX = xAxis; DecoX < xAxis + 45; DecoX++)
						{
							for (int DecoY = yAxis; DecoY < yAxis + 45; DecoY++)
							{
								if ((int)Main.tile[DecoX, DecoY].type == ModContent.TileType<ShadestoneBrickTile>() && !Framing.GetTileSafely(DecoX, DecoY - 1).active() && WorldGen.genRand.Next(20) == 0)
								{
									WorldGen.PlaceObject(DecoX, DecoY - 1, ModContent.TileType<ShadePots>(), true, Main.rand.Next(3), 0, -1, -1);
								}
								if ((int)Main.tile[DecoX, DecoY].type == ModContent.TileType<ShadestoneTile>() && !Framing.GetTileSafely(DecoX, DecoY - 1).active() && WorldGen.genRand.Next(40) == 0)
								{
									WorldGen.PlaceObject(DecoX, DecoY - 1, ModContent.TileType<ShadePots>(), true, Main.rand.Next(3), 0, -1, -1);
								}
							}
						}
					}
				}));
				list.Add(new SubworldGenPass(0.1f, delegate(GenerationProgress progress)
				{
					progress.Message = "Growing Cysts";
					for (int i = 0; i < 1800; i++)
					{
						for (int j = 0; j < 1800; j++)
						{
							if (((int)Main.tile[i, j].type == ModContent.TileType<ShadestoneTile>() || (int)Main.tile[i, j].type == ModContent.TileType<ShadestoneMossyTile>()) && !Framing.GetTileSafely(i, j - 1).active() && WorldGen.InWorld(i, j, 0) && WorldGen.genRand.Next(10) == 0)
							{
								WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ShadeCyst>(), true, 0, 0, -1, -1);
							}
						}
					}
				}));
				list.Add(new SubworldGenPass(0.1f, delegate(GenerationProgress progress)
				{
					progress.Message = "Sprinkling Spooky Objects";
					for (int num = 0; num < 1800; num++)
					{
						int xAxis = WorldGen.genRand.Next(1755);
						int yAxis = WorldGen.genRand.Next(1755);
						for (int DecoX = xAxis; DecoX < xAxis + 45; DecoX++)
						{
							for (int DecoY = yAxis; DecoY < yAxis + 45; DecoY++)
							{
								if ((int)Main.tile[DecoX, DecoY].type == ModContent.TileType<ShadestoneTile>() && !Framing.GetTileSafely(DecoX, DecoY - 1).active() && WorldGen.genRand.Next(3) == 0)
								{
									switch (WorldGen.genRand.Next(9))
									{
									case 0:
										WorldGen.PlaceObject(DecoX, DecoY - 1, ModContent.TileType<ShadeDeco1>(), true, 0, 0, -1, -1);
										break;
									case 1:
										WorldGen.PlaceObject(DecoX, DecoY - 1, ModContent.TileType<ShadeDeco2>(), true, 0, 0, -1, -1);
										break;
									case 2:
										WorldGen.PlaceObject(DecoX, DecoY - 1, ModContent.TileType<ShadeDeco3>(), true, 0, 0, -1, -1);
										break;
									case 3:
										WorldGen.PlaceObject(DecoX, DecoY - 1, ModContent.TileType<ShadeDeco4>(), true, 0, 0, -1, -1);
										break;
									case 4:
										WorldGen.PlaceObject(DecoX, DecoY - 1, ModContent.TileType<ShadeDeco6>(), true, 0, 0, -1, -1);
										break;
									case 5:
										WorldGen.PlaceObject(DecoX, DecoY - 1, ModContent.TileType<ShadeDeco7>(), true, 0, 0, -1, -1);
										break;
									case 6:
										WorldGen.PlaceObject(DecoX, DecoY - 1, ModContent.TileType<ShadeDeco8>(), true, 0, 0, -1, -1);
										break;
									case 7:
										WorldGen.PlaceObject(DecoX, DecoY - 1, ModContent.TileType<ShadeDeco9>(), true, 0, 0, -1, -1);
										break;
									case 8:
										WorldGen.PlaceObject(DecoX, DecoY - 1, ModContent.TileType<ShadeDeco10>(), true, 0, 0, -1, -1);
										break;
									}
								}
							}
						}
					}
					for (int num2 = 0; num2 < 1200; num2++)
					{
						int xAxis2 = WorldGen.genRand.Next(1755);
						int yAxis2 = WorldGen.genRand.Next(1755);
						for (int DecoX2 = xAxis2; DecoX2 < xAxis2 + 45; DecoX2++)
						{
							for (int DecoY2 = yAxis2; DecoY2 < yAxis2 + 45; DecoY2++)
							{
								if ((int)Main.tile[DecoX2, DecoY2].wall == ModContent.WallType<ShadestoneBrickWallTile>() && WorldGen.genRand.Next(1400) == 0)
								{
									switch (WorldGen.genRand.Next(3))
									{
									case 0:
										WorldGen.PlaceObject(DecoX2, DecoY2 - 1, ModContent.TileType<ShadeDeco5>(), false, 0, 0, -1, -1);
										break;
									case 1:
										WorldGen.PlaceObject(DecoX2, DecoY2 - 1, ModContent.TileType<ShadeDeco5_1>(), false, 0, 0, -1, -1);
										break;
									case 2:
										WorldGen.PlaceObject(DecoX2, DecoY2 - 1, ModContent.TileType<ShadeDeco5_2>(), false, 0, 0, -1, -1);
										break;
									}
								}
							}
						}
					}
				}));
				list.Add(new SubworldGenPass(0.01f, delegate(GenerationProgress progress)
				{
					progress.Message = "Here, Have a Fungus";
					WorldGen.AddTrees();
					for (int num = 0; num < 1800; num++)
					{
						int xAxis = WorldGen.genRand.Next(1755);
						int yAxis = WorldGen.genRand.Next(1755);
						for (int DecoX = xAxis; DecoX < xAxis + 45; DecoX++)
						{
							for (int DecoY = yAxis; DecoY < yAxis + 45; DecoY++)
							{
								if (((int)Main.tile[DecoX, DecoY].type == ModContent.TileType<ShadestoneMossyTile>() || (int)Main.tile[DecoX, DecoY].type == ModContent.TileType<ShadestoneBrickMossyTile>()) && !Framing.GetTileSafely(DecoX, DecoY + 1).active())
								{
									if (WorldGen.genRand.Next(15) == 0)
									{
										WorldGen.PlaceObject(DecoX, DecoY + 1, ModContent.TileType<Nooseroot_Large>(), true, Main.rand.Next(3), 0, -1, -1);
									}
									if (WorldGen.genRand.Next(15) == 0)
									{
										WorldGen.PlaceObject(DecoX, DecoY + 1, ModContent.TileType<Nooseroot_Medium>(), true, Main.rand.Next(3), 0, -1, -1);
									}
									if (WorldGen.genRand.Next(15) == 0)
									{
										WorldGen.PlaceObject(DecoX, DecoY + 1, ModContent.TileType<Nooseroot_Small>(), true, Main.rand.Next(3), 0, -1, -1);
									}
								}
							}
						}
					}
				}));
				list.Add(new SubworldGenPass(0.01f, delegate(GenerationProgress progress)
				{
					progress.Message = "Smoothing Tiles";
					int[] TileArray = new int[]
					{
						ModContent.TileType<ShadestoneTile>(),
						ModContent.TileType<AncientDirtTile>(),
						ModContent.TileType<MasksTile>(),
						ModContent.TileType<ShadestoneMossyTile>()
					};
					for (int i = 0; i < 1800; i++)
					{
						for (int j = 0; j < 1800; j++)
						{
							if (Enumerable.Contains<int>(TileArray, (int)Main.tile[i, j].type) && WorldGen.InWorld(i, j, 0))
							{
								BaseWorldGen.SmoothTiles(i, j, i + 1, j + 1);
							}
						}
					}
					WorldGen.noTileActions = false;
				}));
				return list;
			}
		}

		public void SoullessBiome()
		{
			Mod mod = Redemption.Inst;
			Dictionary<Color, int> dictionary = new Dictionary<Color, int>();
			Color key = new Color(0, 255, 0);
			dictionary[key] = ModContent.TileType<ShadestoneTile>();
			Color key2 = new Color(0, 0, 255);
			dictionary[key2] = ModContent.TileType<ShadestoneBrickTile>();
			Color key3 = new Color(255, 0, 0);
			dictionary[key3] = ModContent.TileType<ShadestoneRubbleTile>();
			Color key4 = new Color(255, 255, 255);
			dictionary[key4] = ModContent.TileType<MasksTile>();
			Color key5 = new Color(100, 50, 30);
			dictionary[key5] = ModContent.TileType<AncientDirtTile>();
			Color key6 = new Color(20, 20, 20);
			dictionary[key6] = ModContent.TileType<BedrockTile>();
			Color key7 = new Color(110, 115, 157);
			dictionary[key7] = ModContent.TileType<PrisonBarsTile>();
			Color black = new Color(22, 26, 35);
			dictionary[black] = ModContent.TileType<ShadestoneMossyTile>();
			Color key8 = new Color(0, 255, 255);
			dictionary[key8] = ModContent.TileType<ShadestoneHSlabTile>();
			Color key9 = new Color(150, 150, 150);
			dictionary[key9] = -2;
			Color black2 = Color.Black;
			dictionary[black2] = -1;
			Dictionary<Color, int> colorToTile = dictionary;
			Dictionary<Color, int> dictionary2 = new Dictionary<Color, int>();
			black2 = new Color(0, 0, 255);
			dictionary2[black2] = ModContent.WallType<ShadestoneWallTile>();
			key9 = new Color(255, 0, 0);
			dictionary2[key9] = ModContent.WallType<ShadestoneBrickWallTile>();
			key8 = new Color(100, 100, 100);
			dictionary2[key8] = ModContent.WallType<LeadFenceBlackWall>();
			black = Color.Black;
			dictionary2[black] = -1;
			Dictionary<Color, int> colorToWall = dictionary2;
			BaseWorldGenTex.GetTexGenerator(mod.GetTexture("WorldGeneration/Soulless/SoullessCaverns"), colorToTile, mod.GetTexture("WorldGeneration/Soulless/SoullessCavernsWalls"), colorToWall, mod.GetTexture("WorldGeneration/Soulless/SoullessCavernsLiquids"), mod.GetTexture("WorldGeneration/Soulless/SoullessCavernsSlopes"), null, null).Generate(0, 0, true, true);
		}

		public void SoullessDeco()
		{
			Mod mod = Redemption.Inst;
			Texture2D ObjectTex = mod.GetTexture("WorldGeneration/Soulless/SoullessCavernsObjects");
			Dictionary<Color, int> dictionary = new Dictionary<Color, int>();
			Color key = new Color(255, 0, 0);
			dictionary[key] = 268;
			Color key2 = new Color(150, 0, 0);
			dictionary[key2] = 262;
			Color key3 = new Color(0, 0, 255);
			dictionary[key3] = 255;
			Color key4 = new Color(100, 0, 0);
			dictionary[key4] = 267;
			Color key5 = new Color(0, 255, 0);
			dictionary[key5] = 265;
			Color key6 = new Color(0, 150, 0);
			dictionary[key6] = 258;
			Color key7 = new Color(0, 100, 0);
			dictionary[key7] = 266;
			Color key8 = new Color(255, 255, 0);
			dictionary[key8] = 259;
			Color key9 = new Color(255, 0, 255);
			dictionary[key9] = 264;
			Color key10 = new Color(0, 255, 255);
			dictionary[key10] = 263;
			Color key11 = new Color(0, 100, 100);
			dictionary[key11] = 256;
			Color key12 = new Color(100, 0, 100);
			dictionary[key12] = 261;
			Color key13 = new Color(120, 120, 120);
			dictionary[key13] = 260;
			Color key14 = new Color(180, 180, 180);
			dictionary[key14] = 257;
			Color key15 = new Color(141, 134, 135);
			dictionary[key15] = 431;
			Color key16 = new Color(247, 245, 213);
			dictionary[key16] = 430;
			Color key17 = new Color(203, 185, 151);
			dictionary[key17] = 433;
			Color key18 = new Color(255, 66, 0);
			dictionary[key18] = 426;
			Color key19 = new Color(255, 66, 66);
			dictionary[key19] = 434;
			Color key20 = new Color(255, 200, 66);
			dictionary[key20] = 432;
			Color key21 = new Color(255, 66, 200);
			dictionary[key21] = 156;
			Color key22 = new Color(255, 120, 255);
			dictionary[key22] = 155;
			Color black = new Color(100, 120, 255);
			dictionary[black] = 252;
			Color key23 = new Color(233, 120, 233);
			dictionary[key23] = 274;
			Color key24 = new Color(220, 0, 0);
			dictionary[key24] = 273;
			Color black2 = Color.Black;
			dictionary[black2] = -1;
			Dictionary<Color, int> colorToObj = dictionary;
			BaseWorldGenTex.GetTexGenerator(ObjectTex, colorToObj, null, null, mod.GetTexture("WorldGeneration/Soulless/SoullessCavernsLiquids"), null, null, null).Generate(0, 0, true, true);
			for (int x2 = 0; x2 < ObjectTex.Width; x2++)
			{
				for (int y2 = 0; y2 < ObjectTex.Height; y2++)
				{
					ushort type = Main.tile[x2, y2].type;
					if (type <= 156)
					{
						if (type != 155)
						{
							if (type == 156)
							{
								Main.tile[x2, y2].ClearTile();
								GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadePianoTile>());
							}
						}
						else
						{
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlaceRand1(x2, y2, ModContent.TileType<ShadeHangedCellTile>());
						}
					}
					else
					{
						switch (type)
						{
						case 252:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadePortcullisClose>());
							break;
						case 253:
						case 254:
						case 269:
						case 270:
						case 271:
						case 272:
							break;
						case 255:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeTable2Tile>());
							break;
						case 256:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeWorkbenchTile>());
							break;
						case 257:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeCandleTile>());
							break;
						case 258:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeChandelierTile>());
							break;
						case 259:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeLampTile>());
							break;
						case 260:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeSinkTile>());
							break;
						case 261:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadePillar2>());
							break;
						case 262:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeTableTile>());
							break;
						case 263:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeSofaTile>());
							break;
						case 264:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadePillar1>());
							break;
						case 265:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeLanternTile>());
							break;
						case 266:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeCandelabraTile>());
							break;
						case 267:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeBookshelfTile>());
							break;
						case 268:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeChairTile>());
							break;
						case 273:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlaceAlt(x2, y2, ModContent.TileType<ShadeChairTile>());
							break;
						case 274:
							Main.tile[x2, y2].ClearTile();
							GenUtils.ObjectPlace(x2, y2, 136);
							break;
						default:
							switch (type)
							{
							case 426:
								Main.tile[x2, y2].ClearTile();
								GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeBathtubTile>());
								break;
							case 430:
								Main.tile[x2, y2].ClearTile();
								GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeDresserTile>());
								break;
							case 431:
								Main.tile[x2, y2].ClearTile();
								GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeDoorClosed>());
								break;
							case 432:
								Main.tile[x2, y2].ClearTile();
								GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeClockTile>());
								break;
							case 433:
								Main.tile[x2, y2].ClearTile();
								GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeHangedCell2Tile>());
								break;
							case 434:
								Main.tile[x2, y2].ClearTile();
								GenUtils.ObjectPlace(x2, y2, ModContent.TileType<ShadeBedTile>());
								break;
							}
							break;
						}
					}
				}
			}
			Dictionary<Color, int> dictionary2 = new Dictionary<Color, int>();
			black2 = new Color(255, 0, 0);
			dictionary2[black2] = 153;
			key24 = new Color(0, 255, 0);
			dictionary2[key24] = 154;
			key23 = new Color(255, 0, 255);
			dictionary2[key23] = 155;
			black = Color.Black;
			dictionary2[black] = -1;
			Dictionary<Color, int> colorToTile2 = dictionary2;
			Texture2D platTex = mod.GetTexture("WorldGeneration/Soulless/SoullessCavernsPlatforms");
			BaseWorldGenTex.GetTexGenerator(platTex, colorToTile2, null, null, null, null, null, null).Generate(0, 0, true, true);
			for (int x3 = 0; x3 < platTex.Width; x3++)
			{
				for (int y3 = 0; y3 < platTex.Height; y3++)
				{
					switch (Main.tile[x3, y3].type)
					{
					case 153:
						Main.tile[x3, y3].ClearTile();
						WorldGen.PlaceTile(x3, y3, ModContent.TileType<ShadestonePlatformTile>(), true, false, -1, 0);
						WorldGen.SlopeTile(x3, y3, 1);
						break;
					case 154:
						Main.tile[x3, y3].ClearTile();
						WorldGen.PlaceTile(x3, y3, ModContent.TileType<ShadestonePlatformTile>(), true, false, -1, 0);
						WorldGen.SlopeTile(x3, y3, 2);
						break;
					case 155:
						Main.tile[x3, y3].ClearTile();
						WorldGen.PlaceTile(x3, y3, ModContent.TileType<ShadestonePlatformTile>(), true, false, -1, 0);
						break;
					}
				}
			}
			WorldGen.PlaceObject(584, 1214, (int)((ushort)ModContent.TileType<WhiteAngelStatue_Masked>()), true, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, 584, 1214, (int)((ushort)ModContent.TileType<WhiteAngelStatue_Masked>()), 0, 0, -1, -1);
			WorldGen.PlaceObject(1097, 1244, (int)((ushort)ModContent.TileType<WardenAltar>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, 1097, 1244, (int)((ushort)ModContent.TileType<WardenAltar>()), 0, 0, -1, -1);
			WorldGen.PlaceObject(1218, 1193, (int)((ushort)ModContent.TileType<ShadeStoneGateTile>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, 1218, 1193, (int)((ushort)ModContent.TileType<ShadeStoneGateTile>()), 0, 0, -1, -1);
			WorldGen.PlaceObject(844, 798, (int)((ushort)ModContent.TileType<ShadeStoneGateTile>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, 844, 798, (int)((ushort)ModContent.TileType<ShadeStoneGateTile>()), 0, 0, -1, -1);
			WorldGen.PlaceObject(181, 1403, (int)((ushort)ModContent.TileType<CelestineDreamsongTile>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, 181, 1403, (int)((ushort)ModContent.TileType<CelestineDreamsongTile>()), 0, 0, -1, -1);
			WorldGen.PlaceObject(644, 1264, (int)((ushort)ModContent.TileType<ShadeChairTile>()), false, 0, 0, 0, 1);
			NetMessage.SendObjectPlacment(-1, 644, 1264, (int)((ushort)ModContent.TileType<ShadeChairTile>()), 0, 0, -1, 1);
			WorldGen.PlaceObject(649, 1220, (int)((ushort)ModContent.TileType<ShadeChairTile>()), false, 0, 0, 0, 1);
			NetMessage.SendObjectPlacment(-1, 649, 1220, (int)((ushort)ModContent.TileType<ShadeChairTile>()), 0, 0, -1, 1);
			WorldGen.PlaceObject(659, 1220, (int)((ushort)ModContent.TileType<ShadeChairTile>()), false, 0, 0, 0, 1);
			NetMessage.SendObjectPlacment(-1, 659, 1220, (int)((ushort)ModContent.TileType<ShadeChairTile>()), 0, 0, -1, 1);
			WorldGen.PlaceObject(665, 1242, (int)((ushort)ModContent.TileType<ShadeChairTile>()), false, 0, 0, 0, 1);
			NetMessage.SendObjectPlacment(-1, 665, 1242, (int)((ushort)ModContent.TileType<ShadeChairTile>()), 0, 0, -1, 1);
			WorldGen.PlaceObject(707, 1264, (int)((ushort)ModContent.TileType<ShadeChairTile>()), false, 0, 0, 0, 1);
			NetMessage.SendObjectPlacment(-1, 707, 1264, (int)((ushort)ModContent.TileType<ShadeChairTile>()), 0, 0, -1, 1);
			WorldGen.PlaceObject(428, 808, (int)((ushort)ModContent.TileType<ShadeChairTile>()), false, 0, 0, 0, 1);
			NetMessage.SendObjectPlacment(-1, 428, 808, (int)((ushort)ModContent.TileType<ShadeChairTile>()), 0, 0, -1, 1);
			WorldGen.PlaceObject(427, 1324, (int)((ushort)ModContent.TileType<ShadeChairTile>()), false, 0, 0, 0, 1);
			NetMessage.SendObjectPlacment(-1, 427, 1324, (int)((ushort)ModContent.TileType<ShadeChairTile>()), 0, 0, -1, 1);
			WorldGen.PlaceObject(889, 1659, (int)((ushort)ModContent.TileType<SielukaivoShadowbinderTile>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, 889, 1659, (int)((ushort)ModContent.TileType<SielukaivoShadowbinderTile>()), 0, 0, -1, -1);
			WorldGen.PlaceObject(937, 1103, (int)((ushort)ModContent.TileType<DreambinderElixirTile>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, 937, 1103, (int)((ushort)ModContent.TileType<DreambinderElixirTile>()), 0, 0, -1, -1);
			WorldGen.PlaceObject(565, 1566, (int)((ushort)ModContent.TileType<SongOfTheAbyssTile>()), false, 0, 0, -1, -1);
			NetMessage.SendObjectPlacment(-1, 565, 1566, (int)((ushort)ModContent.TileType<SongOfTheAbyssTile>()), 0, 0, -1, -1);
			this.SpookChest(668, 1196, 0);
			this.SpookChest(658, 799, 0);
			this.SpookChest(480, 888, 0);
			this.SpookChest(673, 1006, 0);
			this.SpookChest(457, 1568, 0);
			this.SpookChest(573, 1573, 0);
			this.SpookChest(638, 1252, 0);
			this.SpookChest(719, 1250, 0);
			this.SpookChest(669, 799, 0);
			this.SpookChest(653, 1099, 0);
			this.SpookChest(154, 798, 0);
			this.SpookChest(137, 1001, 0);
			this.SpookChest(119, 1327, 0);
			this.SpookChest(210, 1005, 0);
			this.SpookChest(265, 1084, 0);
			this.SpookChest(351, 1085, 0);
			this.SpookChest(269, 1577, 1);
			this.SpookChest(750, 1553, 1);
			this.SpookChest(393, 1407, 1);
			this.SpookChest(1021, 1161, 1);
			this.SpookChest(291, 871, 1);
			this.SpookChest(632, 929, 1);
		}

		public void SoullessDecoWiring()
		{
			Mod inst = Redemption.Inst;
			Dictionary<Color, int> dictionary = new Dictionary<Color, int>();
			Color key = new Color(255, 0, 0);
			dictionary[key] = 249;
			Color black = Color.Black;
			dictionary[black] = -1;
			Dictionary<Color, int> colorToTile2 = dictionary;
			Texture2D tex = inst.GetTexture("WorldGeneration/Soulless/SoullessCavernsWiring");
			BaseWorldGenTex.GetTexGenerator(tex, colorToTile2, null, null, null, null, null, null).Generate(0, 0, true, true);
			for (int x = 0; x < tex.Width; x++)
			{
				for (int y = 0; y < tex.Height; y++)
				{
					ushort type = Main.tile[x, y].type;
					if (type == 249)
					{
						Main.tile[x, y].ClearTile();
						Main.tile[x, y].wire(true);
					}
				}
			}
		}

		public void SpookChest(int x, int y, int chestID)
		{
			Redemption inst = Redemption.Inst;
			int PlacementSuccess = WorldGen.PlaceChest(x, y, (ushort)ModContent.TileType<ShadeChestTile>(), false, 0);
			int[] SpookChestLoot = new int[]
			{
				ModContent.ItemType<BlackenedHeart>()
			};
			int[] SpookChestLoot2 = new int[]
			{
				ModContent.ItemType<Shadesoul>()
			};
			int[] SpookChestLoot3 = new int[]
			{
				ModContent.ItemType<SmallShadesoul>(),
				ModContent.ItemType<VesselFrag>()
			};
			int[] SpookChestLoot4 = new int[]
			{
				ModContent.ItemType<ShadeKnife>()
			};
			int[] SpookChestLoot5 = new int[]
			{
				ModContent.ItemType<ShadeBathtub>(),
				ModContent.ItemType<ShadeBed>(),
				ModContent.ItemType<ShadeBookshelf>(),
				ModContent.ItemType<ShadeCandelabra>(),
				ModContent.ItemType<ShadeCandle>(),
				ModContent.ItemType<ShadeChair>(),
				ModContent.ItemType<ShadeChandelier>(),
				ModContent.ItemType<ShadeClock>(),
				ModContent.ItemType<ShadeDoor>(),
				ModContent.ItemType<ShadeDresser>(),
				ModContent.ItemType<ShadeLamp>(),
				ModContent.ItemType<ShadeLantern>(),
				ModContent.ItemType<ShadePiano>(),
				ModContent.ItemType<ShadeSink>(),
				ModContent.ItemType<ShadeSofa>(),
				ModContent.ItemType<ShadeTable>(),
				ModContent.ItemType<ShadeWorkbench>()
			};
			if (PlacementSuccess >= 0)
			{
				Chest chest = Main.chest[PlacementSuccess];
				Item item0 = chest.item[0];
				UnifiedRandom genRand0 = WorldGen.genRand;
				int[] array0 = new int[]
				{
					ModContent.ItemType<SoulScroll>(),
					ModContent.ItemType<MaskOfGrief>(),
					ModContent.ItemType<StatuetteOfFaith>(),
					ModContent.ItemType<ManiacsLantern>(),
					ModContent.ItemType<CageFlail>(),
					ModContent.ItemType<SoulCandles>()
				};
				item0.SetDefaults(Utils.Next<int>(genRand0, array0), false);
				chest.item[1].SetDefaults(Utils.Next<int>(WorldGen.genRand, SpookChestLoot2), false);
				chest.item[1].stack = WorldGen.genRand.Next(1, 3);
				chest.item[2].SetDefaults(Utils.Next<int>(WorldGen.genRand, SpookChestLoot3), false);
				chest.item[2].stack = WorldGen.genRand.Next(8, 12);
				chest.item[3].SetDefaults(Utils.Next<int>(WorldGen.genRand, SpookChestLoot5), false);
				chest.item[3].stack = WorldGen.genRand.Next(1, 4);
				if (WorldGen.genRand.Next(2) == 0)
				{
					chest.item[4].SetDefaults(Utils.Next<int>(WorldGen.genRand, SpookChestLoot4), false);
					chest.item[4].stack = WorldGen.genRand.Next(100, 600);
				}
				if (WorldGen.genRand.Next(6) == 0)
				{
					chest.item[5].SetDefaults(Utils.Next<int>(WorldGen.genRand, SpookChestLoot), false);
				}
			}
		}

		public void SoullessDeco2()
		{
			Redemption inst = Redemption.Inst;
			for (int x = 0; x < 1800; x++)
			{
				for (int y = 0; y < 1800; y++)
				{
					if ((int)Main.tile[x, y].type == ModContent.TileType<PrisonBarsTile>() && WorldGen.InWorld(x, y, 0))
					{
						Wiring.ActuateForced(x, y);
					}
				}
			}
			for (int x2 = 922; x2 < 936; x2++)
			{
				for (int y2 = 798; y2 < 811; y2++)
				{
					if ((int)Main.tile[x2, y2].type == ModContent.TileType<ShadestoneBrickTile>())
					{
						Wiring.ActuateForced(x2, y2);
					}
				}
			}
		}

		public override void Load()
		{
			if (Main.netMode != 1)
			{
				NPC.NewNPC(7456, 12752, ModContent.NPCType<LostLight>(), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(9120, 14368, ModContent.NPCType<LostLight>(), 0, 1f, 0f, 0f, 0f, 255);
				NPC.NewNPC(13232, 22288, ModContent.NPCType<LostLight>(), 0, 2f, 0f, 0f, 0f, 255);
				NPC.NewNPC(18960, 19024, ModContent.NPCType<LostLight>(), 0, 3f, 0f, 0f, 0f, 255);
				NPC.NewNPC(20896, 18944, ModContent.NPCType<LostLight>(), 0, 4f, 0f, 0f, 0f, 255);
			}
			Main.cloudAlpha = 0f;
			Main.numClouds = 0;
			Main.rainTime = 0;
			Main.raining = false;
			Main.maxRaining = 0f;
			Main.slimeRain = false;
			Main.dayTime = true;
			Main.time = 40000.0;
		}

		public override UIState loadingUIState
		{
			get
			{
				return new SoullessSub.SoullessSubworldLoadUI();
			}
		}

		public override void Unload()
		{
			SubworldCache.AddCache("Redemption", "RedeWorld", "downedTheWarden", new bool?(RedeWorld.downedTheWarden), null);
			SubworldCache.AddCache("Redemption", "RedeWorld", "downedMansionWraith", new bool?(RedeWorld.downedMansionWraith), null);
			SubworldCache.AddCache("Redemption", "RedeWorld", "redemptionPoints", null, new int?(RedeWorld.redemptionPoints));
		}

		public class SoullessSubworldLoadUI : UIDefaultSubworldLoad
		{
			public override void OnInitialize()
			{
				this.soullessBackground = ModContent.GetTexture("Redemption/WorldGeneration/Soulless/SoullessSubworldTex");
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, Main.UIScaleMatrix);
				spriteBatch.Draw(this.soullessBackground, new Rectangle(Main.screenWidth - this.soullessBackground.Width, Main.screenHeight - this.soullessBackground.Height + 50 - (int)(this.animationTimer * 10.0), this.soullessBackground.Width, this.soullessBackground.Height), null, Color.White * (float)(this.animationTimer / 5.0) * 0.8f);
				string message = "This subworld is a WIP and has plans for big changes, there is nothing past the boss of this subworld.\nBoth the boss and this subworld will be changed once v0.8 is out";
				Vector2 messageSize2 = Main.fontDeathText.MeasureString(message) * 0.7f;
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontDeathText, message, new Vector2((float)Main.screenWidth / 2f - messageSize2.X / 2f, (float)Main.screenHeight - messageSize2.Y - 20f), Color.White, 0f, Vector2.Zero, new Vector2(0.7f), -1f, 2f);
				spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
				base.DrawSelf(spriteBatch);
			}

			public override void Update(GameTime gameTime)
			{
				this.animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
				if (this.animationTimer > 5.0)
				{
					this.animationTimer = 5.0;
				}
			}

			private double animationTimer;

			private Texture2D soullessBackground;
		}
	}
}
