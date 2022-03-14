using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;


namespace PKEMeter.Logic {
	public partial class PKEScannable : ILoadable {
		public static PKEScannable Instance => ModContent.GetInstance<PKEScannable>();

		////

		internal static IDictionary<string, PKEScannable> Scannables
			=> PKEScannable.Instance?.SingletonScannables;

		internal static IDictionary<int, ISet<string>> ScannableItems
			=> PKEScannable.Instance?.SingletonScannableItems;



		////////////////
		
		public static PKEScannable GetScannable( string name ) {
			return PKEScannable.Scannables.GetOrDefault( name );
		}

		public static IDictionary<string, PKEScannable> GetScannables() {
			return new Dictionary<string, PKEScannable>(
				PKEScannable.Scannables
			);
		}

		////

		public static bool SetScannable( string name, PKEScannable scannable, bool allowRepeat, bool runIfComplete ) {
			return PKEScannable.Instance.SetScannable_Singleton( name, scannable, allowRepeat, runIfComplete );
		}


		////////////////

		public static bool CompleteScan( string name ) {
			PKEScannable singleton = PKEScannable.Instance;
			IDictionary<string, PKEScannable> scannables = singleton.SingletonScannables;

			if( !scannables.ContainsKey(name) ) {
				return false;
			}

			//

			scannables[ name ].RunScanComplete();

			//

			if( scannables[name].ItemType != 0 ) {
				singleton.SingletonScannableItems.Remove2D( scannables[name].ItemType, name );
			}

			//

			var myplayer = Main.LocalPlayer.GetModPlayer<PKEMeterPlayer>();
			myplayer.Scans.Add( name );

			//

			return scannables.Remove( name );
		}



		////////////////

		private IDictionary<string, PKEScannable> SingletonScannables = null;

		private IDictionary<int, ISet<string>> SingletonScannableItems = null;



		////////////////

		void ILoadable.OnModsLoad() {
			this.SingletonScannableItems = new Dictionary<int, ISet<string>>();
			this.SingletonScannables = new Dictionary<string, PKEScannable>();

			/*this.SetScannable_Singleton( "test",
				new PKEScannable(
					canScan: (x, y) => Main.HoverItem.type == ItemID.Wood,
					onScanCompleteAction: () => Main.NewText("done"),
					itemType: ItemID.Wood
				)
			);*/
		}

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }
		
		
		////

		private bool SetScannable_Singleton(
					string name,
					PKEScannable scannable,
					bool allowRepeat,
					bool runIfComplete ) {
			if( this.SingletonScannables.ContainsKey(name) ) {
				return false;
			}

			//

			var myplayer = Main.LocalPlayer.GetModPlayer<PKEMeterPlayer>();

			if( myplayer.Scans.Contains(name) ) {
				if( runIfComplete ) {
					scannable.RunScanComplete();
				}

				if( !allowRepeat ) {
					return false;
				}
			}

			//

			this.SingletonScannables[name] = scannable;

			if( scannable.ItemType != 0 ) {
				this.SingletonScannableItems.Set2D( scannable.ItemType, name );
			}

			return true;
		}
	}
}
