using System;
using Redemption.Buffs;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class XeniumGarland : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Visage");
			base.Tooltip.SetDefault("25% increased druidic damage and critical strike chance\nIncreases pickup range for life hearts\nYour Nature Guardians last longer");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 9, 50, 0);
			base.item.rare = 11;
			base.item.defense = 10;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage *= 1.25f;
			druidDamagePlayer.druidCrit += 25;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			player.lifeMagnet = true;
			redePlayer.longerGuardians = true;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<XeniumBody>() && legs.type == ModContent.ItemType<XeniumLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Grants immunity to the Infection, Radioactive Fallout, and infected waters\nAutomatically consumes mana potions when needed";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			player.manaFlower = true;
			player.buffImmune[ModContent.BuffType<XenomiteDebuff>()] = true;
			player.buffImmune[ModContent.BuffType<XenomiteDebuff2>()] = true;
			player.buffImmune[ModContent.BuffType<RadioactiveFalloutDebuff>()] = true;
			player.buffImmune[ModContent.BuffType<HeavyRadiationDebuff>()] = true;
			redePlayer.labWaterImmune = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 12);
			modRecipe.AddIngredient(null, "ArtificalMuscle", 2);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
