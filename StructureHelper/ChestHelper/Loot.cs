using System;
using Terraria;
using Terraria.ModLoader.IO;

namespace Redemption.StructureHelper.ChestHelper
{
	internal class Loot
	{
		public Loot(Item item, int min, int max = 0, int weight = 1)
		{
			this.min = min;
			this.max = ((max == 0) ? min : max);
			this.weight = weight;
			Item newItem = item.Clone();
			newItem.stack = 1;
			this.LootItem = newItem;
		}

		public Item GetLoot()
		{
			Item item = this.LootItem.Clone();
			item.stack = WorldGen.genRand.Next(this.min, this.max);
			return item;
		}

		public TagCompound Serialize()
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("Item", this.LootItem);
			tagCompound.Add("Min", this.min);
			tagCompound.Add("Max", this.max);
			tagCompound.Add("Weight", this.weight);
			return tagCompound;
		}

		public static Loot Deserialze(TagCompound tag)
		{
			return new Loot(tag.Get<Item>("Item"), tag.GetInt("Min"), tag.GetInt("Max"), tag.GetInt("Weight"));
		}

		public Loot Clone()
		{
			return new Loot(this.LootItem.DeepClone(), this.min, this.max, this.weight);
		}

		public Item LootItem;

		public int min;

		public int max;

		public int weight;
	}
}
