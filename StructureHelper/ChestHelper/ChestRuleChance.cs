using System;
using Terraria;
using Terraria.ModLoader.IO;

namespace Redemption.StructureHelper.ChestHelper
{
	internal class ChestRuleChance : ChestRule
	{
		public override string Name
		{
			get
			{
				return "Chance Rule";
			}
		}

		public override string Tooltip
		{
			get
			{
				return "Attempts to generate all items in the rule, \nwith a configurable chance to generate each.\nItems are attempted in the order they appear here";
			}
		}

		public override void PlaceItems(Chest chest, ref int nextIndex)
		{
			if (nextIndex >= 40)
			{
				return;
			}
			for (int i = 0; i < this.pool.Count; i++)
			{
				if (Utils.NextFloat(WorldGen.genRand, 1f) <= this.chance)
				{
					chest.item[nextIndex] = this.pool[i].GetLoot();
					nextIndex++;
				}
			}
		}

		public override TagCompound Serizlize()
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("Type", "Chance");
			tagCompound.Add("Chance", this.chance);
			tagCompound.Add("Pool", base.SerializePool());
			return tagCompound;
		}

		public new static ChestRule Deserialize(TagCompound tag)
		{
			return new ChestRuleChance
			{
				chance = tag.GetFloat("Chance"),
				pool = ChestRule.DeserializePool(tag.GetCompound("Pool"))
			};
		}

		public override ChestRule Clone()
		{
			ChestRuleChance clone = new ChestRuleChance();
			for (int i = 0; i < this.pool.Count; i++)
			{
				clone.pool.Add(this.pool[i].Clone());
			}
			clone.chance = this.chance;
			return clone;
		}

		public float chance;
	}
}
