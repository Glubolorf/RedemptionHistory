using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class Soul2Head : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wandering Soul's Head");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\n6% increased druidic damage\n3% damage reduction\n4% increased druidic critical strike chance\nIncreased night vision");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 20;
			base.item.value = 500;
			base.item.rare = 4;
			base.item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.06f;
			druidDamagePlayer.druidCrit += 4;
			player.endurance += 0.03f;
			player.nightVision = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("Soul2Body") && legs.type == base.mod.ItemType("Soul2Leggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Spirit summoning weapons will summon 2 extra spirits\nYou emit an aura of light";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.moreSpirits = true;
			redePlayer.wanderingSoulSet = true;
			player.AddBuff(11, 2, true);
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override bool DrawHead()
		{
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LostSoul", 4);
			modRecipe.AddIngredient(501, 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
