using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class GloomDruidCap : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gloom Mycelium Cap");
			base.Tooltip.SetDefault("4% increased druid damage and critical chance \nimproves vision");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 16;
			base.item.value = 9000;
			base.item.rare = 2;
			base.item.defense = 4;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.04f;
			druidDamagePlayer.druidCrit += 4;
			player.nightVision = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<GloomDruidCapplate>() && legs.type == ModContent.ItemType<GloomDruidLeggings>();
		}

		public override void ArmorSetShadows(Player player)
		{
			if (Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
			{
				for (int i = 0; i < 1; i++)
				{
					int index = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, ModContent.DustType<ShroomDust1>(), 0f, 0f, 100, default(Color), 2f);
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
			player.setBonus = "Summoning a nature guardian will summon a second nature guardian from your next stave.";
			DruidDamagePlayer.ModPlayer(player).gloomBonus = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "GloomMushroom", 15);
			modRecipe.AddIngredient(154, 25);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
