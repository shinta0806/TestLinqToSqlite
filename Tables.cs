// ============================================================================
// 
// データベーステーブル定義をカプセル化
// 
// ============================================================================

// ----------------------------------------------------------------------------
// 
// ----------------------------------------------------------------------------

using Shinta;
using System;
using System.Data.Linq.Mapping;

namespace TestLinqToSqlite
{
	// ====================================================================
	// 【基本操作用】テスト用テーブル
	// ID, Name, Height
	// フィールドの型名の後に ? を付けるとプロパティー値が null を取れるようになる（CanBeNull との整合性に注意）
	// ====================================================================

	[Table(Name = "t_test")]
	public class TTestData
	{
		// ID
		[Column(Name = "test_id", DbType = LinqUtils.DB_TYPE_INT32, CanBeNull = false, IsPrimaryKey = true)]
		public Int32 Id { get; set; }

		// 氏名
		[Column(Name = "test_name", DbType = LinqUtils.DB_TYPE_STRING, CanBeNull = false)]
		public String Name { get; set; }

		// 身長
		[Column(Name = "test_height", DbType = LinqUtils.DB_TYPE_DOUBLE, CanBeNull = true)]
		public Double? Height { get; set; }

	}
	// public class TTestData ___END___

	// ====================================================================
	// 【共通カラムジェネリック運用】食べ物テーブル共通カラム部分
	// ====================================================================

	public interface IFoodData
	{
		// ID
		Int32 Id { get; set; }

		// 名前
		String Name { get; set; }
	}
	// public interface IFoodData ___END___

	// ====================================================================
	// 【共通カラムジェネリック運用】フルーツテーブル
	// ====================================================================

	[Table(Name = "t_fruit")]
	public class TFruitData : IFoodData
	{
		// --------------------------------------------------------------------
		// IFoodData 実装
		// --------------------------------------------------------------------

		// ID
		[Column(Name = "fruit_id", DbType = LinqUtils.DB_TYPE_INT32, CanBeNull = false, IsPrimaryKey = true)]
		public Int32 Id { get; set; }

		// 名前
		[Column(Name = "fruit_name", DbType = LinqUtils.DB_TYPE_STRING, CanBeNull = false)]
		public String Name { get; set; }

		// --------------------------------------------------------------------
		// TFruitData 独自項目
		// --------------------------------------------------------------------

		// 色
		[Column(Name = "fruit_color", DbType = LinqUtils.DB_TYPE_STRING, CanBeNull = true)]
		public String Color { get; set; }
	}
	// public class TFruitData ___END___

	// ====================================================================
	// 【共通カラムジェネリック運用】肉テーブル
	// ====================================================================

	[Table(Name = "t_meat")]
	public class TMeatData : IFoodData
	{
		// --------------------------------------------------------------------
		// IFoodData 実装
		// --------------------------------------------------------------------

		// ID
		[Column(Name = "meat_id", DbType = LinqUtils.DB_TYPE_INT32, CanBeNull = false, IsPrimaryKey = true)]
		public Int32 Id { get; set; }

		// 名前
		[Column(Name = "meat_name", DbType = LinqUtils.DB_TYPE_STRING, CanBeNull = false)]
		public String Name { get; set; }

		// --------------------------------------------------------------------
		// TMeatData 独自項目
		// --------------------------------------------------------------------

		// 料理名
		[Column(Name = "meat_cooking", DbType = LinqUtils.DB_TYPE_STRING, CanBeNull = true)]
		public String Cooking { get; set; }
	}
	// public class TMeatData ___END___
}
// namespace TestLinqToSqlite ___END___

