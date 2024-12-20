﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Prefixes
{
	public class DryadPrefix : ModPrefix
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
				base.mod.AddPrefix("Dryad's", new DryadPrefix());
			}
			return false;
		}

		public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
		{
			damageMult += 0.15f;
			useTimeMult -= 0.1f;
			critBonus += 5;
			knockbackMult += 0.15f;
		}

		public override void Apply(Item item)
		{
		}

		public override void ModifyValue(ref float valueMult)
		{
			float multiplier = 3.5f;
			valueMult *= multiplier;
		}
	}
}