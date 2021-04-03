using RentACar.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACar.Data
{
    public class AgendamentoDAO
    {
        readonly SQLiteConnection connection;

        private List<Agendamento> lista            ;

        public List<Agendamento> Lista
        {
            get 
            {
                lista = connection.Table<Agendamento>().ToList();
                return lista; 
            }
            private set { lista = value; }
        }

        public AgendamentoDAO(SQLiteConnection connection)
        {
            this.connection = connection;
            this.connection.CreateTable<Agendamento>();
        }

        public void Salvar(Agendamento agendamento)
        {
            if (connection.Find<Agendamento>(agendamento.Id) == null)
                connection.Insert(agendamento);
            else
                connection.Update(agendamento);
        }
    }
}
