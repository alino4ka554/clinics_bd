using DocumentFormat.OpenXml.Drawing;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Mysqlx.Notice.SessionStateChanged.Types;

namespace clinics_bd
{
    public class DB
    {
        const string connectionString = "server=localhost; port=3306;username=root; password=70739804;database=clinics_registry";

        MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

        public Dictionary<string, Dictionary<string, bool>> grants = new Dictionary<string, Dictionary<string, bool>>();

        public string userName;
        public int userId;
        public void OpenConnection()
        {
            if (mySqlConnection.State == System.Data.ConnectionState.Closed)
                mySqlConnection.Open();
        }

        public void CloseConnection()
        {
            if (mySqlConnection.State == System.Data.ConnectionState.Open)
                mySqlConnection.Close();
        }

        public bool Connecting(string userName, string password)
        {
            try
            {
                OpenConnection();

                MySqlCommand mySqlCommand = new MySqlCommand(
                    "SELECT * FROM users " +
                    $"WHERE username = @username AND " +
                    $"password = md5(@password)", mySqlConnection);

                mySqlCommand.Parameters.AddWithValue("@username", userName);
                mySqlCommand.Parameters.AddWithValue("@password", password);

                using (var reader = mySqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.userId = reader.GetInt32("id");
                    }
                }
                return (mySqlCommand.ExecuteScalar() != null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
                return false;
            }
            finally { mySqlConnection.Close(); }
        }

        public bool ChangePassword(string newPassword)
        {
            try
            {
                OpenConnection();

                MySqlCommand mySqlCommand = new MySqlCommand(
                    "UPDATE users " +
                    $"SET password = md5(@password) WHERE id = @id", mySqlConnection);

                mySqlCommand.Parameters.AddWithValue("@password", newPassword);
                mySqlCommand.Parameters.AddWithValue("@id", userId);

                if (mySqlCommand.ExecuteNonQuery() != -1)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
                return false;
            }
            finally { mySqlConnection.Close(); }
        }

        public Dictionary<string, Type> LoadHandlersDictionary()
        {
            OpenConnection();

            Dictionary<string, Type> functionsForMenu = new Dictionary<string, Type>();

            var command = new MySqlCommand("SELECT name, function_name FROM menu WHERE function_name IS NOT NULL", mySqlConnection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tabName = reader.GetString("name").Trim();
                    string className = reader.GetString("function_name").Trim();

                    Type handlerType = Assembly.GetExecutingAssembly().GetType($"clinics_bd.{className}");

                    if (handlerType != null)
                    {
                        functionsForMenu.Add(tabName, handlerType);
                    }
                    else
                    {
                        MessageBox.Show($"Класс {className} не найден в пространстве имен clinics_bd.");
                    }
                }
            }
            CloseConnection();
            return functionsForMenu;
        }

        public void GetUserPermissions(string user)
        {
            try
            {
                OpenConnection();
                var permissions = new Dictionary<string, Dictionary<string, bool>>();

                string query = "SELECT menu.name, reading, writing, editing, deleting FROM grants " +
                    "join users on id_user = users.id join menu on id_menu = menu.id WHERE users.username = @user";
                using (var command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@user", user);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string menu = reader.GetString("name");
                            permissions[menu] = new Dictionary<string, bool>
                    {
                        { "reading", reader.GetBoolean("reading") },
                        { "writing", reader.GetBoolean("writing") },
                        { "editing", reader.GetBoolean("editing") },
                        { "deleting", reader.GetBoolean("deleting") }
                    };
                        }
                    }
                }
                this.grants = permissions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки прав пользователя: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }


        public void GetMenuItem(MenuStrip menuStrip)
        {
            try
            {
                OpenConnection();
                var command = new MySqlCommand("SELECT * FROM menu ORDER BY id_parent, order_number", mySqlConnection);
                using (var reader = command.ExecuteReader())
                {
                    Dictionary<int, ToolStripMenuItem> menuItems = new Dictionary<int, ToolStripMenuItem>();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        int parentId = reader.IsDBNull(reader.GetOrdinal("id_parent")) ? 0 : reader.GetInt32("id_parent");
                        string name = reader.GetString("name");

                        ToolStripMenuItem menuItem = new ToolStripMenuItem(name);

                        menuItems[id] = menuItem;

                        if (!grants[menuItem.Text]["reading"])
                        {
                            menuItem.Visible = false;
                        }

                        if (parentId != null && menuItems.ContainsKey(parentId))
                        {
                            menuItems[parentId].DropDownItems.Add(menuItem);
                        }
                        else
                        {
                            menuStrip.Items.Add(menuItem); 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при построении меню: {ex.Message}");
            }
            finally { CloseConnection(); }
        }

        public DataTable GetData(string query)
        {
            try
            {
                OpenConnection();

                using (var command = new MySqlCommand(query, mySqlConnection))
                using (var adapter = new MySqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Введите корректный запрос! ");
                return null;
            }
            finally { CloseConnection(); }
        }

        public DataTable SearchData(string query, object param)
        {
            try
            {
                OpenConnection();

                MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
                mySqlCommand.Parameters.AddWithValue("@param", param);

                using (var adapter = new MySqlDataAdapter(mySqlCommand))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return null;
            }
            finally { CloseConnection(); }
        }

        public void ExecuteQuery(string query, Dictionary<string, object> parametres)
        {
            try
            {
                OpenConnection();

                using (var mySqlCommand = new MySqlCommand(query, mySqlConnection))
                {
                    foreach (var param in parametres)
                    {
                        mySqlCommand.Parameters.AddWithValue(param.Key, param.Value);
                    }
                    mySqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally { CloseConnection(); }
        }

        public List<object> ExecuteQueryList(string query, int id)
        {
            try
            {
                List<object> data = new List<object>();
                OpenConnection();
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                data.Add(reader.GetValue(i)); // Чтение всех значений строки в список
                            }
                        }
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return null;
            }
            finally { CloseConnection(); }
        }
    }
}
