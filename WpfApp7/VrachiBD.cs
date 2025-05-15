using System;
using System.Collections.Generic;
using MySqlConnector;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp7
{
    internal class VrachiBD
    {
        DBConnection connection;

        private VrachiBD(DBConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Vrachi vrachi)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Vrachi` Values (0, @vfname, @vlname,@vpatronymic,@specialnost,@dennedeli,@nachalopriema,@primechanie);select LAST_INSERT_ID();");

           
                cmd.Parameters.Add(new MySqlParameter("vfname", vrachi.VFirstname));
                cmd.Parameters.Add(new MySqlParameter("vlname", vrachi.VLastname));
                cmd.Parameters.Add(new MySqlParameter("vpatronymic", vrachi.VPatronymic));
                cmd.Parameters.Add(new MySqlParameter("specialnost", vrachi.Specialnost));
                cmd.Parameters.Add(new MySqlParameter("dennedeli", vrachi.Dennedeli));
                cmd.Parameters.Add(new MySqlParameter("nachalopriema", vrachi.Nachalopriema));
                cmd.Parameters.Add(new MySqlParameter("primechanie", vrachi.Primechanie));



                MySqlParameter lname = new MySqlParameter("lname", vrachi.VLastname);
                cmd.Parameters.Add(lname);
                try
                {
                  
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                       
                 
                        vrachi.ID = id;
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

        internal List<Vrachi> SelectAll()
        {
            List<Vrachi> vrachi = new List<Vrachi>();
            if (connection == null)
                return vrachi;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `vfname`, `vlname`, `vpatronymic`, `specialnost`, `dennedeli`, `nachalopriema`, `primechanie` from `Vrachi` ");
                try
                {
                  
                    MySqlDataReader dr = command.ExecuteReader();
                    // в цикле читаем построчно всю таблицу
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string vfname = string.Empty;
                
                        if (!dr.IsDBNull(1))
                            vfname = dr.GetString("vfname");
                        string vlname = dr.GetString("vlname");
                        string vpatronymic = dr.GetString("vpatronymic");
                        string specialnost = dr.GetString("specialnost");
                        var dennedeli = dr.GetInt32("dennedeli");
                        var nachalopriema = dr.GetTimeOnly("nachalopriema");
                        string primechanie = dr.GetString("primechanie");
                        vrachi.Add(new Vrachi
                        {
                            ID = id,
                            VFirstname = vfname,
                            VLastname = vlname,
                            VPatronymic = vpatronymic,
                            Specialnost = specialnost,
                            Dennedeli = dennedeli,
                            Nachalopriema = nachalopriema,
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
            return vrachi;
        }

        internal bool Update(Vrachi edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Vrachi` set `vfname`=@vfname, `vlname`=@vlname,`vpatronymic`=@vpatronymic, `specialnost`=@specialnost, `dennedeli`=@dennedeli, `nachalopriema`=@nachalopriema, `primchanie`=@primechanie  where `id` = {edit.ID}");
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


        internal bool Remove(Vrachi remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Vrachi` where `id` = {remove.ID}");
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

        static VrachiBD db;
        public static VrachiBD GetDb()
        {
            if (db == null)
                db = new VrachiBD(DBConnection.GetDbConnection());
            return db;
        }
    }
}
