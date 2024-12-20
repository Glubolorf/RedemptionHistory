using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.DruidProjectiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.DruidS
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class SapphireGarland : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sapphire Garland");
			base.Tooltip.SetDefault("5% increased druidic damage\n5% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 12;
			base.item.value = Item.sellPrice(0, 6, 0, 0);
			base.item.rare = 6;
			base.item.defense = 8;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.05f;
			druidDamagePlayer.druidCrit += 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SapphireChestplate>() && legs.type == ModContent.ItemType<SapphireLeggings>();
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "You are surrounded by 3 Corruption Spirits, damages foes and reforms quickly after death";
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).sapphireBonus = true;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().sapphireBonus && Main.rand.Next(100) == 0 && player.ownedProjectileCounts[ModContent.ProjectileType<CorruptSoul2>()] <= 2)
			{
				Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<CorruptSoul2>(), 60, 0f, player.whoAmI, 0f, 0f);
			}
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SapphireBar", 12);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
