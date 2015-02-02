using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LittleSomething
{
    public partial class Form1 : Form
    {
        DataTable _dt; 
        Random _ran;

        public Form1()
        {
            InitializeComponent();
            _ran = new Random();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            errorMessage = ValidForm();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage);
                this.textBox1.Focus();
                return;
            }
            _dt = new DataTable("numRows");
            _dt.Columns.Add("value");
            // CreateRows
            CreateRows();
            this.dataGridView1.DataSource = _dt;

            DoStats();
        }
        #region Private Methods

        private void DoStats()
        { 
            // sum, average, decimal, rows
            int rows = _dt.Rows.Count;
            double sum = 0.0;
            double avg = 0.0;
            double roundedAvg = 0.0;
            int intMaxDecPlaces = 0;
            
            foreach (DataRow dr in _dt.Rows)
            {
                double val = double.Parse(dr[0].ToString());
                int intDecPlaces = GetNumberOfDecimals(val);
                if (intDecPlaces > intMaxDecPlaces)
                {
                    intMaxDecPlaces = intDecPlaces;
                }
                sum += val;
            }
            avg = (sum / _dt.Rows.Count);
            roundedAvg = Math.Round(avg, intMaxDecPlaces);

            // Display status
            this.lblAverage.Text = avg.ToString();
            this.lblDecimals.Text = intMaxDecPlaces.ToString();
            this.lblRows.Text = rows.ToString();
            this.lblSum.Text = sum.ToString();
            this.lblRoundedAvg.Text = roundedAvg.ToString();
        }
        
        private int GetNumberOfDecimals(double val)
        {
            int returnValue = 0;
            string strVal = val.ToString();
            int dec = strVal.IndexOf(".");
            string strDecVal = strVal.Substring(dec + 1);
            returnValue = strDecVal.Length;
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateRows()
        {
            int numRows = int.Parse(this.textBox1.Text.Trim());
            for (int i = 1; i <= numRows; i++)
            { 
                // Calculate number
                double val = CalculateNumber();

                // Add row
                DataRow dr = _dt.NewRow();
                dr["value"] = val;
                _dt.Rows.Add(dr);
            }
        }

        private double CalculateNumber()
        {
            double returnValue = 0.0;
            string decimalValue = string.Empty;
            string totalValue = string.Empty;
            int decimalPlaces = 0;
             
            // Determine decimal places
            decimalPlaces = _ran.Next(0,9);
            for (int i = 1; i < decimalPlaces; i++)
            {
                decimalValue += _ran.Next(0, 9).ToString();
            }
            totalValue = _ran.Next(1, 1000).ToString() + "." + decimalValue;
            returnValue = double.Parse(totalValue);
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string ValidForm()
        {
            string returnValue = string.Empty;
            string textBoxValue = this.textBox1.Text.Trim();
            int numRows;

            if (textBoxValue.Length == 0)
            {
                returnValue = "You must enter a value.";
            } else if (int.TryParse(textBoxValue, out numRows) == false )
            {
                returnValue = "You must enter an integer value.";
            }

            return returnValue;
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
