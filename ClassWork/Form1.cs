﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassWork
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Data Source=DESKTOP-QJ3KTBA\SQLEXPRESS;Initial Catalog=ClassTable;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection Con = new SqlConnection();
        DataTable ClassTable = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void loaddb()
        {
            //load datatable columns          
            datatablecolumns();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Ask a question
                string QueryString = @"SELECT * FROM ClassTable order by id DESC";
                //Open your connection      
                connection.Open();
                SqlCommand Command = new SqlCommand(QueryString, connection);
                //Start your DB reader      
                SqlDataReader reader = Command.ExecuteReader();
                while (reader.Read())
                {
                    //add in each row to the datatable          
                    ClassTable.Rows.Add(
                        reader["ID"],
                        reader["Name"]);
                }
                //Close your DB reader      
                reader.Close();
                //Close your connection       
                connection.Close();
                //add the datatable to the Data Grid View      
                dataGridView1.DataSource = ClassTable;
            }
        }

        public void datatablecolumns()
        {
            //clear the old data            
            ClassTable.Clear();
            //add in the column titles to the datatable            
            try
            {
                ClassTable.Columns.Add("ID");
                ClassTable.Columns.Add("Name");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loaddb();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // a simple Scalar query just returning one value.
            string querystring = "SELECT COUNT(ID) FROM ClassTable";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(querystring, connection);
                connection.Open();
                button2.Text = command.ExecuteScalar().ToString();
                connection.Close();
            }
        }
    }
}
    

