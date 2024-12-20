using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Druid;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class CreatorHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Creation Druid's Hood");
			base.Tooltip.SetDefault("9% increased druidic damage\nIncreases maximum mana by 60\nIncreased life and mana regen");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 28;
			base.item.rare = 10;
			base.item.defense = 16;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.09f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			player.statManaMax2 += 60;
			player.lifeRegen += 5;
			player.manaRegen += 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<CreatorBody>() && legs.type == ModContent.ItemType<CreatorLegs>();
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawOutlines = true;
			player.armorEffectDrawShadow = true;
			if (Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
			{
				for (int i = 0; i < 1; i++)
				{
					int index = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 163, 0f, 0f, 100, default(Color), 2f);
					Main.dust[index].noGravity = true;
					Main.dust[index].noLight = true;
					Dust dust = Main.dust[index];
					dust.velocity.X = dust.velocity.X - player.velocity.X * 0.5f;
					dust.velocity.Y = dust.velocity.Y - player.velocity.Y * 0.5f;
				}
			}
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "You are surrounded by an aura of Nature, buffing any player within it and debuffing enemies.";
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).creationBonus = true;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().creationBonus && player.ownedProjectileCounts[ModContent.ProjectileType<NatureRing>()] == 0)
			{
				Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<NatureRing>(), 100, 0f, player.whoAmI, 0f, 0f);
			}
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CreationFragment", 10);
			modRecipe.AddIngredient(3467, 8);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
