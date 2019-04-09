namespace TestLinqToSqlite
{
	partial class FormTestLinq
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ButtonUpdate = new System.Windows.Forms.Button();
			this.ButtonDelete = new System.Windows.Forms.Button();
			this.ButtonQuery = new System.Windows.Forms.Button();
			this.ButtonCreate = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ButtonQuery2 = new System.Windows.Forms.Button();
			this.ButtonCreate2 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ButtonUpdate);
			this.groupBox1.Controls.Add(this.ButtonDelete);
			this.groupBox1.Controls.Add(this.ButtonQuery);
			this.groupBox1.Controls.Add(this.ButtonCreate);
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(128, 196);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "基本操作";
			// 
			// ButtonUpdate
			// 
			this.ButtonUpdate.Location = new System.Drawing.Point(16, 152);
			this.ButtonUpdate.Name = "ButtonUpdate";
			this.ButtonUpdate.Size = new System.Drawing.Size(96, 28);
			this.ButtonUpdate.TabIndex = 7;
			this.ButtonUpdate.Text = "更新";
			this.ButtonUpdate.UseVisualStyleBackColor = true;
			this.ButtonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
			// 
			// ButtonDelete
			// 
			this.ButtonDelete.Location = new System.Drawing.Point(16, 112);
			this.ButtonDelete.Name = "ButtonDelete";
			this.ButtonDelete.Size = new System.Drawing.Size(96, 28);
			this.ButtonDelete.TabIndex = 6;
			this.ButtonDelete.Text = "削除";
			this.ButtonDelete.UseVisualStyleBackColor = true;
			this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
			// 
			// ButtonQuery
			// 
			this.ButtonQuery.Location = new System.Drawing.Point(16, 72);
			this.ButtonQuery.Name = "ButtonQuery";
			this.ButtonQuery.Size = new System.Drawing.Size(96, 28);
			this.ButtonQuery.TabIndex = 5;
			this.ButtonQuery.Text = "検索";
			this.ButtonQuery.UseVisualStyleBackColor = true;
			this.ButtonQuery.Click += new System.EventHandler(this.ButtonQuery_Click);
			// 
			// ButtonCreate
			// 
			this.ButtonCreate.Location = new System.Drawing.Point(16, 32);
			this.ButtonCreate.Name = "ButtonCreate";
			this.ButtonCreate.Size = new System.Drawing.Size(96, 28);
			this.ButtonCreate.TabIndex = 4;
			this.ButtonCreate.Text = "DB 作成";
			this.ButtonCreate.UseVisualStyleBackColor = true;
			this.ButtonCreate.Click += new System.EventHandler(this.ButtonCreate_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.ButtonQuery2);
			this.groupBox2.Controls.Add(this.ButtonCreate2);
			this.groupBox2.Location = new System.Drawing.Point(160, 16);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(128, 196);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "ジェネリック";
			// 
			// ButtonQuery2
			// 
			this.ButtonQuery2.Location = new System.Drawing.Point(16, 72);
			this.ButtonQuery2.Name = "ButtonQuery2";
			this.ButtonQuery2.Size = new System.Drawing.Size(96, 28);
			this.ButtonQuery2.TabIndex = 7;
			this.ButtonQuery2.Text = "検索";
			this.ButtonQuery2.UseVisualStyleBackColor = true;
			this.ButtonQuery2.Click += new System.EventHandler(this.ButtonQuery2_Click);
			// 
			// ButtonCreate2
			// 
			this.ButtonCreate2.Location = new System.Drawing.Point(16, 32);
			this.ButtonCreate2.Name = "ButtonCreate2";
			this.ButtonCreate2.Size = new System.Drawing.Size(96, 28);
			this.ButtonCreate2.TabIndex = 6;
			this.ButtonCreate2.Text = "DB 作成";
			this.ButtonCreate2.UseVisualStyleBackColor = true;
			this.ButtonCreate2.Click += new System.EventHandler(this.ButtonCreate2_Click);
			// 
			// FormTestLinq
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(309, 233);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTestLinq";
			this.Text = "LINQ to SQLite";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button ButtonUpdate;
		private System.Windows.Forms.Button ButtonDelete;
		private System.Windows.Forms.Button ButtonQuery;
		private System.Windows.Forms.Button ButtonCreate;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button ButtonQuery2;
		private System.Windows.Forms.Button ButtonCreate2;
	}
}

