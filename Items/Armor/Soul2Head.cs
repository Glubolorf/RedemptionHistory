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
	public class Soul2Head : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wandering Soul's Head");
			base.Tooltip.SetDefault("6% increased druidic damage\n3% damage reduction\n4% increased druidic critical strike chance\nIncreased night vision\n[c/bdffff:Spirit Level +1]");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 20;
			base.item.value = 500;
			base.item.rare = 4;
			base.item.defense = 6;
			this.spiritWeapon = false;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer modPlayer2 = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			druidDamagePlayer.druidDamage += 0.06f;
			druidDamagePlayer.druidCrit += 4;
			player.endurance += 0.03f;
			player.nightVision = true;
			modPlayer2.spiritLevel++;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("Soul2Body") && legs.type == base.mod.ItemType("Soul2Leggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increases spirits summoned by 1\nYou emit an aura of light";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritExtras++;
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
