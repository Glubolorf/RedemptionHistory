using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public abstract class DruidDamageSpirit : DruidDamageItem
	{
		public override bool CloneNewInstances
		{
			get
			{
				return true;
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine tt = Enumerable.FirstOrDefault<TooltipLine>(tooltips, (TooltipLine x) => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null)
			{
				string[] array = tt.text.Split(new char[]
				{
					' '
				});
				string damageValue = Enumerable.First<string>(array);
				string damageWord = Enumerable.Last<string>(array);
				tt.text = damageValue + " druidic " + damageWord;
			}
			int tooltipLocation = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("ItemName"));
			int tooltipLocation2 = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("Tooltip0"));
			if (tooltipLocation != -1 && !RedeConfigClient.Instance.NoDruidClassTag)
			{
				tooltips.Insert(tooltipLocation + 1, new TooltipLine(base.mod, "IsDruid", "[c/bdffff:-Druid Class-]"));
			}
			if (tooltipLocation2 != -1 && this.spiritWeapon)
			{
				tooltips.Insert(tooltipLocation2, new TooltipLine(base.mod, "MinSpiritLevel", "[c/c0bdff:Minimum Spirit Level: ]" + this.minSpiritLevel));
				tooltips.Insert(tooltipLocation2 + 1, new TooltipLine(base.mod, "MaxSpiritLevel", "[c/bdffe4:Maximum Spirit Level: ]" + this.maxSpiritLevel));
			}
		}

		protected int maxSpiritLevel;

		protected int minSpiritLevel;

		protected bool spiritWeapon = true;
	}
}
