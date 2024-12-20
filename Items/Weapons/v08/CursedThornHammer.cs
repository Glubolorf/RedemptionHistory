﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class CursedThornHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Hammer");
			base.Tooltip.SetDefault("Creates brambles of cursed thorns when hitting an enemy, dealing summon damage");
		}

		public override void SetDefaults()
		{
			base.item.damage = 700;
			base.item.melee = true;
			base.item.width = 52;
			base.item.height = 48;
			base.item.useTime = 2;
			base.item.useAnimation = 20;
			base.item.hammer = 100;
			base.item.useStyle = 1;
			base.item.knockBack = 11f;
			base.item.value = Item.buyPrice(0, 55, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, base.mod.ProjectileType("ThornSeed2"), base.item.damage, base.item.knockBack, Main.myPlayer, 0f, 0f);
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}
	}
}