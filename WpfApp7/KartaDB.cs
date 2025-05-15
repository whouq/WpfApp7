using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;

namespace WpfApp7
{
    internal class KartaDB
    {
        DBConnection connection;

        private KartaDB(DBConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Ambulatorkarta ambulatorkarta)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Ambulatrokarta` Values (0, @pfname, @plname,@ppatronymic,@birthday,@gender,@telephon,@email, @mestoraboti, @stag, @nomerdoc, @seriadoc,@kemvidan,@datavidachi,@nomerpolisa );select LAST_INSERT_ID();");


                cmd.Parameters.Add(new MySqlParameter("pfname", ambulatorkarta.PFirstname));
                cmd.Parameters.Add(new MySqlParameter("plname", ambulatorkarta.PLastname));
                cmd.Parameters.Add(new MySqlParameter("ppatronymic", ambulatorkarta.PPatronymic));
                cmd.Parameters.Add(new MySqlParameter("birthday", ambulatorkarta.Birthday));
                cmd.Parameters.Add(new MySqlParameter("gender", ambulatorkarta.Telephon));
                cmd.Parameters.Add(new MySqlParameter("email", ambulatorkarta.Email));
                cmd.Parameters.Add(new MySqlParameter("stag", ambulatorkarta.Stag));
                cmd.Parameters.Add(new MySqlParameter("nomerdoc", ambulatorkarta.Nomerdoc));
                cmd.Parameters.Add(new MySqlParameter("seriadoc", ambulatorkarta.Seriadoc));
                cmd.Parameters.Add(new MySqlParameter("kemvidan", ambulatorkarta.Kemvidan));
                cmd.Parameters.Add(new MySqlParameter("mestoraboti", ambulatorkarta.Mestoraboti));
                cmd.Parameters.Add(new MySqlParameter("nomerpolisa", ambulatorkarta.Nomerpolisa));
                cmd.Parameters.Add(new MySqlParameter("datavidachi", ambulatorkarta.Datavidachi));


                MySqlParameter plname = new MySqlParameter("plname", ambulatorkarta.PLastname);
                cmd.Parameters.Add(plname);
                try
                {

                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {


                        ambulatorkarta.ID = id;
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
        //        MySqlParameter lname = new MySqlParameter("lname", vrachi.VPatronymic);
        //        cmd.Parameters.Add(vpatronymic);
        //                try
        //                {

        //                    int id = (int)(ulong)cmd.ExecuteScalar();
        //                    if (id > 0)
        //                    {
        //                        MessageBox.Show(id.ToString());

        //                        vrachi.ID = id;
        //                        result = true;
        //                    } 
        //                    else
        //                    {
        //                        MessageBox.Show("Запись не добавлена");
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.Message);
        //                }
        //            }
        //            connection.CloseConnection();
        //return result;
        //        }

        internal List<Ambulatorkarta> SelectAll()
        {
            List<Ambulatorkarta> ambulatorkarta = new List<Ambulatorkarta>();
            if (connection == null)
                return ambulatorkarta;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `pfname`, `plname`, `ppatronymic`, `birthday`, `email`, `gender`, `telephon`,`mestoraboti`,`stag`,`nomerdoc`,`seriadoc`,`kemvidan`, `datavidachi`, `nomerpolisa` from `Ambulatorkarta` ");
                try
                {

                    MySqlDataReader dr = command.ExecuteReader();
                    // в цикле читаем построчно всю таблицу
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string pfname = string.Empty;

                        if (!dr.IsDBNull(1))
                            pfname = dr.GetString("pfname");
                        string plname = dr.GetString("plname");
                        string ppatronymic = dr.GetString("ppatronymic");
                        DateTime birthday = dr.GetDateTime("birthday");
                        string email = dr.GetString("email");
                        bool gender = dr.GetBoolean("gender");
                        int telephon = dr.GetInt32("telphon");
                        string mestoraboti = dr.GetString("mestoraboti");
                        int stag = dr.GetInt32("stag");
                        int nomerdoc = dr.GetInt32("nomerdoc");
                        int seriadoc = dr.GetInt32("seriadoc");
                        string kemvidan = dr.GetString("kemvidan");
                        DateTime datavidachi = dr.GetDateTime("datavidachi");
                        int Nomerpolisa = dr.GetInt32("nomerpolisa");


                        ambulatorkarta.Add(new Ambulatorkarta
                        {
                            ID = id,
                            PFirstname = pfname,
                            PLastname = plname,
                            PPatronymic = ppatronymic,
                            Gender = gender,
                            Birthday = birthday,
                            Email = email,
                            Telephon = telephon,
                            Mestoraboti = mestoraboti,
                            Stag = stag,
                            Nomerdoc = nomerdoc,
                            Seriadoc = seriadoc,
                            Kemvidan = kemvidan,
                            Datavidachi = datavidachi,
                            Nomerpolisa = nomerpolisa,

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return ambulatorkarta;
        }

        internal bool Update(Ambulatorkarta edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Ambulatorkarta` set `pfname`=@pfname, `plname`=@plname,`ppatronymic`=@ppatronymic, `birthday`=@birthday, `gender`=@gender, `email`=@email, `telephon`=@telephon,`mestoraboti`=@mestoraboti,`stag`=@stag,`nomerdoc`=@nomerdoc,`seriadoc`=@seriadoc,`kemvidan`=@kemvidan,`datavidachi`=@datavidachi,`nomerpolisa`=@nomerpolisa,  where `id` = {edit.ID}");
                mc.Parameters.Add(new MySqlParameter("vfname", edit.VFirstname));
                mc.Parameters.Add(new MySqlParameter("vlname", edit.VLastname));
                mc.Parameters.Add(new MySqlParameter("vpatronymic", edit.VPatronymic));
                mc.Parameters.Add(new MySqlParameter("specialnost", edit.Specialnost));
                mc.Parameters.Add(new MySqlParameter("dennedeli", edit.Dennedeli));
                mc.Parameters.Add(new MySqlParameter("nachalopriema", edit.Nachalopriema));
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


        internal bool Remove(Ambulatorkarta remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Ambulatorkarta` where `id` = {remove.ID}");
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

        static KartaDB db;
        public static KartaDB GetDb()
        {
            if (db == null)
                db = new KartaDB(DBConnection.GetDbConnection());
            return db;
        }
    }
}
