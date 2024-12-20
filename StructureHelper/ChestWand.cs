using System;
using Microsoft.Xna.Framework;
using Redemption.StructureHelper.ChestHelper;
using Redemption.StructureHelper.ChestHelper.GUI;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.StructureHelper
{
	internal class ChestWand : ModItem
	{
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chest Wand");
			base.Tooltip.SetDefault("Right click to open the chest rule menu\nLeft click a chest to set the current rules on it\nRight click a chest with rules to copy them");
		}

		public override void SetDefaults()
		{
			base.item.useStyle = 1;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.rare = 1;
		}

		public override bool UseItem(Player player)
		{
			Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
			if (tile.type == 21 || TileLists.ModdedChests.Contains((int)tile.type))
			{
				int xOff = (int)(tile.frameX % 36 / 18);
				int yOff = (int)(tile.frameY % 36 / 18);
				if (player.altFunctionUse == 2)
				{
					if (TileEntity.ByPosition.ContainsKey(new Point16(Player.tileTargetX - xOff, Player.tileTargetY - yOff)))
					{
						ChestEntity chestEntity = TileEntity.ByPosition[new Point16(Player.tileTargetX - xOff, Player.tileTargetY - yOff)] as ChestEntity;
						Redemption.Inst.ChestCustomizer.ruleElements.Clear();
						for (int i = 0; i < chestEntity.rules.Count; i++)
						{
							ChestRule rule = chestEntity.rules[i].Clone();
							ChestRuleElement elem = new ChestRuleElement(rule);
							if (rule is ChestRuleGuaranteed)
							{
								elem = new GuaranteedRuleElement(rule);
							}
							if (rule is ChestRuleChance)
							{
								elem = new ChanceRuleElement(rule);
							}
							if (rule is ChestRulePool)
							{
								elem = new PoolRuleElement(rule);
							}
							if (rule is ChestRulePoolChance)
							{
								elem = new PoolChanceRuleElement(rule);
							}
							Redemption.Inst.ChestCustomizer.ruleElements.Add(elem);
						}
					}
					else
					{
						Redemption.Inst.ChestCustomizer.ruleElements.Clear();
					}
					Main.NewText(string.Format("Copied chest rules from chest at {0}", new Point16(Player.tileTargetX - xOff, Player.tileTargetY - yOff)), byte.MaxValue, byte.MaxValue, byte.MaxValue, false);
				}
				else
				{
					bool flag = TileEntity.ByPosition.ContainsKey(new Point16(Player.tileTargetX - xOff, Player.tileTargetY - yOff));
					TileEntity.PlaceEntityNet(Player.tileTargetX - xOff, Player.tileTargetY - yOff, ModContent.TileEntityType<ChestEntity>());
					bool cleared = !Redemption.Inst.ChestCustomizer.SetData(TileEntity.ByPosition[new Point16(Player.tileTargetX - xOff, Player.tileTargetY - yOff)] as ChestEntity);
					if (flag)
					{
						if (cleared)
						{
							Main.NewText(string.Format("Removed chest rules for chest at {0}", new Point16(Player.tileTargetX - xOff, Player.tileTargetY - yOff)), Color.Orange, false);
						}
						else
						{
							Main.NewText(string.Format("Overwritten chest rules for chest at {0}", new Point16(Player.tileTargetX - xOff, Player.tileTargetY - yOff)), Color.Yellow, false);
						}
					}
					else if (!cleared)
					{
						Main.NewText(string.Format("Set chest rules for chest at {0}", new Point16(Player.tileTargetX - xOff, Player.tileTargetY - yOff)), Color.GreenYellow, false);
					}
				}
			}
			if (player.altFunctionUse == 2)
			{
				ChestCustomizerState.Visible = true;
			}
			return true;
		}
	}
}
