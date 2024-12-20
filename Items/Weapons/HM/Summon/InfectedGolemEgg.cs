using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Minions;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Summon
{
	public class InfectedGolemEgg : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Golem Egg");
			base.Tooltip.SetDefault("Summons a friendly Xenomite Hatchling to fight for you.");
		}

		public override void SetDefaults()
		{
			base.item.damage = 9;
			base.item.summon = true;
			base.item.mana = 6;
			base.item.width = 24;
			base.item.height = 28;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = ModContent.ProjectileType<XenomiteHatchling>();
			base.item.shootSpeed = 8f;
			base.item.buffType = ModContent.BuffType<XenomiteHatchlingBuff>();
			base.item.buffTime = 3600;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.altFunctionUse != 2;
		}

		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}
	}
}
