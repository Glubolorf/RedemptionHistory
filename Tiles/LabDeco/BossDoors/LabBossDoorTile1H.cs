﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.LabDeco.BossDoors
{
	public class LabBossDoorTile1H : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileSolid[(int)base.Type] = true;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = true;
			TileID.Sets.NotReallySolid[(int)base.Type] = true;
			TileID.Sets.DrawsWalls[(int)base.Type] = true;
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorWall = true;
			TileObjectData.addTile((int)base.Type);
			ModTranslation name = base.CreateMapEntryName(null);
			name.SetDefault("Reinforced Door");
			base.AddMapEntry(new Color(80, 100, 80), name);
			this.minPick = 500;
			this.mineResist = 10f;
			this.dustType = 226;
			this.animationFrameHeight = 18;
			this.disableSmartCursor = true;
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			if (this._activated)
			{
				frame = 1;
				return;
			}
			frame = 0;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return false;
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			if (!NPC.AnyNPCs(base.mod.NPCType("JanitorBot")) && !NPC.AnyNPCs(base.mod.NPCType("Stage3Scientist2")) && !NPC.AnyNPCs(base.mod.NPCType("IrradiatedBehemoth2")) && !NPC.AnyNPCs(base.mod.NPCType("Blisterface2")) && !NPC.AnyNPCs(base.mod.NPCType("TbotMiniboss")) && !NPC.AnyNPCs(base.mod.NPCType("MACEProjectHeadA")) && !NPC.AnyNPCs(base.mod.NPCType("PZ2Fight")))
			{
				Main.tileSolid[(int)base.Type] = false;
				this._activated = false;
				return;
			}
			Main.tileSolid[(int)base.Type] = true;
			this._activated = true;
		}

		private bool _activated;
	}
}