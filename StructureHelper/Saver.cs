using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Redemption.StructureHelper
{
	internal static class Saver
	{
		public static void SaveToFile(Rectangle target, string targetPath = null)
		{
			string path = ModLoader.ModPath.Replace("Mods", "SavedStructures");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			string thisPath = targetPath ?? Path.Combine(path, "SavedStructure_" + DateTime.Now.ToString("d-M-y----H-m-s-f"));
			Main.NewText("Structure saved as " + thisPath, Color.Yellow, false);
			File.Create(thisPath).Close();
			TagIO.ToFile(Saver.SaveStructure(target), thisPath, true);
		}

		public static void SaveMultistructureToFile(ref List<TagCompound> toSave, string targetPath = null)
		{
			string path = ModLoader.ModPath.Replace("Mods", "SavedStructures");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			string thisPath = targetPath ?? Path.Combine(path, "SavedMultiStructure_" + DateTime.Now.ToString("d-M-y----H-m-s-f"));
			Main.NewText("Structure saved as " + thisPath, Color.Yellow, false);
			File.Create(thisPath).Close();
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("Structures", toSave);
			tagCompound.Add("Version", Redemption.Inst.Version.ToString());
			TagIO.ToFile(tagCompound, thisPath, true);
			toSave.Clear();
		}

		public static TagCompound SaveStructure(Rectangle target)
		{
			TagCompound tag = new TagCompound();
			tag.Add("Version", Redemption.Inst.Version.ToString());
			tag.Add("Width", target.Width);
			tag.Add("Height", target.Height);
			List<TileSaveData> data = new List<TileSaveData>();
			for (int x = target.X; x <= target.X + target.Width; x++)
			{
				for (int y = target.Y; y <= target.Y + target.Height; y++)
				{
					Tile tile = Framing.GetTileSafely(x, y);
					string tileName;
					if (tile.type >= 470)
					{
						tileName = ModContent.GetModTile((int)tile.type).mod.Name + " " + ModContent.GetModTile((int)tile.type).Name;
					}
					else
					{
						tileName = tile.type.ToString();
					}
					string wallName;
					if (tile.wall >= 231)
					{
						wallName = ModContent.GetModWall((int)tile.wall).mod.Name + " " + ModContent.GetModWall((int)tile.wall).Name;
					}
					else
					{
						wallName = tile.wall.ToString();
					}
					TileEntity teTarget = null;
					TagCompound entityTag = null;
					if (TileEntity.ByPosition.ContainsKey(new Point16(x, y)))
					{
						teTarget = TileEntity.ByPosition[new Point16(x, y)];
					}
					string teName;
					if (teTarget != null)
					{
						if (teTarget.type < 2)
						{
							teName = teTarget.type.ToString();
						}
						else
						{
							ModTileEntity entityTarget = ModTileEntity.GetTileEntity((int)teTarget.type);
							if (entityTarget != null)
							{
								teName = entityTarget.mod.Name + " " + entityTarget.Name;
								entityTag = (teTarget as ModTileEntity).Save();
							}
							else
							{
								teName = "";
							}
						}
					}
					else
					{
						teName = "";
					}
					data.Add(new TileSaveData(tileName, wallName, tile.frameX, tile.frameY, tile.bTileHeader, tile.bTileHeader2, tile.bTileHeader3, tile.sTileHeader, teName, entityTag));
				}
			}
			tag.Add("TileData", data);
			return tag;
		}
	}
}
