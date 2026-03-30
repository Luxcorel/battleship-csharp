namespace Battleship
{
    partial class GameBoard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainGrid = new TableLayoutPanel();
            panelRight = new TableLayoutPanel();
            panelLeft = new TableLayoutPanel();
            lblPlayerOneBoard = new Label();
            lblPlayerTwoBoard = new Label();
            mainGrid.SuspendLayout();
            SuspendLayout();
            // 
            // mainGrid
            // 
            mainGrid.ColumnCount = 2;
            mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainGrid.Controls.Add(panelRight, 1, 0);
            mainGrid.Controls.Add(panelLeft, 0, 0);
            mainGrid.Dock = DockStyle.Bottom;
            mainGrid.Location = new Point(0, 32);
            mainGrid.Name = "mainGrid";
            mainGrid.RowCount = 1;
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainGrid.Size = new Size(1124, 414);
            mainGrid.TabIndex = 0;
            // 
            // panelRight
            // 
            panelRight.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            panelRight.ColumnCount = 1;
            panelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(565, 3);
            panelRight.Name = "panelRight";
            panelRight.RowCount = 1;
            panelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            panelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            panelRight.Size = new Size(556, 408);
            panelRight.TabIndex = 1;
            // 
            // panelLeft
            // 
            panelLeft.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            panelLeft.ColumnCount = 1;
            panelLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panelLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Location = new Point(3, 3);
            panelLeft.Name = "panelLeft";
            panelLeft.RowCount = 1;
            panelLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            panelLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            panelLeft.Size = new Size(556, 408);
            panelLeft.TabIndex = 0;
            // 
            // lblPlayerOneBoard
            // 
            lblPlayerOneBoard.AutoSize = true;
            lblPlayerOneBoard.Location = new Point(197, 9);
            lblPlayerOneBoard.Name = "lblPlayerOneBoard";
            lblPlayerOneBoard.Size = new Size(106, 15);
            lblPlayerOneBoard.TabIndex = 1;
            lblPlayerOneBoard.Text = "Player One's Board";
            // 
            // lblPlayerTwoBoard
            // 
            lblPlayerTwoBoard.AutoSize = true;
            lblPlayerTwoBoard.Location = new Point(795, 9);
            lblPlayerTwoBoard.Name = "lblPlayerTwoBoard";
            lblPlayerTwoBoard.Size = new Size(105, 15);
            lblPlayerTwoBoard.TabIndex = 1;
            lblPlayerTwoBoard.Text = "Player Two's Board";
            // 
            // GameBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1124, 446);
            Controls.Add(lblPlayerTwoBoard);
            Controls.Add(lblPlayerOneBoard);
            Controls.Add(mainGrid);
            Name = "GameBoard";
            Text = "Battleship";
            mainGrid.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel mainGrid;
        private TableLayoutPanel panelLeft;
        private TableLayoutPanel panelRight;
        private Label lblPlayerOneBoard;
        private Label lblPlayerTwoBoard;
    }
}
