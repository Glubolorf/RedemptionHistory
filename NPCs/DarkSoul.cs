using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class DarkSoul : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Soul");
		}

		public override void SetDefaults()
		{
			base.npc.width = 20;
			base.npc.height = 26;
			base.npc.friendly = false;
			base.npc.damage = 20;
			base.npc.defense = 0;
			base.npc.lifeMax = 100;
			base.npc.HitSound = SoundID.NPCHit3;
			base.npc.DeathSound = SoundID.NPCDeath6;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 2;
			base.npc.noGravity = true;
			this.aiType = 288;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<DarkSoulBanner>();
		}

		public override void AI()
		{
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 0, default(Color), 1f);
			}
			this.deathTimer++;
			if (this.deathTimer >= 300)
			{
				Main.PlaySound(SoundID.NPCDeath6, (int)base.npc.position.X, (int)base.npc.position.Y);
				base.npc.active = false;
			}
		}

		private int deathTimer;
	}
}
