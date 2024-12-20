using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	[AutoloadEquip(new EquipType[]
	{
		10
	})]
	public class InfectionShield : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Thornshield");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n-2 defense\nDouble tap a direction to dash\n14% increased druidic critical strike chance\nBashing an enemy inflicts Cursed Flames\nLeaves ichor-inflicting sparks behind the player");
		}

		public override void SafeSetDefaults()
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

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			target.AddBuff(39, 300, false);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
			{
				for (int i = 0; i < 1; i++)
				{
					if (Main.rand.Next(10) == 0)
					{
						Projectile.NewProjectile(new Vector2(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height)), new Vector2(0f, 0f), base.mod.ProjectileType("IchorSpark"), 1, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			redePlayer.infectedThornshield = true;
			redePlayer.dashMod = 1;
			player.statDefense -= 2;
			druidDamagePlayer.druidCrit += 14;
		}
	}
}
