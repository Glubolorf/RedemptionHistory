using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class PortableHoloProjector : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Portable Hologram Projector");
			base.Tooltip.SetDefault("Summon a random hologram-projected minion to fight for you");
		}

		public override void SetDefaults()
		{
			base.item.damage = 300;
			base.item.summon = true;
			base.item.mana = 25;
			base.item.width = 38;
			base.item.height = 22;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(1, 50, 0, 0);
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = base.mod.ProjectileType("HoloProjector");
			base.item.shootSpeed = 0f;
			base.item.buffType = base.mod.BuffType("HoloMinionBuff");
			base.item.buffTime = 3600;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;
			return player.altFunctionUse != 2;
		}

		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BluePrints", 1);
			modRecipe.AddIngredient(2749, 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
