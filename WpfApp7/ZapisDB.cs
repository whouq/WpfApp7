using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;

namespace WpfApp7
{
    internal class ZapisDB
    {
        DBConnection connection;

        private ZapisDB(DBConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Zapis zapis)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Zapis` Values (0, @nachalo, @konez,@vrach,@kabinet,@den,@datapriema,@pereriv, @primechanie);select LAST_INSERT_ID();");


                cmd.Parameters.Add(new MySqlParameter("nachalo", zapis.Nachalo));
                cmd.Parameters.Add(new MySqlParameter("konez", zapis.Konez));
                cmd.Parameters.Add(new MySqlParameter("vrach", zapis.Vrach));
                cmd.Parameters.Add(new MySqlParameter("kabinet", zapis.Kabinet));
                cmd.Parameters.Add(new MySqlParameter("den", zapis.Den));
                cmd.Parameters.Add(new MySqlParameter("datapriema", zapis.DataPriema));
                cmd.Parameters.Add(new MySqlParameter("pereriv", zapis.Pereriv));
                cmd.Parameters.Add(new MySqlParameter("primechanie", zapis.Primechanie));




                MySqlParameter konez = new MySqlParameter("lname", zapis.Konez);
                cmd.Parameters.Add(konez);
                try
                {

                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {


                       zapis.ID = id;
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Запись не добавлена");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            connection.CloseConnection();
            return result;
        }
      

        internal List<Zapis> SelectAll()
        {
            List<Zapis> zapis = new List<Zapis>();
            if (connection == null)
                return zapis;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `nachalo`, `konez`, `vrach`, `kabinet`, `den`, `datapriema`, `pereriv`, `primechanie` from `Zapis` ");
                try
                {

                    MySqlDataReader dr = command.ExecuteReader(); 
                    // в цикле читаем построчно всю таблицу
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                       var nachalo = new TimeOnly();

                        if (!dr.IsDBNull(1))
                            nachalo = dr.GetTimeOnly("nachalo");
                        var konez = dr.GetTimeOnly("konez");
                        string vrach = dr.GetString("kabinet");
                        var den = dr.GetDateTime("datapriema");
                        var pereriv = dr.GetTimeOnly("pereriv");
              
                        string primechanie = dr.GetString("primechanie");
                        zapis.Add(new Zapis
                        {
                            ID = id,
                           Nachalo = nachalo,
                            Konez = konez,
                            Vrach = vrach,
                            Den = den,
                            Pereriv = pereriv,
                          
                            Primechanie = primechanie,

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return zapis;
        }

        internal bool Update(Zapis edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Zapis` set `nachalo`=@nachalo, `konez`=konez,`vrach`=@vrach, `den`=@den, `pereiv`=@pereriv, `primchanie`=@primechanie  where `id` = {edit.ID}");
                mc.Parameters.Add(new MySqlParameter("nachalo", edit.Nachalo));
                mc.Parameters.Add(new MySqlParameter("konez", edit.Konez));
                mc.Parameters.Add(new MySqlParameter("vrach", edit.Vrach));
                mc.Parameters.Add(new MySqlParameter("den", edit.Den));
                mc.Parameters.Add(new MySqlParameter("pereriv", edit.Pereriv));
                mc.Parameters.Add(new MySqlParameter("primechanie", edit.Primechanie));

                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }


        internal bool Remove(Zapis remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Zapis` where `id` = {remove.ID}");
                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }

        static ZapisDB db;
        public static ZapisDB GetDb()
        {
            if (db == null)
                db = new ZapisDB(DBConnection.GetDbConnection());
            return db;
        }
    }
}

