﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Prefixes
{
	public class LivelyPrefix : ModPrefix
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
				base.mod.AddPrefix("Lively", new LivelyPrefix());
			}
			return false;
		}

		public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
		{
			damageMult += 0.12f;
			useTimeMult -= 0.12f;
		}

		public override void Apply(Item item)
		{
		}

		public override void ModifyValue(ref float valueMult)
		{
			float multiplier = 1f;
			valueMult *= multiplier;
		}
	}
}
