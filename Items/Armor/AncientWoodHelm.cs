using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class AncientWoodHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Wood Helmet");
			base.Tooltip.SetDefault("Increases your max number of minions\nIncreases minion damage by 4%");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 24;
			base.item.value = 100;
			base.item.rare = 1;
			base.item.defense = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions++;
			player.minionDamage *= 1.04f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<AncientWoodBody>() && legs.type == ModContent.ItemType<AncientWoodLeggings>();
		}

		public override void ArmorSetShadows(Player player)
		{
			if (Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
			{
				for (int i = 0; i < 1; i++)
				{
					int index = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 3, 0f, 0f, 100, default(Color), 2f);
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
			player.setBonus = "Increases your max number of minions, Immune to poison";
			player.maxMinions++;
			player.buffImmune[20] = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 20);
			modRecipe.AddTile(null, "AncientWoodWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
