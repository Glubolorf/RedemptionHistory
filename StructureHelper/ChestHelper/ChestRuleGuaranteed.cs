using System;
using Terraria;
using Terraria.ModLoader.IO;

namespace Redemption.StructureHelper.ChestHelper
{
	internal class ChestRuleGuaranteed : ChestRule
	{
		public override string Name
		{
			get
			{
				return "Guaranteed Rule";
			}
		}

		public override string Tooltip
		{
			get
			{
				return "Always generates every item in the rule\nItems are generated in the order they appear here";
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
				if (nextIndex >= 40)
				{
					return;
				}
				chest.item[nextIndex] = this.pool[i].GetLoot();
				nextIndex++;
			}
		}

		public override TagCompound Serizlize()
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("Type", "Guaranteed");
			tagCompound.Add("Pool", base.SerializePool());
			return tagCompound;
		}

		public new static ChestRule Deserialize(TagCompound tag)
		{
			return new ChestRuleGuaranteed
			{
				pool = ChestRule.DeserializePool(tag.GetCompound("Pool"))
			};
		}

		public override ChestRule Clone()
		{
			ChestRuleGuaranteed clone = new ChestRuleGuaranteed();
			for (int i = 0; i < this.pool.Count; i++)
			{
				clone.pool.Add(this.pool[i].Clone());
			}
			return clone;
		}
	}
}
