using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data;



public class UserInfo
{
    public int id;
    public string name;
    public string surname;
    public string username;
    public int userType;
}




public class SqlHandle
{
    private MySqlConnection conn = new MySqlConnection("Server=localhost;Database=m5;Uid=root;Pwd=''");

    public DataTable Query(string query)
    {

        DataTable table = new DataTable();
        conn.Open();
        MySqlDataAdapter read_adapter = new MySqlDataAdapter(query, conn);
        read_adapter.Fill(table);
        conn.Close();

        return table;
    }

    public int NonQuery(string query)
    {
        conn.Open();
        MySqlCommand myCmdObject = new MySqlCommand(query ,conn);
        int res = myCmdObject.ExecuteNonQuery();
        conn.Close();

        return res;
    }

}


class Utils
{











}
 
