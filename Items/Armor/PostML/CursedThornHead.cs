using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class CursedThornHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Helmet");
			base.Tooltip.SetDefault("10% increased melee and ranged damage\n20% chance not to consume ammo\n12% reduced mana usage\n+25 max life");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 26;
		}

		public override void UpdateEquip(Player player)
		{
			player.ammoCost80 = true;
			player.meleeDamage *= 1.1f;
			player.rangedDamage *= 1.1f;
			player.statLifeMax2 += 25;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("CursedThornBody") && legs.type == base.mod.ItemType("CursedThornLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Standing still increases damage reduction by 25% and grants knockback immunity.";
			if (player.velocity.X == 0f && player.velocity.Y == 0f)
			{
				player.endurance += 0.25f;
				player.noKnockback = true;
			}
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
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

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThorns", 12);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
