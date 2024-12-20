using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class SunkenParrot : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sunken Parrot");
			Main.npcFrameCount[base.npc.type] = 5;
		}

		public override void SetDefaults()
		{
			base.npc.width = 44;
			base.npc.height = 40;
			base.npc.friendly = false;
			base.npc.damage = 30;
			base.npc.defense = 0;
			base.npc.lifeMax = 25;
			base.npc.HitSound = SoundID.NPCHit46;
			base.npc.DeathSound = SoundID.NPCDeath48;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0.2f;
			base.npc.aiStyle = 5;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.aiType = 252;
			this.animationType = 252;
			base.npc.alpha = 255;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 89, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
		}

		public override void AI()
		{
			this.sunkenTimer++;
			if (this.sunkenTimer <= 120)
			{
				base.npc.velocity.X = 0f;
				base.npc.alpha--;
				base.npc.dontTakeDamage = true;
			}
			if (this.sunkenTimer > 120)
			{
				base.npc.dontTakeDamage = false;
			}
		}

		private int sunkenTimer;
	}
}
