using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.IO;

namespace Redemption.StructureHelper.ChestHelper
{
	internal class ChestRule
	{
		public virtual bool UsesWeight
		{
			get
			{
				return false;
			}
		}

		public virtual string Name
		{
			get
			{
				return "Unknown Rule";
			}
		}

		public virtual string Tooltip
		{
			get
			{
				return "Probably a bug! Report me!";
			}
		}

		public Loot AddItem(Item item)
		{
			Loot loot = new Loot(item.DeepClone(), 1, 0, 1);
			this.pool.Add(loot);
			return loot;
		}

		public void RemoveItem(Loot loot)
		{
			this.pool.Remove(loot);
		}

		public virtual void PlaceItems(Chest chest, ref int nextIndex)
		{
		}

		public virtual TagCompound Serizlize()
		{
			return null;
		}

		public static ChestRule Deserialize(TagCompound tag)
		{
			string str = tag.GetString("Type");
			ChestRule rule;
			if (!(str == "Guaranteed"))
			{
				if (!(str == "Chance"))
				{
					if (!(str == "Pool"))
					{
						if (!(str == "PoolChance"))
						{
							rule = null;
						}
						else
						{
							rule = ChestRulePoolChance.Deserialize(tag);
						}
					}
					else
					{
						rule = ChestRulePool.Deserialize(tag);
					}
				}
				else
				{
					rule = ChestRuleChance.Deserialize(tag);
				}
			}
			else
			{
				rule = ChestRuleGuaranteed.Deserialize(tag);
			}
			return rule;
		}

		public TagCompound SerializePool()
		{
			TagCompound tag = new TagCompound();
			tag.Add("Count", this.pool.Count);
			for (int i = 0; i < this.pool.Count; i++)
			{
				tag.Add("Pool" + i, this.pool[i].Serialize());
			}
			return tag;
		}

		public static List<Loot> DeserializePool(TagCompound tag)
		{
			List<Loot> loot = new List<Loot>();
			int count = tag.GetInt("Count");
			for (int i = 0; i < count; i++)
			{
				loot.Add(Loot.Deserialze(tag.GetCompound("Pool" + i)));
			}
			return loot;
		}

		public virtual ChestRule Clone()
		{
			ChestRule clone = new ChestRule();
			for (int i = 0; i < this.pool.Count; i++)
			{
				clone.pool.Add(this.pool[i].Clone());
			}
			return clone;
		}

		public List<Loot> pool = new List<Loot>();
	}
}
