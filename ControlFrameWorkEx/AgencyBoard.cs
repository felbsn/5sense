using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlFrameWorkEx
{
    public partial class AgencyBoard : Form
    {
        UserInfo U;
        Channel[] channels;
        DataTable users;
        public class Channel
        {
            public int id;
            public string name;

            public Channel(string name , int id)
            {
                this.name = name;
                this.id = id;
            }

            public override string ToString()
            {
                return name;
            }
        }

        public AgencyBoard(UserInfo currentUser)
        {
            U = currentUser;
            InitializeComponent();


            SqlHandle handle = new SqlHandle();
            users = handle.Query("select name , surname ,id from user where utype = '0'");
         

            dataGridView1.DataSource = users;

 

            var ctable = handle.Query("select name , id  from mediachannel");

            channels = new Channel[ctable.Rows.Count];
            for (int i = 0; i < channels.Length; i++)
            {
                //Int64 id64 = ((Int64)ctable.Rows[i][1]);
                int id = (int)ctable.Rows[i][1];
                var ch = new Channel((string)ctable.Rows[i][0], id);
                channels[i] = ch;
                this.comboBox1.Items.Add(ch);
            }
            this.comboBox1.SelectedItem = this.comboBox1.Items[0];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var handle = new SqlHandle();




            var si = dataGridView1.CurrentRow.Index;

            int chID = ((Channel)comboBox1.SelectedItem).id;

            //"2019-05-01 07:32:17"

            string datestr = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string text = richTextBox1.Text;
            string ename = textBox1.Text;

            int targetID = (int)users.Rows[si][2]; ;
            string query = $"INSERT INTO `event` (`id`, `uid`, `eventname`, `eventdate`, `eventinfo`, `channel`, `modality`) VALUES(NULL, '{targetID}', '{ename}', '{datestr}', '{text}', '{chID}', '0')";
            Console.WriteLine("QUERY=>" + query);

            if(handle.NonQuery(query)  > 0)
            {
                MessageBox.Show("Başarılı bir şekilde girildi", "Bilgi", MessageBoxButtons.OK);

                richTextBox1.Text = "";

            }
            else
            {
                MessageBox.Show("Sisteme kaydedilirken hata oluştu", "Hata", MessageBoxButtons.OK);

            }

        }
    }
}
