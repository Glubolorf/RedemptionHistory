using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class DarkSoul4 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dying Soul");
		}

		public override void SetDefaults()
		{
			base.npc.width = 20;
			base.npc.height = 26;
			base.npc.friendly = false;
			base.npc.damage = 15;
			base.npc.defense = 0;
			base.npc.lifeMax = 40;
			base.npc.HitSound = SoundID.NPCHit3;
			base.npc.DeathSound = SoundID.NPCDeath6;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 2;
			base.npc.noGravity = true;
			this.aiType = 288;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("DarkSoulBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 173, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 3f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(4) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
			}
		}

		public override void AI()
		{
			if (Main.rand.Next(1) == 0)
			{
				int dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 173, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 3f);
				Main.dust[dust].noGravity = true;
			}
			this.deathTimer++;
			if (this.deathTimer >= 600)
			{
				Main.PlaySound(SoundID.NPCDeath6, (int)base.npc.position.X, (int)base.npc.position.Y);
				base.npc.active = false;
			}
			if (!NPC.AnyNPCs(base.mod.NPCType("TheKeeper")))
			{
				base.npc.active = false;
			}
		}

		private int deathTimer;
	}
}
