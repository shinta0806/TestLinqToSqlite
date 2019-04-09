// ============================================================================
// 
// Linq to SQLite テストアプリ
// Copyright (C) 2015-2019 by SHINTA
// http://shinta0806be.ldblog.jp/
// 
// ============================================================================

// ============================================================================
//  Ver.  |      更新日      |                    更新内容
// ----------------------------------------------------------------------------
//  1.00  | 2015/11/21 (Sat) | オリジナルバージョン。
//  2.00  | 2019/04/09 (Tue) | ジェネリックを追加。
// ============================================================================

// ----------------------------------------------------------------------------
// 必要な参照：System.Data.SQLite.dll（x86 版）
// 必要な DLL：x86/SQLite.Interop.dll（x86 版）
// ----------------------------------------------------------------------------

using Shinta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TestLinqToSqlite
{
	public partial class FormTestLinq : Form
	{
		// ====================================================================
		// public メンバー関数
		// ====================================================================

		// --------------------------------------------------------------------
		// コンストラクター
		// --------------------------------------------------------------------
		public FormTestLinq()
		{
			InitializeComponent();
		}

		// ====================================================================
		// private メンバー定数
		// ====================================================================

		// 基本操作用のデータベースファイル名
		private const String DB_NAME_BASIC = "basic";

		// 共通カラムジェネリック運用のデータベースファイル名
		private const String DB_NAME_GENERIC = "generic";

		// ====================================================================
		// private メンバー関数
		// ====================================================================

		// --------------------------------------------------------------------
		// データベースに接続
		// --------------------------------------------------------------------
		private SQLiteConnection CreateDatabaseConnection(String oDbName)
		{
			SQLiteConnectionStringBuilder aConnectionString = new SQLiteConnectionStringBuilder
			{
				DataSource = DatabasePath(oDbName),
			};
			SQLiteConnection aConnection = new SQLiteConnection(aConnectionString.ToString());
			return aConnection.OpenAndReturn();
		}

		// --------------------------------------------------------------------
		// データベースファイル名
		// --------------------------------------------------------------------
		private String DatabasePath(String oDbName)
		{
			return Path.GetDirectoryName(Application.ExecutablePath) + "\\db_" + oDbName + ".sqlite3";
		}

		// --------------------------------------------------------------------
		// 既存のデータベースファイルを削除
		// --------------------------------------------------------------------
		private void DeleteExistDatabase(String oDbName)
		{
			String aDbPath = DatabasePath(oDbName);
			if (!File.Exists(aDbPath))
			{
				// 既存のデータベースファイルは無いので何もしない
				return;
			}

			Console.WriteLine("DB を削除します...");
			try
			{
				File.Delete(aDbPath);
			}
			catch
			{
				// アプリ起動中に作成したデータベースファイルは削除できないので、ドロップで対応する
				using (SQLiteConnection aConnection = CreateDatabaseConnection(oDbName))
				{
					LinqUtils.DropAllTables(aConnection);
				}
			}
		}

		// --------------------------------------------------------------------
		// 食べ物テーブルを名前で検索（ジェネリック運用）
		// --------------------------------------------------------------------
		private void QueryFoodByName<T>(String oKeyword) where T : class, IFoodData
		{
			Console.WriteLine(LinqUtils.TableName(typeof(T)) + " 内で名前に「" + oKeyword + "」を含むレコードを検索");
			using (SQLiteConnection aConnection = CreateDatabaseConnection(DB_NAME_GENERIC))
			using (DataContext aContext = new DataContext(aConnection))
			{
				Table<T> aTableTest = aContext.GetTable<T>();
				IQueryable<T> aQueryResult =
						from x in aTableTest
						where x.Name.Contains(oKeyword)
						select x;
				Console.WriteLine("検索結果：" + aQueryResult.Count() + " 件");
				foreach (T aRecord in aQueryResult)
				{
					Console.WriteLine(aRecord.Name);
				}
			}
		}

		// --------------------------------------------------------------------
		// クエリー結果の表示（基本操作用）
		// --------------------------------------------------------------------
		private void ShowQueryResult(String oTitle, IQueryable<TTestData> oQueryResult)
		{
			String aMsg = oTitle + "：" + oQueryResult.Count().ToString() + "件\n";
			foreach (TTestData aData in oQueryResult)
			{
				aMsg += aData.Id.ToString() + ", " + aData.Name + ", ";
				if (aData.Height.HasValue)
				{
					aMsg += aData.Height.Value.ToString() + "cm";
				}
				else
				{
					aMsg += "-";
				}
				aMsg += "\n";
			}
			Console.WriteLine(aMsg);
		}

		// ====================================================================
		// IDE 生成イベントハンドラー
		// ====================================================================

		private void ButtonCreate_Click(object sender, EventArgs e)
		{
			try
			{
				DeleteExistDatabase(DB_NAME_BASIC);

				Console.WriteLine("DB を作成します...");
				using (SQLiteConnection aConnection = CreateDatabaseConnection(DB_NAME_BASIC))
				using (SQLiteCommand aCmd = new SQLiteCommand(aConnection))
				{
					// ユニーク制約
					List<String> aUniques = new List<String>();
					aUniques.Add("test_name");

					// テーブル作成
					LinqUtils.CreateTable(aCmd, typeof(TTestData), aUniques);

					// インデックス作成
					List<String> aIndices = new List<String>();
					aIndices.Add("test_name");
					aIndices.Add("test_height");
					LinqUtils.CreateIndex(aCmd, LinqUtils.TableName(typeof(TTestData)), aIndices);

					// データ挿入
					using (DataContext aContext = new DataContext(aConnection))
					{
						Table<TTestData> aTableTest = aContext.GetTable<TTestData>();
						aTableTest.InsertOnSubmit(new TTestData { Id = 1, Name = "Fukada Kyoko" });
						aTableTest.InsertOnSubmit(new TTestData { Id = 2, Name = "Eda Ha", Height = 180.0 });
						aTableTest.InsertOnSubmit(new TTestData { Id = 3, Name = "Dan Gerou", Height = 150.5 });
						aTableTest.InsertOnSubmit(new TTestData { Id = 4, Name = "Baba Takashi" });
						aTableTest.InsertOnSubmit(new TTestData { Id = 5, Name = "Aikawa Ai", Height = 145.6 });
						aContext.SubmitChanges();

						Console.WriteLine("DB にテーブルを作成しました。件数：" + aTableTest.Count().ToString());
					}
				}
			}
			catch (Exception oExcep)
			{
				Console.WriteLine(oExcep.Message);
			}
		}

		private void ButtonQuery_Click(object sender, EventArgs e)
		{
			using (SQLiteConnection aConnection = CreateDatabaseConnection(DB_NAME_BASIC))
			using (DataContext aContext = new DataContext(aConnection))
			{

				Table<TTestData> aTableTest = aContext.GetTable<TTestData>();
				IQueryable<TTestData> aQueryResult =
						from x in aTableTest
						where x.Name == "Eda Ha" || x.Height < 150.0
						orderby x.Height
						select x;
				ShowQueryResult("条件に合致する人", aQueryResult);
			}
		}

		private void ButtonDelete_Click(object sender, EventArgs e)
		{
			using (SQLiteConnection aConnection = CreateDatabaseConnection(DB_NAME_BASIC))
			using (DataContext aContext = new DataContext(aConnection))
			{
				Table<TTestData> aTableTest = aContext.GetTable<TTestData>();
				IQueryable<TTestData> aDelTargets =
						from x in aTableTest
						where 140 < x.Height && x.Height < 160
						select x;
				Int32 aNum = aDelTargets.Count();
				aTableTest.DeleteAllOnSubmit(aDelTargets);
				aContext.SubmitChanges();
				Console.WriteLine(aNum.ToString() + " 件削除しました。");
			}
		}

		private void ButtonUpdate_Click(object sender, EventArgs e)
		{
			using (SQLiteConnection aConnection = CreateDatabaseConnection(DB_NAME_BASIC))
			using (DataContext aContext = new DataContext(aConnection))
			{
				Table<TTestData> aTableTest = aContext.GetTable<TTestData>();

#if true
				// クエリ方式（OK）
				IQueryable<TTestData> aUpdateTargets =
						from x in aTableTest
						where x.Name.Contains("i")
						select x;
				foreach (TTestData aTestData in aUpdateTargets)
				{
					aTestData.Name += "(Up-Q)";
				}
				aContext.SubmitChanges();
				Console.WriteLine(aUpdateTargets.Count().ToString() + " 件更新しました。");
#endif
#if false
				// Single() 方式（OK）
				try
				{
					TTestData aTestData = aTableTest.Single(x => x.Id == 5);
					aTestData.Name += "(Up-S)";
					aContext.SubmitChanges();
					Console.WriteLine("1 件更新しました。");
				}
				catch (Exception oExcep)
				{
					Console.WriteLine("更新できませんでした：" + oExcep.Message);
				}
#endif
#if false
				// インスタンス自体を交換してしまう（NG）
				TTestData aTestData = aTableTest.Single(x => x.Id == 5);
				aTestData = new TTestData { Id = 5, Name = "(Up-I)", Height = 180.0 };
				aContext.SubmitChanges();
				Console.WriteLine("1 件更新しました。");
#endif
			}
		}

		private void ButtonCreate2_Click(object sender, EventArgs e)
		{
			try
			{
				DeleteExistDatabase(DB_NAME_GENERIC);

				Console.WriteLine("DB を作成します...");
				using (SQLiteConnection aConnection = CreateDatabaseConnection(DB_NAME_GENERIC))
				using (SQLiteCommand aCmd = new SQLiteCommand(aConnection))
				{
					// フルーツテーブル作成
					LinqUtils.CreateTable(aCmd, typeof(TFruitData));

					// データ挿入
					using (DataContext aContext = new DataContext(aConnection))
					{
						Table<TFruitData> aTableTest = aContext.GetTable<TFruitData>();
						aTableTest.InsertOnSubmit(new TFruitData { Id = 1, Name = "Apple", Color = "Red" });
						aTableTest.InsertOnSubmit(new TFruitData { Id = 2, Name = "Banana", Color = "Yellow" });
						aTableTest.InsertOnSubmit(new TFruitData { Id = 3, Name = "Strawberry", Color = "Red" });
						aTableTest.InsertOnSubmit(new TFruitData { Id = 4, Name = "Orange", Color = "Orange" });
						aTableTest.InsertOnSubmit(new TFruitData { Id = 5, Name = "Grape" });
						aContext.SubmitChanges();

						Console.WriteLine("DB にフルーツテーブルを作成しました。件数：" + aTableTest.Count().ToString());
					}

					// 肉テーブル作成
					LinqUtils.CreateTable(aCmd, typeof(TMeatData));

					// データ挿入
					using (DataContext aContext = new DataContext(aConnection))
					{
						Table<TMeatData> aTableTest = aContext.GetTable<TMeatData>();
						aTableTest.InsertOnSubmit(new TMeatData { Id = 1, Name = "Pork" });
						aTableTest.InsertOnSubmit(new TMeatData { Id = 2, Name = "Beef", Cooking = "Burger" });
						aTableTest.InsertOnSubmit(new TMeatData { Id = 3, Name = "Chicken", Cooking = "Fried chicken" });
						aContext.SubmitChanges();

						Console.WriteLine("DB に肉テーブルを作成しました。件数：" + aTableTest.Count().ToString());
					}
				}
			}
			catch (Exception oExcep)
			{
				Console.WriteLine(oExcep.Message);
			}
		}

		private void ButtonQuery2_Click(object sender, EventArgs e)
		{
			QueryFoodByName<TFruitData>("p");
			QueryFoodByName<TMeatData>("e");
		}
	}
	// public partial class FormTestLinq ___END___
}
// namespace TestLinqToSqlite ___END___
