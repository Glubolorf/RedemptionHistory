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
			Vector2 vector = Main.MouseWorld;
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 70.5f;
			float speed = (float)(3.0 + (double)Utils.NextFloat(Main.rand) * 6.0);
			Vector2 start = Utils.RotatedByRandom(Vector2.UnitY, 6.32);
			Projectile.NewProjectile(position.X, position.Y, start.X * speed, start.Y * speed, type, damage, knockBack, player.whoAmI, vector.X, vector.Y);
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().lostSoulSet)
			{
				Vector2 vector2 = Main.MouseWorld;
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 70.5f;
				float speed2 = (float)(3.0 + (double)Utils.NextFloat(Main.rand) * 6.0);
				Vector2 start2 = Utils.RotatedByRandom(Vector2.UnitY, 6.32);
				Projectile.NewProjectile(position.X, position.Y, start2.X * speed2, start2.Y * speed2, type, damage, knockBack, player.whoAmI, vector2.X, vector2.Y);
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().wanderingSoulSet)
			{
				Vector2 vector3 = Main.MouseWorld;
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 70.5f;
				float speed3 = (float)(3.0 + (double)Utils.NextFloat(Main.rand) * 6.0);
				Vector2 start3 = Utils.RotatedByRandom(Vector2.UnitY, 6.32);
				Projectile.NewProjectile(position.X, position.Y, start3.X * speed3, start3.Y * speed3, type, damage, knockBack, player.whoAmI, vector3.X, vector.Y);
			}
			return false;
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
					return 1.75f;
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
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "LostSoul", 5);
			modRecipe2.AddIngredient(null, "ScarletBar", 5);
			modRecipe2.AddIngredient(1508, 15);
			modRecipe2.AddIngredient(182, 1);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
