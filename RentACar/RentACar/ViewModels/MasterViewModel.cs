using RentACar.Media;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RentACar.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        #region PROPRIEDADES
        private Usuario usuario { get; set; }

        private ImageSource fotoPerfil = "perfil.png";
        public ImageSource FotoPerfil
        {
            get { return fotoPerfil; }
            private set
            {
                fotoPerfil = value;
                OnPropertyChanged();
            }
        }

        public string Nome
        {
            get { return this.usuario.nome; }
            set { this.usuario.nome = value; }
        }

        public string Email
        {
            get { return this.usuario.email; }
            set { this.usuario.email = value; }
        }

        public string DataNascimento
        {
            get { return this.usuario.dataNascimento; }
            set { this.usuario.dataNascimento = value; }
        }

        public string Telefone
        {
            get { return this.usuario.telefone; }
            set { this.usuario.telefone = value; }
        }

        private bool editando = false;
        public bool Editando
        {
            get { return editando; }
            set { editando = value; OnPropertyChanged(); }
        }

        public ICommand EditarPerfilCommand { get; private set; }
        public ICommand SalvarPerfilCommand { get; private set; }
        public ICommand EditarCommand { get; private set; }
        public ICommand TirarFotoCommand { get; private set; }
        public ICommand MeusAgendamentosCommand { get; private set; }
        public ICommand NovoAgendamentoCommand { get; private set; }
        #endregion

        #region CONTRUTOR
        public MasterViewModel(Usuario usuario)
        {
            this.usuario = usuario;
            DefinirComandos(usuario);
            AssinarMensagens();
        }

        private void AssinarMensagens()
        {
            MessagingCenter.Subscribe<byte[]>(this, "FotoTirada", (bytes) =>
            {
                FotoPerfil = ImageSource.FromStream(() => new MemoryStream(bytes));
            });
        }

        #endregion

        #region METODOS
        private void DefinirComandos(Usuario usuario)
        {
            EditarPerfilCommand = new Command(() =>
            {
                MessagingCenter.Send<Usuario>(usuario, "EditarPerfil");
            });

            SalvarPerfilCommand = new Command(() =>
            {
                this.Editando = false;
                MessagingCenter.Send<Usuario>(usuario, "SucessoSalvarUsuario");
            });

            EditarCommand = new Command(() =>
            {
                this.Editando = true;
            });

            TirarFotoCommand = new Command(() =>
            {
                DependencyService.Get<ICamera>().TirarFoto();
            });

            MeusAgendamentosCommand = new Command(() =>
            {
                MessagingCenter.Send<Usuario>(usuario, "MeusAgendamentos");
            });

            NovoAgendamentoCommand = new Command(() =>
            {
                MessagingCenter.Send<Usuario>(usuario, "NovoAgendamento");
            });
        }

        #endregion
    }
}