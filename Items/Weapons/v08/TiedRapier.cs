using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class TiedRapier : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tied's Rapier");
			base.Tooltip.SetDefault("Strikes foes in an arc, then stabs in the direction of the cursor\nOnly usable after Lunatic Cultist have been defeated\n[c/aa00ff:Epic]");
		}

		public override void SetDefaults()
		{
			base.item.width = 86;
			base.item.height = 86;
			base.item.maxStack = 1;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.useStyle = 5;
			base.item.useAnimation = 20;
			base.item.useTime = 20;
			base.item.UseSound = SoundID.Item1;
			base.item.damage = 100;
			base.item.knockBack = 3f;
			base.item.melee = true;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.rare = 11;
			base.item.shoot = base.mod.ProjType("TiedRapierPro");
			base.item.shootSpeed = 4f;
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedAncientCultist;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(new Color(170, 0, 255));
				}
			}
		}
	}
}
