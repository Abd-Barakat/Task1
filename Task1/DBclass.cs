using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;
namespace Task1
{
    public class DBclass
    {
        private SqlConnection connection;

        private SqlCommand command;

        private DataTable [] dataTables = new DataTable[4];

        public DBclass()
        {
            connection  = new SqlConnection(); 
            command = new SqlCommand();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString;
            command.Connection = connection;
        }
        public void Open_connection()//to open SQL connection if it closed otherwise leave it open 
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }
        public void Insert(int Groupbox_index, string[] Tables, Question q)
        {
            Open_connection();
            switch (Groupbox_index)
            {
                case 0:
                    command.Connection = connection;
                    command.CommandText = string.Format("insert into {0} values ('{1}',{2},'{3}')", Tables[0], q.Question_text, q.Question_order, Tables[Groupbox_index + 1]);
                    command.ExecuteNonQuery();
                    command.CommandText = string.Format("insert into {0} values ({1},{2},{3},{4},{5})", Tables[1], q.Question_order, q.Current_values().ElementAt(0), q.Current_values().ElementAt(1), q.Current_values().ElementAt(2), q.Current_values().ElementAt(3));
                    command.ExecuteNonQuery();
                    break;
                case 1:
                    command.Connection = connection;
                    command.CommandText = string.Format("insert into {0} values ('{1}',{2},'{3}')", Tables[0], q.Question_text, q.Question_order, Tables[Groupbox_index + 1]);
                    command.ExecuteNonQuery();
                    command.CommandText = string.Format("insert into {0} values ({1},{2})", Tables[2], q.Question_order, q.Current_values().ElementAt(0));
                    command.ExecuteNonQuery();
                    break;
                case 2:
                    command.Connection = connection;

                    command.CommandText = string.Format("insert into {0} values ('{1}',{2},'{3}')", Tables[0], q.Question_text, q.Question_order, Tables[Groupbox_index + 1]);
                    command.ExecuteNonQuery();
                    command.CommandText = string.Format("insert into {0} values ({1},{2})", Tables[3], q.Question_order, q.Current_values().ElementAt(0));
                    command.ExecuteNonQuery();
                    break;
            }
        }
        public void Update(Question q)
        {
            command.CommandText = string.Format("update questions set question_text ='{0}' where question_order={1}",q.Question_text, q.Question_order);
            Open_connection();
            command.ExecuteNonQuery();
            switch (q.Question_type)
            {
                case "Slider":
                    command.CommandText = string.Format("update Slider set Start_Value ={0},End_Value ={1},Start_Value_Caption ={2},End_Value_Caption ={3} where question_order={4}", q.Current_values().ElementAt(0), q.Current_values().ElementAt(1), q.Current_values().ElementAt(2), q.Current_values().ElementAt(3), q.Question_order);
                    Open_connection();
                    command.ExecuteNonQuery();
                    break;
                case "Smiley":

                    command.CommandText = string.Format("update Smiley set Num_Faces ={0} where question_order={1}", q.Current_values().ElementAt(0), q.Question_order);
                    Open_connection();
                    command.ExecuteNonQuery();
                    break;
                case "Stars":
                    command.CommandText = string.Format("update Stars set Num_Stars ={0} where question_order={1}", q.Current_values().ElementAt(0), q.Question_order);
                    Open_connection();
                    command.ExecuteNonQuery();
                    break;
            }
        }
        public void Delete (string type,int order)
        {
            command.CommandText = string.Format("delete from {0} where question_order={1}",type,order);//sql command
            Open_connection();
            command.ExecuteNonQuery();//execute command 

            command.CommandText = string.Format("delete from questions where question_order= {0}", order);//change sql command text 
            Open_connection();
            command.ExecuteNonQuery();//execute command 
        }
        public DataTable load ()
        {
            SqlDataAdapter dataAdapter;
            Open_connection();
            command.CommandText = "select * from questions";//new command to database 
            dataAdapter = new SqlDataAdapter(command);//execute command and save it in adapter
            dataTables[0] = new DataTable();
            dataAdapter.Fill(dataTables[0]);//fill data table with data retrived from database 

            command.CommandText = "select * from Slider";
            dataAdapter = new SqlDataAdapter(command);
            dataTables[1] = new DataTable();
            dataAdapter.Fill(dataTables[1]);

            command.CommandText = "select * from Smiley";
            dataAdapter = new SqlDataAdapter(command);
            dataTables[2] = new DataTable();
            dataAdapter.Fill(dataTables[2]);

            command.CommandText = "select * from Stars";
            dataAdapter = new SqlDataAdapter(command);
            dataTables[3] = new DataTable();
            dataAdapter.Fill(dataTables[3]);

            return dataTables[0].DefaultView.ToTable(false, "question_text");//extract one column from data table ;

        }
        public DataRow [] extract_row(int order,int index)
        {
            DataRow[] temp = new DataRow[2];
            temp[0] = dataTables[0].Select(string.Format("Convert(question_order,'System.String') LIKE '%{0}%'", order)).First();//Like method compare two elements with same type so question order is int type and order converted to string implcitly , we must convert question_order to string first 
            temp[1] = dataTables[index].Select(string.Format("Convert(question_order,'System.String') LIKE '%{0}%'", order)).First();//Like method compare two elements with same type so question order is int type and order converted to string implcitly , w
            return temp;
        }
        public DataTable question_table ()
        {
            return dataTables[0];
        }
    }
}
