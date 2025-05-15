﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySqlConnector;

namespace WpfApp7
{
    internal class DBConnection
    {
        MySqlConnection _connection;

        public void Config()
        {
          
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.UserID = "student";
            sb.Password = "student";
            sb.Server =  "192.168.200.13";//"95.154.107.102";//
            sb.Database = "1125_new_2025";
            sb.CharacterSet = "utf8mb4";

            _connection = new MySqlConnection(sb.ToString());
        }

        public bool OpenConnection()
        {
            if (_connection == null)
                Config();
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        internal void CloseConnection()
        {
            if (_connection == null)
                return;

            try
            {
                _connection.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }

        internal MySqlCommand CreateCommand(string sql)
        {
            return new MySqlCommand(sql, _connection);
        }


        static DBConnection dbConnection;
        private DBConnection() { }
        public static DBConnection GetDbConnection()
        {
            if (dbConnection == null)
                dbConnection = new DBConnection();
            return dbConnection;
        }


    }
}
