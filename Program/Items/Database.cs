using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Forms;

public static class Database
{
    public static SqlConnection connection;//соединение
    static SqlDataAdapter adapter;//адаптер для хранения результата запроса SELECT
    static SqlCommand command;//инкапсулирует sql-выражение, которое должно быть выполнено
    static SqlDataReader reader;//возвращает строки из потока после выполнения запроса

    public static int UserId;
    public static string UserName;

    private static string BuildConnectionString()//строим строку подключения к базе
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        return connectionString;
    }

    public static bool IsConnected()
    {
        return (connection.State == ConnectionState.Open ? true : false);
    }

    public static void OpenDatabase()//создаем подключение и открываем базу
    {
        string connectionString = BuildConnectionString();

        if (connectionString.Length != 0)
        {
            // создаем экземпляр класса SqlConnection
            connection = new SqlConnection(connectionString);
            // открываем соединение с БД
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка подключения");
            }
        }
        else
        {
            MessageBox.Show("База данных не найдена", "Ошибка");
        }
    }

    public static void CloseDatabase()//закрыть подключение
    {
        if (connection.State != ConnectionState.Closed)
            connection.Close();
    }

    public static DataTable SelectQuery(string query)//запрос выборки SELECT для вывода в DataGrid
    {
        command = new SqlCommand(query, connection);
        DataTable dt = new DataTable();
        adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        return dt;
    }

    public static SqlDataReader BasicSelectQuery(string query)//простой запрос выборки для построчного считывания
    {
        command = new SqlCommand(query, connection);
        reader = command.ExecuteReader();
        return reader;
    }

    public static void InsertInto(string query)//запрос INSERT
    {
        OpenDatabase();
        try
        {
            command = connection.CreateCommand();
            command.CommandText = query;
            reader = command.ExecuteReader();
        }
        catch (SqlException ex)
        {
            MessageBox.Show(ex.Message, "Ошибка");
        }
        CloseDatabase();
    }

    public static void DeleteQuery(string query)//запрос DELETE
    {
        OpenDatabase();
        try
        {
            command = connection.CreateCommand();
            command.CommandText = query;
            reader = command.ExecuteReader();
        }
        catch (SqlException ex)
        {
            MessageBox.Show(ex.Message, "Ошибка");
        }
        CloseDatabase();
    }

    public static void UpdateQuery(string query)//запрос UPDATE
    {
        OpenDatabase();
        try
        {
            command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            MessageBox.Show(ex.Message, "Ошибка");
        }
        CloseDatabase();

    }
}
