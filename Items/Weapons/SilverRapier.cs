using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class SilverRapier : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vepdor's Silver Rapier");
			base.Tooltip.SetDefault("'A hard-hitting Rapier made of Silver & Gold.'\n[c/1c4dff:Rare]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 15;
			base.item.melee = true;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 7;
			base.item.useAnimation = 7;
			base.item.useStyle = 3;
			base.item.knockBack = 1f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 5;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(10) == 0)
			{
				target.AddBuff(31, 400, false);
			}
		}
	}
}
