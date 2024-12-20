using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.IO;

namespace Redemption.StructureHelper.ChestHelper
{
	internal class ChestRulePoolChance : ChestRule
	{
		public override bool UsesWeight
		{
			get
			{
				return true;
			}
		}

		public override string Name
		{
			get
			{
				return "Chance + Pool Rule";
			}
		}

		public override string Tooltip
		{
			get
			{
				return "Has a configurable chance to generate a \nconfigurable amount of items randomly \nselected from the rule. \nCan make use of weight.";
			}
		}

		public override void PlaceItems(Chest chest, ref int nextIndex)
		{
			if (nextIndex >= 40)
			{
				return;
			}
			if (Utils.NextFloat(WorldGen.genRand) <= this.chance)
			{
				List<Loot> toLoot = this.pool;
				for (int i = 0; i < this.itemsToGenerate; i++)
				{
					if (nextIndex >= 40)
					{
						return;
					}
					int maxWeight = 1;
					foreach (Loot loot in toLoot)
					{
						maxWeight += loot.weight;
					}
					int selection = Main.rand.Next(maxWeight);
					int weightTotal = 0;
					Loot selectedLoot = null;
					for (int j = 0; j < toLoot.Count; j++)
					{
						weightTotal += toLoot[j].weight;
						if (selection < weightTotal + 1)
						{
							selectedLoot = toLoot[j];
							toLoot.Remove(selectedLoot);
							break;
						}
					}
					chest.item[nextIndex] = ((selectedLoot != null) ? selectedLoot.GetLoot() : null);
					nextIndex++;
				}
			}
		}

		public override TagCompound Serizlize()
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("Type", "PoolChance");
			tagCompound.Add("Chance", this.chance);
			tagCompound.Add("ToGenerate", this.itemsToGenerate);
			tagCompound.Add("Pool", base.SerializePool());
			return tagCompound;
		}

		public new static ChestRule Deserialize(TagCompound tag)
		{
			return new ChestRulePoolChance
			{
				itemsToGenerate = tag.GetInt("ToGenerate"),
				chance = tag.GetFloat("Chance"),
				pool = ChestRule.DeserializePool(tag.GetCompound("Pool"))
			};
		}

		public override ChestRule Clone()
		{
			ChestRulePoolChance clone = new ChestRulePoolChance();
			for (int i = 0; i < this.pool.Count; i++)
			{
				clone.pool.Add(this.pool[i].Clone());
			}
			clone.itemsToGenerate = this.itemsToGenerate;
			clone.chance = this.chance;
			return clone;
		}

		public int itemsToGenerate;

		public float chance;
	}
}
