using RentACar.Data;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace RentACar.ViewModels
{
    public class AgendamentoUsuarioViewModel : BaseViewModel
    {
        #region PROPRIEDADES
        ObservableCollection<Agendamento> lista = new ObservableCollection<Agendamento>();
        public ObservableCollection<Agendamento> Lista
        {
            get
            {
                return lista;
            }
            private set
            {
                lista = value;
                OnPropertyChanged();
            }
        }

        private Agendamento agendamentoSelecionado;

        public Agendamento AgendamentoSelecionado
        {
            get { return agendamentoSelecionado; }
            set
            {
                if (value != null)
                {
                    agendamentoSelecionado = value;
                    MessagingCenter.Send<Agendamento>(agendamentoSelecionado, "AgedamentoSelecionado");
                }

            }
        }

        #endregion

        #region CONSTRUTOR
        public AgendamentoUsuarioViewModel()
        {
            AtualizarLista();
        }
        #endregion

        #region METODOS
        public void AtualizarLista()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                AgendamentoDAO dao = new AgendamentoDAO(conexao);

                var listaDB = dao.Lista;
                var ordenado = listaDB.OrderBy(l => l.DataAgendamento).ThenBy(l => l.HoraAgendamento);

                this.Lista.Clear();
                foreach (var item in ordenado)
                {
                    this.Lista.Add(item);
                }
            }
        }
        #endregion
    }
}

