using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.StructureHelper
{
	internal class TestWand : ModItem
	{
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Structure Placer Wand");
			base.Tooltip.SetDefault("left click to place the selected structure, right click to open the structure selector");
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
			if (player.altFunctionUse == 2)
			{
				TestWand.UIVisible = !TestWand.UIVisible;
				return true;
			}
			if (ManualGeneratorMenu.selected != null)
			{
				Point16 pos;
				pos..ctor(Player.tileTargetX, Player.tileTargetY);
				if (ManualGeneratorMenu.multiMode)
				{
					Generator.GenerateMultistructureSpecific(ManualGeneratorMenu.selected.Path, pos, Redemption.Inst, ManualGeneratorMenu.multiIndex, true, ManualGeneratorMenu.ignoreNulls);
				}
				else
				{
					Generator.GenerateStructure(ManualGeneratorMenu.selected.Path, pos, Redemption.Inst, true, ManualGeneratorMenu.ignoreNulls);
				}
			}
			else
			{
				Main.NewText("No structure selected! Right click and select a structure from the menu to generate it.", Color.Red, false);
			}
			return true;
		}

		public static bool ignoreNulls;

		public static bool UIVisible;
	}
}
