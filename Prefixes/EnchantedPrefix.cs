using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Prefixes
{
	public class EnchantedPrefix : ModPrefix
	{
		public override float RollChance(Item item)
		{
			return 0f;
		}

		public override bool CanRoll(Item item)
		{
			return false;
		}

		public override PrefixCategory Category
		{
			get
			{
				return 5;
			}
		}

		public override bool Autoload(ref string name)
		{
			if (base.Autoload(ref name))
			{
				base.mod.AddPrefix("Enchanted", new EnchantedPrefix());
			}
			return false;
		}

		public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
		{
			damageMult += 0.1f;
		}

		public override void Apply(Item item)
		{
			item.GetGlobalItem<RedeItem>().prefixLifetimeModifier = 1.05f;
		}

		public override void ModifyValue(ref float valueMult)
		{
			float multiplier = 1.6f;
			valueMult *= multiplier;
		}
	}
}
