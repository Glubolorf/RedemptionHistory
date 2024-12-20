using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class WaraxeOfShade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Waraxe of Shade");
			base.Tooltip.SetDefault("Makes the target become soulless\nWhile holding this, life regen is greatly increased...");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1200;
			base.item.melee = true;
			base.item.width = 118;
			base.item.height = 96;
			base.item.axe = 45;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.useStyle = 1;
			base.item.knockBack = 17f;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = true;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(RedeColor.SoullessColour);
				}
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(base.mod.BuffType("BlackenedHeartDebuff"), 120, false);
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("BlackenedHeartBuff2"), 4, true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LifeFruitAxe", 1);
			modRecipe.AddIngredient(null, "BlackenedHeart", 1);
			modRecipe.AddIngredient(null, "SmallShadesoul", 18);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
