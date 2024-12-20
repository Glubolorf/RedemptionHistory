using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Minions;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Summon
{
	public class AndroidMinion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Android Hologram");
			base.Tooltip.SetDefault("Summons a little Android to fight for you.");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SetDefaults()
		{
			base.item.damage = 85;
			base.item.summon = true;
			base.item.mana = 15;
			base.item.width = 40;
			base.item.height = 30;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.rare = 9;
			base.item.noUseGraphic = true;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = ModContent.ProjectileType<AndroidMinionPro>();
			base.item.shootSpeed = 10f;
			base.item.buffType = ModContent.BuffType<AndroidMinionBuff>();
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
