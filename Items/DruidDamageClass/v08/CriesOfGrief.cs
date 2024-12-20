using System;
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
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).wanderingSoulSet)
			{
				base.item.damage = 550;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).shadeSet)
			{
				base.item.damage = 600;
			}
			else if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).lostSoulSet)
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
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterSpirits)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).wanderingSoulSet)
				{
					return 1.45f;
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).shadeSet)
				{
					return 1.65f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterSpirits)
				{
					return 1.35f;
				}
				return 1f;
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num = 3;
			for (int i = 0; i < num; i++)
			{
				int num2 = Main.rand.Next(3);
				if (num2 != 0)
				{
					Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
					float num3 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					vector *= num3;
					Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				else
				{
					Vector2 vector2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
					float num4 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					vector2 *= num4;
					Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, base.mod.ProjectileType("CriesOfGriefPro1"), damage * 2, knockBack, player.whoAmI, 0f, 0f);
				}
			}
			return false;
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
