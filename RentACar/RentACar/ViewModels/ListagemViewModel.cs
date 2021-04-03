using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RentACar.Models;
using Xamarin.Forms;

namespace RentACar.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {
        public ObservableCollection<Veiculo> Veiculos { get; set; }
       
        const string URL = "http://aluracar.herokuapp.com/";
        
        Veiculo veiculoSelecionado;

        private bool aguarde;
        public bool Aguarde
        {
            get { return aguarde; }
            set
            {
                aguarde = value;
                OnPropertyChanged();
            }
        }

        public Veiculo VeiculoSelecionado
        {
            get
            {
                return veiculoSelecionado;
            }
            set
            {
                veiculoSelecionado = value;
                if (value != null)
                    MessagingCenter.Send(veiculoSelecionado, "VeiculoSelecionado");
            }
        }

        public ListagemViewModel()
        {
            this.Veiculos = new ObservableCollection<Veiculo>();
        }

        public async Task GetVeiculosAsync()
        {
            Aguarde = true;
            HttpClient cliente = new HttpClient();
            var resultado = await cliente.GetStringAsync(URL);
            var veiculosJson = JsonConvert.DeserializeObject<VeiculoJson[]>(resultado);

            foreach (var veiculoJson in veiculosJson)
            {
                this.Veiculos.Add(new Veiculo
                {
                    Nome = veiculoJson.nome,
                    Preco = veiculoJson.preco
                });
            }
            Aguarde = false;
        }
    }
}
