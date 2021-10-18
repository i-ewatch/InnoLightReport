
namespace SunnineReport.Views
{
    partial class SenserHourReportUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BarpanelControl = new DevExpress.XtraEditors.PanelControl();
            this.DevicecheckedComboBoxEdit = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.EndtimeSpanEdit = new DevExpress.XtraEditors.TimeEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.StarttimeSpanEdit = new DevExpress.XtraEditors.TimeEdit();
            this.SearchsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.ExportsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.GridpanelControl = new DevExpress.XtraEditors.PanelControl();
            this.SensergridControl = new DevExpress.XtraGrid.GridControl();
            this.SensergridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.BarpanelControl)).BeginInit();
            this.BarpanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevicecheckedComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndtimeSpanEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StarttimeSpanEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridpanelControl)).BeginInit();
            this.GridpanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SensergridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SensergridView)).BeginInit();
            this.SuspendLayout();
            // 
            // BarpanelControl
            // 
            this.BarpanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.BarpanelControl.Controls.Add(this.DevicecheckedComboBoxEdit);
            this.BarpanelControl.Controls.Add(this.labelControl3);
            this.BarpanelControl.Controls.Add(this.EndtimeSpanEdit);
            this.BarpanelControl.Controls.Add(this.labelControl2);
            this.BarpanelControl.Controls.Add(this.StarttimeSpanEdit);
            this.BarpanelControl.Controls.Add(this.SearchsimpleButton);
            this.BarpanelControl.Controls.Add(this.ExportsimpleButton);
            this.BarpanelControl.Controls.Add(this.labelControl1);
            this.BarpanelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarpanelControl.Location = new System.Drawing.Point(0, 0);
            this.BarpanelControl.Name = "BarpanelControl";
            this.BarpanelControl.Size = new System.Drawing.Size(1076, 38);
            this.BarpanelControl.TabIndex = 1;
            // 
            // DevicecheckedComboBoxEdit
            // 
            this.DevicecheckedComboBoxEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.DevicecheckedComboBoxEdit.EditValue = "";
            this.DevicecheckedComboBoxEdit.Location = new System.Drawing.Point(544, 0);
            this.DevicecheckedComboBoxEdit.Name = "DevicecheckedComboBoxEdit";
            this.DevicecheckedComboBoxEdit.Properties.AllowFocused = false;
            this.DevicecheckedComboBoxEdit.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.DevicecheckedComboBoxEdit.Properties.Appearance.Options.UseFont = true;
            this.DevicecheckedComboBoxEdit.Properties.AppearanceDropDown.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.DevicecheckedComboBoxEdit.Properties.AppearanceDropDown.Options.UseFont = true;
            this.DevicecheckedComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DevicecheckedComboBoxEdit.Size = new System.Drawing.Size(357, 34);
            this.DevicecheckedComboBoxEdit.TabIndex = 27;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl3.Location = new System.Drawing.Point(470, 0);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(74, 38);
            this.labelControl3.TabIndex = 26;
            this.labelControl3.Text = " 區域 :";
            // 
            // EndtimeSpanEdit
            // 
            this.EndtimeSpanEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.EndtimeSpanEdit.EditValue = null;
            this.EndtimeSpanEdit.Location = new System.Drawing.Point(289, 0);
            this.EndtimeSpanEdit.Name = "EndtimeSpanEdit";
            this.EndtimeSpanEdit.Properties.AllowFocused = false;
            this.EndtimeSpanEdit.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.EndtimeSpanEdit.Properties.Appearance.Options.UseFont = true;
            this.EndtimeSpanEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.EndtimeSpanEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.EndtimeSpanEdit.Properties.Mask.EditMask = "yyyy/MM/dd HH";
            this.EndtimeSpanEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.EndtimeSpanEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.EndtimeSpanEdit.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            this.EndtimeSpanEdit.Size = new System.Drawing.Size(181, 34);
            this.EndtimeSpanEdit.TabIndex = 25;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl2.Location = new System.Drawing.Point(255, 0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(34, 38);
            this.labelControl2.TabIndex = 21;
            this.labelControl2.Text = " ~";
            // 
            // StarttimeSpanEdit
            // 
            this.StarttimeSpanEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.StarttimeSpanEdit.EditValue = null;
            this.StarttimeSpanEdit.Location = new System.Drawing.Point(74, 0);
            this.StarttimeSpanEdit.Name = "StarttimeSpanEdit";
            this.StarttimeSpanEdit.Properties.AllowFocused = false;
            this.StarttimeSpanEdit.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.StarttimeSpanEdit.Properties.Appearance.Options.UseFont = true;
            this.StarttimeSpanEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StarttimeSpanEdit.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.StarttimeSpanEdit.Properties.Mask.EditMask = "yyyy/MM/dd HH";
            this.StarttimeSpanEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.StarttimeSpanEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.StarttimeSpanEdit.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            this.StarttimeSpanEdit.Size = new System.Drawing.Size(181, 34);
            this.StarttimeSpanEdit.TabIndex = 18;
            // 
            // SearchsimpleButton
            // 
            this.SearchsimpleButton.AllowFocus = false;
            this.SearchsimpleButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.SearchsimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.SearchsimpleButton.Appearance.Options.UseBackColor = true;
            this.SearchsimpleButton.Appearance.Options.UseFont = true;
            this.SearchsimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.SearchsimpleButton.Location = new System.Drawing.Point(926, 0);
            this.SearchsimpleButton.Name = "SearchsimpleButton";
            this.SearchsimpleButton.Size = new System.Drawing.Size(75, 38);
            this.SearchsimpleButton.TabIndex = 5;
            this.SearchsimpleButton.Text = "查詢";
            // 
            // ExportsimpleButton
            // 
            this.ExportsimpleButton.AllowFocus = false;
            this.ExportsimpleButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ExportsimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.ExportsimpleButton.Appearance.Options.UseBackColor = true;
            this.ExportsimpleButton.Appearance.Options.UseFont = true;
            this.ExportsimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ExportsimpleButton.Location = new System.Drawing.Point(1001, 0);
            this.ExportsimpleButton.Name = "ExportsimpleButton";
            this.ExportsimpleButton.Size = new System.Drawing.Size(75, 38);
            this.ExportsimpleButton.TabIndex = 4;
            this.ExportsimpleButton.Text = "匯出";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微軟正黑體", 18F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 38);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = " 時間 :";
            // 
            // GridpanelControl
            // 
            this.GridpanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.GridpanelControl.Controls.Add(this.SensergridControl);
            this.GridpanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridpanelControl.Location = new System.Drawing.Point(0, 38);
            this.GridpanelControl.Name = "GridpanelControl";
            this.GridpanelControl.Size = new System.Drawing.Size(1076, 650);
            this.GridpanelControl.TabIndex = 2;
            // 
            // SensergridControl
            // 
            this.SensergridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SensergridControl.Location = new System.Drawing.Point(0, 0);
            this.SensergridControl.MainView = this.SensergridView;
            this.SensergridControl.Name = "SensergridControl";
            this.SensergridControl.Size = new System.Drawing.Size(1076, 650);
            this.SensergridControl.TabIndex = 0;
            this.SensergridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.SensergridView});
            // 
            // SensergridView
            // 
            this.SensergridView.GridControl = this.SensergridControl;
            this.SensergridView.Name = "SensergridView";
            this.SensergridView.OptionsView.ShowGroupPanel = false;
            // 
            // SenserHourReportUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridpanelControl);
            this.Controls.Add(this.BarpanelControl);
            this.Name = "SenserHourReportUserControl";
            this.Size = new System.Drawing.Size(1076, 688);
            ((System.ComponentModel.ISupportInitialize)(this.BarpanelControl)).EndInit();
            this.BarpanelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevicecheckedComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndtimeSpanEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StarttimeSpanEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridpanelControl)).EndInit();
            this.GridpanelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SensergridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SensergridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BarpanelControl;
        private DevExpress.XtraEditors.SimpleButton ExportsimpleButton;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton SearchsimpleButton;
        private DevExpress.XtraEditors.TimeEdit StarttimeSpanEdit;
        private DevExpress.XtraEditors.CheckedComboBoxEdit DevicecheckedComboBoxEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TimeEdit EndtimeSpanEdit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraEditors.PanelControl GridpanelControl;
        private DevExpress.XtraGrid.GridControl SensergridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView SensergridView;
    }
}
