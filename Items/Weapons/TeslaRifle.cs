using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TeslaRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				TeslaRifle.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = TeslaRifle.customGlowMask;
			base.DisplayName.SetDefault("Tesla Rifle");
			base.Tooltip.SetDefault("Rapidly fires tesla blasts");
		}

		public override void SetDefaults()
		{
			base.item.damage = 208;
			base.item.ranged = true;
			base.item.width = 70;
			base.item.height = 42;
			base.item.useAnimation = 8;
			base.item.useTime = 8;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(1, 50, 0, 0);
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/XenoEyeLaser1");
			base.item.autoReuse = true;
			base.item.shoot = 440;
			base.item.shootSpeed = 5f;
			base.item.glowMask = TeslaRifle.customGlowMask;
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

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 2;
			int num2 = 2;
			float num3 = 0.3f;
			Vector2 vector = default(Vector2);
			for (int i = 0; i < num; i++)
			{
				float num4 = 8f * speedX + (float)Main.rand.Next(-num2, num2 + 1) * num3;
				float num5 = 8f * speedY + (float)Main.rand.Next(-num2, num2 + 1) * num3;
				float num6 = (float)Math.Atan((double)(num5 / num4));
				vector..ctor(position.X + 75f * (float)Math.Cos((double)num6), position.Y + 75f * (float)Math.Sin((double)num6));
				float num7 = (float)Main.mouseX + Main.screenPosition.X;
				if (num7 < player.position.X)
				{
					vector..ctor(position.X - 75f * (float)Math.Cos((double)num6), position.Y - 75f * (float)Math.Sin((double)num6));
				}
				int num8 = Projectile.NewProjectile(vector.X, vector.Y, num4, num5, 440, damage, knockBack, Main.myPlayer, 0f, 0f);
				Main.projectile[num8].ranged = true;
				Main.projectile[num8].magic = false;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BluePrints", 1);
			modRecipe.AddIngredient(null, "TeslaManipulatorPrototype", 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
