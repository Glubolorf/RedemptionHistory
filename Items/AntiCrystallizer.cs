using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class AntiCrystallizer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Anti-Crystallizer Band");
			base.Tooltip.SetDefault("Makes you immune to all Xenomite infections");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(10, 4));
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 30;
			base.item.value = 10000;
			base.item.rare = 11;
			base.item.accessory = true;
			base.item.expert = true;
		}

		public override void PostUpdate()
		{
			Lighting.AddLight(base.item.Center, Color.Green.ToVector3() * 0.55f * Main.essScale);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.buffImmune[ModContent.BuffType<XenomiteDebuff>()] = true;
			player.buffImmune[ModContent.BuffType<XenomiteDebuff2>()] = true;
		}
	}
}
