using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Prefixes
{
	public class BlessedPrefix : ModPrefix
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
				base.mod.AddPrefix("Blessed", new BlessedPrefix());
			}
			return false;
		}

		public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
		{
			damageMult += 0.25f;
			critBonus -= 5;
		}

		public override void Apply(Item item)
		{
		}

		public override void ModifyValue(ref float valueMult)
		{
			float multiplier = 3f;
			valueMult *= multiplier;
		}
	}
}
