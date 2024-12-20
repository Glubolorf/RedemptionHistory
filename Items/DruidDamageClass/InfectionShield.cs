using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	[AutoloadEquip(new EquipType[]
	{
		10
	})]
	public class InfectionShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Thornshield");
			base.Tooltip.SetDefault("\n-2 defense\nDouble tap a direction to dash\n14% increased druidic critical strike chance\nInflicts Infection upon dashing into an enemy\nReleases acid-like sparks as you move");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 32;
			base.item.rare = 7;
			base.item.value = 80000;
			base.item.damage = 40;
			base.item.accessory = true;
			base.item.crit = 4;
			base.item.knockBack = 6f;
			base.item.expert = true;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3097, 1);
			modRecipe.AddIngredient(null, "Xenomite", 10);
			modRecipe.AddIngredient(null, "StarliteBar", 6);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer modPlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer2 = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
			{
				for (int i = 0; i < 1; i++)
				{
					if (Main.rand.Next(10) == 0)
					{
						Projectile.NewProjectile(new Vector2(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height)), new Vector2(0f, 0f), ModContent.ProjectileType<IchorSpark>(), 0, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			modPlayer2.infectedThornshield = true;
			modPlayer2.dashMod = 1;
			player.statDefense -= 2;
			modPlayer.druidCrit += 14;
		}
	}
}
