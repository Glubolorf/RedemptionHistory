using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class KingsGreatstave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King's Oak Greatstave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nRapidly shoots either Acorns, Leaves, Pine Needles, Nature Orbs, or Spores\nRight-clicking will summon a Pixie Trinity [c/bee7c9:(10 Second Duration)]\n[c/71ee8d:-Guardian Info-]\n[c/a0db98:Type:] Pixie\n[c/98dbc3:Special Ability:] Swift-Swing/Stream-Shot/Druidic Embrace/Warmth\n[c/98c1db:Effects:] Combined effects of all pixies");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 18;
			base.item.height = 78;
			base.item.width = 78;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 1;
			base.item.crit = 18;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("KingsOakShot" + (Main.rand.Next(5) + 1));
			base.item.shootSpeed = 12f;
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
				base.item.buffType = base.mod.BuffType("NatureGuardian25Buff");
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().longerGuardians)
				{
					base.item.buffTime = 1200;
				}
				else
				{
					base.item.buffTime = 600;
				}
				base.item.shoot = base.mod.ProjectileType("NatureGuardian25");
				return !player.HasBuff(base.mod.BuffType("GuardianCooldownDebuff"));
			}
			base.item.mana = 0;
			base.item.buffType = 0;
			base.item.buffTime = 0;
			base.item.shoot = base.mod.ProjectileType("KingsOakShot" + (Main.rand.Next(5) + 1));
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

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2 && player.itemAnimation == 0)
			{
				return true;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveTripleShot)
			{
				float numberProjectiles = 3f;
				float rotation = MathHelper.ToRadians(15f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int i = 0;
				while ((float)i < numberProjectiles)
				{
					Vector2 perturbedSpeed2 = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.8f;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, base.mod.ProjectileType("KingsOakShot" + (Main.rand.Next(5) + 1)), damage, knockBack, player.whoAmI, 0f, 0f);
					i++;
				}
				return false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveScatterShot && Main.rand.Next(5) == 0)
			{
				int numberProjectiles2 = 2 + Main.rand.Next(2);
				for (int j = 0; j < numberProjectiles2; j++)
				{
					Vector2 perturbedSpeed3 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
					float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed3 *= scale;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed3.X, perturbedSpeed3.Y, base.mod.ProjectileType("KingsOakShot" + (Main.rand.Next(5) + 1)), damage, knockBack, player.whoAmI, 0f, 0f);
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
					Vector2 perturbedSpeed4 = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation2, rotation2, (float)k / (numberProjectiles3 - 1f)), default(Vector2)) * 0.8f;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed4.X, perturbedSpeed4.Y, base.mod.ProjectileType("KingsOakShot" + (Main.rand.Next(5) + 1)), damage, knockBack, player.whoAmI, 0f, 0f);
					k++;
				}
				return false;
			}
			return true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().burnStaves)
			{
				target.AddBuff(24, 180, false);
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

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AcornStaff", 1);
			modRecipe.AddIngredient(null, "AncientWoodStave", 1);
			modRecipe.AddIngredient(null, "BorealStave", 1);
			modRecipe.AddIngredient(null, "EbonwoodStave", 1);
			modRecipe.AddIngredient(null, "LivingWoodStave", 1);
			modRecipe.AddIngredient(null, "MahoganyStave", 1);
			modRecipe.AddIngredient(null, "PalmStave", 1);
			modRecipe.AddIngredient(null, "SmallLostSoul", 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "AcornStaff", 1);
			modRecipe2.AddIngredient(null, "AncientWoodStave", 1);
			modRecipe2.AddIngredient(null, "BorealStave", 1);
			modRecipe2.AddIngredient(null, "ShadewoodStave", 1);
			modRecipe2.AddIngredient(null, "LivingWoodStave", 1);
			modRecipe2.AddIngredient(null, "MahoganyStave", 1);
			modRecipe2.AddIngredient(null, "PalmStave", 1);
			modRecipe2.AddIngredient(null, "SmallLostSoul", 5);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
