﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class CriesOfGrief : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cries of Grief");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\nReleases cries of grief that have a chance to deal double damage\nGets buffed from soul-related armoury");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(10, 3));
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 530;
			base.item.width = 20;
			base.item.height = 26;
			base.item.useTime = 13;
			base.item.useAnimation = 13;
			base.item.useStyle = 4;
			base.item.mana = 7;
			base.item.crit = 4;
			base.item.knockBack = 1f;
			base.item.value = Item.buyPrice(0, 0, 1, 75);
			base.item.UseSound = SoundID.NPCDeath52.WithVolume(0.5f);
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("CriesOfGriefPro2");
			base.item.shootSpeed = 15f;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().wanderingSoulSet)
			{
				base.item.damage = 550;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().shadeSet)
			{
				base.item.damage = 600;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>().lostSoulSet)
			{
				base.item.damage = 535;
			}
			else
			{
				base.item.damage = 530;
			}
			return true;
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterSpirits)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().wanderingSoulSet)
				{
					return 1.45f;
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().shadeSet)
				{
					return 1.65f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterSpirits)
				{
					return 1.35f;
				}
				return 1f;
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 3;
			for (int i = 0; i < numberProjectiles; i++)
			{
				if (Main.rand.Next(3) != 0)
				{
					Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
					float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed *= scale;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				else
				{
					Vector2 perturbedSpeed2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
					float scale2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed2 *= scale2;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, base.mod.ProjectileType("CriesOfGriefPro1"), damage * 2, knockBack, player.whoAmI, 0f, 0f);
				}
			}
			return false;
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

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(31, 1);
			modRecipe.AddIngredient(null, "Shadesoul", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
