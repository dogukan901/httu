﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HTTU
{
    class UserAndPassControl 
    {
        SqlConnection con =
            new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\doki_\OneDrive\Belgeler\hastadb.mdf;Integrated Security=True;Connect Timeout=30");

        private SqlCommand cmd;
        private SqlDataReader reader;
        
        public void newUser(TextBox Username, TextBox Password, TextBox repassword, TextBox City, GroupBox group)
        {
            con.Open();
            cmd=new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select *from tableSignLog where Username='" + Username.Text + "'";
            reader = cmd.ExecuteReader();
            if (reader.Read()==false)
            {
                if (Password.Text == repassword.Text)
                {
                    con.Close();
                    con.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into tableSignLog values('" + Username.Text + "','" + Password.Text + "','" + City.Text + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User Added!");
                    foreach (Control item in group.Controls)
                    {
                        if (item is TextBox)
                        {
                            item.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Passwords don't match please check again", "Error");
                } 
            }
            else
            {
                MessageBox.Show("Username already exists","Error");
            }
        }
    }
}
