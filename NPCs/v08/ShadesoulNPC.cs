using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class ShadesoulNPC : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadesoul");
			Main.npcFrameCount[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.width = 30;
			base.npc.height = 30;
			base.npc.damage = 1;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.HitSound = SoundID.NPCHit36;
			base.npc.DeathSound = SoundID.NPCDeath39;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 2;
			base.npc.alpha = 60;
			base.npc.noGravity = true;
			base.npc.catchItem = (short)base.mod.ItemType("Shadesoul");
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 40;
				if (base.npc.frame.Y > 280)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			base.npc.TargetClosest(true);
			Vector2 vector = Main.player[base.npc.target].Center - base.npc.Center;
			base.npc.rotation = Utils.ToRotation(vector);
			this.deathTimer++;
			if (this.deathTimer >= 1200)
			{
				Main.PlaySound(SoundID.NPCDeath39.WithVolume(0.3f), (int)base.npc.position.X, (int)base.npc.position.Y);
				base.npc.active = false;
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(base.mod.BuffType("BlackenedHeartDebuff"), Main.rand.Next(10, 15), true);
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Shadesoul"), 1, false, 0, false, false);
		}

		private int deathTimer;
	}
}
