using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class DragonLeadHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dragon-Lead Helm");
			base.Tooltip.SetDefault("6% increased druid and ranged damage \nGrants immunity to lava");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 28;
			base.item.value = Item.sellPrice(0, 8, 50, 0);
			base.item.rare = 4;
			base.item.defense = 6;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedDamage += 0.06f;
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.06f;
			player.lavaImmune = true;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DragonLeadBody>() && legs.type == ModContent.ItemType<DragonLeadLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Ranged attacks will fertilise nearby druid plants, making them last longer";
			DruidDamagePlayer.ModPlayer(player).dragonLeadBonus = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DragonLeadBar", 10);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
