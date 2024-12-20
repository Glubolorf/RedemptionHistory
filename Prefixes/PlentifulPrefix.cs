using System;
using Redemption.Items;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Prefixes
{
	public class PlentifulPrefix : ModPrefix
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
				base.mod.AddPrefix("Plentiful", new PlentifulPrefix());
			}
			return false;
		}

		public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
		{
			useTimeMult -= 0.08f;
			shootSpeedMult -= 0.08f;
		}

		public override void Apply(Item item)
		{
			item.GetGlobalItem<RedeItem>().prefixLifetimeModifier = 1.15f;
		}

		public override void ModifyValue(ref float valueMult)
		{
			float multiplier = 1.05f;
			valueMult *= multiplier;
		}
	}
}
