using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class GeigerCounter : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Geiger Counter");
			base.Tooltip.SetDefault("Summons the Xenomite Crystal\n'Finally...'\n[c/67ff3e:Begins the Infection]");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 4));
		}

		public override void SetDefaults()
		{
			base.item.width = 40;
			base.item.height = 42;
			base.item.maxStack = 20;
			base.item.rare = 2;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.useStyle = 4;
			base.item.UseSound = SoundID.Item44;
			base.item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(base.mod.NPCType("XenomiteCrystal")) && !NPC.AnyNPCs(base.mod.NPCType("XenomiteCrystalPhase2"));
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, base.mod.NPCType("XenomiteCrystal"));
			Main.PlaySound(15, player.position, 0);
			return true;
		}
	}
}
