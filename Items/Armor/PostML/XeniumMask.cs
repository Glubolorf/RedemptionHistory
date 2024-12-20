using System;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class XeniumMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Mask");
			base.Tooltip.SetDefault("25% increased magic damage and critical strike chance\n20% reducted mana cost\nIncreased mana regen");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 9, 50, 0);
			base.item.rare = 11;
			base.item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicDamage *= 1.25f;
			player.magicCrit += 25;
			player.manaCost *= 0.8f;
			player.manaRegen += 7;
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
			player.setBonus = "Grants immunity to the Infection, Radioactive Fallout, and infected waters\n+100 max mana\nIncreased pickup range for mana stars\nThe lower your life is, the higher your magic crit chance";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			player.statManaMax2 += 100;
			player.manaMagnet = true;
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.8f && (float)player.statLife > (float)player.statLifeMax2 * 0.6f)
			{
				player.magicCrit += 10;
			}
			else if ((float)player.statLife <= (float)player.statLifeMax2 * 0.6f && (float)player.statLife > (float)player.statLifeMax2 * 0.4f)
			{
				player.magicCrit += 20;
			}
			else if ((float)player.statLife <= (float)player.statLifeMax2 * 0.4f && (float)player.statLife > (float)player.statLifeMax2 * 0.2f)
			{
				player.magicCrit += 35;
			}
			else if ((float)player.statLife <= (float)player.statLifeMax2 * 0.2f)
			{
				player.magicCrit += 50;
			}
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
