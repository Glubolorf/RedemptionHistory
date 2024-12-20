using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Redemption.StructureHelper.ChestHelper
{
	internal class ChestEntity : ModTileEntity
	{
		public void SaveChestRulesFile()
		{
			string path = ModLoader.ModPath.Replace("Mods", "SavedStructures");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			string thisPath = Path.Combine(path, "SavedChest_" + DateTime.Now.ToString("d-M-y----H-m-s-f"));
			Main.NewText("Chest data saved as " + thisPath, Color.Yellow, false);
			File.Create(thisPath).Close();
			TagIO.ToFile(this.SaveChestRules(), thisPath, true);
		}

		public TagCompound SaveChestRules()
		{
			TagCompound tag = new TagCompound();
			tag.Add("Count", this.rules.Count);
			for (int i = 0; i < this.rules.Count; i++)
			{
				tag.Add("Rule" + i, this.rules[i].Serizlize());
			}
			return tag;
		}

		public static List<ChestRule> LoadChestRules(TagCompound tag)
		{
			List<ChestRule> rules = new List<ChestRule>();
			int count = tag.GetInt("Count");
			for (int i = 0; i < count; i++)
			{
				rules.Add(ChestRule.Deserialize(tag.GetCompound("Rule" + i)));
			}
			return rules;
		}

		public static void SetChest(Chest chest, List<ChestRule> rules)
		{
			int index = 0;
			rules.ForEach(delegate(ChestRule n)
			{
				n.PlaceItems(chest, ref index);
			});
		}

		public override void Update()
		{
			Dust.NewDustPerfect(Utils.ToVector2(this.Position) * 16f + Vector2.UnitY * 8f + Utils.RotatedByRandom(Vector2.One, 6.28000020980835) * 6f, 111, new Vector2?(Vector2.Zero), 0, default(Color), 0.5f);
		}

		public override TagCompound Save()
		{
			return this.SaveChestRules();
		}

		public override void Load(TagCompound tag)
		{
			this.rules = ChestEntity.LoadChestRules(tag);
		}

		public override bool ValidTile(int i, int j)
		{
			Tile tile = Framing.GetTileSafely(i, j);
			return tile.type == 21 || TileLists.ModdedChests.Contains((int)tile.type);
		}

		public List<ChestRule> rules = new List<ChestRule>();
	}
}
