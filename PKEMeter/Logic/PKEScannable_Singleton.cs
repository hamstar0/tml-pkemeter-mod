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
		internal static PKEScannable Singleton => ModContent.GetInstance<PKEScannable>();

		////

		internal static IDictionary<string, PKEScannable> Scannables
			=> PKEScannable.Singleton?.SingletonScannables;

		internal static IDictionary<int, ISet<string>> ScannableItems
			=> PKEScannable.Singleton?.SingletonScannableItems;



		////////////////

		internal static PKEScannable GetScannable( string name ) {
			return PKEScannable.Scannables?.GetOrDefault( name );
		}

		internal static IDictionary<string, PKEScannable> GetScannables() {
			IDictionary<string, PKEScannable> scannables = PKEScannable.Scannables;
			if( scannables == null ) {
				return new Dictionary<string, PKEScannable>();
			}

			return new Dictionary<string, PKEScannable>( scannables );
		}

		////

		internal static bool SetScannable(
					string name,
					PKEScannable scannable,
					bool allowRepeat,
					bool runIfComplete ) {
			return PKEScannable.Singleton.SetScannable_Singleton( name, scannable, allowRepeat, runIfComplete );
		}


		////////////////

		public static bool CompleteScan( string name ) {
			PKEScannable singleton = PKEScannable.Singleton;
			IDictionary<string, PKEScannable> scannables = singleton?.SingletonScannables;

			if( scannables?.ContainsKey(name) ?? false ) {
				return false;
			}

			//

			scannables[ name ].RunScanComplete();

			//

			foreach( int iType in scannables[name].AnyOfItemTypes ) {
				singleton.SingletonScannableItems.Remove2D( iType, name );
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

			foreach( int iType in scannable.AnyOfItemTypes ) {
				this.SingletonScannableItems.Set2D( iType, name );
			}

			return true;
		}
	}
}
