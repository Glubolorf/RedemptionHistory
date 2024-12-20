using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class StaffOfSoulless : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scepter of the Soulless");
			base.Tooltip.SetDefault("Shoots 5 tiny blackened hearts in an arc, making targets soulless\nWhile holding this, life regen is greatly increased...");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 333;
			base.item.magic = true;
			base.item.mana = 16;
			base.item.width = 92;
			base.item.height = 92;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("TinyBlackenedHeartPro");
			base.item.shootSpeed = 29f;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(RedeColor.SoullessColour);
				}
			}
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("BlackenedHeartBuff2"), 4, true);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float num = 5f;
			float num2 = MathHelper.ToRadians(25f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int num3 = 0;
			while ((float)num3 < num)
			{
				Vector2 vector = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-num2, num2, (float)num3 / (num - 1f)), default(Vector2)) * 0.2f;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				num3++;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LifeFruitStaff", 1);
			modRecipe.AddIngredient(null, "BlackenedHeart", 1);
			modRecipe.AddIngredient(null, "SmallShadesoul", 12);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
