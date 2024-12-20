using System;
using System.Collections.Generic;
using Redemption.StructureHelper.ChestHelper;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Redemption.StructureHelper
{
	public static class Generator
	{
		public static bool GenerateStructure(string path, Point16 pos, Mod mod, bool fullPath = false, bool ignoreNull = false)
		{
			return Generator.Generate(Generator.GetTag(path, mod, fullPath), pos, ignoreNull);
		}

		public static bool GenerateMultistructureRandom(string path, Point16 pos, Mod mod, bool fullPath = false, bool ignoreNull = false)
		{
			List<TagCompound> structures = (List<TagCompound>)Generator.GetTag(path, mod, fullPath).GetList<TagCompound>("Structures");
			int index = WorldGen.genRand.Next(structures.Count);
			return Generator.Generate(structures[index], pos, ignoreNull);
		}

		public static bool GenerateMultistructureSpecific(string path, Point16 pos, Mod mod, int index, bool fullPath = false, bool ignoreNull = false)
		{
			List<TagCompound> structures = (List<TagCompound>)Generator.GetTag(path, mod, fullPath).GetList<TagCompound>("Structures");
			if (index >= structures.Count || index < 0)
			{
				Redemption.Inst.Logger.Warn(string.Format("Attempted to generate structure {0} in mutistructure containing {1} structures.", index, structures.Count - 1));
				return false;
			}
			return Generator.Generate(structures[index], pos, ignoreNull);
		}

		public static bool GetDimensions(string path, Mod mod, ref Point16 dims, bool fullPath = false)
		{
			TagCompound tag = Generator.GetTag(path, mod, fullPath);
			dims = new Point16(tag.GetInt("Width"), tag.GetInt("Height"));
			return true;
		}

		public static bool GetMultistructureDimensions(string path, Mod mod, int index, ref Point16 dims, bool fullPath = false)
		{
			List<TagCompound> structures = (List<TagCompound>)Generator.GetTag(path, mod, fullPath).GetList<TagCompound>("Structures");
			if (index >= structures.Count || index < 0)
			{
				dims = new Point16(0, 0);
				Redemption.Inst.Logger.Warn(string.Format("Attempted to get dimensions of structure {0} in mutistructure containing {1} structures.", index, structures.Count - 1));
				return false;
			}
			TagCompound targetStructure = structures[index];
			dims = new Point16(targetStructure.GetInt("Width"), targetStructure.GetInt("Height"));
			return true;
		}

		internal static bool Generate(TagCompound tag, Point16 pos, bool ignoreNull = false)
		{
			List<TileSaveData> data = (List<TileSaveData>)tag.GetList<TileSaveData>("TileData");
			if (data == null)
			{
				Redemption.Inst.Logger.Warn("Corrupt or Invalid structure data.");
				return false;
			}
			int width = tag.GetInt("Width");
			int height = tag.GetInt("Height");
			for (int x = 0; x <= width; x++)
			{
				for (int y = 0; y <= height; y++)
				{
					bool isNullTile = false;
					bool isNullWall = false;
					int index = y + x * (height + 1);
					TileSaveData d = data[index];
					Tile tile = Framing.GetTileSafely((int)pos.X + x, (int)pos.Y + y);
					int type;
					if (!int.TryParse(d.Tile, out type))
					{
						string[] parts = d.Tile.Split(new char[0]);
						if (parts[0] == "Redemption" && parts[1] == "NullBlock" && !ignoreNull)
						{
							isNullTile = true;
						}
						else if (parts.Length > 1 && ModLoader.GetMod(parts[0]) != null && ModLoader.GetMod(parts[0]).TileType(parts[1]) != 0)
						{
							type = ModLoader.GetMod(parts[0]).TileType(parts[1]);
						}
						else
						{
							type = 0;
						}
					}
					int wallType;
					if (!int.TryParse(d.Wall, out wallType))
					{
						string[] parts2 = d.Wall.Split(new char[0]);
						if (parts2[0] == "Redemption" && parts2[1] == "NullWall" && !ignoreNull)
						{
							isNullWall = true;
						}
						else if (parts2.Length > 1 && ModLoader.GetMod(parts2[0]) != null && ModLoader.GetMod(parts2[0]).WallType(parts2[1]) != 0)
						{
							wallType = ModLoader.GetMod(parts2[0]).WallType(parts2[1]);
						}
						else
						{
							wallType = 0;
						}
					}
					if (!d.Active)
					{
						isNullTile = false;
					}
					if (!isNullTile || ignoreNull)
					{
						tile.ClearEverything();
						tile.type = (ushort)type;
						tile.frameX = d.FrameX;
						tile.frameY = d.FrameY;
						tile.bTileHeader = d.BHeader1;
						tile.bTileHeader2 = d.BHeader2;
						tile.bTileHeader3 = d.BHeader3;
						tile.sTileHeader = d.SHeader;
						if (!d.Active)
						{
							tile.inActive(false);
						}
						if (d.TEType != "")
						{
							int typ;
							if (!int.TryParse(d.TEType, out typ))
							{
								string[] parts3 = d.TEType.Split(new char[0]);
								typ = ModLoader.GetMod(parts3[0]).TileEntityType(parts3[1]);
							}
							if (d.TEType != "")
							{
								if (d.TEType == "Redemption ChestEntity" && !ignoreNull)
								{
									Generator.GenerateChest(new Point16((int)pos.X + x, (int)pos.Y + y), d.TEData);
								}
								else
								{
									TileEntity.PlaceEntityNet((int)pos.X + x, (int)pos.Y + y, typ);
									if (d.TEData != null && typ > 2)
									{
										(TileEntity.ByPosition[new Point16((int)pos.X + x, (int)pos.Y + y)] as ModTileEntity).Load(d.TEData);
									}
								}
							}
						}
						else if (type == 21 && d.FrameX % 36 == 0 && d.FrameY % 36 == 0)
						{
							Chest.CreateChest((int)pos.X + x, (int)pos.Y + y, -1);
						}
					}
					if (!isNullWall || ignoreNull)
					{
						tile.wall = (ushort)wallType;
					}
				}
			}
			return true;
		}

		public static void GenerateChest(Point16 pos, TagCompound rules)
		{
			int i = Chest.CreateChest((int)pos.X, (int)pos.Y, -1);
			if (i == -1)
			{
				return;
			}
			new Item().SetDefaults(1, false);
			ChestEntity.SetChest(Main.chest[i], ChestEntity.LoadChestRules(rules));
		}

		internal static bool LoadFile(string path, Mod mod, bool fullPath = false)
		{
			TagCompound tag;
			if (!fullPath)
			{
				tag = TagIO.FromStream(mod.GetFileStream(path, false), true);
			}
			else
			{
				tag = TagIO.FromFile(path, true);
			}
			if (tag == null)
			{
				Redemption.Inst.Logger.Warn("Structure was unable to be found. Are you passing the correct path?");
				return false;
			}
			Generator.StructureDataCache.Add(path, tag);
			return true;
		}

		internal static TagCompound GetTag(string path, Mod mod, bool fullPath = false)
		{
			TagCompound tag;
			if (!Generator.StructureDataCache.ContainsKey(path))
			{
				if (!Generator.LoadFile(path, mod, fullPath))
				{
					return null;
				}
				tag = Generator.StructureDataCache[path];
			}
			else
			{
				tag = Generator.StructureDataCache[path];
			}
			return tag;
		}

		internal static Dictionary<string, TagCompound> StructureDataCache = new Dictionary<string, TagCompound>();
	}
}
