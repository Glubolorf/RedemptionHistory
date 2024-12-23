﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class MythrilsBane : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mythril's Bane");
			base.Tooltip.SetDefault("'A mighty weapon wielded by Zephos...'\nOnly usable after Plantera has been defeated\n[c/aa00ff:Epic]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 100;
			base.item.melee = true;
			base.item.width = 84;
			base.item.height = 84;
			base.item.useTime = 8;
			base.item.useAnimation = 8;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 6;
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedPlantBoss;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(69, 1800, false);
		}
	}
}
