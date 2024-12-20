using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Cooldowns;
using Redemption.Projectiles.Druid.Stave;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid
{
	public class MartianShieldGenerator : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Portable Martian Shield Generator");
			base.Tooltip.SetDefault("Constructs a Martian Shield Generator at your cursor point\nThe generator emits a forcefield that reflects any projectiles\nCan only place one at a time");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 26;
			base.item.height = 30;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.mana = 100;
			base.item.value = Item.buyPrice(0, 25, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/WorldTree1");
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<WorldTree4>();
			base.item.shootSpeed = 0f;
			base.item.potion = false;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<WorldStaveCooldownDebuff>());
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(ModContent.BuffType<WorldStaveCooldownDebuff>(), 1200, true);
			position = Main.MouseWorld;
			return true;
		}
	}
}
