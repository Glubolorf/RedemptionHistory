﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Materials.PostML;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Friendly
{
	public class LostSoul3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lost Soul");
		}

		public override void SetDefaults()
		{
			base.npc.width = 24;
			base.npc.height = 28;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.HitSound = SoundID.NPCHit36;
			base.npc.DeathSound = SoundID.NPCDeath39;
			base.npc.lavaImmune = true;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 2;
			base.npc.alpha = 100;
			base.npc.noGravity = true;
			this.aiType = 288;
			base.npc.catchItem = (short)ModContent.ItemType<LargeLostSoul>();
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Vector2 direction = Main.player[base.npc.target].Center - base.npc.Center;
			base.npc.rotation = Utils.ToRotation(direction);
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 20, base.npc.velocity.X * 0.2f, base.npc.velocity.Y * 0.2f, 20, default(Color), 2f);
			}
			this.deathTimer++;
			if (this.deathTimer >= 600)
			{
				Main.PlaySound(SoundID.NPCDeath39.WithVolume(0.3f), (int)base.npc.position.X, (int)base.npc.position.Y);
				base.npc.active = false;
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<LargeLostSoul>(), 1, false, 0, false, false);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		private int deathTimer;
	}
}
