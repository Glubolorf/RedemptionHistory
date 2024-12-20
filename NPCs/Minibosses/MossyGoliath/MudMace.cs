using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Minibosses.MossyGoliath
{
	public class MudMace : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mud Mace");
			base.Tooltip.SetDefault("Inflicts confusion");
		}

		public override void SetDefaults()
		{
			base.item.damage = 41;
			base.item.melee = true;
			base.item.width = 54;
			base.item.height = 54;
			base.item.useTime = 38;
			base.item.useAnimation = 38;
			base.item.useStyle = 1;
			base.item.knockBack = 9f;
			base.item.value = Item.buyPrice(0, 1, 50, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(31, 180, false);
			}
		}
	}
}
