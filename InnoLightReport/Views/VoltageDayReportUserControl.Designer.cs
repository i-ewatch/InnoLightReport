
namespace InnoLightReport.Views
{
    partial class VoltageDayReportUserControl
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
            this.CurrentgridControl = new DevExpress.XtraGrid.GridControl();
            this.CurrentgridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DevicecheckedComboBoxEdit = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.SearchsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.ExportsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.EnddateEdit = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.StartdateEdit = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.GridpanelControl = new DevExpress.XtraEditors.PanelControl();
            this.BarpanelControl = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentgridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentgridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevicecheckedComboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnddateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnddateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartdateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartdateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridpanelControl)).BeginInit();
            this.GridpanelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BarpanelControl)).BeginInit();
            this.BarpanelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // CurrentgridControl
            // 
            this.CurrentgridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentgridControl.Location = new System.Drawing.Point(0, 0);
            this.CurrentgridControl.MainView = this.CurrentgridView;
            this.CurrentgridControl.Name = "CurrentgridControl";
            this.CurrentgridControl.Size = new System.Drawing.Size(1076, 650);
            this.CurrentgridControl.TabIndex = 0;
            this.CurrentgridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.CurrentgridView});
            // 
            // CurrentgridView
            // 
            this.CurrentgridView.GridControl = this.CurrentgridControl;
            this.CurrentgridView.Name = "CurrentgridView";
            this.CurrentgridView.OptionsView.ShowGroupPanel = false;
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
            this.DevicecheckedComboBoxEdit.TabIndex = 7;
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
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = " 區域 :";
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
            // EnddateEdit
            // 
            this.EnddateEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.EnddateEdit.EditValue = null;
            this.EnddateEdit.Location = new System.Drawing.Point(289, 0);
            this.EnddateEdit.Name = "EnddateEdit";
            this.EnddateEdit.Properties.AllowFocused = false;
            this.EnddateEdit.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.EnddateEdit.Properties.Appearance.Options.UseFont = true;
            this.EnddateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.EnddateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.EnddateEdit.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.EnddateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.EnddateEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.EnddateEdit.Size = new System.Drawing.Size(181, 34);
            this.EnddateEdit.TabIndex = 3;
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
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = " ~";
            // 
            // StartdateEdit
            // 
            this.StartdateEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.StartdateEdit.EditValue = null;
            this.StartdateEdit.Location = new System.Drawing.Point(74, 0);
            this.StartdateEdit.Name = "StartdateEdit";
            this.StartdateEdit.Properties.AllowFocused = false;
            this.StartdateEdit.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.StartdateEdit.Properties.Appearance.Options.UseFont = true;
            this.StartdateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StartdateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StartdateEdit.Properties.Mask.EditMask = "yyyy/MM/dd";
            this.StartdateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.StartdateEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.StartdateEdit.Size = new System.Drawing.Size(181, 34);
            this.StartdateEdit.TabIndex = 1;
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
            this.GridpanelControl.Controls.Add(this.CurrentgridControl);
            this.GridpanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridpanelControl.Location = new System.Drawing.Point(0, 38);
            this.GridpanelControl.Name = "GridpanelControl";
            this.GridpanelControl.Size = new System.Drawing.Size(1076, 650);
            this.GridpanelControl.TabIndex = 11;
            // 
            // BarpanelControl
            // 
            this.BarpanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.BarpanelControl.Controls.Add(this.DevicecheckedComboBoxEdit);
            this.BarpanelControl.Controls.Add(this.labelControl3);
            this.BarpanelControl.Controls.Add(this.SearchsimpleButton);
            this.BarpanelControl.Controls.Add(this.ExportsimpleButton);
            this.BarpanelControl.Controls.Add(this.EnddateEdit);
            this.BarpanelControl.Controls.Add(this.labelControl2);
            this.BarpanelControl.Controls.Add(this.StartdateEdit);
            this.BarpanelControl.Controls.Add(this.labelControl1);
            this.BarpanelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarpanelControl.Location = new System.Drawing.Point(0, 0);
            this.BarpanelControl.Name = "BarpanelControl";
            this.BarpanelControl.Size = new System.Drawing.Size(1076, 38);
            this.BarpanelControl.TabIndex = 10;
            // 
            // VoltageDayReportUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridpanelControl);
            this.Controls.Add(this.BarpanelControl);
            this.Name = "VoltageDayReportUserControl";
            this.Size = new System.Drawing.Size(1076, 688);
            ((System.ComponentModel.ISupportInitialize)(this.CurrentgridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentgridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DevicecheckedComboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnddateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnddateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartdateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartdateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridpanelControl)).EndInit();
            this.GridpanelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BarpanelControl)).EndInit();
            this.BarpanelControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl CurrentgridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView CurrentgridView;
        private DevExpress.XtraEditors.CheckedComboBoxEdit DevicecheckedComboBoxEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton SearchsimpleButton;
        private DevExpress.XtraEditors.SimpleButton ExportsimpleButton;
        private DevExpress.XtraEditors.DateEdit EnddateEdit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit StartdateEdit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl GridpanelControl;
        private DevExpress.XtraEditors.PanelControl BarpanelControl;
    }
}
