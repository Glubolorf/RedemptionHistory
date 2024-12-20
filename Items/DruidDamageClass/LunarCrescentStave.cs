using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class LunarCrescentStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lunar Crescent Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots a star from the sky\nRight-clicking will summon a Lunar Statuette [c/bee7c9:(15 Second Duration)]\n[c/71ee8d:-Guardian Info-]\n[c/a0db98:Type:] Mystic\n[c/98dbc3:Special Ability:] Triple-Shot/Swift-Swing/Nightshade's Embrace\n[c/98c1db:Effects:] Staves that shoot a single projectile will shoot 2 more in an arc, Staves swing a lot faster,\nMana Enhancement/Improved Sight/Mobility Enhancement at night");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 35;
			base.item.width = 56;
			base.item.height = 60;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 1, 8, 30);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = 9;
			base.item.shootSpeed = 16f;
			base.item.autoReuse = false;
			base.item.useTurn = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2 && player.itemAnimation == 0)
			{
				base.item.mana = 1;
				base.item.buffType = base.mod.BuffType("NatureGuardian14Buff");
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().longerGuardians)
				{
					base.item.buffTime = 1500;
				}
				else
				{
					base.item.buffTime = 900;
				}
				base.item.shoot = base.mod.ProjectileType("NatureGuardian14");
				return !player.HasBuff(base.mod.BuffType("GuardianCooldownDebuff"));
			}
			base.item.mana = 0;
			base.item.buffType = 0;
			base.item.buffTime = 0;
			base.item.shoot = 9;
			return true;
		}

		public override void UseStyle(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().rapidStave)
				{
					player.AddBuff(base.mod.BuffType("GuardianCooldownDebuff"), 2700, true);
					return;
				}
				player.AddBuff(base.mod.BuffType("GuardianCooldownDebuff"), 3600, true);
			}
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterStaves)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().rapidStave)
				{
					return 1.45f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().rapidStave)
				{
					return 1.35f;
				}
				return 1f;
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2 && player.itemAnimation == 0)
			{
				return true;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveStreamShot && Main.rand.Next(5) == 0)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX * 1.25f, speedY * 1.25f, type, damage, knockBack, player.whoAmI, 0f, 0f);
				Projectile.NewProjectile(position.X, position.Y, speedX * 0.75f, speedY * 0.75f, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveTripleShot)
			{
				float numberProjectiles = 3f;
				float rotation = MathHelper.ToRadians(15f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int i = 0;
				while ((float)i < numberProjectiles)
				{
					Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.8f;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
					i++;
				}
				return false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveScatterShot && Main.rand.Next(5) == 0)
			{
				int numberProjectiles2 = 2 + Main.rand.Next(2);
				for (int j = 0; j < numberProjectiles2; j++)
				{
					Vector2 perturbedSpeed2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
					float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed2 *= scale;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				return false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveQuadShot)
			{
				float numberProjectiles3 = 5f;
				float rotation2 = MathHelper.ToRadians(15f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int k = 0;
				while ((float)k < numberProjectiles3)
				{
					Vector2 perturbedSpeed3 = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation2, rotation2, (float)k / (numberProjectiles3 - 1f)), default(Vector2)) * 0.8f;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed3.X, perturbedSpeed3.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
					k++;
				}
				return false;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DemoniteStave", 1);
			modRecipe.AddIngredient(null, "GrassStave", 1);
			modRecipe.AddIngredient(null, "DonjonStave", 1);
			modRecipe.AddIngredient(null, "HellstoneStave", 1);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "CrimtaneStave", 1);
			modRecipe2.AddIngredient(null, "GrassStave", 1);
			modRecipe2.AddIngredient(null, "DonjonStave", 1);
			modRecipe2.AddIngredient(null, "HellstoneStave", 1);
			modRecipe2.AddTile(26);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 21, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
