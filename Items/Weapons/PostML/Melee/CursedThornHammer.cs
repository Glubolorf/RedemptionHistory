using System;
using Redemption.Projectiles.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class CursedThornHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Twinrose Hammer");
			base.Tooltip.SetDefault("Creates brambles of cursed thorns when hitting an enemy, dealing summon damage");
		}

		public override void SetDefaults()
		{
			base.item.damage = 700;
			base.item.melee = true;
			base.item.width = 52;
			base.item.height = 48;
			base.item.useTime = 2;
			base.item.useAnimation = 20;
			base.item.hammer = 100;
			base.item.useStyle = 1;
			base.item.knockBack = 11f;
			base.item.value = Item.buyPrice(0, 55, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().thornCrown)
			{
				flat += 50f;
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<ThornSeed2>(), base.item.damage, base.item.knockBack, Main.myPlayer, 0f, 0f);
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
