using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class AncientSoulCaller : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Soul Caller");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\nSummons a swarm of Soul Skulls\nGets buffed from soul-related armoury");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 104;
			base.item.width = 38;
			base.item.height = 42;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 4;
			base.item.mana = 5;
			base.item.crit = 4;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.NPCDeath6.WithVolume(0.5f);
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("SoulSkull");
			base.item.shootSpeed = 26f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 mouseWorld = Main.MouseWorld;
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 70.5f;
			float num = (float)(3.0 + (double)Utils.NextFloat(Main.rand) * 6.0);
			Vector2 vector = Utils.RotatedByRandom(Vector2.UnitY, 6.32);
			Projectile.NewProjectile(position.X, position.Y, vector.X * num, vector.Y * num, type, damage, knockBack, player.whoAmI, mouseWorld.X, mouseWorld.Y);
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).lostSoulSet)
			{
				Vector2 mouseWorld2 = Main.MouseWorld;
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 70.5f;
				float num2 = (float)(3.0 + (double)Utils.NextFloat(Main.rand) * 6.0);
				Vector2 vector2 = Utils.RotatedByRandom(Vector2.UnitY, 6.32);
				Projectile.NewProjectile(position.X, position.Y, vector2.X * num2, vector2.Y * num2, type, damage, knockBack, player.whoAmI, mouseWorld2.X, mouseWorld2.Y);
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).wanderingSoulSet)
			{
				Vector2 mouseWorld3 = Main.MouseWorld;
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 70.5f;
				float num3 = (float)(3.0 + (double)Utils.NextFloat(Main.rand) * 6.0);
				Vector2 vector3 = Utils.RotatedByRandom(Vector2.UnitY, 6.32);
				Projectile.NewProjectile(position.X, position.Y, vector3.X * num3, vector3.Y * num3, type, damage, knockBack, player.whoAmI, mouseWorld3.X, mouseWorld.Y);
			}
			return false;
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
					return 1.75f;
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

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LostSoul", 5);
			modRecipe.AddIngredient(null, "SapphireBar", 5);
			modRecipe.AddIngredient(1508, 15);
			modRecipe.AddIngredient(182, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LostSoul", 5);
			modRecipe.AddIngredient(null, "ScarletBar", 5);
			modRecipe.AddIngredient(1508, 15);
			modRecipe.AddIngredient(182, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
