﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class DarkSoul3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Soul");
		}

		public override void SetDefaults()
		{
			base.npc.width = 16;
			base.npc.height = 20;
			base.npc.friendly = false;
			base.npc.damage = 15;
			base.npc.defense = 0;
			base.npc.lifeMax = 25;
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

		public override void AI()
		{
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("VoidFlame"), 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
