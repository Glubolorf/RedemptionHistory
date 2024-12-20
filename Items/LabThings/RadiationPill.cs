using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class RadiationPill : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radiation Pill");
			base.Tooltip.SetDefault("Cures radiation poisoning\nREAD INSTRUCTIONS:\n'Radiation normally cannot be cured, but with this new medicine, we are slowly progressing.\n- Make sure you know for a fact you have radiation poisoning, this will do more harm than good!\n- Only use after the first symptom is gone, this will most likely be a headache.\n- This medicine only works when the user is in a specific stage of poisoning,\nthe stage which is recommended to use contains the following symptoms:\nFatigue, and Nausea\n- After successful use, you will feel weak and fragile, this will go away in a few minutes.'");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item1;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.consumable = true;
			base.item.width = 20;
			base.item.height = 26;
			base.item.maxStack = 30;
			base.item.value = Item.buyPrice(0, 15, 0, 0);
			base.item.rare = 8;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(base.mod.BuffType("PillSickness"));
		}

		public override bool UseItem(Player player)
		{
			if (player.GetModPlayer<RedePlayer>().irradiatedEffect == 1 || player.GetModPlayer<RedePlayer>().irradiatedEffect == 2)
			{
				player.GetModPlayer<RedePlayer>().irradiatedEffect = 0;
				player.GetModPlayer<RedePlayer>().irradiatedLevel = 0;
				player.GetModPlayer<RedePlayer>().irradiatedTimer = 0;
				player.AddBuff(base.mod.BuffType("PillSickness"), Main.rand.Next(3600, 7200), true);
				player.AddBuff(33, Main.rand.Next(3600, 7200), true);
			}
			else
			{
				player.AddBuff(base.mod.BuffType("PillSickness"), Main.rand.Next(3600, 7200), true);
			}
			return true;
		}
	}
}
